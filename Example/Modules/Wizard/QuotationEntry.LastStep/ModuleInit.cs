using System.ComponentModel.Composition;

using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.MefExtensions.Modularity;

namespace QuotationEntry.LastStep
{
    [ModuleExport("QuotationEntry.LastStep.ModuleInit", typeof(QuotationEntry.LastStep.ModuleInit))]
    public class ModuleInit : IModule
    {
        [ImportingConstructor]
        public ModuleInit()
        {

        }


        public void Initialize()
        {

        }
    }
}
