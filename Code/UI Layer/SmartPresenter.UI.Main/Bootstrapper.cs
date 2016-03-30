using Microsoft.Practices.Prism.MefExtensions;
using Microsoft.Practices.Prism.Regions;
using SmartPresenter.UI.Common;
using SmartPresenter.UI.Common.Interfaces;
using SmartPresenter.UI.Controls;
using SmartPresenter.UI.Controls.Media;
using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Windows;

namespace SmartPresenter.UI.Main
{
    /// <summary>
    /// An MEF Bootstrapper for Prism.
    /// </summary>
    [CLSCompliant(false)]
    public class Bootstrapper : MefBootstrapper
    {
        /// <summary>
        /// Creates the shell.
        /// </summary>
        /// <returns></returns>
        protected override DependencyObject CreateShell()
        {
            return this.Container.GetExportedValue<Shell>();
        }

        /// <summary>
        /// Initializes the shell.
        /// </summary>
        protected override void InitializeShell()
        {
            base.InitializeShell();
            Application.Current.MainWindow = (Window)this.Shell;
            Application.Current.MainWindow.Show();
        }

        /// <summary>
        /// Configures the aggregate catalog.
        /// </summary>
        protected override void ConfigureAggregateCatalog()
        {
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(Bootstrapper).Assembly));
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(AutoPopulateExportedViewsBehavior).Assembly));
            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(RibbonControl).Assembly));
        }

        /// <summary>
        /// Configures the container.
        /// </summary>
        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            this.Container.ComposeExportedValue<CompositionContainer>(this.Container);
            this.Container.ComposeExportedValue<IInteractionService>(new UIInteractionService());
            this.Container.ComposeExportedValue<VideoEditorView>(new VideoEditorView());
            this.Container.ComposeExportedValue<ImageEditorView>(new ImageEditorView());
        }

        /// <summary>
        /// Configures the default region behaviors.
        /// </summary>
        /// <returns></returns>
        protected override IRegionBehaviorFactory ConfigureDefaultRegionBehaviors()
        {
            var factory = base.ConfigureDefaultRegionBehaviors();

            factory.AddIfMissing("AutoPopulateExportedViewsBehavior", typeof(AutoPopulateExportedViewsBehavior));

            return factory;
        }
    }
}
