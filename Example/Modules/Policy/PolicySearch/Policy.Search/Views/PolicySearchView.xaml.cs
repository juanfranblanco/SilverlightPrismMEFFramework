using System.ComponentModel.Composition;
using System.Windows.Controls;

using Policy.Search.ViewModels;
using Policy.Search.Contracts.Views;

namespace Policy.Search.Views
{

    [Export(ViewContractNames.POLICY_SEARCH_VIEW)]
    public partial class PolicySearchView : UserControl
    {
        #region Constructors and Destructors

        public PolicySearchView()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Properties

        [Import(AllowRecomposition = false)]
        public PolicySearchViewModel ViewModel
        {
            get
            {
                return this.DataContext as PolicySearchViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }

        #endregion
    }
}