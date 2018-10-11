using System.ComponentModel.DataAnnotations;

using Infrastructure.Core.Model;

using Policy.Contracts.Validation;
using Policy.Validation.Core;

namespace Policy.Contracts.Models
{
    [CustomValidation(typeof(PolicySearchCrossFieldValidation), "ValidationPolicySearch")]
    public class PolicySearch : DomainObject
    {
        private string companyNameSearch;
        private int? policyId;

        public PolicySearch()
        {
            //Set the default errors for the whole object (ValidationPolicySearch)
            ValidateObject();
        }

        [CustomValidation(typeof (PolicySearchValidation), "ValidationPolicyId")]
        public int? PolicyId
        {
            get { return policyId; }
            set
            {
                if (policyId == value) return;
                ValidateProperty("PolicyId", value);
                policyId = value;
                RaisePropertyChanged(() => PolicyId);
            }
        }

        [CustomValidation(typeof (PolicySearchValidation), "ValidationCompanyNameSearch")]
        public string CompanyNameSearch
        {
            get { return companyNameSearch; }
            set
            {
                if (companyNameSearch == value) return;
                ValidateProperty("CompanyNameSearch", value);
                companyNameSearch = value;
                RaisePropertyChanged(() => CompanyNameSearch);
            }
        }

    }
}