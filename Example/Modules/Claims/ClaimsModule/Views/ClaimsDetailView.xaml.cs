using System.ComponentModel.Composition;
using System.Windows.Controls;

using ClaimsModule.ViewModels;

namespace ClaimsModule.Views
{
    [Export(ViewContractNames.CLAIM_DETAIL_VIEW)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class ClaimDetailView : UserControl
    {
        #region Constructors and Destructors

        public ClaimDetailView()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Properties

        [Import(AllowRecomposition = false)]
        public ClaimDetailViewModel ViewModel
        {
            get
            {
                return this.DataContext as ClaimDetailViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }

        #endregion
    }
}