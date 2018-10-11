using System.ComponentModel.Composition;

using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.ServiceLocation;

namespace Infrastructure.MessageBox.Service
{
    [ModuleExport("Infrastructure.MessageBox.Service.ModuleInit", typeof(MessageBox.Service.ModuleInit))]
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
