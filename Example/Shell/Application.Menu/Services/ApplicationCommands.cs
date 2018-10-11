using System.ComponentModel.Composition;

using Application.Shell.Contracts.Services;
using Application.Shell.Contracts.Shell;

using Microsoft.Practices.Prism.Regions;

namespace Application.Menu.Services
{
    using System;
    using System.Collections;

    using Application.Menu.ViewModels;

    using Infrastructure.Menu.Controller;

    [Export(typeof(IApplicationCommands))]
    public class ApplicationCommands : IApplicationCommands
    {
        #region IApplicationCommands Members

        private IRegionManager regionManager;

        private readonly ApplicationMenuViewModel applicationMenuViewModel;

        [ImportingConstructor]
        public ApplicationCommands(IRegionManager regionManager, ApplicationMenuViewModel applicationMenuViewModel)
        {
            this.regionManager = regionManager;
            
            this.applicationMenuViewModel = applicationMenuViewModel;
        }

        public void CloseView(object view)
        {
            regionManager.Regions[RegionNames.MAIN_REGION].Remove(view);
        }

        public void OpenView(string targetName)
        {
            regionManager.Regions[RegionNames.MAIN_REGION].RequestNavigate(targetName);
        }


        #endregion

    }


    //Example of logic used by the menu view model to initialise the state of menu items.
}