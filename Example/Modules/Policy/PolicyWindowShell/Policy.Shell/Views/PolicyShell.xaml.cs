using System.ComponentModel.Composition;

using Infrastructure.PrismMEFChildContainer.ViewInterfaces;

using Policy.Shell.ViewModels;

namespace Policy.Shell.Views
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class PolicyShell : IChildContainerShell
    {
        public PolicyShell()     
        {         
            InitializeComponent();
        }

        [Import(typeof(PolicyShellViewModel), AllowRecomposition = false)]
        public IChildContainerViewModel ViewModel
        {
            get { return DataContext as PolicyShellViewModel; }
            set { DataContext = value; }
        }

       
    }
}