using System.ComponentModel.Composition;
using System.Text;

using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Regions;

using Policy.Detail.Views;
using Policy.Shell.Contracts.Navigation;
using Policy.Shell.Contracts.Shell;

namespace Policy.Detail.Navigation
{
    [Export(typeof(IPolicyDetailNavigator))]
    public class PolicyDetailNavigator : IPolicyDetailNavigator
    {
        private readonly IRegionManager regionManager;

        [ImportingConstructor]
        public PolicyDetailNavigator(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public void NavigateToPolicyDetail(int policyId)
        {
            var query = new UriQuery();
            AddPolicyIdParameter(query, policyId);
            OpenView(ViewContractNames.POLICY_DETAIL_VIEW, query);
        }

        public void AddPolicyIdParameter(UriQuery query, int policyId)
        {
            query.Add(NavigationParameters.POLICY_ID, policyId.ToString());
        }

        public void OpenView(string targetName, UriQuery query)
        {
            var builder = new StringBuilder();
            builder.Append(targetName);
            builder.Append(query);
            GetPolicyDetailRegion().RequestNavigate(builder.ToString());
        }

        public IRegion GetPolicyDetailRegion()
        {
            return regionManager.Regions[RegionNames.POLICY_DETAIL_REGION];
        }
    }
}