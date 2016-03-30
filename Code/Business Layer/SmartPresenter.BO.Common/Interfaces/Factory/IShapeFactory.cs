using SmartPresenter.BO.Common.Entities;

namespace SmartPresenter.BO.Common.Interfaces
{
    /// <summary>
    /// A factory interface to creat shape objects
    /// </summary>
    public interface IShapeFactory
    {
        #region Methods

        /// <summary>
        /// Creates the shape.
        /// </summary>
        /// <returns></returns>
        Shape CreateElement();

        /// <summary>
        /// Creates an shape from specified path.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        Shape CreateElement(string path);

        /// <summary>
        /// Creates the element.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        //Shape CreateElement(int x, int y, int width, int height);

        #endregion
    }
}
