using System.ComponentModel.Composition;
using System.Windows.Controls;

using ClaimsModule.ViewModels;

namespace ClaimsModule.Views
{
    [Export(ViewContractNames.CLAIM_LIST_VIEW)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class ClaimListView : UserControl
    {
        #region Constructors and Destructors

        public ClaimListView()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Properties

        [Import(AllowRecomposition = false)]
        public ClaimsListViewModel ViewModel
        {
            get
            {
                return this.DataContext as ClaimsListViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }

        #endregion
    }
}