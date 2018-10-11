using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Infrastructure.Wizard.ViewModel;
using Infrastructure.Wizard.Contracts.Services;


namespace Policy.Shell.Menu.ViewModel
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class QuotationDetailWizardMenuViewModel : WizardMenuViewModel<int, QuotationDetailWizardStepMenuItemViewOpener>
    {
      
        [ImportingConstructor]
        public QuotationDetailWizardMenuViewModel(IWizardStepsService wizardStepsService, IServiceLocator serviceLocator) : base(wizardStepsService, serviceLocator)
        {
          
        }

    }
}
