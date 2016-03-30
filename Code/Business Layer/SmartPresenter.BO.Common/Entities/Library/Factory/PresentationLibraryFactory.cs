using SmartPresenter.BO.Common.Interfaces;

namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// Factory class for creating Presentation Libraries.
    /// </summary>
    public class PresentationLibraryFactory : ILibraryFactory<IPresentation, IPlaylist<IPresentation>>
    {
        #region Methods

        /// <summary>
        /// Creates new Presentation Libraries.
        /// </summary>
        /// <returns></returns>
        public ILibrary<IPresentation, IPlaylist<IPresentation>> CreateLibrary()
        {
            return new PresentationLibrary();
        }

        #endregion
    }
}
