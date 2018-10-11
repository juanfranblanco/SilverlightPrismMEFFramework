using System;
using System.ComponentModel.Composition;
using Infrastructure.Wizard.Contracts.Services;
using Microsoft.Practices.Prism.Events;
using Infrastructure.Wizard.ViewModel;
using QuotationEntry.Contracts.Navigator;

namespace Policy.Shell.Menu.ViewModel
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class QuotationDetailWizardStepMenuItemViewOpener : WizardStepMenuItemViewOpener<int>
    {
        [ImportingConstructor]
        public QuotationDetailWizardStepMenuItemViewOpener(IQuotationWizardNavigator navigator, IEventAggregator eventAggregator, IQuotationWizardStepProgressService wizardStepProgressService, IWizardStepsService quotationWizardStepService)
            : base(navigator, eventAggregator, wizardStepProgressService, quotationWizardStepService)
        {
          
        }
    }
}