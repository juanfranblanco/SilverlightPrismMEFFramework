using System.ComponentModel.Composition;

using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.MefExtensions.Modularity;


namespace QuotationEntry.PolicyDetail
{
    [ModuleExport("QuotationEntry.PolicyDetail.ModuleInit", typeof(QuotationEntry.PolicyDetail.ModuleInit))]
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
