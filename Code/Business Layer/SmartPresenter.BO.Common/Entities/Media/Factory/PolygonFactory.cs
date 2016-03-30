using SmartPresenter.BO.Common.Interfaces;
using System;

namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// A Concrete Factory class to create Polygons.
    /// </summary>
    public class PolygonFactory : IShapeFactory
    {
        /// <summary>
        /// Creates a Polygon.
        /// </summary>
        /// <returns></returns>
        public Shape CreateElement()
        {
            Polygon polygon = new Polygon();
            return polygon;
        }

        /// <summary>
        /// Creates an Polygon from specified path.
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
