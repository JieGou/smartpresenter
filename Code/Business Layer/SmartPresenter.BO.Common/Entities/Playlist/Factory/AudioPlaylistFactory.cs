using SmartPresenter.BO.Common.Interfaces;

namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// A concrete factory for creating audio playlists objects.
    /// </summary>
    public sealed class AudioPlaylistFactory : IPlaylistFactory<Audio>
    {
        #region Methods

        /// <summary>
        /// Creates a new playlist.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IPlaylist<Audio> CreatePlaylist()
        {
            return new AudioPlaylist();
        }

        /// <summary>
        /// Creates a playlist group
        /// </summary>
        /// <returns></returns>
        public IPlaylist<IPlaylist<Audio>> CreatePlaylistGroup()
        {
            return new AudioPlaylistGroup();
        }

        #endregion
    }
}
