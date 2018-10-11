using System.Windows.Controls;
using System.ComponentModel.Composition;

using Application.Menu.ViewModels;

namespace Application.Menu.Views
{
    [Export]
    public partial class ApplicationMenuView : UserControl
    {
        public ApplicationMenuView()
        {
            InitializeComponent();
        }

        [Import(AllowRecomposition = false)]
        public ApplicationMenuViewModel ViewModel
        {
            get
            {
                return this.DataContext as ApplicationMenuViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }
    }
}
