using SmartPresenter.BO.Common;
using SmartPresenter.BO.Common.Interfaces;
using System.Collections.ObjectModel;

namespace SmartPresenter.UI.Controls.ViewModel
{
    /// <summary>
    /// A class for PlaylistGroup Object UI.
    /// </summary>
    public class PlaylistGroupView : PlaylistView
    {
        #region Private Data Members

        private ObservableCollection<PlaylistView> _playlists;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new playlist group.
        /// </summary>
        public PlaylistGroupView(IPlaylist<IPlaylist<IPresentation>> IPlaylistItem)
        {
            _playlists = new ObservableCollection<PlaylistView>();
            CreateView();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Collection of Playlists.
        /// </summary>
        public ObservableCollection<PlaylistView> Playlists
        {
            get
            {
                return _playlists;
            }
        }

        /// <summary>
        /// Type of Playlist.
        /// </summary>
        public override PlaylistType Type
        {
            get { return PlaylistType.PlaylistGroup; }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Creates the view.
        /// </summary>
        protected override void CreateView()
        {
            foreach (IPlaylist<IPlaylist<IPresentation>> playlist in Playlists)
            {
                if (playlist.Type == PlaylistType.Playlist)
                {
                    PlaylistView newPlaylistView = new PlaylistView(playlist as IPlaylist<IPresentation>);
                    newPlaylistView.Parent = this;
                    Playlists.Add(newPlaylistView);
                }
                else if (playlist.Type == PlaylistType.PlaylistGroup)
                {
                    PlaylistGroupView newPlaylistView = new PlaylistGroupView(playlist);
                    newPlaylistView.Parent = this;
                    Playlists.Add(newPlaylistView);
                }
            }
        }

        #endregion

        #region Public Methods

        //public void UpdateBusinessObject()
        //{

        //}

        #endregion

    }
}
