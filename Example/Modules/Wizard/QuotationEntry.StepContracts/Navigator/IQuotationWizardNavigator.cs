using Microsoft.Practices.Prism.Regions;
using Infrastructure.Wizard.Contracts.Navigator;

namespace QuotationEntry.Contracts.Navigator
{
    public interface IQuotationWizardNavigator:IWizardNavigator<int>
    {
        int? GetRequestedPolicyId(NavigationContext navigationContext);
    }

}