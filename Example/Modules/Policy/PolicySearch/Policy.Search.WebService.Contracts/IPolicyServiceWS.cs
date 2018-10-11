using Policy.Contracts.Models;

using System;
using System.Collections.Generic;

namespace Policy.Search.WebService.Contracts
{
    public interface IPolicyServiceWS
    {
        IAsyncResult BeginFindPolicies(PolicySearch policySearch, AsyncCallback callback, object userState);

        IEnumerable<Policy.Contracts.Models.Policy> EndFindPolicies(IAsyncResult asyncResult);
    }
}