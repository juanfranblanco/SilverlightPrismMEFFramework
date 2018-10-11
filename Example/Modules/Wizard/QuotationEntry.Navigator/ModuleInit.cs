using System.ComponentModel.Composition;

using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.MefExtensions.Modularity;

namespace QuotationEntry.Navigator
{
    [ModuleExport("QuotationEntry.Navigator.ModuleInit", typeof(QuotationEntry.Navigator.ModuleInit))]
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
