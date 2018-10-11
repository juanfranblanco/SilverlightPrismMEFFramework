using System.ComponentModel.Composition;
using System.Text;

using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Regions;

using Policy.Shell.Contracts.Navigation;
using Policy.Shell.Contracts.Shell;

namespace Policy.Shell.Menu.Commands
{
    [Export(typeof (IPolicyDetailMenuCommands))]
    public class PolicyDetailMenuCommands : IPolicyDetailMenuCommands
    {
        private readonly IRegionManager regionManager;
  
        [ImportingConstructor]
        public PolicyDetailMenuCommands(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        #region IPolicyDetailMenuCommands Members

        public void CloseView(object view)
        {
            regionManager.Regions[RegionNames.POLICY_DETAIL_REGION].Remove(view);
        }

        public void OpenView(string targetName, int policyId)
        {
            OpenView(targetName, new UriQuery { { NavigationParameters.POLICY_ID, policyId.ToString() } });
        }

        public void OpenView(string targetName, UriQuery uriQuery)
        {
            var builder = new StringBuilder();
            builder.Append(targetName);
            builder.Append(uriQuery);
            regionManager.Regions[RegionNames.POLICY_DETAIL_REGION].RequestNavigate(builder.ToString());
        }

        #endregion
    }
}