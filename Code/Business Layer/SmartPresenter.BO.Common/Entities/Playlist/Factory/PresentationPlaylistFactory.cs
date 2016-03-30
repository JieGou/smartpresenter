using SmartPresenter.BO.Common.Interfaces;
using SmartPresenter.Common;

namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// A concrete factory for creating presentation playlists objects.
    /// </summary>
    public sealed class PresentationPlaylistFactory : IPlaylistFactory<IPresentation>
    {
        #region Methods

        /// <summary>
        /// Creates a new playlist.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IPlaylist<IPresentation> CreatePlaylist()
        {
            return new PresentationPlaylist() { Name = Constants.New_Playlist_Name };
        }

        /// <summary>
        /// Creates a playlist group
        /// </summary>
        /// <returns></returns>
        public IPlaylist<IPlaylist<IPresentation>> CreatePlaylistGroup()
        {
            return new PresentationPlaylistGroup() { Name = Constants.New_Playlist_Group_Name };
        }

        #endregion
    }
}
