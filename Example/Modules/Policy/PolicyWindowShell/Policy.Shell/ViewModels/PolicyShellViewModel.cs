using System;
using System.ComponentModel.Composition;
using System.Windows.Input;

using Infrastructure.PrismMEFChildContainer.ViewInterfaces;

using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;

using UI.Infrastructure.State.Contracts.Services;

using Model = Policy.Contracts.Models;

namespace Policy.Shell.ViewModels
{
    /// <summary>
    /// View model to support the Policy view.
    /// </summary>

    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PolicyShellViewModel : NotificationObject, IChildContainerViewModel
    {
        private readonly IStateService stateService;
        private readonly DelegateCommand saveWorkCommand;

        public ICommand SaveWorkCommand
        {
            get { return this.saveWorkCommand; }
        }

        [ImportingConstructor]
        public PolicyShellViewModel(IStateService stateService)
        {
            if (stateService == null) throw new ArgumentNullException("stateService");
            this.stateService = stateService;
            this.saveWorkCommand = new DelegateCommand(this.SaveWork);
        }

        private Model.Policy currentPolicy;

        public Model.Policy CurrentPolicy
        {
            get { return currentPolicy; }
            set
            {
                currentPolicy = value;
                this.RaisePropertyChanged(() => CurrentPolicy);
            }
        }

        private IRegionManager regionManager;
        public IRegionManager RegionManager
        {
            get { return regionManager; }
            set { regionManager = value; 
                RaisePropertyChanged(() => RegionManager);
            }
        }

        public void SaveWork()
        {
            this.stateService.Save(currentPolicy.PolicyId.ToString());
        }
    }
}