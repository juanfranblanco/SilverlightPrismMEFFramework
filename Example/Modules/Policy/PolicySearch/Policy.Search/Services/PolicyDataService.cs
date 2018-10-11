using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Threading;

using Infrastructure.Core.Services;

using Policy.Contracts.Models;
using Policy.Search.WebService.Contracts;

namespace Policy.Search.Services
{
    [Export(typeof(IPolicyDataService))]
    public class PolicyDataService : IPolicyDataService
    {
        #region Constants and Fields

        private readonly SynchronizationContext synchronizationContext = SynchronizationContext.Current ??
                                                                         new SynchronizationContext();

        #endregion

        #region Constructors and Destructors

        [ImportingConstructor]
        public PolicyDataService(IPolicyServiceWS policyServiceWS)
        {
            this.PolicyServiceWS = policyServiceWS;
        }

        #endregion

        #region Properties

        public IPolicyServiceWS PolicyServiceWS { get; set; }

        #endregion

        #region Public Methods

        public PolicyCollection MapPolicySearchResultToPolicyCollection(IEnumerable<Policy.Contracts.Models.Policy> policies)
        {
            var policyCollection = new PolicyCollection();
            foreach (var policy in policies)
            {
                policyCollection.Add(policy);
            }
            return policyCollection;
        }

        #endregion

        #region Implemented Interfaces

        #region IPolicyDataService

        public void FindPoliciesAsync(PolicySearch policySearch, Action<IOperationResult<PolicyCollection>> callback)
        {
            this.PolicyServiceWS.BeginFindPolicies(
                policySearch,
                (ar) =>
                    {
                        var operationResult = new OperationResult<PolicyCollection>();
                        try
                        {
                            PolicyCollection policies =
                                this.MapPolicySearchResultToPolicyCollection(this.PolicyServiceWS.EndFindPolicies(ar));
                            operationResult.Result = policies;
                        }
                        catch (Exception ex)
                        {
                            operationResult.Error = ex;
                        }

                        this.synchronizationContext.Post((state) => callback(operationResult), null);
                    },
                null);
        }

        #endregion

        #endregion
    }
}