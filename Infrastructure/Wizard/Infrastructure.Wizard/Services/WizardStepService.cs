using System.Collections.Generic;
using System.Linq;
using Infrastructure.Wizard.Contracts.Model;
using Infrastructure.Wizard.Contracts.Services;
using Infrastructure.Wizard.Contracts.ViewModel;

namespace Infrastructure.Wizard.Services
{
    public class WizardStepService:IWizardStepsService
    {
        protected IWizardStepRepository WizardStepRepository { get; private set; }
        private List<WizardStep> steps;

        public WizardStepService(IWizardStepRepository wizardStepRepository)
        {
            this.WizardStepRepository = wizardStepRepository;
            InitialiseSteps();
        }

        protected void InitialiseSteps()
        {
            steps = WizardStepRepository.GetAllSteps();
        }

        public WizardStep GetNextStep(WizardStep currentStep)
        {
            return steps.FirstOrDefault(step => step.StepOrder == currentStep.StepOrder + 1);
        }

        public WizardStep GetPreviousStep(WizardStep currentStep)
        {
            return steps.FirstOrDefault(step => step.StepOrder == currentStep.StepOrder - 1);
        }

        public WizardStep GetWizardStep(IWizardStepViewModel wizardStepViewModel)
        {
            return steps.FirstOrDefault(step => step.StepName == wizardStepViewModel.StepName);
        }

        public WizardStep GetFirstStep()
        {
            return steps.FirstOrDefault(step => step.StepOrder == 1);
        }

        public bool IsLastStep(WizardStep step)
        {
            return step.StepOrder == GetLastStep().StepOrder;
        }

        public WizardStep GetLastStep()
        {
            return steps.OrderByDescending(step => step.StepOrder).First();
        }

        public List<WizardStep> GetAllSteps()
        {
            return this.steps;
        }
    }
}
