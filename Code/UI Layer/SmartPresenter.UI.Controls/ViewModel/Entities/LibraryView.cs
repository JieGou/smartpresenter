using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;
using SmartPresenter.BO.Common;
using SmartPresenter.BO.Common.Entities;
using SmartPresenter.BO.Common.Interfaces;
using SmartPresenter.UI.Controls.Events;
using System.Collections.ObjectModel;

namespace SmartPresenter.UI.Controls.ViewModel
{
    /// <summary>
    /// A class for Library Object UI.
    /// </summary>
    public class LibraryView : BindableBase
    {
        #region Private Data Members

        /// <summary>
        /// The presentation library
        /// </summary>
        private PresentationLibrary _library;
        /// <summary>
        /// The collection of presentations
        /// </summary>
        private ObservableCollection<PresentationView> _presentations;
        /// <summary>
        /// The selected presentation
        /// </summary>
        private PresentationView _selectedPresentation;

        /// <summary>
        /// List of Playlist.
        /// </summary>
        private ObservableCollection<PlaylistView> _playlists;

        private bool _isCategoryColumnVisible = true;
        private bool _isSizeColumnVisible = true;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new Library.
        /// </summary>
        public LibraryView(PresentationLibrary library)
        {
            _library = library;

            _presentations = new ObservableCollection<PresentationView>();
            _playlists = new ObservableCollection<PlaylistView>();

            CreateView();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the selected library.
        /// </summary>
        /// <value>
        /// The selected library.
        /// </value>
        public PresentationLibrary Library
        {
            get
            {
                return _library;
            }
        }

        /// <summary>
        /// Name of the Library.
        /// </summary>
        public string Name
        {
            get
            {
                return _library.Name;
            }
            set
            {
                _library.Name = value;
                OnPropertyChanged("Name");
            }
        }

        /// <summary>
        /// Physical location of the Library on Hard drive.
        /// </summary>
        public string Location
        {
            get
            {
                return _library.Location;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether category column is visible.
        /// </summary>
        /// <value>
        /// <c>true</c> if category column is visible; otherwise, <c>false</c>.
        /// </value>
        public bool IsCategoryColumnVisible
        {
            get
            {
                return _isCategoryColumnVisible;
            }
            set
            {
                _isCategoryColumnVisible = value;
                OnPropertyChanged("IsCategoryColumnVisible");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether size column is visible.
        /// </summary>
        /// <value>
        /// <c>true</c> if size column is visible; otherwise, <c>false</c>.
        /// </value>
        public bool IsSizeColumnVisible
        {
            get
            {
                return _isSizeColumnVisible;
            }
            set
            {
                _isSizeColumnVisible = value;
                OnPropertyChanged("IsSizeColumnVisible");
            }
        }

        /// <summary>
        /// Presentation which is selected on view.
        /// </summary>
        public PresentationView SelectedPresentation
        {
            get
            {
                return _selectedPresentation;
            }
            set
            {
                _selectedPresentation = value;
                OnPropertyChanged("SelectedPresentation");
                EventAggregator.GetEvent<SelectedPresentationChangedEvent>().Publish(value);

            }
        }

        /// <summary>
        /// Collection of presentations stored in Library.
        /// </summary>
        public ObservableCollection<PresentationView> Presentations
        {
            get
            {
                return _presentations;
            }
        }

        /// <summary>
        /// List of user playlists
        /// </summary>
        public ObservableCollection<PlaylistView> Playlists
        {
            get
            {
                return _playlists;
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

        #region Private Methods

        /// <summary>
        /// Creates the view.
        /// </summary>
        private void CreateView()
        {
            if (_library.Items != null)
            {
                foreach (Presentation document in _library.Items)
                {
                    if (document.Type == PresentationType.Presentation)
                    {
                        PresentationView newDocumentView = new PresentationView(document);
                        Presentations.Add(newDocumentView);
                    }
                }
            }

            if (_library.Playlists != null)
            {
                foreach (IPlaylist<IPlaylist<IPresentation>> playlist in _library.Playlists)
                {
                    PlaylistView playlistView = null;
                    if (playlist.Type == PlaylistType.Playlist)
                    {
                        playlistView = new PlaylistView(playlist as IPlaylist<IPresentation>);
                    }
                    else if (playlist.Type == PlaylistType.PlaylistGroup)
                    {
                        playlistView = new PlaylistGroupView(playlist);
                    }
                }
            }
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            InitializeEvents();
        }

        /// <summary>
        /// Initializes the events.
        /// </summary>
        private void InitializeEvents()
        {
            EventAggregator.GetEvent<SelectedPresentationAskedEvent>().Subscribe(NotifySelectedPresentationChanged);
        }

        /// <summary>
        /// Notifies the selected presentation changed.
        /// </summary>
        /// <param name="dummyParameter">The dummy parameter.</param>
        private void NotifySelectedPresentationChanged(PresentationView dummyParameter)
        {
            EventAggregator.GetEvent<SelectedPresentationChangedEvent>().Publish(SelectedPresentation);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Deletes this library.
        /// </summary>
        internal void Delete()
        {
            _library.Delete();
        }

        /// <summary>
        /// Updates the business object.
        /// </summary>
        public void UpdateBusinessObject()
        {
            _library.Items.Clear();
            _library.Playlists.Clear();

            foreach (PresentationView presentationView in Presentations)
            {
                presentationView.ParentLibraryLocation = _library.Location;
                _library.Items.Add(presentationView.Presentation);
            }

            foreach (PlaylistView playlistView in Playlists)
            {
                _library.Playlists.Add(playlistView.Playlist);
            }

            _library.Save();
            ApplicationSettings.Instance.ActiveUserAccount.ActiveProfile.Save();
        }

        #endregion

    }
}
