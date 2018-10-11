using System.ComponentModel.Composition;

using Infrastructure.Menu;

namespace Application.Menu.ViewModels
{
    using Application.Menu.Services;

    [Export]
    public class ApplicationMenuViewModel:MenuViewModel<object>
    {
       
    }
}
