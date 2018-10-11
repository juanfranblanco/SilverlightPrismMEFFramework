using System.ComponentModel.Composition;
using System.Text;

using ClaimsModule.Models;
using ClaimsModule.Views;
using Infrastructure.Navigator;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Regions;

using Policy.Shell.Contracts.Shell;

namespace ClaimsModule.Navigation
{
    [Export(typeof(IClaimsNavigator))]
    public class ClaimsNavigator : IClaimsNavigator
    {
        #region Constants and Fields

      
        private readonly NavigationService navigationService;

        #endregion

        #region Constructors and Destructors

        [ImportingConstructor]
        public ClaimsNavigator (
            [Import("NavigationService")]NavigationService navigationService)
        {
           
            this.navigationService = navigationService;
        }

        #endregion

        #region Public Methods

        public void AddClaimIdParameter(UriQuery query, int claimId)
        {
            query.Add(NavigationParameters.CLAIM_ID, claimId.ToString());
        }

        public void AddPolicyIdParameter(UriQuery query, int policyId)
        {
            query.Add(Policy.Shell.Contracts.Navigation.NavigationParameters.POLICY_ID, policyId.ToString());
        }

        #endregion

        #region Implemented Interfaces

        #region IClaimsNavigator

        public void NavigateToClaimDetail(int policyId, Claim claim)
        {
            var query = new UriQuery();
            this.AddPolicyIdParameter(query, policyId);
            this.AddClaimIdParameter(query, claim.ClaimId);
            navigationService.OpenView(ViewContractNames.CLAIM_DETAIL_VIEW, query, RegionNames.POLICY_DETAIL_REGION);
        }

        public void NavigateToClaimList(int policyId)
        {
            var query = new UriQuery();
            this.AddPolicyIdParameter(query, policyId);
            navigationService.OpenView(ViewContractNames.CLAIM_LIST_VIEW, query, RegionNames.POLICY_DETAIL_REGION);
        }


        public int? GetRequestedClaimId(NavigationContext navigationContext)
        {
            return navigationService.GetNavigationParameterIntValue(navigationContext, NavigationParameters.CLAIM_ID);
            
        }

        public int? GetRequestedPolicyId(NavigationContext navigationContext)
        {
            return navigationService.GetNavigationParameterIntValue(navigationContext,
                                                                    Policy.Shell.Contracts.Navigation.
                                                                        NavigationParameters.POLICY_ID);
            
        }

        #endregion

        #endregion
    }
}