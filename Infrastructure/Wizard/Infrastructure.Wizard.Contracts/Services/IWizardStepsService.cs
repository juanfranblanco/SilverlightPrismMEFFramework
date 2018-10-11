using System.Collections.Generic;
using Infrastructure.Wizard.Contracts.Model;
using Infrastructure.Wizard.Contracts.ViewModel;

namespace Infrastructure.Wizard.Contracts.Services
{
    /// Menu
    /// Region with a view
    /// PreviousStep / next / etc


    ///Module register wizard step
    /// 
    ///Wizard View contains menu
    ///Region and flow controls
    ///
    ///Wizard View is bound to a context item
    ///on context item change, should be a check on / navigation controls enable disable
    ///on next it should raise event to enable / navigate to the next view.
    /// 

    //The implementation of this will bring the steps for the application (and get the current)
    public interface IWizardStepsService
    {
        WizardStep GetNextStep(WizardStep currentStep);
        WizardStep GetPreviousStep(WizardStep currentStep);
       
        WizardStep GetWizardStep(IWizardStepViewModel wizardStepViewModel);
        
        WizardStep GetFirstStep();
        bool IsLastStep(WizardStep step);
        List<WizardStep> GetAllSteps();
    }
}