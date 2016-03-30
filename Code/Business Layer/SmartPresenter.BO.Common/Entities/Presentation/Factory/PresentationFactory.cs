using SmartPresenter.BO.Common.Interfaces;
using SmartPresenter.Common;
using SmartPresenter.Data.Common;

namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// A concrete factory for creating presentation objects.
    /// </summary>
    public sealed class PresentationFactory : IPresentationFactory
    {
        /// <summary>
        /// Creates the presentation.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IPresentation CreatePresentation()
        {
            IPresentation presentation = new Presentation()
            {
                Name = Constants.Default_Presentation_Name,
                Category = Constants.Default_Presentation_Category,
            };
            presentation.AddNewSlide();

            return presentation;
        }

        /// <summary>
        /// Loads the presentation from specified path.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public IPresentation LoadPresentation(string filePath)
        {
            Serializer<Presentation> serializer = new Serializer<Presentation>();

            return serializer.Load(filePath);
        }
    }
}
