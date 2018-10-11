namespace Infrastructure.PrismMEFChildContainer.PrismMEFDependencies
{
    public class PrismMEFChildContainerDependenciesTypeNamesRepository : IPrismMEFChildContainerDependenciesTypeNamesRepository
    {
     
        public static string[] NonRequiredTypeNames = new[]
                {
                    //This creates a duplicate due to service locator usage
                    "Microsoft.Practices.Prism.Regions.IRegionNavigationJournalEntry",
                    "Microsoft.Practices.Prism.Events.IEventAggregator",
                    "Microsoft.Practices.Prism.Regions.Behaviors.AutoPopulateRegionBehavior",
                    "Microsoft.Practices.Prism.Regions.Behaviors.BindRegionContextToDependencyObjectBehavior",
                    "Microsoft.Practices.Prism.Regions.Behaviors.DelayedRegionCreationBehavior",
                    "Microsoft.Practices.Prism.Regions.Behaviors.RegionActiveAwareBehavior",
                    "Microsoft.Practices.Prism.Regions.Behaviors.RegionManagerRegistrationBehavior",
                    "Microsoft.Practices.Prism.Regions.Behaviors.RegionMemberLifetimeBehavior",
                    "Microsoft.Practices.Prism.Regions.Behaviors.SelectorItemsSourceSyncBehavior",
                    "Microsoft.Practices.Prism.Regions.Behaviors.SyncRegionContextWithHostBehavior",
                    "Microsoft.Practices.Prism.Regions.ContentControlRegionAdapter",
                    "Microsoft.Practices.Prism.Regions.ItemsControlRegionAdapter",
                    "Microsoft.Practices.Prism.Regions.RegionAdapterMappings",
                    "Microsoft.Practices.Prism.Regions.IRegionBehaviorFactory",
                    //Our custom region manager is created in the bootstrapper
                    "Microsoft.Practices.Prism.Regions.IRegionManager",
                    "Microsoft.Practices.Prism.Regions.IRegionViewRegistry",
                    "Microsoft.Practices.Prism.Regions.SelectorRegionAdapter",
                    "Microsoft.Practices.Prism.Regions.TabControlRegionAdapter"
                };

        //Here I am listing all the Prism elements that get registered
        public static string[] RequiredTypeNames = new[]
                {
                    "Microsoft.Practices.Prism.MefExtensions.Modularity.DownloadedPartCatalogCollection",
                    "Microsoft.Practices.Prism.Modularity.IModuleInitializer",
                    "Microsoft.Practices.Prism.Modularity.IModuleManager",
                    "Microsoft.Practices.Prism.Regions.IRegionNavigationContentLoader",
                    "Microsoft.Practices.Prism.Regions.IRegionNavigationJournal",
                    "Microsoft.Practices.Prism.Regions.IRegionNavigationService",
                    "Microsoft.Practices.Prism.MefExtensions.Modularity.MefXapModuleTypeLoader"
                };

        #region Public Methods

        public string[] GetNonRequiredForRegistrationTypeNames()
        {
            return NonRequiredTypeNames;
        }

        public string[] GetRequiredForRegistrationTypeNames()
        {
            return RequiredTypeNames;
        }

        #endregion
    }
}