using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Windows.Input;

using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;

using Policy.Contracts.Models;
using Policy.Search.Events;
using Policy.Search.Services;

namespace Policy.Search.ViewModels
{
    [Export]
    public class PolicySearchViewModel : NotificationObject
    {
        #region Constants and Fields

        private readonly IEventAggregator eventAggregator;

        private readonly IPolicyDataService policyDataService;

        private readonly DelegateCommand<object> searchCommand;

        private PolicySearch policySearch;

        private PolicySearchResult policySearchResult;

        #endregion

        #region Constructors and Destructors

        [ImportingConstructor]
        public PolicySearchViewModel(IPolicyDataService policyDataService, IEventAggregator eventAggregator)
        {
            this.policyDataService = policyDataService;
            this.eventAggregator = eventAggregator;
            this.PolicySearch = new PolicySearch();
            this.searchCommand = new DelegateCommand<object>(this.Search, this.CanSearch);
            this.policySearch.ErrorsChanged += this.policySearch_ErrorsChanged;
        }

        #endregion

        #region Properties

        public PolicySearch PolicySearch
        {
            get
            {
                return this.policySearch;
            }
            set
            {
                this.policySearch = value;
                this.RaisePropertyChanged(() => this.PolicySearch);
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
                this.RaisePropertyChanged(() => this.PolicySearchResult);
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                return this.searchCommand;
            }
        }

        #endregion

        #region Public Methods

        public bool CanSearch(object arg)
        {
            return !this.policySearch.HasErrors;
        }

        public void Search(object arg)
        {
            PolicySearch tempHolderPolicySearch = this.PolicySearch;
            this.policyDataService.FindPoliciesAsync(
                this.PolicySearch,
                (result) =>
                    {
                        //Handle error etc
                        // no results etc can go here
                        this.PolicySearchResult = new PolicySearchResult
                            { PolicySearch = tempHolderPolicySearch, Result = result.Result };

                        this.eventAggregator.GetEvent<PolicySearchFoundResultEvent>().Publish(this.PolicySearchResult);
                    });
        }

        #endregion

        #region Methods

        private void policySearch_ErrorsChanged(object sender, DataErrorsChangedEventArgs e)
        {
            this.searchCommand.RaiseCanExecuteChanged();
        }

        #endregion
    }
}