namespace Infrastructure.Menu
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Practices.Prism.Commands;
    using Microsoft.Practices.Prism.ViewModel;

    public class MenuItem<TMenuItemContext>: NotificationObject, IMenuItem<TMenuItemContext>
    {
        private string description;
        private string name;
        private DelegateCommand menuItemCommand;


        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                RaisePropertyChanged(() => Description);
            }
        }

        private int orderHint;

        public int OrderHint
        {
            get
            {
                return this.orderHint;
            }
            set
            {
                this.orderHint = value;
                RaisePropertyChanged(() => OrderHint);
            }
        }

        public DelegateCommand MenuItemCommand
        {
            get { return menuItemCommand; }
            set
            {
                menuItemCommand = value;
               
                RaisePropertyChanged(() => MenuItemCommand);
            }
        }

        private TMenuItemContext menuItemContext;

        public TMenuItemContext MenuItemContext
        {
            get { return menuItemContext; }
            set
            {
                menuItemContext = value;
                RaisePropertyChanged(() => MenuItemContext);
            }
        }

        private bool enabled;

        public bool Enabled
        {
            get { return enabled; }
            set
            {
                enabled = value;
                RaisePropertyChanged(() => Enabled);
            }

        }

        private bool visible;

        public bool Visible
        {
            get { return visible; }
            set
            {
                visible = value;
                RaisePropertyChanged(() => Visible);
            }

        }

    }
}