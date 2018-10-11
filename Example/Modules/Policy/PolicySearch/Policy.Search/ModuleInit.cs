using System.ComponentModel.Composition;

using Application.Shell.Contracts.Services;

using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;

using Policy.Search.Controllers;
using Policy.Search.Regions;
using Policy.Search.Views;
using Policy.Search.Contracts.Views;

namespace Policy.Search
{
    

    [ModuleExport("Policy.Search.ModuleInit", typeof(ModuleInit))]
    public class ModuleInit : IModule
    {
        #region Constants and Fields

        private readonly IApplicationMenuRegistry applicationMenuRegistry;

        private readonly IRegionManager regionManager;

        private readonly IServiceLocator serviceLocator;

        private PolicySearchController policySearchController;

        #endregion

        #region Constructors and Destructors

        [ImportingConstructor]
        public ModuleInit(
            IRegionManager regionManager,
            IServiceLocator serviceLocator,
            IApplicationMenuRegistry applicationMenuRegistry)
        {
            this.regionManager = regionManager;
            this.serviceLocator = serviceLocator;
            this.applicationMenuRegistry = applicationMenuRegistry;
        }

        #endregion

        #region Implemented Interfaces

        #region IModule

        public void Initialize()
        {
            if (policySearchController == null)
            {
                this.applicationMenuRegistry.RegisterMenuItemViewOpener(
                    "Policy Search", "Policy Search", typeof(PolicySearchView), ViewContractNames.POLICY_SEARCH_VIEW, 1);

                this.regionManager.RegisterViewWithRegion(
                    RegionNames.POLICY_SEARCH_RESULTS_REGION,
                    () => this.serviceLocator.GetInstance<PolicySearchResultsView>());

                policySearchController = serviceLocator.GetInstance<PolicySearchController>();
            }
        }

        #endregion

        #endregion
    }
}