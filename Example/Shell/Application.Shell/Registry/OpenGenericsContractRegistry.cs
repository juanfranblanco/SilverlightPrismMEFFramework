using System.ComponentModel.Composition;

using Infrastructure.UI.Window.Service.Services;
using Infrastructure.UI.Window.Service.ViewModels;
using Infrastructure.Window.Contracts.Services;
using Infrastructure.Window.Contracts.ViewModel;

using MefContrib.Hosting.Generics;

namespace Application.Shell.Registry
{
    /// <summary>
    /// Register all the classes used for open generics, for more information check MEFContrib and the following blog post http://pwlodek.blogspot.com/2010/12/introduction-to-interceptingcatalog.html
    /// </summary>
    [Export(typeof(IGenericContractRegistry))]
    public class OpenGenericsContractRegistry : GenericContractRegistryBase
    {
        #region Methods

        protected override void Initialize()
        {
            this.Register(typeof(IWindowService<>), typeof(WindowService<>));
            this.Register(typeof(IWindowMenuViewModel<>), typeof(WindowMenuViewModel<>));
        }

        #endregion
    }
}