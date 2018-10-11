using System.ComponentModel.Composition;
using System.Windows.Controls;
using QuotationEntry.Contracts.Steps;
using QuotationEntry.LastStep.ViewModels;

namespace QuotationEntry.LastStep.Views
{
    [Export(StepNames.LAST_ONE_VIEW_TARGET_NAME)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class LastStep : UserControl
    {
        public LastStep()
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
