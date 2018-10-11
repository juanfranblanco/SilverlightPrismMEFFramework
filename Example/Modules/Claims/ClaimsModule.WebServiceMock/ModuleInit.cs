using System.ComponentModel.Composition;

using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.MefExtensions.Modularity;

namespace ClaimsModule.WebServiceMock
{
    [ModuleExport("ClaimsModule.WebServiceMock.ModuleInit", typeof(ClaimsModule.WebServiceMock.ModuleInit))]
    public class ModuleInit : IModule
    {
       

        [ImportingConstructor]
        public ModuleInit()
        {
            
        }

        #region IModule Members

        public void Initialize()
        {
            
        }

        #endregion
    }
}
