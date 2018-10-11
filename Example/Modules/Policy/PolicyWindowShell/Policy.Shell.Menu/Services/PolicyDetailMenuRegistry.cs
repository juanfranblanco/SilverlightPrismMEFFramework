using System.ComponentModel.Composition;

using Microsoft.Practices.ServiceLocation;

using Policy.Shell.Contracts;

using Infrastructure.Menu.Registry;

using Policy.Shell.Menu.ViewModel;

namespace Policy.Shell.Menu.Services
{
    [Export(typeof (IPolicyDetailMenuRegistry))]
    public class PolicyDetailMenuRegistry : MenuRegistry<PolicyDetailMenuViewModel,PolicyDetailMenuItemViewOpener,PolicyActionMenuItem,int>, IPolicyDetailMenuRegistry
    {
        [ImportingConstructor]
        public PolicyDetailMenuRegistry(PolicyDetailMenuViewModel policyShellViewModel, IServiceLocator serviceLocator):base(policyShellViewModel, serviceLocator)
        {
          
        }
    }
}