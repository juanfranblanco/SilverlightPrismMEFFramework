using System;
using System.ComponentModel.Composition;

using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.ServiceLocation;


namespace QuotationEntry.RiskQuality
{
    [ModuleExport("QuotationEntry.RiskQuality.ModuleInit", typeof(QuotationEntry.RiskQuality.ModuleInit))]
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
