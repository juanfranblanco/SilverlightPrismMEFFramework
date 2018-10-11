using System.ComponentModel.Composition;

using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.ServiceLocation;

using Policy.Shell.Contracts.Shell;
using Policy.Shell.Menu.Views;

namespace Policy.Shell.Menu
{
    [ModuleExport("Policy.Shell.Menu.ModuleInit", typeof(ModuleInit))]
    public class ModuleInit : IModule
    {
        private readonly IRegionManager regionManager;
        public IServiceLocator ServiceLocator;
 
        [ImportingConstructor]
        public ModuleInit(IRegionManager regionManager, IServiceLocator serviceLocator)
        {
            this.regionManager = regionManager;
            ServiceLocator = serviceLocator;
        }

        #region IModule Members

        public void Initialize()
        {
           
            var menu = ServiceLocator.GetInstance<PolicyDetailMenuView>();
            regionManager.AddToRegion(RegionNames.POLICY_DETAIL_MENU_REGION, menu);
        }

        #endregion
    }
}
