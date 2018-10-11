using System.ComponentModel.Composition;
using Infrastructure.Wizard.Contracts.ViewModel;
using Microsoft.Practices.Prism.Events;
using QuotationEntry.Contracts.Navigator;
using QuotationEntry.Contracts.Steps;
using QuotationEntry.Wizard.Views;

namespace QuotationEntry.RiskQuality.ViewModels
{
    [Export]
    public class ViewModel2:StepViewModelBase
    {
        [ImportingConstructor]
        public ViewModel2(IEventAggregator eventAggregator, IQuotationWizardNavigator quotationWizardNavigator)
            : base(eventAggregator, quotationWizardNavigator)
        {
        }

        public override string StepName
        {
            get { return StepNames.RISK_QUALITY_QUESTIONS_STEP_NAME; }
        }
    }
}