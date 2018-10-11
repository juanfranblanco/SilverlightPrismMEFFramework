namespace Infrastructure.Menu
{
    using System;

    using Microsoft.Practices.Prism.Commands;

    public class ActionMenuItem<TMenuItemContext> : MenuItem<TMenuItemContext>, IActionMenuItem<TMenuItemContext>
    {
        private Action<TMenuItemContext> action;

        public Action<TMenuItemContext> Action
        {
            get
            {
                return this.action;
            }
            set
            {
                this.action = value;
                RaisePropertyChanged(() => Action);
                this.SetCommand();
            }
        }

        public void SetCommand()
        {
            MenuItemCommand =
                new CompositeBeforeAfterDelegateCommand(() => this.Action(MenuItemContext));
        }
        
    }
}