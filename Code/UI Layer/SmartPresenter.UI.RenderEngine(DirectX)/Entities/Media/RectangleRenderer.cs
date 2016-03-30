using SharpDX;
using SharpDX.Direct2D1;
using SmartPresenter.UI.Controls.ViewModel;
using System.Windows;

namespace SmartPresenter.UI.RenderEngine.DirectX.Entities
{
    /// <summary>
    /// A Renderer for rectangle.
    /// </summary>
    class RectangleRenderer : ShapeRenderer
    {
        #region Private Data Members

        private bool _disposed = false;
        private RectangleView _rectangle;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="RectangleRenderer"/> class.
        /// </summary>
        /// <param name="rectangle">The rectangle.</param>
        public RectangleRenderer(RectangleView rectangle)
            : base(rectangle)
        {
            _rectangle = rectangle;
        }

        #endregion

        #region IDrawable Members

        /// <summary>
        /// Renders this object.
        /// </summary>
        public override void Draw(RenderTarget renderTarget2D)
        {
            base.Draw(renderTarget2D);

            renderTarget2D.DrawRectangle(new SharpDX.RectangleF(_rectangle.X, _rectangle.Y, _rectangle.Width, _rectangle.Height), new SolidColorBrush(renderTarget2D, Color4.Black));                      
            renderTarget2D.FillRectangle(new SharpDX.RectangleF(_rectangle.X, _rectangle.Y, _rectangle.Width, _rectangle.Height), new SolidColorBrush(renderTarget2D, new Color4(0, 250, 30, 255)));
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (_disposed)
            {
                if (disposing)
                {

                }
                _disposed = true;
            }
            base.Dispose(disposing);
        }

        #endregion
    }
}
