
namespace SmartPresenter.BO.Common.Interfaces
{
    /// <summary>
    /// A factory interface for creating presentations of different types.
    /// </summary>
    public interface IPresentationFactory
    {
        #region Methods

        /// <summary>
        /// Creates the presentation.
        /// </summary>
        /// <returns></returns>
        IPresentation CreatePresentation();

        /// <summary>
        /// Loads the presentation from specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        IPresentation LoadPresentation(string filePath);

        #endregion
    }
}
