using SmartPresenter.BO.Common.Interfaces;
using System;
using System.Windows.Media;

namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// A concrete factory class to create Shape objects.
    /// </summary>
    public class CircleFactory : IShapeFactory
    {
        /// <summary>
        /// Creates a Circle.
        /// </summary>
        /// <returns></returns>
        public Shape CreateElement()
        {
            Circle circle = new Circle(50);
            circle.Background = Brushes.White;
            return circle;
        }

        /// <summary>
        /// Creates an Circle from specified path.
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
