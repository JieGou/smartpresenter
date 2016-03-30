using SmartPresenter.BO.Common.Interfaces;

namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// A concrete factory class to create slide objects.
    /// </summary>
    public sealed class SlideFactory : ISlideFactory
    {
        #region ISlideFactory

        /// <summary>
        /// Creates a slide.
        /// </summary>
        /// <returns></returns>
        public ISlide CreateSlide()
        {
            return new Slide();
        }

        #endregion
    }
}
