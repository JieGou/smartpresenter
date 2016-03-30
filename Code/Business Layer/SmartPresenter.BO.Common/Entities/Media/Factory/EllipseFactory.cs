using SmartPresenter.BO.Common.Interfaces;
using System;
using System.Windows.Media;

namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// A concrete factory class to create Shape objects.
    /// </summary>
    public class EllipseFactory : IShapeFactory
    {
        /// <summary>
        /// Creates an Ellipse.
        /// </summary>
        /// <returns></returns>
        public Shape CreateElement()
        {
            Ellipse ellipse = new Ellipse(200, 100);
            ellipse.Background = Brushes.White;
            return ellipse;
        }

        /// <summary>
        /// Creates an Ellipse from specified path.
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
