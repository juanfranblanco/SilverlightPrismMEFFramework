using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

using Infrastructure.Window.Contracts.Events;
using Infrastructure.Window.Contracts.Model;
using Infrastructure.Window.Contracts.Services;

using MessageBox.Contracts;

using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;

using Policy.Shell.Contracts;

using UI.Infrastructure.State.Contracts.Services;

using Model = Policy.Contracts.Models;



namespace Policy.Shell.Services.Windows
{
    [Export(typeof(IPolicyWindowService))]
    public class PolicyWindowService : IPolicyWindowService
    {
        private readonly IEventAggregator eventAggregator;

        private readonly IWindowService<Model.Policy> windowService;

        private readonly IStateService stateService;
        private readonly IMessageBoxService messageBoxService;
        private readonly CompositionContainer compositionContainer;
        private readonly IRegionManager regionManager;

        [ImportingConstructor]
        public PolicyWindowService(
            IRegionManager regionManager,
            IEventAggregator eventAggregator,
            IWindowService<Model.Policy> windowService,
            IStateService stateService,
            IMessageBoxService messageBoxService,
            CompositionContainer compositionContainer
            )
        {
            if (regionManager == null) throw new ArgumentNullException("regionManager");
            if (eventAggregator == null) throw new ArgumentNullException("eventAggregator");
            if (windowService == null)
            {
                throw new ArgumentNullException("windowService");
            }

            if (stateService == null)
            {
                throw new ArgumentNullException("stateService");
            }
            if (messageBoxService == null)
            {
                throw new ArgumentNullException("messageBoxService");
            }
            if (compositionContainer == null)
            {
                throw new ArgumentNullException("compositionContainer");
            }

            this.regionManager = regionManager;
            this.eventAggregator = eventAggregator;
            this.windowService = windowService;
           
            this.stateService = stateService;
            this.messageBoxService = messageBoxService;
            this.compositionContainer = compositionContainer;

            this.eventAggregator.GetEvent<WindowOpen<Model.Policy>>().Subscribe(OpenWindow, true);
            this.eventAggregator.GetEvent<WindowClose<Model.Policy>>().Subscribe(CloseWindow, true);

            SetWindowServicePolicyComparison();

        }

        public void SetWindowServicePolicyComparison()
        {
            this.windowService.CompareKeys = (policy1, policy2) => (policy1.PolicyId == policy2.PolicyId);
        }

        public void CloseWindow(Model.Policy policy)
        {
            var policyWindowItem = this.windowService.GetWindow(policy);
            CloseWindow(policyWindowItem);
        }

        private void CloseWindow(WindowItem<Model.Policy> policyWindowItem)
        {

            CheckIfRequiredToSaveWork(policyWindowItem);

            IRegion mainRegion = GetMainRegion();
            mainRegion.Remove(policyWindowItem.CurrentView);
            this.windowService.RemoveWindow(policyWindowItem.Key);
        }

        private void CheckIfRequiredToSaveWork(WindowItem<Policy.Contracts.Models.Policy> policyWindowItem)
        {
            var policyId = policyWindowItem.Key.PolicyId;
            var confirmMessage = string.Format("Would you like to save the work before closing for policy Id: {0}?", policyId);
                                         
            this.messageBoxService.ShowConfirm(
                confirmMessage,
                () =>
                    {
                        this.stateService.Save(policyId.ToString());
                    
                    },
                () =>
                    {
                        this.stateService.RemoveContextState(policyId.ToString());
                    });
        }

        public void OpenWindow(WindowItem<Model.Policy> windowItem)
        {
            IRegion mainRegion = GetMainRegion();
            mainRegion.Activate(windowItem.CurrentView);
        }

        public void OpenPolicyWindow(Model.Policy policy)
        {
            IRegion mainRegion = GetMainRegion();
            if (mainRegion == null) return;
            
            WindowItem<Model.Policy> windowItem;

            if (!this.windowService.ContainsWindow(policy))
            {

                if (stateService.ExistsContextState(policy.PolicyId.ToString()))
                {
                    var confirmMessage = string.Format("We have found saved work for Policy: {0}, would you like to continue with this work?", policy.PolicyId);

                    messageBoxService.ShowConfirm(
                        confirmMessage,
                        () =>
                            { 
                                windowItem = CreateNewWindow(policy);
                                OpenWindow(windowItem);
                            },
                        () =>
                            {
                                stateService.RemoveContextState(policy.PolicyId.ToString());
                                windowItem = CreateNewWindow(policy);
                                OpenWindow(windowItem);

                            });

                }
                else
                {
                   windowItem = CreateNewWindow(policy);
                   OpenWindow(windowItem);

                }

            }
            else
            {
                windowItem = this.windowService.GetWindow(policy);
                OpenWindow(windowItem);
            }

        }

        private IRegion GetMainRegion()
        {
            return regionManager.Regions[Application.Shell.Contracts.Shell.RegionNames.MAIN_REGION];
        }

        private WindowItem<Model.Policy> CreateNewWindow(Model.Policy policy)
        {
            var bootStrapper = new PolicyShellBootStrapper(compositionContainer, policy);
            bootStrapper.Run();
            RegisterPolicyContextState(policy);
            var windowItem = this.windowService.CreateWindow(policy, bootStrapper.Shell);
            GetMainRegion().Add(windowItem.CurrentView);
            return windowItem;
        }

        private void RegisterPolicyContextState(Model.Policy policy)
        {
            this.stateService.RegisterContextState(policy.PolicyId.ToString(), "Policy:" + policy.PolicyId, null);
        }
    }
}
