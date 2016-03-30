
using SmartPresenter.Common.Interfaces;

namespace SmartPresenter.BO.Common.Interfaces
{
    /// <summary>
    /// A factory interface for creating playlists of different types.
    /// </summary>
    public interface IPlaylistFactory<T> where T : class, IEntity
    {
        #region Methods

        /// <summary>
        /// Creates a new playlist.
        /// </summary>
        /// <returns></returns>
        IPlaylist<T> CreatePlaylist();

        /// <summary>
        /// Creates a playlist group
        /// </summary>
        IPlaylist<IPlaylist<T>> CreatePlaylistGroup();

        #endregion
    }
}
