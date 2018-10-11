using System;
using System.Collections.Generic;
using System.Threading;

using Infrastructure.Core.Async;

using Policy.Contracts.Models;

using Model = Policy.Contracts.Models;
using Policy.Search.WebService.Contracts;

using System.ComponentModel.Composition;

namespace Policy.Search.WebService.Mock
{
    [Export(typeof(IPolicyServiceWS))]
    public class PolicyServiceWS : IPolicyServiceWS
    {
        // This is to support demonstration of a failed submit.
        public static bool FailOnSubmit { get; set; }

        #region IPolicyServiceWS Members

        public IAsyncResult BeginFindPolicies(PolicySearch policySearch, AsyncCallback callback, object userState)
        {
            var asyncResult = new AsyncResult<IEnumerable<Model.Policy>>(callback, userState);
            ThreadPool.QueueUserWorkItem(
                o => asyncResult.SetComplete(this.CreatePolicies(policySearch), false));

            return asyncResult;
        }


        public IEnumerable<Model.Policy> EndFindPolicies(IAsyncResult asyncResult)
        {
            AsyncResult<IEnumerable<Model.Policy>> localAsyncResult = AsyncResult<IEnumerable<Model.Policy>>.End(asyncResult);

            return localAsyncResult.Result;
        }

        #endregion

        private IEnumerable<Model.Policy> CreatePolicies(PolicySearch policySearch)
        {
            if (policySearch.PolicyId != null)
            {
                return new[]
                           {
                               new Model.Policy
                                   {
                                       PolicyId = policySearch.PolicyId.Value,
                                       CompanyName = "My company name",
                                       Description = "Great customer, low risk!!"
                                   }
                           };
            }

            return new[]
                       {
                           new Model.Policy
                               {
                                   PolicyId = 1,
                                   CompanyName = policySearch.CompanyNameSearch + " company",
                                   Description = "Great customer"
                               },
                           new Model.Policy
                               {
                                   PolicyId = 2,
                                   CompanyName = policySearch.CompanyNameSearch + " company 2 xxx",
                                   Description = "The best customer ever"
                               }
                       };
        }
    }
}