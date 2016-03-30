using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;
using SmartPresenter.UI.Common;
using SmartPresenter.UI.Common.Events;
using SmartPresenter.UI.Controls.SlideShow;
using SmartPresenter.UI.RenderEngine.WPF;
using System;
using System.ComponentModel.Composition;
using System.Windows.Input;

namespace SmartPresenter.UI.Main
{
    /// <summary>
    /// ViewModel class for Shell.
    /// </summary>
    [CLSCompliant(false)]
    [Export]
    public class ShellViewModel : BindableBase
    {
        #region Private Data Members

        //private SmartPresenter.UI.RenderEngine.DirectX.OutputWindow _outputWindow = new RenderEngine.DirectX.OutputWindow();
        private SmartPresenter.UI.RenderEngine.WPF.OutputWindow _outputWindow = new RenderEngine.WPF.OutputWindow();

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ShellViewModel"/> class.
        /// </summary>
        public ShellViewModel()
        {            
            Initialize();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the event aggregator.
        /// </summary>
        /// <value>
        /// The event aggregator.
        /// </value>
        private IEventAggregator EventAggregator
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IEventAggregator>();
            }
        }

        /// <summary>
        /// Gets the options notification request.
        /// </summary>
        /// <value>
        /// The options notification request.
        /// </value>
        public InteractionRequest<INotification> SettingsNotificationRequest { get; private set; }

        public CommandBindingCollection CommandBindings { get; set; }

        #endregion

        #region Commands

        /// <summary>
        /// Gets the options command.
        /// </summary>
        /// <value>
        /// The options command.
        /// </value>
        public DelegateCommand<Object> SettingsCommand { get; private set; }

        #region Command Handlers

        /// <summary>
        /// Shows the output command_ executed.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void StartSlideShowCommand_Executed(Object sender, ExecutedRoutedEventArgs args)
        {
            SlideShowManager.Instance.StartSlideShow();
            _outputWindow.ShowDialog();                    
        }

        /// <summary>
        /// Optionses the command_ executed.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void SettingsCommand_Executed(Object parameter)
        {
            RaiseSettingsNotificationRequest();
        }

        #endregion

        #endregion

        #region Methods

        #region Private Methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            CreateCommands();
            InitializeEvents();

            SettingsNotificationRequest = new InteractionRequest<INotification>();            
        }

        /// <summary>
        /// Creates the commands.
        /// </summary>
        private void CreateCommands()
        {
            this.CommandBindings = new CommandBindingCollection();
            this.CommandBindings.Add(new CommandBinding(SlideShowCommands.StartSlideShowCommand, StartSlideShowCommand_Executed, Commands_CanExecuted));

            SettingsCommand = new DelegateCommand<object>(SettingsCommand_Executed);
        }

        private void InitializeEvents()
        {
            EventAggregator.GetEvent<OpenSettingsDialogEvent>().Subscribe(OpenSettingsDialog);
        }

        /// <summary>
        /// Raises the options notification request.
        /// </summary>
        private void RaiseSettingsNotificationRequest()
        {
            this.SettingsNotificationRequest.Raise(
                           new Notification { Content = "Notification Message", Title = "Settings" });
        }

        /// <summary>
        /// Opens the options dialog.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void OpenSettingsDialog(Object parameter)
        {
            RaiseSettingsNotificationRequest();
        }

        private void Commands_CanExecuted(object sender, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = true;
        }

        #endregion

        #endregion

    }
}
