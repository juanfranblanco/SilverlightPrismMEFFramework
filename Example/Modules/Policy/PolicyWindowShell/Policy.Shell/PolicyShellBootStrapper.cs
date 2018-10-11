using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Infrastructure.Navigator;
using Infrastructure.PrismMEFChildContainer;

using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Policy.Shell.Contracts;
using Policy.Shell.Services.ContextServices;
using Policy.Shell.ViewModels;
using Policy.Shell.Views;

using Model = Policy.Contracts.Models;

namespace Policy.Shell
{
    public class PolicyShellBootStrapper : PrismMEFChildContainerBootstrapper
    {
        #region Constructors and Destructors

        public PolicyShellBootStrapper(CompositionContainer parentContainer, Model.Policy policy)
        {
            if (parentContainer == null)
            {
                throw new ArgumentNullException("parentContainer");
            }
            if (policy == null)
            {
                throw new ArgumentNullException("policy");
            }
            this.ParentContainer = parentContainer;
            this.Policy = policy;
        }

        #endregion

        #region Properties

        public Model.Policy Policy { get; private set; }

        #endregion


        protected override IModuleCatalog CreateModuleCatalog()
        {
            // Create the module catalogue from a XAML file, these are the isolated child container items
            return
                Microsoft.Practices.Prism.Modularity.ModuleCatalog.CreateFromXaml(
                    new Uri("/Policy.Shell;component/ModuleCatalog.xaml", UriKind.Relative));
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            var policyDetailContext = new PolicyDetailContext(Policy.PolicyId);
           
            Container.ComposeExportedValue<IPolicyDetailContext>(policyDetailContext);

        }

        protected override void CreateShellInstance()
        {
            this.Shell = this.ParentContainer.GetExportedValue<PolicyShell>();
            ((PolicyShellViewModel)this.Shell.ViewModel).CurrentPolicy = Policy;
        }

        protected override void LoadModuleCompleted(object sender, LoadModuleCompletedEventArgs e)
        {
         

        }

        protected override void RegisterTypesForTheWindowRegionManager()
        {
            var navigationService = new NavigationService(this.ChildContainerRegionManager);
            Container.ComposeExportedValue("NavigationService", navigationService);
        }

    }
}