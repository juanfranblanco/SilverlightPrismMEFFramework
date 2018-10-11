namespace Infrastructure.PrismMEFChildContainer.ViewInterfaces
{
    public interface IChildContainerShell
    {
        #region Properties

        IChildContainerViewModel ViewModel { get; set; }

        #endregion
    }
}