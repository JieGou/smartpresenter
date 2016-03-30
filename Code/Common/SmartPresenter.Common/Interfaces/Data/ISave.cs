
namespace SmartPresenter.Common.Interfaces
{
    /// <summary>
    /// An interface for all the item which needs to be saved, providing methods for saving.
    /// </summary>
    public interface ISave
    {
        #region Methods

        /// <summary>
        /// Saves this instance to default location.
        /// </summary>
        void Save();
        /// <summary>
        /// Saves this instance to a specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        void Save(string path);

        #endregion
    }
}
