namespace Infrastructure.PrismMEFChildContainer.ViewInterfaces
{
    using Microsoft.Practices.Prism.Regions;

    public interface IChildContainerViewModel
    {
        #region Properties

        IRegionManager RegionManager { get; set; }

        #endregion
    }
}