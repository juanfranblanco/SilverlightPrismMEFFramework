using System;
using System.ComponentModel.Composition;

using Microsoft.Practices.Prism.Events;

using Policy.Contracts.Models;
using Policy.Search.Events;
using Policy.Shell.Contracts;

namespace Policy.Search.Controllers
{
    [Export]
    public class PolicySearchController
    {
        #region Constants and Fields

        private readonly IEventAggregator eventAggregator;

        private readonly IPolicyWindowService policyWindowService;

        #endregion

        #region Constructors and Destructors

        [ImportingConstructor]
        public PolicySearchController(IEventAggregator eventAggregator, IPolicyWindowService policyWindowService)
        {
            if (eventAggregator == null)
            {
                throw new ArgumentNullException("eventAggregator");
            }
            if (policyWindowService == null)
            {
                throw new ArgumentNullException("policyWindowService");
            }

            this.eventAggregator = eventAggregator;
            this.policyWindowService = policyWindowService;

            this.eventAggregator.GetEvent<PolicySearchFoundResultEvent>().Subscribe(this.PolicySearchFoundResult, true);
            this.eventAggregator.GetEvent<PolicySearchSelectedEvent>().Subscribe(this.OpenPolicyWindow, true);
        }

        #endregion

        #region Public Methods

        public void OpenPolicyWindow(Policy.Contracts.Models.Policy policy)
        {
            this.policyWindowService.OpenPolicyWindow(policy);
        }

        public void PolicySearchFoundResult(PolicySearchResult policySearchResult)
        {
            if (policySearchResult.Result != null && policySearchResult.Result.Count == 1)
            {
                this.OpenPolicyWindow(policySearchResult.Result[0]);
            }
        }

        #endregion
    }
}