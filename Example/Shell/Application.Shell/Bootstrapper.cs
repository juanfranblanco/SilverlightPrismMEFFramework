using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Windows;

using Application.Shell.Registry;
using Application.Shell.Views;

using MefContrib.Hosting.Generics;

using Microsoft.Practices.Prism.MefExtensions;
using Microsoft.Practices.Prism.Modularity;

namespace Application.Shell
{
    public class Bootstrapper : MefBootstrapper
    {
        #region Methods

        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();
            // Add this assembly to the catalog.
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(Bootstrapper).Assembly));
            //Add the open generic catalog.
            //Open generics are not supported in MEF so we use the contrib framework
            //http://pwlodek.blogspot.com/2010/12/introduction-to-interceptingcatalog.html
            this.AggregateCatalog.Catalogs.Add(new GenericCatalog(new OpenGenericsContractRegistry()));
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            this.Container.ComposeExportedValue(this.Container);
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            // Create the module catalog from a XAML file.
            return
                Microsoft.Practices.Prism.Modularity.ModuleCatalog.CreateFromXaml(
                    new Uri("/Application.Shell;component/ModuleCatalog.xaml", UriKind.Relative));
        }

        protected override DependencyObject CreateShell()
        {
            var shell = this.Container.GetExportedValue<ShellView>();
            return shell;
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();
            System.Windows.Application.Current.RootVisual = (UIElement)this.Shell;
        }

        #endregion
    }
}