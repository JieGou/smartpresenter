using SmartPresenter.BO.Common.Interfaces;
using System;
using System.Windows.Media;

namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// A concrete factory class to create Square objects.
    /// </summary>
    public class SquareFactory : IShapeFactory
    {
        /// <summary>
        /// Creates a Square.
        /// </summary>
        /// <returns></returns>
        public Shape CreateElement()
        {
            Square square = new Square(100);
            square.Background = Brushes.White;
            return square;
        }

        /// <summary>
        /// Creates a Square from specified path.
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
