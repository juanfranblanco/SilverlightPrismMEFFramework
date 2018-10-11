using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Policy.Shell.Menu.ViewModel;

namespace QuotationEntry.Wizard.Views
{
    [Export(CONTRACT_TYPE)]
   // [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class QuotationDetailWizardView : UserControl
    {
        public const string CONTRACT_TYPE = "QuotationDetailWizardView";

        public QuotationDetailWizardView()
        {
            InitializeComponent();
        }

        [Import(AllowRecomposition = false)]
        public QuotationDetailWizardViewModel ViewModel
        {
            get { return DataContext as QuotationDetailWizardViewModel; }
            set { DataContext = value; }
        }
    }
}
