using System.ComponentModel.Composition;
using System.Windows.Controls;
using QuotationEntry.Contracts.Steps;
using QuotationEntry.RiskQuality.ViewModels;

namespace QuotationEntry.RiskQuality.Views
{
     [Export(StepNames.RISK_QUALITY_QUESTIONS_VIEW_TARGET_NAME)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class RiskQualityQuestions : UserControl
    {
         public RiskQualityQuestions()
        {
            InitializeComponent();
        }

        [Import(AllowRecomposition = false)]
        public ViewModel2 ViewModel
        {
            get { return this.DataContext as ViewModel2; }
            set { this.DataContext = value; }
        }
    }
}
