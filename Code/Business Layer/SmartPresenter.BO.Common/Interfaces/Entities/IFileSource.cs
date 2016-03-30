
namespace SmartPresenter.BO.Common.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    interface IFileSource
    {
        #region Properties

        /// <summary>
        /// Gets or sets the path to the file.
        /// </summary>
        /// <value>
        /// The path to the file.
        /// </value>
        string Path { get; set; }
        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        string Name { get; set; }

        #endregion
    }
}
