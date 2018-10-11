using Microsoft.Practices.Prism.ViewModel;

namespace Policy.Contracts.Models
{
    public class PolicySearchResult : NotificationObject
    {
        private PolicySearch policySearch;
        public PolicySearch PolicySearch
        {
            get { return policySearch; }
            set { policySearch = value; 
            RaisePropertyChanged(() => PolicySearch);}
        }

        private PolicyCollection result;
        public PolicyCollection Result
        {
            get { return result; }
            set { result = value; 
            RaisePropertyChanged(() => Result);
            }
        }
    }
}