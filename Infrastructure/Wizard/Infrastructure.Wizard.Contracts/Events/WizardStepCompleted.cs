using Infrastructure.Wizard.Contracts.Model;
using Microsoft.Practices.Prism.Events;

namespace Infrastructure.Wizard.Contracts.Events
{
    public class WizardStepCompleted : CompositePresentationEvent<WizardStep>
    {

    }
}