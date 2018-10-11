using System.ComponentModel.Composition;
using Infrastructure.Wizard.Contracts.ViewModel;
using Microsoft.Practices.Prism.Events;
using QuotationEntry.Contracts.Navigator;
using QuotationEntry.Contracts.Steps;
using QuotationEntry.Wizard.Views;

namespace QuotationEntry.PolicyDetail.ViewModels
{
    [Export]
    public class ViewModel1:StepViewModelBase
    {
        [ImportingConstructor]
        public ViewModel1(IEventAggregator eventAggregator, IQuotationWizardNavigator quotationWizardNavigator)
            : base(eventAggregator, quotationWizardNavigator)
        {
        }

        public override string StepName
        {
            get { return StepNames.POLICY_DETAIL_STEP_NAME; }
        }
    }
}