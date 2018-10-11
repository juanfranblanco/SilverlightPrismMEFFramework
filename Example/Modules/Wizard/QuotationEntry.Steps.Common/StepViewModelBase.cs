using System;
using System.Threading;
using Infrastructure.Wizard.Contracts.Events;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;
using Infrastructure.Wizard.Contracts.ViewModel;
using QuotationEntry.Contracts.Navigator;


namespace QuotationEntry.Wizard.Views
{
    public abstract class StepViewModelBase:NotificationObject, IActiveAware, INavigationAware, IWizardStepViewModel
    {
        //private readonly IWizardViewModel wizardViewModel;
        private readonly IEventAggregator eventAggregator;
        private readonly IQuotationWizardNavigator quotationWizardNavigator;
        
        private int? policyId;
        public int? PolicyId
        {
            get { return policyId; }
            set { policyId = value;
                RaisePropertyChanged(() => PolicyId);
            }
        }

        private bool isActive;
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value;
                eventAggregator.GetEvent<WizardStepViewActivated>().Publish(this);
                RaisePropertyChanged(() => IsActive);
            }
        }

        private string text;
        public string Text
        {
            get { return text; }
            set {
                if (!String.IsNullOrEmpty(value) && value != Text)IsDirty = true;
                text = value;
                RaisePropertyChanged(() => Text);
                RaiseDataChanged();
            }
        }

        protected bool IsDirty { get; set; }


        private string status;
        public string Status
        {
            get { return status; }
            set { status = value;
                RaisePropertyChanged(() => Status);}
        }


        protected StepViewModelBase(IEventAggregator eventAggregator, IQuotationWizardNavigator quotationWizardNavigator)
        {
            //this.wizardViewModel = wizardViewModel;
            this.eventAggregator = eventAggregator;
            this.quotationWizardNavigator = quotationWizardNavigator;
            Status = "NOT Saved";
            IsDirty = false;

        }

        

        public event EventHandler IsActiveChanged = delegate { };
 
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            PolicyId = quotationWizardNavigator.GetRequestedPolicyId(navigationContext);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            if(PolicyId == null) return true;
            var policyIdNav = quotationWizardNavigator.GetRequestedPolicyId(navigationContext);
            return policyIdNav == PolicyId;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            //nothing here;
        }

        public virtual void Save(Action<SaveResult> result)
        {
            Status = "Saved";
            IsDirty = false;
            RaiseDataChanged();
            //Save successful
            if (result != null)
            {
                result(SaveResult.Success);
            }

        }

        protected void RaiseDataChanged()
        {
            if(DataChanged != null)
            {
                DataChanged(this, null);
            }
        }

        public bool CanSave()
        {
            return IsDirty;
        }

        public bool CanGoNext()
        {
            return !String.IsNullOrEmpty(Text);
        }

        public event EventHandler DataChanged;

        public void Reset()
        {
            IsDirty = false;
            Text = String.Empty;
        }

        public abstract string StepName { get; }
    }
}