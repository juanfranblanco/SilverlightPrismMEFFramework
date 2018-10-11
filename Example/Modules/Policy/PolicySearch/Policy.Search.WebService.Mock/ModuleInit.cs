using System.ComponentModel.Composition;

using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.ServiceLocation;

namespace Policy.Search.WebService.Mock
{
    [ModuleExport("Policy.Search.WebService.Mock.ModuleInit", typeof(ModuleInit))]
    public class ModuleInit : IModule
    {
        public IServiceLocator ServiceLocator;

        [ImportingConstructor]
        public ModuleInit(IServiceLocator serviceLocator)
        {
            this.ServiceLocator = serviceLocator;
        }

        #region IModule Members

        public void Initialize()
        {
            
        }

        #endregion
    }
}
