namespace Infrastructure.PrismMEFChildContainer.PrismMEFDependencies
{
    public interface IPrismMEFChildContainerDependenciesTypeNamesRepository
    {
        string[] GetNonRequiredForRegistrationTypeNames();
        string[] GetRequiredForRegistrationTypeNames();
    }
}