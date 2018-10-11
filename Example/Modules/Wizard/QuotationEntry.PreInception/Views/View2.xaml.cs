using System.ComponentModel.Composition;
using System.Windows.Controls;
using QuotationEntry.Contracts.Steps;
using QuotationEntry.PreInception.ViewModels;

namespace QuotationEntry.PreInception.Views
{
    [Export(StepNames.PRE_INCEPTION_VIEW_TARGET_NAME)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class PreInception : UserControl
    {
        public PreInception()
        {
            InitializeComponent();
        }

        [Import(AllowRecomposition = false)]
        public ViewModel3 ViewModel
        {
            get { return this.DataContext as ViewModel3; }
            set { this.DataContext = value; }
        }
    }
}
