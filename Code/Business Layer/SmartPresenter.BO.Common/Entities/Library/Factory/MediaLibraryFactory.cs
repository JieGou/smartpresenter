using SmartPresenter.BO.Common.Interfaces;

namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// Factory class for creating Media Libraries.
    /// </summary>
    public class MediaLibraryFactory : ILibraryFactory<Image, IPlaylist<Image>>
    {
        #region Methods

        /// <summary>
        /// Creates new Media library.
        /// </summary>
        /// <returns></returns>
        public ILibrary<Image, IPlaylist<Image>> CreateLibrary()
        {
            return new MediaLibrary();
        }

        #endregion
    }
}
