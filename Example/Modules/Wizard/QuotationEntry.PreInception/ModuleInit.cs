using System.ComponentModel.Composition;

using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.MefExtensions.Modularity;


namespace QuotationEntry.PreInception
{
    [ModuleExport("QuotationEntry.PreInception.ModuleInit", typeof(QuotationEntry.PreInception.ModuleInit))]
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
