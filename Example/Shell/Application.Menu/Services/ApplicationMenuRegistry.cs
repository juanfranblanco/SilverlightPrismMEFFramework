using System.ComponentModel.Composition;

using Application.Menu.MenuItems;
using Application.Menu.ViewModels;
using Application.Shell.Contracts.Services;

using Microsoft.Practices.ServiceLocation;

using Infrastructure.Menu.Registry;

namespace Application.Menu.Services
{
    [Export(typeof(IApplicationMenuRegistry))]
    public class ApplicationMenuRegistry : MenuRegistry<ApplicationMenuViewModel, ApplicationMenuItemViewOpener, ApplicationMenuAction, object>, IApplicationMenuRegistry
    {      
        [ImportingConstructor]
        public ApplicationMenuRegistry(ApplicationMenuViewModel applicationMenuViewModel, IServiceLocator serviceLocator):base(applicationMenuViewModel, serviceLocator)
        {
  
        }
    }
}
