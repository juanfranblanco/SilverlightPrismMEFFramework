using System.ComponentModel.Composition;

using Application.Shell.Contracts.Services;

using Infrastructure.Menu;

using Microsoft.Practices.Prism.Commands;

namespace Application.Menu.MenuItems
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ApplicationMenuItemViewOpener:MenuItemViewOpener<object>
    {
        [ImportingConstructor]
        public ApplicationMenuItemViewOpener(IApplicationCommands commands)
        {
            MenuItemCommand = new CompositeBeforeAfterDelegateCommand(() => commands.OpenView(this.TargetName));
        }
    }
}
