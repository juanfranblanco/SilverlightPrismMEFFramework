namespace Infrastructure.Wizard.Contracts.ViewModel
{
    public interface IWizardViewModel
    {
        void SetViewAsActive(IWizardStepViewModel wizardStepViewModel);
        IWizardStepViewModel ActiveViewModel { get; }
    }
}