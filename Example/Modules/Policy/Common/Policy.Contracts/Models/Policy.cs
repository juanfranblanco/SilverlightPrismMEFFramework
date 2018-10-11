using Microsoft.Practices.Prism.ViewModel;

namespace Policy.Contracts.Models
{
    public class Policy : NotificationObject
    {
        private string _companyName;
        private string _description;
        private int _policyId;

        public int PolicyId
        {
            get { return _policyId; }
            set
            {
                _policyId = value;
                RaisePropertyChanged(() => PolicyId);
            }
        }

        public string CompanyName
        {
            get { return _companyName; }
            set
            {
                _companyName = value;
                RaisePropertyChanged(() => CompanyName);
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                RaisePropertyChanged(() => Description);
            }
        }
    }
}