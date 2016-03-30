using SharpDX.Direct2D1;

namespace SmartPresenter.UI.RenderEngine.DirectX
{
    /// <summary>
    /// Interface for every object which can be drawn on screen.
    /// </summary>
    interface IDrawable
    {
        #region Methods

        /// <summary>
        /// Draws the specified object on rendertarget2D.
        /// </summary>
        /// <param name="renderTarget2D">The rendertarget2D.</param>
        void Draw(RenderTarget renderTarget2D);

        #endregion
    }
}
