using System.Collections.Generic;
using Infrastructure.Wizard.Contracts.Model;

namespace Infrastructure.Wizard.Contracts.Services
{
    public interface IWizardStepRepository
    {
        List<WizardStep> GetAllSteps();
    }
}