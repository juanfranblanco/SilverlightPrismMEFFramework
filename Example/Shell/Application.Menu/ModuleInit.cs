using System.ComponentModel.Composition;

using Application.Menu.Views;
using Application.Shell.Contracts.Shell;

using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.ServiceLocation;

namespace Application.Menu
{
    using Application.Menu.Services;

    [ModuleExport("Application.Menu.ModuleInit", typeof(ModuleInit))]
    public class ModuleInit : IModule
    {
        private readonly IRegionManager regionManager;
        private readonly IServiceLocator serviceLocator;

        private ApplicationMenuStateController applicationMenuStateController;

        private bool loaded = false;

        [ImportingConstructor]
        public ModuleInit(IRegionManager regionManager, IServiceLocator serviceLocator)
        {
            this.regionManager = regionManager;
            this.serviceLocator = serviceLocator;
           
        }

        #region IModule Members

        public void Initialize()
        {
            if (!loaded)
            {
                this.regionManager.RegisterViewWithRegion(
                    RegionNames.MAIN_NAVIGATION_REGION, () => this.serviceLocator.GetInstance<ApplicationMenuView>());
                loaded = true;

                this.applicationMenuStateController = serviceLocator.GetInstance<ApplicationMenuStateController>();
            }

        }

        #endregion
    }
}
