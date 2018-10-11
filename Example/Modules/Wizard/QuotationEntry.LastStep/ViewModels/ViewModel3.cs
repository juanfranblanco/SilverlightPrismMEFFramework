using System.ComponentModel.Composition;
using Infrastructure.Wizard.Contracts.ViewModel;
using Microsoft.Practices.Prism.Events;
using QuotationEntry.Contracts.Navigator;
using QuotationEntry.Contracts.Steps;
using QuotationEntry.Wizard.Views;

namespace QuotationEntry.LastStep.ViewModels
{
    [Export]
    public class ViewModel3:StepViewModelBase
    {
        [ImportingConstructor]
        public ViewModel3(IEventAggregator eventAggregator, IQuotationWizardNavigator quotationWizardNavigator)
            : base(eventAggregator, quotationWizardNavigator)
        {
        }

        public override string StepName
        {
            get { return StepNames.LAST_ONE_STEP_NAME; }
        }
    }
}