using System;
using Infrastructure.Menu;
using Infrastructure.Wizard.Contracts.Services;
using Microsoft.Practices.ServiceLocation;

namespace Infrastructure.Wizard.ViewModel
{
    public class WizardMenuViewModel<TWizardContext, TWizardMenuItem>: MenuViewModel<TWizardContext> where TWizardMenuItem: WizardStepMenuItemViewOpener<TWizardContext>
    {
        protected IWizardStepsService WizardStepsService { get; private set; }
        protected IServiceLocator ServiceLocator { get; private set; }

        public WizardMenuViewModel(IWizardStepsService wizardStepsService, IServiceLocator serviceLocator)
        {
            if (wizardStepsService == null) throw new ArgumentNullException("wizardStepsService");
            if (serviceLocator == null) throw new ArgumentNullException("serviceLocator");
            this.WizardStepsService = wizardStepsService;
            this.ServiceLocator = serviceLocator;
            LoadMenuItems();
        }

        protected void LoadMenuItems()
        {
            var steps = WizardStepsService.GetAllSteps();
            foreach (var wizardStep in steps)
            {
                var menuItem = ServiceLocator.GetInstance<TWizardMenuItem>();
                menuItem.WizardStep = wizardStep;
                menuItem.Name = wizardStep.StepName;
                menuItem.Description = wizardStep.Description;
                menuItem.OrderHint = wizardStep.StepOrder;
                menuItem.MenuItemContext = this.MenuContext;
                this.AddMenuItem(menuItem);
            }
        }
    }
}