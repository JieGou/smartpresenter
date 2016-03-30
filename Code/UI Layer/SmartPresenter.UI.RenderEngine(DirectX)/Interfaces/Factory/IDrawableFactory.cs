using SmartPresenter.UI.Controls.ViewModel;

namespace SmartPresenter.UI.RenderEngine.DirectX
{
    /// <summary>
    /// an Interface for a Factory class to create Renderers of different Types.
    /// </summary>
    interface IDrawableFactory
    {
        #region Methods

        /// <summary>
        /// Creates the renderer.
        /// </summary>
        /// <param name="shape">The shape.</param>
        /// <returns></returns>
        IDrawable CreateDrawable(ShapeView shape);

        #endregion
    }
}
