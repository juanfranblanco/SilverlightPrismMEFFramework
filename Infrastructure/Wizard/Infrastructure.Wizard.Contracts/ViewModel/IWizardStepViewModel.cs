using System;

namespace Infrastructure.Wizard.Contracts.ViewModel
{
    public interface IWizardStepViewModel
    {
        //can put here canSave, canReset to enable / disable commands on the the WizardViewModel
        void Save(Action<SaveResult> result);
        void Reset();
        string StepName { get; }
        bool CanSave();
        bool CanGoNext();
        event EventHandler DataChanged;
    }
}