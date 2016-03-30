
namespace SmartPresenter.Common.Interfaces
{
    /// <summary>
    /// An interface representing items which can be saved.
    /// </summary>
    public interface ISavable
    {
        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether this instance has been changed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance was changes; otherwise, <c>false</c>.
        /// </value>
        bool IsDirty { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Marks this instance as dirty.
        /// </summary>
        void MarkDirty();

        #endregion
    }
}
