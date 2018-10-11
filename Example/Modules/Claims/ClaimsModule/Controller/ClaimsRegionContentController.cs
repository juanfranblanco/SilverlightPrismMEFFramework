using System;
using System.ComponentModel.Composition;

using ClaimsModule.Events;
using ClaimsModule.Events.Payloads;
using ClaimsModule.Navigation;
using ClaimsModule.Services;

using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;

namespace ClaimsModule.Controller
{
    [Export]
    public class ClaimsRegionContentController
    {
        #region Constants and Fields

        private readonly IClaimsNavigator claimsNavigator;

        private readonly IEventAggregator eventAggregator;

        #endregion

        #region Constructors and Destructors

        [ImportingConstructor]
        public ClaimsRegionContentController(
            
            IEventAggregator eventAggregator,
          
            IClaimsNavigator claimsNavigator)
        {
            
            if (eventAggregator == null)
            {
                throw new ArgumentNullException("eventAggregator");
            }
           
            if (claimsNavigator == null)
            {
                throw new ArgumentNullException("claimsNavigator");
            }

           
            this.eventAggregator = eventAggregator;
           
            this.claimsNavigator = claimsNavigator;
            this.eventAggregator.GetEvent<ClaimSelectedEvent>().Subscribe(this.ClaimSelected, true);
        }

        #endregion

        #region Public Methods

        public void ClaimSelected(ClaimSelectedPayload claimSelected)
        {
            this.claimsNavigator.NavigateToClaimDetail(claimSelected.PolicyId, claimSelected.Claim);
        }

        #endregion
    }
}