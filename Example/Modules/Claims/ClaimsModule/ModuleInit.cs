using System.ComponentModel.Composition;

using ClaimsModule.Controller;
using ClaimsModule.Views;
using Infrastructure.Navigator;
using MessageBox.Contracts;

using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;

using Policy.Shell.Contracts;

namespace ClaimsModule
{
    [ModuleExport("ClaimsModule.ModuleInit", typeof(ModuleInit))]
    public class ModuleInit : IModule
    {
        #region Constants and Fields

        private readonly IMessageBoxService messageBoxService;

        private readonly IPolicyDetailMenuRegistry policyDetailMenuRegistry;

        private readonly IRegionManager regionManager;

        private readonly IServiceLocator serviceLocator;

        private ClaimsRegionContentController claimsRegionContentController;

        #endregion

        #region Constructors and Destructors

        [ImportingConstructor]
        public ModuleInit(
            IRegionManager regionManager,
            IServiceLocator serviceLocator,
            IPolicyDetailMenuRegistry policyDetailMenuRegistry,
            IMessageBoxService messageBoxService)
        {
            this.regionManager = regionManager;
            this.serviceLocator = serviceLocator;
            this.policyDetailMenuRegistry = policyDetailMenuRegistry;
            this.messageBoxService = messageBoxService;
        }

        #endregion

        #region Implemented Interfaces

        #region IModule

        public void Initialize()
        {
            this.policyDetailMenuRegistry.RegisterMenuItemViewOpener(
                "Claims", "Claims list", typeof(ClaimListView), ViewContractNames.CLAIM_LIST_VIEW, 3);

            this.claimsRegionContentController = this.serviceLocator.GetInstance<ClaimsRegionContentController>();

            this.policyDetailMenuRegistry.RegisterMenuItemAction(
                "Say Hello to Policy",
                "Testing",
                4,
                (policyId) => this.messageBoxService.ShowMessage("Hello Policy:" + policyId));
        }

        #endregion

        #endregion
    }
}