using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Infrastructure.Wizard.Contracts.ViewModel;
using Infrastructure.Wizard.ViewModel;
using Infrastructure.Wizard.Contracts.Services;
using QuotationEntry.Contracts.Navigator;


namespace Policy.Shell.Menu.ViewModel
{
    [Export(typeof(IWizardViewModel))]
    [Export(typeof(QuotationDetailWizardViewModel))]
    public class QuotationDetailWizardViewModel:WizardViewModel<int>, INavigationAware
    {
        private IRegionManager regionManager;
        public IRegionManager RegionManager
        {
            get { return regionManager; }
            set { regionManager = value;
                RaisePropertyChanged(() => RegionManager);
            }
        }

        private readonly IQuotationWizardNavigator quotationWizardNavigator;

        [ImportingConstructor]
        public QuotationDetailWizardViewModel(IWizardStepsService wizardStepsService, IQuotationWizardNavigator quotationWizardNavigator, IQuotationWizardStepProgressService quotationWizardStepProgressService, IRegionManager regionManager, IEventAggregator eventAggregator) : base(wizardStepsService, quotationWizardNavigator, quotationWizardStepProgressService, eventAggregator)
        {
            if (regionManager == null) throw new ArgumentNullException("regionManager");
            RegionManager = regionManager;
            this.quotationWizardNavigator = quotationWizardNavigator;
        }

        void INavigationAware.OnNavigatedTo(NavigationContext navigationContext)
        {
            this.WizardContext = this.quotationWizardNavigator.GetRequestedPolicyId(navigationContext).Value;
        }

        bool INavigationAware.IsNavigationTarget(NavigationContext navigationContext)
        {
            
           var policyId =  this.quotationWizardNavigator.GetRequestedPolicyId(navigationContext);
            return policyId.HasValue && this.WizardContext == policyId;
        }

        void INavigationAware.OnNavigatedFrom(NavigationContext navigationContext)
        {
            //not implemented
        }
    }
}