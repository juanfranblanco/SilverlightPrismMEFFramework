using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.ServiceLocation;

using Policy.Shell.Contracts;
using Policy.Shell.Views;

namespace Policy.Shell
{
    [ModuleExport("Policy.Shell.ModuleInit", typeof(ModuleInit))]
    public class ModuleInit : IModule
    {
        private readonly IRegionManager regionManager;
        private readonly IServiceLocator serviceLocator;

        private IPolicyWindowService policyWindowService;

        [ImportingConstructor]
        public ModuleInit(IRegionManager regionManager, IServiceLocator serviceLocator)
        {
            this.regionManager = regionManager;
            this.serviceLocator = serviceLocator;
        }

        #region IModule Members

        public void Initialize()
        {
            if (policyWindowService == null)
            {
                regionManager.RegisterViewWithRegion(
                    Application.Shell.Contracts.Shell.RegionNames.MAIN_WINDOW_NAVIGATION_REGION,
                    () => serviceLocator.GetInstance<PolicyWindowMenuView>());



                policyWindowService = serviceLocator.GetInstance<IPolicyWindowService>();
            }
        }

        #endregion
    }
}
