using System;
using System.ComponentModel.Composition;
using System.Windows.Input;

using ClaimsModule.Events;
using ClaimsModule.Events.Payloads;
using ClaimsModule.Models;
using ClaimsModule.Navigation;
using ClaimsModule.Services;

using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;

using Policy.Shell.Contracts.Navigation;

namespace ClaimsModule.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ClaimsListViewModel : NotificationObject, INavigationAware
    {
        #region Constants and Fields

        private readonly IClaimDataService claimsDataService;
        private readonly IEventAggregator eventAggregator;
        private readonly IClaimsNavigator claimsNavigator;

        private readonly DelegateCommand<Claim> openClaimCommand;

        #endregion

        #region Constructors and Destructors

        [ImportingConstructor]
        public ClaimsListViewModel(IClaimDataService claimsDataService, IEventAggregator eventAggregator, IClaimsNavigator claimsNavigator)
        {
            this.claimsDataService = claimsDataService;
            this.eventAggregator = eventAggregator;
            this.claimsNavigator = claimsNavigator;
            this.Claims = new ClaimsCollection();
            this.openClaimCommand = new DelegateCommand<Claim>(this.OpenClaim);
        }

        #endregion

        #region Properties

        public ClaimsCollection Claims { get; private set; }

        public ICommand OpenClaimCommand
        {
            get
            {
                return this.openClaimCommand;
            }
        }

        public int? PolicyId { get; set; }

        #endregion

        #region Public Methods

        public void OpenClaim(Claim claim)
        {
            this.eventAggregator.GetEvent<ClaimSelectedEvent>().Publish(
                new ClaimSelectedPayload { Claim = claim, PolicyId = this.PolicyId.Value });
        }

        #endregion

        #region Implemented Interfaces

        #region INavigationAware

        bool INavigationAware.IsNavigationTarget(NavigationContext navigationContext)
        {
            if (this.PolicyId == null)
            {
                return true;
            }

            int? requestedPolicyId = claimsNavigator.GetRequestedPolicyId(navigationContext);

            return requestedPolicyId.HasValue && requestedPolicyId == this.PolicyId;
        }

        void INavigationAware.OnNavigatedFrom(NavigationContext navigationContext)
        {
            // Intentionally not implemented.
        }

        void INavigationAware.OnNavigatedTo(NavigationContext navigationContext)
        {
            this.PolicyId = claimsNavigator.GetRequestedPolicyId(navigationContext);

            if (this.PolicyId.HasValue)
            {
                this.claimsDataService.GetClaimsForCostingAsync(
                    this.PolicyId.Value, (result) => { this.RefreshClaims(result.Result); });
            }
        }

        #endregion

        #endregion

        //This should be refactored to a infrastructure class passing as a parameter the "parameter name"

        #region Methods

        private void RefreshClaims(ClaimsCollection claims)
        {
            this.Claims.Clear();
            if (claims == null)
            {
                return;
            }

            foreach (Claim claim in claims)
            {
                this.Claims.Add(claim);
            }
        }

        #endregion
    }
}