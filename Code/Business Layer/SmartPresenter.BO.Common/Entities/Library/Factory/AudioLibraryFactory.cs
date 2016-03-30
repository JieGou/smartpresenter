using SmartPresenter.BO.Common.Interfaces;

namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// Factory class for creating Audio Libraries.
    /// </summary>
    public class AudioLibraryFactory : ILibraryFactory<Audio, IPlaylist<Audio>>
    {
        #region Methods

        /// <summary>
        /// Creates new Audio library.
        /// </summary>
        /// <returns></returns>
        public ILibrary<Audio, IPlaylist<Audio>> CreateLibrary()
        {
            return new AudioLibrary();
        }

        #endregion
    }
}
