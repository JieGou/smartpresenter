using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;
using SmartPresenter.BO.Common.Enums;
using SmartPresenter.UI.Controls.Events;
using SmartPresenter.UI.Controls.ViewModel;
using System;
using System.Linq;
using System.Timers;

namespace SmartPresenter.UI.Controls.SlideShow
{
    /// <summary>
    /// Class to manage Slide Show.
    /// </summary>
    public class SlideShowManager
    {

        #region Private Data Members

        private System.Timers.Timer _slideShowTimer;
        private SlideView _currentSlide;
        private bool _isCurrentSlideAsked;
        private bool _hasSlideShowStarted;

        #endregion

        #region Contsructor

        #endregion

        #region Properties

        #region Singleton Implementation

        // Private variable for single instance of this class.
        private static volatile SlideShowManager _instance;
        // Lock object to synchronize.
        private static Object _lock = new Object();

        // Private constructor to avoid creation of more than one objects.
        private SlideShowManager()
        {
            _slideShowTimer = new System.Timers.Timer();
            _slideShowTimer.Elapsed += SlideShowTimer_Elapsed;
            InitializeEvents();
            Initialize();
        }

        /// <summary>
        /// Gets the single instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static SlideShowManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new SlideShowManager();
                        }
                    }
                }
                return _instance;
            }
        }

        #endregion

        /// <summary>
        /// Gets or sets the start index.
        /// </summary>
        /// <value>
        /// The start index.
        /// </value>
        public int StartIndex { get; set; }

        /// <summary>
        /// Gets or sets the end index.
        /// </summary>
        /// <value>
        /// The end index.
        /// </value>
        public int EndIndex { get; set; }

        /// <summary>
        /// Gets or sets the slide show start mode.
        /// </summary>
        /// <value>
        /// The slide show start mode.
        /// </value>
        public SlideShowStartMode SlideShowStartMode { get; set; }

        /// <summary>
        /// Gets or sets the current slide.
        /// </summary>
        /// <value>
        /// The current slide.
        /// </value>
        public SlideView CurrentSlide
        {
            get
            {
                return _currentSlide;
            }
            set
            {
                _currentSlide = value;
                if (value != null)
                {
                    if (value.DelayBeforeNextSlide.TotalMilliseconds > 0)
                    {
                        _slideShowTimer.Interval = value.DelayBeforeNextSlide.TotalMilliseconds;
                        if (_hasSlideShowStarted == true)
                        {
                            _slideShowTimer.Enabled = true;
                        }
                    }
                }
                OnCurrentSlideUpdated();
            }
        }

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
        /// Gets or sets the current presentation.
        /// </summary>
        /// <value>
        /// The current presentation.
        /// </value>
        public PresentationView CurrentPresentation { get; set; }

        #endregion

        #region Commands

        #region Command Handlers



        #endregion

        #endregion

        #region Methods

        #region Private Methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize()
        {
            EventAggregator.GetEvent<SelectedPresentationAskedEvent>().Publish(null);
        }

        /// <summary>
        /// Initializes the events.
        /// </summary>
        private void InitializeEvents()
        {
            EventAggregator.GetEvent<SelectedPresentationChangedEvent>().Unsubscribe(UpdateSelectedPresentation);
            EventAggregator.GetEvent<SelectedSlideChangedEvent>().Unsubscribe(UpdateSelectedSlide);

            EventAggregator.GetEvent<SelectedPresentationChangedEvent>().Subscribe(UpdateSelectedPresentation);
            EventAggregator.GetEvent<SelectedSlideChangedEvent>().Subscribe(UpdateSelectedSlide);
        }

        /// <summary>
        /// Updates the selected slide.
        /// </summary>
        /// <param name="newSlideView">The new slide view.</param>
        private void UpdateSelectedSlide(SlideView newSlideView)
        {
            if (_isCurrentSlideAsked == true)
            {
                _isCurrentSlideAsked = false;
                EventAggregator.GetEvent<SelectedSlideChangedEvent>().Unsubscribe(UpdateSelectedSlide);
                CurrentSlide = newSlideView as SlideView;
                EventAggregator.GetEvent<SelectedSlideChangedEvent>().Subscribe(UpdateSelectedSlide);
            }
        }

        /// <summary>
        /// Updates the selected presentation.
        /// </summary>
        /// <param name="newPresentationView">The new presentation view.</param>
        private void UpdateSelectedPresentation(PresentationView newPresentationView)
        {
            EventAggregator.GetEvent<SelectedPresentationChangedEvent>().Unsubscribe(UpdateSelectedPresentation);
            CurrentPresentation = newPresentationView as PresentationView;
            EventAggregator.GetEvent<SelectedPresentationChangedEvent>().Subscribe(UpdateSelectedPresentation);

            SetupFirstSlide();
        }

        /// <summary>
        /// Setups the first slide.
        /// </summary>
        private void SetupFirstSlide()
        {
            if (SlideShowManager.Instance.SlideShowStartMode == SlideShowStartMode.FromFirst)
            {
                CurrentSlide = CurrentPresentation.Slides.FirstOrDefault();
            }
            else if (SlideShowManager.Instance.SlideShowStartMode == SlideShowStartMode.FromCurrent)
            {
                _isCurrentSlideAsked = true;
                EventAggregator.GetEvent<SelectedSlideAskedEvent>().Publish(null);
            }
        }

        /// <summary>
        /// Handles the Elapsed event of the SlideShowTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ElapsedEventArgs"/> instance containing the event data.</param>
        private void SlideShowTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _slideShowTimer.Enabled = false;
            PlayNextSlide();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Nexts the slide.
        /// </summary>
        public void PlayNextSlide()
        {
            if (CurrentSlide.LoopToFirst == false)
            {
                int index = CurrentPresentation.Slides.IndexOf(CurrentSlide);
                if ((index + 1) < CurrentPresentation.Slides.Count)
                {
                    while (CurrentPresentation.Slides[index + 1].IsEnabled == false && CurrentPresentation.Slides.Count > (index + 1))
                    {
                        index++;
                    }
                    if ((index + 1) < CurrentPresentation.Slides.Count)
                    {
                        CurrentSlide = CurrentPresentation.Slides[index + 1];
                    }
                }
            }
            else
            {
                CurrentSlide = CurrentPresentation.Slides.FirstOrDefault();
            }
        }

        /// <summary>
        /// Previouses the slide.
        /// </summary>
        public void PlayPreviousSlide()
        {
            int index = CurrentPresentation.Slides.IndexOf(CurrentSlide);
            if ((index - 1) >= 0)
            {
                while (CurrentPresentation.Slides[index - 1].IsEnabled == false && (index - 1) > 0)
                {
                    index--;
                }
                if ((index - 1) >= 0)
                {
                    CurrentSlide = CurrentPresentation.Slides[index - 1];
                }
            }
        }

        /// <summary>
        /// Starts the slide show.
        /// </summary>
        public void StartSlideShow()
        {
            _hasSlideShowStarted = true;
            SetupFirstSlide();
        }

        public void StopSlideShow()
        {
            _hasSlideShowStarted = false;
            _slideShowTimer.Stop();
            CurrentSlide = null;
        }

        #endregion

        #endregion

        #region Events

        /// <summary>
        /// Occurs when [current slide updated].
        /// </summary>
        public event EventHandler CurrentSlideUpdated;

        /// <summary>
        /// Called when [current slide updated].
        /// </summary>
        private void OnCurrentSlideUpdated()
        {
            if (CurrentSlideUpdated != null)
            {
                CurrentSlideUpdated(this, new EventArgs());
            }
        }

        #endregion

    }
}
