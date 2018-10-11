using System;
using System.ComponentModel.Composition;
using System.Windows.Input;

using ClaimsModule.Models;
using ClaimsModule.Navigation;
using ClaimsModule.Services;

using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;

namespace ClaimsModule.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ClaimDetailViewModel : NotificationObject, INavigationAware
    {
        #region Constants and Fields

        private readonly IClaimDataService claimDataService;

        private readonly IClaimsNavigator claimsNavigator;

        private readonly DelegateCommand goBackCommand;

        private Claim claim;

        private IRegionNavigationJournal regionNavigationJournal;

        #endregion

        #region Constructors and Destructors

        [ImportingConstructor]
        public ClaimDetailViewModel(IClaimDataService claimDataService, IClaimsNavigator claimsNavigator)
        {
            this.goBackCommand = new DelegateCommand(this.GoBack);
            this.claimDataService = claimDataService;
            this.claimsNavigator = claimsNavigator;
        }

        #endregion

        #region Properties

        public Claim Claim
        {
            get
            {
                return this.claim;
            }

            set
            {
                if (this.claim != value)
                {
                    this.claim = value;
                    this.RaisePropertyChanged(() => this.Claim);
                }
            }
        }

        public ICommand GoBackCommand
        {
            get
            {
                return this.goBackCommand;
            }
        }

        public int? PolicyId { get; set; }

        #endregion

        //This should be refactored to a infrastructure class passing as a parameter the "parameter name"

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
            int? claimId = claimsNavigator.GetRequestedClaimId(navigationContext);

            // When this view model is navigated to, it gathers the
            // requested ClaimId from the navigation context's parameters.
            if (claimId != null)
            {
                this.Claim = this.claimDataService.GetClaim(claimId.Value);
            }

            this.regionNavigationJournal = navigationContext.NavigationService.Journal;
        }

        #endregion

        #endregion

        #region Methods

        

        private void GoBack()
        {
            //Probably better to return where we want as opposed to use the journal as it may take us somewhere else 
            // claimsNavigator.NavigateToClaimList(policyId.Value);
            this.regionNavigationJournal.GoBack();
        }

        #endregion
    }
}