using System.ComponentModel.Composition;
using System.Windows.Controls;

using Microsoft.Practices.Prism.Regions;

using Policy.Shell.Menu.ViewModel;

namespace Policy.Shell.Menu.Views
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class PolicyDetailMenuView : UserControl
    {
        public PolicyDetailMenuView()     
        {         
           
            InitializeComponent();

            RegionContext.GetObservableContext(this).PropertyChanged += (s, e)
                                                                        =>
                                                                        ViewModel.MenuContext =
                                                                       (int)RegionContext.GetObservableContext(this).Value;
        }

       [Import(AllowRecomposition = false)]
        public PolicyDetailMenuViewModel ViewModel
        {
            get { return DataContext as PolicyDetailMenuViewModel; }
            set { DataContext = value; }
        }

       
    }
}