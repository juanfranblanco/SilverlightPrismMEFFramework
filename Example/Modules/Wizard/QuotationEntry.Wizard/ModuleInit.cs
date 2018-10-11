using System.ComponentModel.Composition;

using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.ServiceLocation;
using Policy.Shell.Contracts;
using Policy.Shell.Menu.ViewModel;
using QuotationEntry.Contracts.Regions;
using QuotationEntry.Wizard.Views;

namespace QuotationEntry.Wizard
{
    [ModuleExport("QuotationEntry.Wizard.ModuleInit", typeof(QuotationEntry.Wizard.ModuleInit))]
    public class ModuleInit : IModule
    {
        private readonly IRegionManager regionManager;
        public IServiceLocator serviceLocator;
        private readonly IPolicyDetailMenuRegistry policyDetailMenuRegistry;
        private readonly IPolicyDetailContext policyDetailContext;

        [ImportingConstructor]
        public ModuleInit(IRegionManager regionManager, IServiceLocator serviceLocator, IPolicyDetailMenuRegistry policyDetailMenuRegistry, IPolicyDetailContext policyDetailContext)
        {
            this.regionManager = regionManager;
            this.serviceLocator = serviceLocator;
            this.policyDetailMenuRegistry = policyDetailMenuRegistry;
            this.policyDetailContext = policyDetailContext;
        }

        #region IModule Members

        public void Initialize()
        {
            //There is an issue with the scoped regions (ie not getting the right region manager) so it has to be done manually the injection of the menu.
            var wizardView = serviceLocator.GetInstance<QuotationDetailWizardView>(QuotationDetailWizardView.CONTRACT_TYPE);
          

            var policyDetailRegion = regionManager.Regions[Policy.Shell.Contracts.Shell.RegionNames.POLICY_DETAIL_REGION];
            policyDetailRegion.Add(wizardView);

            ((QuotationDetailWizardViewModel)(wizardView.DataContext)).WizardContext =
              policyDetailContext.CurrentPolicyId;
         
            var wizardMenuRegion = regionManager.Regions[RegionNames.WIZARD_QUOTATION_MENU_REGION];
            var menuView = serviceLocator.GetInstance<QuotationDetailMenuView>();
            wizardMenuRegion.Add(menuView);
            wizardMenuRegion.Activate(menuView);
           
            //Register the wizard as a menu item for the Policy Detail
            policyDetailMenuRegistry.RegisterMenuItemViewOpener("Quotation wizard", "Quotation wizard",
                                                                typeof (QuotationDetailMenuView), QuotationDetailWizardView.CONTRACT_TYPE, 5);
                

            
            
        }

        #endregion
    }
}
