using SmartPresenter.BO.Common.Interfaces;
using System;
using System.Windows.Media;

namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// A concrete factory class to create Rectangle objects.
    /// </summary>
    public class RectangleFactory : IShapeFactory
    {
        /// <summary>
        /// Creates a Rectangle.
        /// </summary>
        /// <returns></returns>
        public Shape CreateElement()
        {
            Rectangle rectangle = new Rectangle(200, 100);
            rectangle.Background = Brushes.White;
            return rectangle;
        }

        /// <summary>
        /// Creates a Rectangle from specified path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Shape CreateElement(string path)
        {
            throw new NotImplementedException();
        }
    }
}
