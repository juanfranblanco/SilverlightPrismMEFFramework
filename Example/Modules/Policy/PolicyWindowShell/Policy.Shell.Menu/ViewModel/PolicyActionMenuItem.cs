using System.ComponentModel.Composition;

using Infrastructure.Menu;

namespace Policy.Shell.Menu.ViewModel
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PolicyActionMenuItem : ActionMenuItem<int>
    {
       
    }
}