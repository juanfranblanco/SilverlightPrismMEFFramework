using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Windows.Data;
using System.Windows.Input;

using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;

using Policy.Contracts.Models;
using Policy.Search.Events;


namespace Policy.Search.ViewModels
{
    [Export]
    public class PolicySearchResultsViewModel : NotificationObject
    {
        #region Constants and Fields

        private readonly IEventAggregator eventAggregator;

        private readonly DelegateCommand<Policy.Contracts.Models.Policy> openPolicyCommand;

        private ICollectionView policies;

        private PolicySearchResult policySearchResult;

        #endregion

        #region Constructors and Destructors

        [ImportingConstructor]
        public PolicySearchResultsViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.openPolicyCommand = new DelegateCommand<Policy.Contracts.Models.Policy>(this.OpenPolicy);
        }

        #endregion

        #region Properties

        public ICommand OpenPolicyCommand
        {
            get
            {
                return this.openPolicyCommand;
            }
        }

        public ICollectionView Policies
        {
            get
            {
                return this.policies;
            }
            private set
            {
                this.policies = value;
                this.RaisePropertyChanged(() => this.Policies);
            }
        }

        public PolicySearchResult PolicySearchResult
        {
            get
            {
                return this.policySearchResult;
            }
            set
            {
                this.policySearchResult = value;
                this.RefreshPolicies(value);
                this.RaisePropertyChanged(() => this.PolicySearchResult);
            }
        }

        #endregion

        #region Public Methods

        public void OpenPolicy(Policy.Contracts.Models.Policy policy)
        {
            this.eventAggregator.GetEvent<PolicySearchSelectedEvent>().Publish(policy);
        }

        #endregion

        #region Methods

        private void RefreshPolicies(PolicySearchResult value)
        {
            if (value != null && this.policySearchResult.Result != null)
            {
                this.Policies = new PagedCollectionView(this.policySearchResult.Result);
            }
        }

        #endregion
    }
}