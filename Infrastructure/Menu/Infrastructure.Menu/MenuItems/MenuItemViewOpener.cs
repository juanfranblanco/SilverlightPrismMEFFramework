namespace Infrastructure.Menu
{
    using System;
    using System.ComponentModel.Composition;

    public class MenuItemViewOpener<TMenuItemContext> : MenuItem<TMenuItemContext>, IMenuItemViewOpener<TMenuItemContext>
    {
        private string targetName;
        private Type viewType;

        public Type ViewType
        {
            get { return viewType; }
            set
            {
                viewType = value;
                RaisePropertyChanged(() => ViewType);
            }
        }

        public string TargetName
        {
            get { return targetName; }
            set
            {
                targetName = value;
                RaisePropertyChanged(() => TargetName);
            }
        }

    }
}