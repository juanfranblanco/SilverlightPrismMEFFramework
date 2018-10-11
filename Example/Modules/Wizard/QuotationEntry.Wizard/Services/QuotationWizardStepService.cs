using System.ComponentModel.Composition;
using Infrastructure.Wizard.Contracts.Services;
using Infrastructure.Wizard.Services;


namespace Policy.Shell.Menu.ViewModel
{
    [Export(typeof(IWizardStepsService))]
    public class QuotationWizardStepService : WizardStepService
    {
        [ImportingConstructor]
        public QuotationWizardStepService(IWizardStepRepository wizardStepRepository)
            : base(wizardStepRepository)
        {
        }
    }
}