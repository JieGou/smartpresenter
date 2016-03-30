using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;
using SmartPresenter.UI.Controls.Events;
using System;

namespace SmartPresenter.UI.Controls.ViewModel
{
    /// <summary>
    /// Class for ViewModel of Next Slide Timer Popup.
    /// </summary>
    public class NextSlideTimerPopupViewModel : BindableBase
    {

        #region Private Data Members

        private int _nextSlideDelay;
        private bool _loopToFirst;

        #endregion

        #region Contsructor

        /// <summary>
        /// Initializes a new instance of the <see cref="NextSlideTimerPopupViewModel"/> class.
        /// </summary>
        public NextSlideTimerPopupViewModel()
        {
            Initialize();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the next slide delay.
        /// </summary>
        /// <value>
        /// The next slide delay.
        /// </value>
        public int NextSlideDelay
        {
            get
            {
                return _nextSlideDelay;
            }
            set
            {
                _nextSlideDelay = value;
                OnPropertyChanged("NextSlideDelay");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [loop to first].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [loop to first]; otherwise, <c>false</c>.
        /// </value>
        public bool LoopToFirst
        {
            get
            {
                return _loopToFirst;
            }
            set
            {
                _loopToFirst = value;
                OnPropertyChanged("LoopToFirst");
            }
        }

        /// <summary>
        /// Gets or sets the finish interaction.
        /// </summary>
        /// <value>
        /// The finish interaction.
        /// </value>
        public Action FinishInteraction { get; set; }

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

        #endregion

        #region Commands

        /// <summary>
        /// Gets the ok command.
        /// </summary>
        /// <value>
        /// The ok command.
        /// </value>
        public DelegateCommand OkCommand { get; private set; }

        /// <summary>
        /// Gets the clear command.
        /// </summary>
        /// <value>
        /// The clear command.
        /// </value>
        public DelegateCommand ClearCommand { get; private set; }

        #region Command Handlers

        /// <summary>
        /// Oks the command_ executed.
        /// </summary>
        private void OkCommand_Executed()
        {
            EventAggregator.GetEvent<NextSlideTimerUpdatedEvent>().Publish(new SlideTimer() { NextSlideDelay = NextSlideDelay, LoopToFirst = LoopToFirst });
            this.FinishInteraction();
        }

        /// <summary>
        /// Clears the command_ executed.
        /// </summary>
        private void ClearCommand_Executed()
        {
            NextSlideDelay = 0;
            LoopToFirst = false;
            EventAggregator.GetEvent<NextSlideTimerUpdatedEvent>().Publish(new SlideTimer() { NextSlideDelay = NextSlideDelay, LoopToFirst = LoopToFirst });
            this.FinishInteraction();
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
        }

        /// <summary>
        /// Creates the commands.
        /// </summary>
        private void CreateCommands()
        {
            OkCommand = new DelegateCommand(OkCommand_Executed);
            ClearCommand = new DelegateCommand(ClearCommand_Executed);
        }

        #endregion

        #region Public Methods



        #endregion

        #endregion

        #region Events



        #endregion

    }
}
