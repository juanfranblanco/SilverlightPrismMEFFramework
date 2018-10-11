using System;
using System.ComponentModel.DataAnnotations;

namespace Policy.Validation.Core
{
    public class PolicySearchValidation
    {
        public static ValidationResult ValidationCompanyNameSearch(string value, ValidationContext context)
        {
            if (!string.IsNullOrEmpty(value) && value.Length <= 3)
            {
                return new ValidationResult
                    (
                    "Company search should have at least 3 characters",
                    new[] {context.MemberName});
            }

            return ValidationResult.Success;
        }

        public static ValidationResult ValidationPolicyId(int? value, ValidationContext context)
        {
            if (value != null && value.Value < 0)
            {
                return new ValidationResult
                    (
                    "Policy Id has to be a positive number",
                    new[] {context.MemberName});
            }

            return ValidationResult.Success;
        }

        public static ValidationResult ValidationPolicySearch(int? policyId, string  companyNameSearch, ValidationContext context)
        {
            if (policyId == null && String.IsNullOrEmpty(companyNameSearch))
            {
                return new ValidationResult
                    (
                    "PolicyId or CompanySearchName needs to be populated",
                    new[] { context.MemberName });
            }

            return ValidationResult.Success;
        }
    }
}