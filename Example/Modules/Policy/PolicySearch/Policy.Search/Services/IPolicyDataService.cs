using System;

using Infrastructure.Core.Services;

using Policy.Contracts.Models;

namespace Policy.Search.Services
{
    public interface IPolicyDataService
    {
        #region Public Methods

        void FindPoliciesAsync(PolicySearch policySearch, Action<IOperationResult<PolicyCollection>> callback);

        #endregion
    }
}