using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using Microsoft.Practices.Prism;
using QuotationEntry.Contracts.Steps;
using QuotationEntry.PolicyDetail.ViewModels;

namespace QuotationEntry.PolicyDetail.Views
{
    [Export(StepNames.POLICY_DETAIL_VIEW_TARGET_NAME)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class PolicyDetail : UserControl
    {
        public PolicyDetail()
        {
            InitializeComponent();
        }

        [Import(AllowRecomposition = false)]
        public ViewModel1 ViewModel
        {
            get { return this.DataContext as ViewModel1; }
            set { this.DataContext = value; }
        }

    }
}
