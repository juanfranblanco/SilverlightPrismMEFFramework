using ClaimsModule.Models;
using Microsoft.Practices.Prism.Regions;

namespace ClaimsModule.Navigation
{
    public interface IClaimsNavigator
    {
        #region Public Methods

        void NavigateToClaimDetail(int policyId, Claim claim);

        void NavigateToClaimList(int policyId);

        #endregion

        int? GetRequestedClaimId(NavigationContext navigationContext);
        int? GetRequestedPolicyId(NavigationContext navigationContext);
    }
}