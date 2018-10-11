using System.ComponentModel.Composition;

using Microsoft.Practices.Prism.Commands;

namespace Application.Shell.ViewModels
{
    [Export]
    public class ShellViewModel
    {
        #region Constructors and Destructors

        public ShellViewModel()
        {
            // Initialize this ViewModel's commands.
            this.ExitCommand = new DelegateCommand<object>(this.AppExit, this.CanAppExit);
        }

        #endregion

        #region Properties

        public DelegateCommand<object> ExitCommand { get; private set; }

        #endregion

        #region Methods

        private void AppExit(object commandArg)
        {
        }

        private bool CanAppExit(object commandArg)
        {
            return true;
        }

        #endregion
    }
}