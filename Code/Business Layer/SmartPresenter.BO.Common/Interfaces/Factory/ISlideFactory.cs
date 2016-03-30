
namespace SmartPresenter.BO.Common.Interfaces
{
    /// <summary>
    /// An interface for Factory objects to create 
    /// </summary>
    public interface ISlideFactory
    {
        #region Methods

        /// <summary>
        /// Creates a slide.
        /// </summary>
        /// <returns></returns>
        ISlide CreateSlide();

        #endregion
    }
}
