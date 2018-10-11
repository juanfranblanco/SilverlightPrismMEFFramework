using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;
using Policy.Shell.Menu.ViewModel;

namespace QuotationEntry.Wizard.Views
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class QuotationDetailMenuView : UserControl
    {
        private string id;
        public QuotationDetailMenuView()     
        {         
           
            InitializeComponent();

            RegionContext.GetObservableContext(this).PropertyChanged += (s, e)
                                                                        =>
                                                                        ViewModel.MenuContext =
          
                                                                        (int)RegionContext.GetObservableContext(this).Value;

            id = Guid.NewGuid().ToString();
        }

        [Import(AllowRecomposition = false)]
        public QuotationDetailWizardMenuViewModel ViewModel
        {
            get { return DataContext as QuotationDetailWizardMenuViewModel; }
            set { DataContext = value; }
        }

       
    }
}