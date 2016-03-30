using SmartPresenter.BO.Common.Interfaces;
using System;

namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// A concrete factory class to create DVD objects.
    /// </summary>
    public class DVDFactory : IShapeFactory
    {
        /// <summary>
        /// Creates a DVD.
        /// </summary>
        /// <returns></returns>
        public Shape CreateElement()
        {
            return new DVD();
        }

        /// <summary>
        /// Creates an DVD from specified path.
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
