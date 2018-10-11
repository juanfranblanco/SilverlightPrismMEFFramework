using Infrastructure.Wizard.Contracts.Model;
using Infrastructure.Wizard.Contracts.ViewModel;
using Microsoft.Practices.Prism.Events;

namespace Infrastructure.Wizard.Contracts.Events
{
    public class WizardStepViewActivated : CompositePresentationEvent<IWizardStepViewModel>
    {

    }
}