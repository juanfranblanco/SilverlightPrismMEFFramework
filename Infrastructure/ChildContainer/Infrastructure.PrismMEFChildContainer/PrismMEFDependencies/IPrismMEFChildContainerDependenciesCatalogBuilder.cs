namespace Infrastructure.PrismMEFChildContainer.PrismMEFDependencies
{
    using System.ComponentModel.Composition.Primitives;

    public interface IPrismMEFChildContainerDependenciesCatalogBuilder
    {
        /// <summary>
        /// Due to the usage of the service locator in Prism we need to resolve some types (ie Mef loading and Navigation)
        /// </summary>
        ComposablePartCatalog GetPrismRequiredPartCatalog();
    }
}