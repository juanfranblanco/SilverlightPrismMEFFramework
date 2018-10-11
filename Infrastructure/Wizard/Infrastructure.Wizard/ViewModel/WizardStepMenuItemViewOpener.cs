using System;
using Infrastructure.Menu;
using Infrastructure.Wizard.Contracts.Events;
using Infrastructure.Wizard.Contracts.Model;
using Infrastructure.Wizard.Contracts.Navigator;
using Infrastructure.Wizard.Contracts.Services;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;

namespace Infrastructure.Wizard.ViewModel
{
    public class WizardStepMenuItemViewOpener<TWizardContext> : MenuItem<TWizardContext>
    {
        protected  IWizardStepsService WizardService;
        protected  IWizardNavigator<TWizardContext> Navigator { get; private set; }
        protected  IEventAggregator EventAggregator { get; private set; }
        protected  IWizardStepProgressService<TWizardContext> WizardStepProgressService { get; private set; }

        public WizardStepMenuItemViewOpener(IWizardNavigator<TWizardContext> navigator, IEventAggregator eventAggregator, IWizardStepProgressService<TWizardContext> wizardStepProgressService, IWizardStepsService wizardService)
        {
           
            if (navigator == null) throw new ArgumentNullException("navigator");
            if (eventAggregator == null) throw new ArgumentNullException("eventAggregator");
            if (wizardStepProgressService == null) throw new ArgumentNullException("wizardStepProgressService");
            if (wizardService == null) throw new ArgumentNullException("wizardService");
            this.WizardService = wizardService;
            this.Navigator = navigator;
            this.EventAggregator = eventAggregator;
            this.EventAggregator.GetEvent<WizardStepCompleted>().Subscribe(OnWizardStepCompleted, true);
            this.WizardStepProgressService = wizardStepProgressService;
            this.PropertyChanged += WizardStepMenuItemViewOpenerPropertyChanged;
            
        }

        protected void WizardStepMenuItemViewOpenerPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "MenuItemContext")
            {
                CheckIfStepIsEnabled();
            }
        }

        private void OnWizardStepCompleted(WizardStep wizardStepCompleted)
        {
            CheckIfStepIsEnabled();
        }

        private WizardStep wizardStep;
        public WizardStep WizardStep
        {
            get { return wizardStep; }
            set
            {
                wizardStep = value;
                InitialiseNewWizardStep();
            }
        }

        private void InitialiseNewWizardStep()
        {
            SetCommand();
            CheckIfStepIsEnabled();
        }

        private void CheckIfStepIsEnabled()
        {
            IsEnabled = WizardStepProgressService.IsStepEnabled(MenuItemContext, wizardStep);
        }

        public void SetCommand()
        {
            MenuItemCommand =
                new CompositeBeforeAfterDelegateCommand(() => Navigator.OpenView(WizardStep.ViewTargetName, MenuItemContext));
        }

        private bool isEnabled;
        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                isEnabled = value;
                RaisePropertyChanged(() => IsEnabled);
            }
        }

        public bool IsLastStep
        {
            get { return WizardService.IsLastStep(this.WizardStep); }
        }
    }
}