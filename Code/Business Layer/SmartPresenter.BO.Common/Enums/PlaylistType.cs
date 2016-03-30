
namespace SmartPresenter.BO.Common
{
    /// <summary>
    /// Enum for playlist type.
    /// </summary>
    public enum PlaylistType
    {
        /// <summary>
        /// The null playlist.
        /// </summary>
        None,
        /// <summary>
        /// A Playlist - Collection of Presentations.
        /// </summary>
        Playlist,
        /// <summary>
        /// A PlaylistGroup - Collection of Playlists.
        /// </summary>
        PlaylistGroup,
        /// <summary>
        /// The media playlist - Collection of media items
        /// </summary>
        MediaPlaylist,
        /// <summary>
        /// The audio playlist - Collection of audio items
        /// </summary>
        AudioPlaylist,
        /// <summary>
        /// The media playlist group - Collection of media playlists
        /// </summary>
        MediaPlaylistGroup,
        /// <summary>
        /// The audio playlist group - Collection of audio playlists
        /// </summary>
        AudioPlaylistGroup,
    }
}
