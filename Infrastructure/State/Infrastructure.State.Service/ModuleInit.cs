using System.ComponentModel.Composition;

using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.ServiceLocation;

using UI.Infrastructure.State.Contracts.Services;

namespace Infrastructure.State.Service
{
    [ModuleExport("Infrastructure.State.Service.ModuleInit", typeof(ModuleInit))]
    public class ModuleInit : IModule
    {
        public IServiceLocator ServiceLocator { get; private set; }
        public IStateService StateService { get; private set; }

        [ImportingConstructor]
        public ModuleInit(IServiceLocator serviceLocator)
        {
            ServiceLocator = serviceLocator;
        }
        #region IModule Members

        public void Initialize()
        {
            StateService = ServiceLocator.GetInstance<IStateService>();
        }

        #endregion
    }
}
