using System.ComponentModel.Composition;

using Microsoft.Practices.Prism.Commands;

using Infrastructure.Menu;

using Policy.Shell.Menu.Commands;

namespace Policy.Shell.Menu.ViewModel
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PolicyDetailMenuItemViewOpener : MenuItemViewOpener<int>
    {
        private readonly IPolicyDetailMenuCommands commands;

        [ImportingConstructor]
        public PolicyDetailMenuItemViewOpener(IPolicyDetailMenuCommands commands)
        {
            this.commands = commands;
            SetCommand();
        }

        public void SetCommand()
        {
            MenuItemCommand =
                new CompositeBeforeAfterDelegateCommand(() => commands.OpenView(TargetName, MenuItemContext));
        }
    }
}