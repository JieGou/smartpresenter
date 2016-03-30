using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;
using SmartPresenter.UI.Common.Events;
using System;
using System.ComponentModel.Composition;
using System.Windows.Input;

namespace SmartPresenter.UI.Controls.ViewModel
{
    /// <summary>
    /// A viewmodel class for RibbonControl.
    /// </summary>
    [Export]
    public class RibbonControlViewModel : BindableBase
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="RibbonControlViewModel"/> class.
        /// </summary>
        public RibbonControlViewModel()
        {
            Initialize();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the command bindings.
        /// </summary>
        /// <value>
        /// The command bindings.
        /// </value>
        public CommandBindingCollection CommandBindings { get; set; }

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
        /// Gets the slide show tab view model.
        /// </summary>
        /// <value>
        /// The slide show tab view model.
        /// </value>
        public SlideShowTabViewModel SlideShowTabViewModel { get; private set; }

        /// <summary>
        /// Gets the home tab view model.
        /// </summary>
        /// <value>
        /// The home tab view model.
        /// </value>
        public HomeTabViewModel HomeTabViewModel { get; private set; }

        /// <summary>
        /// Gets the transitions tab view model.
        /// </summary>
        /// <value>
        /// The transitions tab view model.
        /// </value>
        public TransitionsTabViewModel TransitionsTabViewModel { get; private set; }

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
        /// Optionses the command_ executed.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void SettingsCommand_Executed(Object parameter)
        {
            EventAggregator.GetEvent<OpenSettingsDialogEvent>().Publish(null);
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
            HomeTabViewModel = new HomeTabViewModel();
            SlideShowTabViewModel = new SlideShowTabViewModel();
            TransitionsTabViewModel = new TransitionsTabViewModel();

            CreateCommands();

            this.CommandBindings = new CommandBindingCollection();
            this.CommandBindings.AddRange(HomeTabViewModel.CommandBindings);
        }

        private void CreateCommands()
        {
            SettingsCommand = new DelegateCommand<object>(SettingsCommand_Executed);
        }

        #endregion

        #region Public Methods

        #endregion

        #endregion
    }
}
