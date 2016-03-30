using SmartPresenter.BO.Common;
using System;
using System.Configuration;
using System.Threading;
using System.Windows;

namespace SmartPresenter.UI.Main
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            SetCurrentCulture();
            Initialize();

#if (DEBUG)
            RunInDebugMode();
#else
            RunInReleaseMode();
#endif
            this.ShutdownMode = ShutdownMode.OnMainWindowClose;
        }

        /// <summary>
        /// Runs the application in debug mode.
        /// </summary>
        private static void RunInDebugMode()
        {
            Bootstrapper bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }

        /// <summary>
        /// Runs the applicatio in release mode.
        /// </summary>
        private static void RunInReleaseMode()
        {
            AppDomain.CurrentDomain.UnhandledException += AppDomainUnhandledException;
            try
            {
                Bootstrapper bootstrapper = new Bootstrapper();
                bootstrapper.Run();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        /// <summary>
        /// Applications the domain unhandled exception.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="UnhandledExceptionEventArgs"/> instance containing the event data.</param>
        private static void AppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            HandleException(e.ExceptionObject as Exception);
        }

        /// <summary>
        /// Handles the exception.
        /// </summary>
        /// <param name="ex">The ex.</param>
        private static void HandleException(Exception ex)
        {
            if (ex == null)
                return;

            MessageBox.Show(ex.Message);
            Environment.Exit(1);
        }

        /// <summary>
        /// Set Current UI culture based on the settings for "Language" key in app.config.
        /// </summary>
        private static void SetCurrentCulture()
        {
            //set Thread Culture and UICulture                        
            if (ConfigurationManager.AppSettings["Language"] != null)
            {
                string culture = ConfigurationManager.AppSettings["Language"].ToString();
                if (!string.IsNullOrEmpty(culture))
                {
                    Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(culture);
                }
            }
        }

        /// <summary>
        /// Initializes the application.
        /// </summary>
        private void Initialize()
        {
            log4net.Config.XmlConfigurator.Configure();

            ApplicationSettings.Load();
        }
    }
}
