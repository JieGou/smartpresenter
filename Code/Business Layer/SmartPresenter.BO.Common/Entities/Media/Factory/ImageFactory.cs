using SmartPresenter.BO.Common.Interfaces;
using System.Windows.Media;

namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// A concrete factory class to create Image objects.
    /// </summary>
    public class ImageFactory : IShapeFactory
    {
        /// <summary>
        /// Creates an Image.
        /// </summary>
        /// <returns></returns>
        public Shape CreateElement()
        {
            Image image = new Image();
            image.Background = Brushes.Green;
            image.Width = image.Height = 100;
            return image;
        }

        /// <summary>
        /// Creates an Image from a specified path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Shape CreateElement(string path)
        {
            Image image = new Image(path);
            image.Background = Brushes.Green;
            image.Width = image.Height = 100;
            return image;
        }
    }
}
