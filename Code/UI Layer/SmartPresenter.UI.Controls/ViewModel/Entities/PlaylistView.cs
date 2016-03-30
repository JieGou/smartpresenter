using Microsoft.Practices.Prism.Mvvm;
using SmartPresenter.BO.Common;
using SmartPresenter.BO.Common.Entities;
using SmartPresenter.BO.Common.Interfaces;
using System;
using System.Collections.ObjectModel;

namespace SmartPresenter.UI.Controls.ViewModel
{
    /// <summary>
    /// A class for Playlist Object UI.
    /// </summary>
    public class PlaylistView : BindableBase
    {
        #region Private Data Members

        /// <summary>
        /// The collection of presentations
        /// </summary>
        private ObservableCollection<PresentationView> _presentations;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaylistView"/> class.
        /// </summary>
        public PlaylistView()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaylistView"/> class.
        /// </summary>
        /// <param name="playlist">The playlist.</param>
        public PlaylistView(IPlaylist<IPresentation> playlist)
        {
            Playlist = playlist;
            _presentations = new ObservableCollection<PresentationView>();
            CreateView();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets or sets the playlist.
        /// </summary>
        /// <value>
        /// The playlist.
        /// </value>
        public IPlaylist<IPresentation> Playlist { get; protected set; }

        /// <summary>
        /// Name of playlist.
        /// </summary>
        public string Name
        {
            get
            {
                return Playlist.Name;
            }
            set
            {
                Playlist.Name = value;
                OnPropertyChanged("Name");
            }
        }

        /// <summary>
        /// Type of Playlist.
        /// </summary>
        public virtual PlaylistType Type
        {
            get { return Playlist.Type; }
        }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        /// <value>
        /// The parent.
        /// </value>
        public PlaylistView Parent { get; set; }

        /// <summary>
        /// Collection of Presentations.
        /// </summary>
        public ObservableCollection<PresentationView> Presentations
        {
            get
            {
                return _presentations;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Creates the view.
        /// </summary>
        protected virtual void CreateView()
        {
            foreach (Presentation document in ((PresentationPlaylist)Playlist).Items)
            {
                if (document.Type == PresentationType.Presentation)
                {
                    PresentationView documentView = new PresentationView(document);
                    documentView.Parent = this;
                    Presentations.Add(documentView);
                }
            }
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {

        }

        #endregion

        #region Public Methods

        //public void UpdateBusinessObject()
        //{

        //}

        #endregion

    }
}
