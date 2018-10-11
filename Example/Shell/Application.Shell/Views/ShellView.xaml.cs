using System.ComponentModel.Composition;
using System.Windows.Controls;

using Application.Shell.ViewModels;

namespace Application.Shell.Views
{
    [Export]
    public partial class ShellView : UserControl
    {
        #region Constructors and Destructors

        public ShellView()
        {
            this.InitializeComponent();
        }

        #endregion

        #region Properties

        [Import(AllowRecomposition = false)]
        public ShellViewModel ViewModel
        {
            get
            {
                return this.DataContext as ShellViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }

        #endregion
    }
}