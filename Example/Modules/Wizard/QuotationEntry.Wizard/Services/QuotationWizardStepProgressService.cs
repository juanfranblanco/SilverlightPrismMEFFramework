using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using Infrastructure.Wizard.Contracts.Model;
using Infrastructure.Wizard.Contracts.Events;

namespace Policy.Shell.Menu.ViewModel
{
    [Export(typeof(IQuotationWizardStepProgressService))]
    public class QuotationWizardStepProgressService : IQuotationWizardStepProgressService
    {
        private readonly IEventAggregator eventAggregator;

        [ImportingConstructor]
        public QuotationWizardStepProgressService(IEventAggregator eventAggregator)
        {
            if (eventAggregator == null) throw new ArgumentNullException("eventAggregator");
            this.eventAggregator = eventAggregator;
        }

        //start always at zero.
        private int currentStepCompleted = 2;

        public void SetStepProgressCompleted(int policyId, WizardStep stepCompleted)
        {
            currentStepCompleted = stepCompleted.StepOrder;
            eventAggregator.GetEvent<WizardStepCompleted>().Publish(stepCompleted);
        }

        public int GetCurrentStepProgress(int policyId)
        {
            return currentStepCompleted;
        }

        public bool IsStepEnabled(int policyId, WizardStep wizardStep)
        {
            return wizardStep.StepOrder <= currentStepCompleted + 1;
        }
    }
}