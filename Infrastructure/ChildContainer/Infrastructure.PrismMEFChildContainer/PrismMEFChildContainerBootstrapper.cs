namespace Infrastructure.PrismMEFChildContainer
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.Linq;
    using System.Windows;

    using Infrastructure.PrismMEFChildContainer.PrismMEFDependencies;
    using Infrastructure.PrismMEFChildContainer.ViewInterfaces;

    using Microsoft.Practices.Prism.Events;
    using Microsoft.Practices.Prism.MefExtensions;
    using Microsoft.Practices.Prism.Modularity;
    using Microsoft.Practices.Prism.Regions;
    using Microsoft.Practices.ServiceLocation;

    public abstract class PrismMEFChildContainerBootstrapper
    {
        protected IRegionManager ChildContainerRegionManager { get; set; }

        protected CompositionContainer ParentContainer { get; set; }
        
        /// <summary>
        /// Gets the default <see cref="IModuleCatalog"/> for the application.
        /// </summary>
        /// <value>The default <see cref="IModuleCatalog"/> instance.</value>
        protected IModuleCatalog ModuleCatalog { get; set; }

        /// <summary>
        /// Gets or sets the default <see cref="AggregateCatalog"/> for the application.
        /// </summary>
        /// <value>The default <see cref="AggregateCatalog"/> instance.</value>
        protected AggregateCatalog AggregateCatalog { get; set; }

        /// <summary>
        /// Gets or sets the default <see cref="CompositionContainer"/> for the application.
        /// </summary>
        /// <value>The default <see cref="CompositionContainer"/> instance.</value>
        protected CompositionContainer Container { get; set; }

        protected virtual AggregateCatalog CreateAggregateCatalog()
        {
            return new AggregateCatalog();
        }

        protected CompositionContainer CreateContainer()
        {
            //Set the parent container here as it will be use as a provider to find registered types which are not defined in the "child" catalog
            return new CompositionContainer(this.AggregateCatalog, ParentContainer);
        }

        protected virtual IModuleCatalog CreateModuleCatalog()
        {
            // Create the module catalogue from a XAML file, these are the isolated child container items
            return null;
        }

        /// <summary>
        /// Run the bootstrapper process.
        /// </summary>
        public void Run()
        {
            this.ModuleCatalog = CreateModuleCatalog();

            this.AggregateCatalog = CreateAggregateCatalog();

            RegisterRequiredPrismTypes();

            Container = CreateContainer();

            ConfigureContainer();

            ConfigureParentContainerExports();

            CreateShell();

            //Create the child container region manager
            CreateChildContainerRegionManager();

            RegisterTypesForTheWindowRegionManager();

            //Initialise all modules
            IEnumerable<Lazy<object, object>> exports = Container.GetExports(typeof (IModuleManager), null, null);
            if ((exports != null) && (exports.Count() > 0))
            {
                InitializeModules();
            }
        }
        
        protected virtual void RegisterTypesForTheWindowRegionManager()
        {
            
        }

        protected virtual void ConfigureParentContainerExports()
        {
            //Here we can configure the ParentRegionManager, ParentEventAgregator, etc
        }

        public virtual IChildContainerShell Shell { get; set; }

        protected DependencyObject CreateShell()
        {
            //Create the shell instance
            this.CreateShellInstance();

            return (DependencyObject)Shell;
        }

        private void CreateChildContainerRegionManager()
        {
            var regionManager = this.ParentContainer.GetExportedValue<IRegionManager>();
            //Create a new scoped region manager so we can work within the scope (and don't have classing names)
             ChildContainerRegionManager = regionManager.CreateRegionManager();
            //Set the container export value to the new region manager so it can be resolved, I am giving at name to ensure we don't use the default region manager
            //this should not be an issue
             this.Container.ComposeExportedValue(ChildContainerRegionManager);
             ChildContainerRegionManager.Regions.CollectionChanged += this.Regions_CollectionChanged;
            //Set the new region manager in the viewmodel
             this.Shell.ViewModel.RegionManager = ChildContainerRegionManager;
        }

        protected abstract void CreateShellInstance();

        private void Regions_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var navigationService = Container.GetExportedValue<IRegionNavigationService>();
            foreach (IRegion region in e.NewItems)
            {
                region.NavigationService = navigationService;
                navigationService.Region = region;
            }
        }
        /// <summary>
        /// Helper method for configuring the <see cref="CompositionContainer"/>. 
        /// Registers all the types direct instantiated by the bootstrapper with the container.
        /// </summary>
        protected virtual void RegisterBootstrapperProvidedTypes()
        {
            //Adding the module catalog so it can be resolved for initialisation
            Container.ComposeExportedValue(this.ModuleCatalog);
            //Adding the catalog
            Container.ComposeExportedValue(this.AggregateCatalog);
            //Adding a new service locator as part of this catalog so it contains all the new registered modules
            Container.ComposeExportedValue<IServiceLocator>((new MefServiceLocatorAdapter(Container)));
            //Create a new event aggregator so our different child containers don't get the same event
            var neweventAggregator = new EventAggregator();
            Container.ComposeExportedValue<IEventAggregator>(neweventAggregator);
        }

        /// <summary>
        /// Initializes the modules. May be overwritten in a derived class to use a custom Modules Catalog
        /// </summary>
        protected virtual void InitializeModules()
        {
            var manager = Container.GetExportedValue<IModuleManager>();
            manager.LoadModuleCompleted += LoadModuleCompleted;
            manager.Run();
        }

        /// <summary>
        /// Allows to handle if overriden the Load Module Completed event of the ModuleManager
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">The Load Completed event argument</param>
        protected virtual void LoadModuleCompleted(object sender, LoadModuleCompletedEventArgs e)
        {
            
        }


        protected virtual void ConfigureContainer()
        {
            RegisterBootstrapperProvidedTypes();
            //Configure the container to be accessible if required (we should use the service locator where possible)
            Container.ComposeExportedValue(Container);
        }

        /// <summary>
        /// Due to the usage of the service locator in Prism we need to resolve some types (ie Mef loading and Navigation)
        /// </summary>
        public virtual void RegisterRequiredPrismTypes()
        {
            var catalogBuilder =
                new PrismMEFChildContainerDependenciesCatalogBuilder(
                    new PrismMEFChildContainerDependenciesTypeNamesRepository());

            this.AggregateCatalog.Catalogs.Add(catalogBuilder.GetPrismRequiredPartCatalog());
        }
        
    }
}