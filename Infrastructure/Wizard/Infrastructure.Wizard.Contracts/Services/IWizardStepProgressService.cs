using Infrastructure.Wizard.Contracts.Model;

namespace Infrastructure.Wizard.Contracts.Services
{
    public interface IWizardStepProgressService<TWizardContext>
    {
        void SetStepProgressCompleted(TWizardContext wizardContextId, WizardStep stepCompleted);
        int  GetCurrentStepProgress(TWizardContext wizardContextId);
        bool IsStepEnabled(TWizardContext wizardContext, WizardStep wizardStep);
    }
}