using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;
using SmartPresenter.UI.Controls.Events;
using System;
using System.ComponentModel.Composition;
using System.Windows;

namespace SmartPresenter.UI.Controls.ViewModel
{
    /// <summary>
    /// ViewModel for Librart Tab.
    /// </summary>
    [Export]
    public class SlidesTabViewModel : BindableBase
    {
        #region Constructor

        /// <summary>
        /// Creates a new LibraryTabViewModel.
        /// </summary>
        public SlidesTabViewModel()
        {
            CreateCommands();
            Initialize();
            LoadData();
        }

        #endregion

        #region Private Member Variables

        /// <summary>
        /// List of libraries.
        /// </summary>
        private PresentationView _selectedPresentation;

        /// <summary>
        /// The Selected library on UI.
        /// </summary>
        private SlideView _selectedSlide;

        #endregion

        #region Public Properties

        /// <summary>
        /// List of user presentation libraries
        /// </summary>
        public PresentationView SelectedPresentation
        {
            get
            {
                return _selectedPresentation;
            }
            private set
            {
                _selectedPresentation = value;
                OnPropertyChanged("SelectedPresentation");
                OnPropertyChanged("SelectedPresentation.Slides");
            }
        }

        /// <summary>
        /// Currently selected slide on slides view.
        /// </summary>
        public SlideView SelectedSlide
        {
            get
            {
                return _selectedSlide;
            }
            set
            {
                _selectedSlide = value;
                OnPropertyChanged("SelectedSlide");
                EventAggregator.GetEvent<SelectedSlideChangedEvent>().Publish(value);
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

        #endregion

        #region Commands



        #region Command Handlers



        #endregion

        #endregion

        #region Private Methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            // In case of Unit Testing it will be null.
            if (Application.Current != null)
            {
                Application.Current.Dispatcher.ShutdownStarted += Dispatcher_ShutdownStarted;
            }
            InitializeEvents();

        }

        /// <summary>
        /// Handles the ShutdownStarted event of the Dispatcher control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Dispatcher_ShutdownStarted(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Creates the commands.
        /// </summary>
        private void CreateCommands()
        {
        }

        /// <summary>
        /// Loads the data.
        /// </summary>
        private void LoadData()
        {
        }

        /// <summary>
        /// Initializes the events.
        /// </summary>
        private void InitializeEvents()
        {
            EventAggregator.GetEvent<SelectedPresentationChangedEvent>().Subscribe(UpdateSelectedPresentation);
            EventAggregator.GetEvent<SelectedPresentationAskedEvent>().Subscribe(NotifySelectedPresentationChanged);
            EventAggregator.GetEvent<SelectedSlideChangedEvent>().Subscribe(UpdateSelectedSlide);
            EventAggregator.GetEvent<SelectedSlideAskedEvent>().Subscribe(NotifySelectedSlideChanged);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Updates the selected presentation.
        /// </summary>
        /// <param name="newSelectedPresentation">The new selected presentation.</param>
        public void UpdateSelectedPresentation(PresentationView newSelectedPresentation)
        {
            SelectedPresentation = newSelectedPresentation ?? SelectedPresentation;
        }

        /// <summary>
        /// Notifies the selected presentation changed.
        /// </summary>
        /// <param name="dummyParameter">The dummy parameter.</param>
        public void NotifySelectedPresentationChanged(PresentationView dummyParameter)
        {
            EventAggregator.GetEvent<SelectedPresentationChangedEvent>().Publish(SelectedPresentation);
        }

        /// <summary>
        /// Updates the selected slide.
        /// </summary>
        /// <param name="newSelectedSlide">The new selected slide.</param>
        public void UpdateSelectedSlide(SlideView newSelectedSlide)
        {
            EventAggregator.GetEvent<SelectedSlideChangedEvent>().Unsubscribe(UpdateSelectedSlide);
            SelectedSlide = newSelectedSlide ?? SelectedSlide;
            EventAggregator.GetEvent<SelectedSlideChangedEvent>().Subscribe(UpdateSelectedSlide);
        }

        /// <summary>
        /// Notifies the selected slide changed.
        /// </summary>
        /// <param name="dummyParameter">The dummy parameter.</param>
        public void NotifySelectedSlideChanged(SlideView dummyParameter)
        {
            EventAggregator.GetEvent<SelectedSlideChangedEvent>().Publish(SelectedSlide);
        }

        #endregion
    }
}
