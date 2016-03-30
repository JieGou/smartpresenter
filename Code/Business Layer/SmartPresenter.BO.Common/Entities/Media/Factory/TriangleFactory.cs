using SmartPresenter.BO.Common.Interfaces;
using System;

namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// A Concrete Factory class to create Triangle Objects.
    /// </summary>
    public class TriangleFactory : IShapeFactory
    {
        /// <summary>
        /// Creates a Triangle.
        /// </summary>
        /// <returns></returns>
        public Shape CreateElement()
        {
            Triangle triangle = new Triangle();

            return triangle;
        }

        /// <summary>
        /// Creates a Triangle from specified path.
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
