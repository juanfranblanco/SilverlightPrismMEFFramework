namespace Infrastructure.Menu
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel.Composition;
    using System.Linq;

    using Infrastructure.Menu.Controller;

    using Microsoft.Practices.Prism.ViewModel;

    public class MenuViewModel<TMenuContext>:NotificationObject, IMenuViewModel<TMenuContext>
    {
        public delegate void MenuItemAddedHandler(IMenuItem menuItem);

        public event MenuItemAddedHandler MenuItemAdded;

        private readonly ObservableCollection<IMenuItem<TMenuContext>> menuItems =
            new ObservableCollection<IMenuItem<TMenuContext>>();
        
        private TMenuContext menuContext;

        public TMenuContext MenuContext
        {
            get
            {
                return this.menuContext;
            }
            set
            {
                this.menuContext = value;
                RaisePropertyChanged(() => MenuContext);
                UpdateMenuItemsContext();
            }
        }

        public virtual void UpdateMenuItemsContext()
        {
            foreach (var menuItem in MenuItems)
            {
                menuItem.MenuItemContext = MenuContext;
            }
        }

        public ObservableCollection<IMenuItem<TMenuContext>> MenuItems
        {
            get { return menuItems; }
        }

        public void AddMenuItem(IMenuItem<TMenuContext> menuItem)
        {
            menuItem.MenuItemContext = menuContext;
            var menuItemsCollection = MenuItems.ToList();
            menuItemsCollection.Add(menuItem);
            menuItemsCollection.Sort(new OrderHintComparisonMenuItem<TMenuContext>());
            MenuItems.Clear();
            menuItemsCollection.ForEach((sortedMenuItem) => MenuItems.Add(sortedMenuItem));

            if(MenuItemAdded != null)
            {
                MenuItemAdded(menuItem);
            }
        }

        public void EnableMenuItem(string menuItemName)
        {
            var menuItem = this.FindMenuItem(menuItemName);
            EnableMenuItem(menuItem);
        }

        private void EnableMenuItem(IMenuItem menuItem)
        {
            if(menuItem != null)
            {
                menuItem.Enabled = true;
            }
        }

        protected IMenuItem FindMenuItem(string menuItemName)
        {
            return menuItems.FirstOrDefault(x => x.Name == menuItemName);

        }

        public void DisableMenuItem(string menuItemName)
        {
            var menuItem = this.FindMenuItem(menuItemName);
            DisableMenuItem(menuItem);
        }

        private void DisableMenuItem(IMenuItem menuItem)
        {
            if (menuItem != null)
            {
                menuItem.Enabled = false;
            }
        }

        public void HideMenuItem(string menuItemName)
        {
            var menuItem = this.FindMenuItem(menuItemName);
            HideMenuItem(menuItem);
        }

        private void HideMenuItem(IMenuItem menuItem)
        {
            if (menuItem != null)
            {
                menuItem.Visible = false;
            }
        }

        public void ShowMenuItem(string menuItemName)
        {
            var menuItem = this.FindMenuItem(menuItemName);
            ShowMenuItem(menuItem);
        }

        private void ShowMenuItem(IMenuItem menuItem)
        {
            if (menuItem != null)
            {
                menuItem.Visible = true;
            }
        }

        public bool IsMenuItemVisible(string menuItemName)
        {
            var menuItem = this.FindMenuItem(menuItemName);
            if (menuItem == null) return false;
            return menuItem.Visible;
        }

        public bool IsMenuItemEnabled(string menuItemName)
        {
            var menuItem = this.FindMenuItem(menuItemName);
            if (menuItem == null) return false;
            return menuItem.Enabled;
        }

        public bool ContainsMenuItem(string menuItemName)
        {
            return this.FindMenuItem(menuItemName) != null;
        }

    }
}