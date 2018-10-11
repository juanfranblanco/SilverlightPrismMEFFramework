using System.ComponentModel.DataAnnotations;

using Policy.Contracts.Models;
using Policy.Validation.Core;

namespace Policy.Contracts.Validation
{
    public class PolicySearchCrossFieldValidation
    {
        public static ValidationResult ValidationPolicySearch(PolicySearch policySearch, ValidationContext context)
        {
            if (policySearch != null)
            {
                return PolicySearchValidation.ValidationPolicySearch(
                    policySearch.PolicyId, policySearch.CompanyNameSearch, context);
            }

            return ValidationResult.Success;
        }
    }
}