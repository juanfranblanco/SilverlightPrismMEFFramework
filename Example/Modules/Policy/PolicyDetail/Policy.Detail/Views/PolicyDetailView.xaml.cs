using System.ComponentModel.Composition;
using System.Windows.Controls;

using Microsoft.Practices.Prism.Regions;

using Policy.Detail.ViewModels;
using Model = Policy.Contracts.Models;

namespace Policy.Detail.Views
{
    [Export(ViewContractNames.POLICY_DETAIL_VIEW)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class PolicyDetailView : UserControl
    {
        public PolicyDetailView()
        {
            InitializeComponent();
            // This view is displayed in a region with a region context.
            // The region context is defined as the currently selected policy
            // When the region context is changed, we need to propogate the
            // change to this view's view model.

            RegionContext.GetObservableContext(this).PropertyChanged += (s, e)
                                                                        =>
                                                                        ViewModel.Policy =
                                                                        RegionContext.GetObservableContext(this).Value
                                                                        as Model.Policy;
        }

        [Import(AllowRecomposition = false)]
        public PolicyDetailViewModel ViewModel
        {
            get { return DataContext as PolicyDetailViewModel; }
            set { DataContext = value; }
        }
    }
}