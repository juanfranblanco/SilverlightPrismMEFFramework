using System.ComponentModel.Composition;
using System.Windows.Controls;

using Infrastructure.Window.Contracts.ViewModel;

using Model = Policy.Contracts.Models;

namespace Policy.Shell.Views
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class PolicyWindowMenuView : UserControl
    {
        public PolicyWindowMenuView()
        {
            InitializeComponent();
        }

        [Import]
        public IWindowMenuViewModel<Model.Policy> ViewModel
        {
            get { return DataContext as IWindowMenuViewModel<Model.Policy>; }
            set { DataContext = value; }
        }
    }
}