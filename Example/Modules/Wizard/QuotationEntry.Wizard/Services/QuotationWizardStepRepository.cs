using System.Collections.Generic;
using System.ComponentModel.Composition;
using Infrastructure.Wizard.Contracts.Services;
using Infrastructure.Wizard.Contracts.Model;
using QuotationEntry.Contracts.Steps;


namespace Policy.Shell.Menu.ViewModel
{
    [Export(typeof(IWizardStepRepository))]
    public class QuotationWizardStepRepository:IWizardStepRepository
    {
        

        public List<WizardStep> GetAllSteps()
        {
            return new List<WizardStep>()
                       {
                           new WizardStep()
                               {
                                   StepName = StepNames.POLICY_DETAIL_STEP_NAME,
                                   Description = "View 1",
                                   StepOrder = 1,
                                   ViewTargetName = StepNames.POLICY_DETAIL_VIEW_TARGET_NAME

                               },

                           new WizardStep()
                               {
                                   StepName = StepNames.RISK_QUALITY_QUESTIONS_STEP_NAME,
                                   Description = "View 2",
                                   StepOrder = 2,
                                   ViewTargetName = StepNames.RISK_QUALITY_QUESTIONS_VIEW_TARGET_NAME

                               },

                           new WizardStep()
                               {
                                   StepName = StepNames.PRE_INCEPTION_STEP_NAME,
                                   Description = "View 3",
                                   StepOrder = 3,
                                   ViewTargetName = StepNames.PRE_INCEPTION_VIEW_TARGET_NAME

                               },

                           new WizardStep()
                               {
                                   StepName = StepNames.LAST_ONE_STEP_NAME,
                                   Description = "View 4",
                                   StepOrder = 4,
                                   ViewTargetName = StepNames.LAST_ONE_VIEW_TARGET_NAME

                               },

                       };
        }
    }
}