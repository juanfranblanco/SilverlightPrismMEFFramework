using System.ComponentModel.Composition;

using Infrastructure.Menu;

namespace Application.Menu.MenuItems
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ApplicationMenuAction : ActionMenuItem<object>
    {
        
    }
}