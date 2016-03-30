using SmartPresenter.BO.Common.Interfaces;

namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// A concrete factory for creating media playlists objects.
    /// </summary>
    public sealed class MediaPlaylistFactory : IPlaylistFactory<Image>
    {
        #region Methods

        /// <summary>
        /// Creates a new playlist.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IPlaylist<Image> CreatePlaylist()
        {
            return new MediaPlaylist();
        }

        /// <summary>
        /// Creates a playlist group
        /// </summary>
        /// <returns></returns>
        public IPlaylist<IPlaylist<Image>> CreatePlaylistGroup()
        {
            return new MediaPlaylistGroup();
        }

        #endregion
    }
}
