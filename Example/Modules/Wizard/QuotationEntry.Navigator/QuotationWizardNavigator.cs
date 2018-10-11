using System;
using System.ComponentModel.Composition;
using Infrastructure.Navigator;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Policy.Shell.Contracts.Navigation;
using QuotationEntry.Contracts.Navigator;
using QuotationEntry.Contracts.Regions;

namespace Policy.Shell.Menu.Commands
{
    [Export(typeof (IQuotationWizardNavigator))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class QuotationWizardNavigator : IQuotationWizardNavigator
    {
        private readonly NavigationService navigator;
        private string text;

        [ImportingConstructor]
        public QuotationWizardNavigator([Import("NavigationService")]NavigationService navigator)
        {
            if (navigator == null) throw new ArgumentNullException("navigator");
            this.navigator = navigator;
            this.text = Guid.NewGuid().ToString();
        }

        public void OpenView(string targetName, int policyId)
        {
            navigator.OpenView(targetName, new UriQuery {{NavigationParameters.POLICY_ID, policyId.ToString()}},
                               RegionNames.WIZARD_QUOTATION_DETAIL_REGION);
        }

        public int? GetRequestedPolicyId(NavigationContext navigationContext)
        {
            return navigator.GetNavigationParameterIntValue(navigationContext, NavigationParameters.POLICY_ID);
        }
    }
}