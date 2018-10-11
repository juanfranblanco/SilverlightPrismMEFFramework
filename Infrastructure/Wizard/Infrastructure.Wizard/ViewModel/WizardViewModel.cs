using System;
using Infrastructure.Wizard.Contracts.Events;
using Infrastructure.Wizard.Contracts.Model;
using Infrastructure.Wizard.Contracts.Navigator;
using Infrastructure.Wizard.Contracts.Services;
using Infrastructure.Wizard.Contracts.ViewModel;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;

namespace Infrastructure.Wizard.ViewModel
{
    public class WizardViewModel<TWizardContext>: NotificationObject, IWizardViewModel    
    {
        private readonly IEventAggregator eventAggregator;
        protected IWizardStepsService WizardStepsService { get; private set; }
        protected IWizardNavigator<TWizardContext> WizardNavigator { get; private set; }
        protected IWizardStepProgressService<TWizardContext> WizardStepProgressService { get; private set; }
        private bool canGoPreviousStep;
        private bool canGoNextStep;

        private DelegateCommand nextStepCommand;
        private DelegateCommand resetCommand;
        private DelegateCommand previousStepCommand;
        private DelegateCommand saveCommand;
        
        public bool CanGoPreviousStep
        {
            get { return canGoPreviousStep; }
            set
            {
                canGoPreviousStep = value;
                RaisePropertyChanged(() => CanGoPreviousStep);
            }
        }

        public bool CanGoNextStep
        {
            get { return canGoNextStep; }
            set
            {
                canGoNextStep = value;
                RaisePropertyChanged(() => CanGoNextStep);
            }
        }

        public DelegateCommand NextStepCommand
        {
            get { return nextStepCommand; }
            set
            {
                nextStepCommand = value;
                RaisePropertyChanged(() => NextStepCommand);
            }
        }

        public DelegateCommand PreviousStepCommand
        {
            get { return previousStepCommand; }
            set
            {
                previousStepCommand = value;
                RaisePropertyChanged(() => PreviousStepCommand);
            }
        }

        public DelegateCommand SaveCommand
        {
            get { return saveCommand; }
            set
            {
                saveCommand = value;
                RaisePropertyChanged(() => SaveCommand);
            }
        }

        public DelegateCommand ResetCommand
        {
            get { return resetCommand; }
            set
            {
                resetCommand = value;
                RaisePropertyChanged(() => SaveCommand);
            }
        }

        private TWizardContext wizardContext;

        public TWizardContext WizardContext
        {
            get
            {
                return this.wizardContext;
            }
            set
            {
                this.wizardContext = value;
                RaisePropertyChanged(() => WizardContext);
                NavigateToFirstStep();
            }
        }

        private void NavigateToFirstStep()
        {
            this.ActiveStep = null;
            this.ActiveViewModel = null;
            var firstStep = WizardStepsService.GetFirstStep();
            WizardNavigator.OpenView(firstStep.ViewTargetName, WizardContext);
        }

        public WizardViewModel(IWizardStepsService wizardStepsService, IWizardNavigator<TWizardContext> wizardNavigator, IWizardStepProgressService<TWizardContext> wizardStepProgressService, IEventAggregator eventAggregator )
        {
            this.eventAggregator = eventAggregator;
            if (wizardStepsService == null) throw new ArgumentNullException("wizardStepsService");
            if (wizardNavigator == null) throw new ArgumentNullException("wizardNavigator");
            if (wizardStepProgressService == null) throw new ArgumentNullException("wizardStepProgressService");
            if (eventAggregator == null) throw new ArgumentNullException("eventAggregator");
            this.eventAggregator.GetEvent<WizardStepViewActivated>().Subscribe(SetViewAsActive);
            this.WizardStepsService = wizardStepsService;
            this.WizardNavigator = wizardNavigator;
            this.WizardStepProgressService = wizardStepProgressService;
            this.SaveCommand = new DelegateCommand(Save, CanSave);
            this.ResetCommand = new DelegateCommand(Reset);
            this.PreviousStepCommand = new DelegateCommand(MoveStepPreviousStep);
            this.NextStepCommand = new DelegateCommand(MoveStepForward, CanGoNext);
        }

        private bool CanGoNext()
        {
            //This could be current step completed || this.CanSave()
            return this.ActiveViewModel != null && this.ActiveViewModel.CanGoNext();
        }

        private bool CanSave()
        {
            return this.ActiveViewModel != null && this.ActiveViewModel.CanSave();
        }

        public void SetViewAsActive(IWizardStepViewModel wizardStepViewModel)
        {
            if (this.ActiveViewModel != null) this.ActiveViewModel.DataChanged -= ActiveViewModelDataChanged;
           
            this.ActiveViewModel = wizardStepViewModel;
            
            CheckComnandsCanExecute();
           
            this.ActiveViewModel.DataChanged += ActiveViewModelDataChanged;
            
            this.ActiveStep = WizardStepsService.GetWizardStep(wizardStepViewModel);
            
            EnableDisablePreviousStepAndForwardButtons();
            
        }

        void ActiveViewModelDataChanged(object sender, EventArgs e)
        {
            CheckComnandsCanExecute();
        }

        private void CheckComnandsCanExecute()
        {
            this.SaveCommand.RaiseCanExecuteChanged();
            this.NextStepCommand.RaiseCanExecuteChanged();
        }

        private void EnableDisablePreviousStepAndForwardButtons()
        {
            CanGoPreviousStep = this.ActiveStep.StepOrder != 1;
            CanGoNextStep = !WizardStepsService.IsLastStep(ActiveStep);
        }

        public IWizardStepViewModel ActiveViewModel { get; private set; }

        public WizardStep ActiveStep { get; private set; }

        public void MoveStepForward()
        {
            ActiveViewModel.Save(MoveToNextStepAfterSuccesfulSave);
        }

        private void MoveToNextStepAfterSuccesfulSave(SaveResult saveResult)
        {
            if (saveResult != SaveResult.Success) return;

            WizardStepProgressService.SetStepProgressCompleted(WizardContext, ActiveStep);
            var nextWizardStep = WizardStepsService.GetNextStep(ActiveStep);
            WizardNavigator.OpenView(nextWizardStep.ViewTargetName, WizardContext);
        }

        public void MoveStepPreviousStep()
        {
            var previousWizardStep = WizardStepsService.GetPreviousStep(ActiveStep);
            WizardNavigator.OpenView(previousWizardStep.ViewTargetName, WizardContext);
        }

        public void Save()
        {
            ActiveViewModel.Save(null);
        }

        public void Reset()
        {
            ActiveViewModel.Reset();
        }
    }
}