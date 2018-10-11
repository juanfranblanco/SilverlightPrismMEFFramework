namespace Application.Menu.Services
{
    using System.Collections.Specialized;
    using System.ComponentModel.Composition;

    using Application.Menu.ViewModels;
    using Application.Shell.Contracts.Shell;

    using Infrastructure.Menu;

    using Microsoft.Practices.Prism.Regions;

    [Export]
    public class ApplicationMenuStateController
    {
        //this should come from a constant contract defined somewhere
        private const string ABOUT_MENU_NAME = "About";

        private readonly IRegionManager regionManager;
        private readonly ApplicationMenuViewModel applicationMenuViewModel;

        [ImportingConstructor]   
        public ApplicationMenuStateController(IRegionManager regionManager, ApplicationMenuViewModel applicationMenuViewModel)
        {
            this.regionManager = regionManager;
            this.applicationMenuViewModel = applicationMenuViewModel;

            //We listen for changes into the collection, just in case the region has not been added so we can listen to the events
            //this.regionManager.Regions.CollectionChanged += this.Regions_CollectionChanged;

            //If the region has been added already we add the events
           // this.RegisterNavigatedWithRegion(RegionNames.MAIN_REGION);

            //Add event to set the initial state of the menu
            applicationMenuViewModel.MenuItemAdded += this.ApplicationMenuViewModel_MenuItemAdded;
        }

        void ApplicationMenuViewModel_MenuItemAdded(IMenuItem menuItem)
        {
            this.SetRequiredMenuState(menuItem);
        }

        private void SetRequiredMenuState(IMenuItem menuItem)
        {
            menuItem.Enabled = this.IsMenuItemRequiredToBeEnabled(menuItem.Name);
            menuItem.Visible = this.IsMenuItemRequiredToBeVisbile(menuItem.Name);   
        }

        public bool IsMenuItemRequiredToBeEnabled(string menuItemName)
        {
            return true;
        }

        public bool IsMenuItemRequiredToBeVisbile(string menuItemName)
        {
            //if (menuItemName == ABOUT_MENU_NAME)
            //{
            //    if (applicationMenuViewModel.ContainsMenuItem(ABOUT_MENU_NAME))
            //    {
            //        return applicationMenuViewModel.IsMenuItemVisible(ABOUT_MENU_NAME);
            //    }

            //    return false;
            //}
            
            return true;
        }


        //We listen for changes into the collection, just in case the region has not been added so we can listen to the events
        void Regions_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.RegisterNavigatedWithRegion(RegionNames.MAIN_REGION);
            
        }

        void NavigationService_Navigated(object sender, RegionNavigationEventArgs e)
        {
            //Example of implementation

            //Check if we have open the policy search view
            if (e.NavigationContext.Uri.ToString() == Policy.Search.Contracts.Views.ViewContractNames.POLICY_SEARCH_VIEW)
            {
                //Example of logic should be refactored out in implementation..... or consume other services
                
                //Check if we have a menu item "About"
                if (applicationMenuViewModel.ContainsMenuItem(ABOUT_MENU_NAME))
                {
                    //If is visible 
                    if (applicationMenuViewModel.IsMenuItemVisible(ABOUT_MENU_NAME))
                    {
                        //hide it
                        applicationMenuViewModel.HideMenuItem(ABOUT_MENU_NAME);
                    }
                    else
                    {
                        //show it
                        applicationMenuViewModel.ShowMenuItem(ABOUT_MENU_NAME);
                    }
                }
            }
        }

        //Add the events when navigated
        public void RegisterNavigatedWithRegion(string regionName)
        {
            if (regionManager.Regions.ContainsRegionWithName(regionName))
            {
                //deregister just in case to not have duplicated events
                this.regionManager.Regions[RegionNames.MAIN_REGION].NavigationService.Navigated -= this.NavigationService_Navigated;
                this.regionManager.Regions[RegionNames.MAIN_REGION].NavigationService.Navigated += this.NavigationService_Navigated;
            }
        }

        //Deregister events to avoid leaks
        ~ApplicationMenuStateController()
        {
            if (regionManager.Regions.ContainsRegionWithName(RegionNames.MAIN_REGION))
            {
                regionManager.Regions[RegionNames.MAIN_REGION].NavigationService.Navigated -= this.NavigationService_Navigated;
            }

            if(applicationMenuViewModel != null)
            {
                applicationMenuViewModel.MenuItemAdded -= this.ApplicationMenuViewModel_MenuItemAdded;
            }
        }
    }
}