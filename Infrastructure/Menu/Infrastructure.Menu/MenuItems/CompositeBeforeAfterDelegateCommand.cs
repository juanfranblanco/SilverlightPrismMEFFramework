namespace Infrastructure.Menu
{
    using System;

    using Microsoft.Practices.Prism.Commands;

    /// <summary>
    /// </summary>
    public class CompositeBeforeAfterDelegateCommand:DelegateCommand
    {
        public CompositeBeforeAfterDelegateCommand(Action executeMethod)
            : base(executeMethod)
        {
           
        }

        public CompositeBeforeAfterDelegateCommand(Action executeMethod, Func<bool> canExecuteMethod)
            : base(executeMethod, canExecuteMethod)
        {
           
        }

        public Action AfterMenuItemCommandAction { get; set; }
        public Action BeforeMenuItemCommandAction { get; set; }

        protected new void Execute(object parameter)
        { 
            if(BeforeMenuItemCommandAction != null)
            {
                BeforeMenuItemCommandAction();
            }

            base.Execute(parameter);  
            
            if(AfterMenuItemCommandAction != null)
            {
                AfterMenuItemCommandAction();
            }
        }
    }
}