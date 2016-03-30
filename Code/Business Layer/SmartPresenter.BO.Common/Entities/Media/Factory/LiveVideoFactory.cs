using SmartPresenter.BO.Common.Interfaces;
using System;

namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// A concrete factory class to create LiveVideo objects.
    /// </summary>
    public class LiveVideoFactory : IShapeFactory
    {
        /// <summary>
        /// Creates an audio shape.
        /// </summary>
        /// <returns></returns>
        public Shape CreateElement()
        {
            return new LiveVideo();
        }

        /// <summary>
        /// Creates a LiveVideo from specified path.
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
