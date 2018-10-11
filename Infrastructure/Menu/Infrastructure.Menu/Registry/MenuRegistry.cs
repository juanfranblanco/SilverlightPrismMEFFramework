using System;

namespace Infrastructure.Menu.Registry
{
    using Infrastructure.Menu.Controller;

    using Microsoft.Practices.ServiceLocation;

      public class MenuRegistry<TMenuViewModel, TMenuItemViewOpener, TMenuItemAction, TMenuItemContext>
        where TMenuViewModel: IMenuViewModel<TMenuItemContext>
        where TMenuItemViewOpener: IMenuItemViewOpener<TMenuItemContext>
        where TMenuItemAction : IActionMenuItem<TMenuItemContext>
    {
        protected TMenuViewModel menuViewModel;
        protected readonly IServiceLocator serviceLocator;
      

        public MenuRegistry(TMenuViewModel menuViewModel, IServiceLocator serviceLocator)
        {
            this.menuViewModel = menuViewModel;
            this.serviceLocator = serviceLocator;
        }

        public void RegisterMenuItemViewOpener(string menuItemName, string description, Type viewType, string targetName, int orderHint)
        {
            var menuItemViewModel = serviceLocator.GetInstance<TMenuItemViewOpener>();
            InitialiseMenuItemViewOpener(menuItemViewModel, menuItemName, description, viewType, targetName, orderHint);
            menuViewModel.AddMenuItem(menuItemViewModel);
        }

        protected void InitialiseMenuItemViewOpener(TMenuItemViewOpener menuItemViewModel, string menuItemName, string description, Type viewType, string targetName, int orderHint)
        {
            menuItemViewModel.Name = menuItemName;
            menuItemViewModel.Description = description;
            menuItemViewModel.ViewType = viewType;
            menuItemViewModel.TargetName = targetName;
            menuItemViewModel.OrderHint = orderHint;
           
        }

        public void RegisterMenuItemAction(string menuItemName, string description, int orderHint, Action<TMenuItemContext> action)
        {
            var menuItemViewModel = serviceLocator.GetInstance<TMenuItemAction>();
            menuItemViewModel.Name = menuItemName;
            menuItemViewModel.Description = description;
            menuItemViewModel.OrderHint = orderHint;
            menuItemViewModel.Action = action;
            menuViewModel.AddMenuItem(menuItemViewModel);

        }
    }
}

