namespace Infrastructure.PrismMEFChildContainer.PrismMEFDependencies
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition.Hosting;
    using System.ComponentModel.Composition.Primitives;
    using System.Linq;

    using Microsoft.Practices.Prism.MefExtensions;

    public class PrismMEFChildContainerDependenciesCatalogBuilder : IPrismMEFChildContainerDependenciesCatalogBuilder
    {
        private readonly IPrismMEFChildContainerDependenciesTypeNamesRepository
            prismMEFChildContainerDependenciesTypeNamesRepository;

        public PrismMEFChildContainerDependenciesCatalogBuilder(IPrismMEFChildContainerDependenciesTypeNamesRepository prismMEFChildContainerDependenciesTypeNamesRepository)
        {
            this.prismMEFChildContainerDependenciesTypeNamesRepository =
                prismMEFChildContainerDependenciesTypeNamesRepository;
        }

        /// <summary>
        /// Due to the usage of the service locator in Prism we need to resolve some types (ie Mef loading and Navigation)
        /// </summary>
        public ComposablePartCatalog GetPrismRequiredPartCatalog()
        {
            var prismAggregateCatalog = new AggregateCatalog();
            prismAggregateCatalog =
                DefaultPrismServiceRegistrar.RegisterRequiredPrismServicesIfMissing(prismAggregateCatalog);

            var simpleCatalog =
                new SimpleCatalog(GetRequiredPrismPartsToRegister(
                    prismAggregateCatalog));


            return simpleCatalog;
        }

        public IEnumerable<ComposablePartDefinition> GetRequiredPrismPartsToRegister(AggregateCatalog newCatalog)
        {
            var partsToRegister = new List<ComposablePartDefinition>();
            AggregateCatalog catalogWithDefaults = newCatalog;

            foreach (ComposablePartDefinition part in catalogWithDefaults.Parts)
            {
                foreach (ExportDefinition export in part.ExportDefinitions)
                {
                    var contractName = export.ContractName;
                    if (IsRegistrationRequired(contractName))
                    {
                        if (!partsToRegister.Contains(part))
                        {
                            partsToRegister.Add(part);
                        }
                    }
                }
            }
            return partsToRegister;
        }

        public bool IsRegistrationRequired(string contractName)
        {
            return this.prismMEFChildContainerDependenciesTypeNamesRepository.GetRequiredForRegistrationTypeNames().Where(
                typeName => string.Compare(typeName, contractName, StringComparison.Ordinal) == 0).Count() > 0;
        }
    }
}