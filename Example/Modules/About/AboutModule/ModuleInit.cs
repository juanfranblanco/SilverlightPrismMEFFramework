using System.ComponentModel.Composition;

using AboutModule.Controller;
using AboutModule.Events;

using Application.Shell.Contracts.Services;

using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.ServiceLocation;

namespace AboutModule
{
    [ModuleExport("AboutModule.ModuleInit", typeof(ModuleInit))]
    public class ModuleInit : IModule
    {
        #region Constants and Fields

        private readonly IApplicationMenuRegistry applicationMenuRegistry;

        private readonly IEventAggregator eventAggregator;

        private readonly IServiceLocator serviceLocator;

        private AboutContentController aboutContentController;

        private bool loaded;

        #endregion

        #region Constructors and Destructors

        [ImportingConstructor]
        public ModuleInit(
            IServiceLocator serviceLocator,
            IApplicationMenuRegistry applicationMenuRegistry,
            IEventAggregator eventAggregator)
        {
            this.serviceLocator = serviceLocator;
            this.applicationMenuRegistry = applicationMenuRegistry;
            this.eventAggregator = eventAggregator;
        }

        #endregion

        #region Implemented Interfaces

        #region IModule

        public void Initialize()
        {
            if (!this.loaded)
            {
                this.aboutContentController = this.serviceLocator.GetInstance<AboutContentController>();
                this.applicationMenuRegistry.RegisterMenuItemAction(
                    "About", "About", 2, (x) => this.eventAggregator.GetEvent<ShowAboutEvent>().Publish(x));
                this.loaded = true;
            }
        }

        #endregion

        #endregion
    }
}