using System.ComponentModel.Composition;
using System.Windows.Controls;

using Microsoft.Practices.Prism.Regions;

using Policy.Contracts.Models;
using Policy.Search.ViewModels;

namespace Policy.Search.Views
{
    [Export]
    public partial class PolicySearchResultsView : UserControl
    {
        #region Constructors and Destructors

        public PolicySearchResultsView()
        {
            this.InitializeComponent();

            RegionContext.GetObservableContext(this).PropertyChanged +=
                (s, e) =>
                this.ViewModel.PolicySearchResult = RegionContext.GetObservableContext(this).Value as PolicySearchResult;
        }

        #endregion

        #region Properties

        [Import(AllowRecomposition = false)]
        public PolicySearchResultsViewModel ViewModel
        {
            get
            {
                return this.DataContext as PolicySearchResultsViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }

        #endregion
    }
}