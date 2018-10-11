using System.ComponentModel.Composition;

using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.ServiceLocation;

using Policy.Detail.Navigation;
using Policy.Detail.Views;
using Policy.Shell.Contracts;

namespace Policy.Detail
{
    [ModuleExport("Policy.Detail.ModuleInit", typeof(ModuleInit))]
    public class ModuleInit : IModule
    {
        private readonly IRegionManager regionManager;
        private readonly IServiceLocator serviceLocator;

        private readonly IPolicyDetailMenuRegistry policyDetailMenuRegistry;

        private readonly IPolicyDetailContext policyDetailContext;

        private readonly IPolicyDetailNavigator policyDetailNavigator;

        [ImportingConstructor]
        public ModuleInit(IRegionManager regionManager, 
                          IServiceLocator serviceLocator, 
                          IPolicyDetailMenuRegistry policyDetailMenuRegistry,
                          IPolicyDetailContext policyDetailContext,
                          IPolicyDetailNavigator policyDetailNavigator  
                            )
        {
            this.regionManager = regionManager;
            this.serviceLocator = serviceLocator;
            this.policyDetailMenuRegistry = policyDetailMenuRegistry;
            this.policyDetailContext = policyDetailContext;
            this.policyDetailNavigator = policyDetailNavigator;
        }

        #region IModule Members

        public void Initialize()
        {
            //TODO: Localisation
            policyDetailMenuRegistry.RegisterMenuItemViewOpener("Policy Detail", "Policy Detail", typeof(PolicyDetailView), ViewContractNames.POLICY_DETAIL_VIEW, 1);
            policyDetailNavigator.NavigateToPolicyDetail(policyDetailContext.CurrentPolicyId);
        }

        #endregion
    }
}
