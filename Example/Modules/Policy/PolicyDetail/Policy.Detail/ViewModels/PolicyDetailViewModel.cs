using System;
using System.ComponentModel.Composition;

using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;

using Policy.Shell.Contracts.Navigation;

using Model = Policy.Contracts.Models;

namespace Policy.Detail.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class PolicyDetailViewModel : NotificationObject, INavigationAware
    {
        private Model.Policy policy;


        [ImportingConstructor]
        public PolicyDetailViewModel()
        {
        }

        public Model.Policy Policy
        {
            get { return policy; }
            set
            {
                policy = value;
                this.RaisePropertyChanged(() => Policy);
            }
        }

        bool INavigationAware.IsNavigationTarget(NavigationContext navigationContext)
        {
            if (this.Policy == null)
            {
                return true;
            }

            var requestedPolicyId = GetRequestedPolicyId(navigationContext);

            return requestedPolicyId.HasValue && requestedPolicyId == this.Policy.PolicyId;
        }

        void INavigationAware.OnNavigatedFrom(NavigationContext navigationContext)
        {
            // Intentionally not implemented.
        }

        void INavigationAware.OnNavigatedTo(NavigationContext navigationContext)
        {


        }

        //This should be refactored to a infrastructure class passing as a parameter the "parameter name"
        private int? GetRequestedPolicyId(NavigationContext navigationContext)
        {
            var policyIdParam = navigationContext.Parameters[NavigationParameters.POLICY_ID];

            int policyId;
            if (policyIdParam != null && Int32.TryParse(policyIdParam, out policyId))
            {
                return policyId;
            }

            return null;
        }

    }
}