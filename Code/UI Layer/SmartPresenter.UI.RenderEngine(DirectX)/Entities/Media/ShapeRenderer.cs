
using SharpDX.Direct2D1;
using SmartPresenter.UI.Controls.ViewModel;
using System;
namespace SmartPresenter.UI.RenderEngine.DirectX.Entities
{
    /// <summary>
    /// A Renderer to render shape objects.
    /// </summary>
    abstract class ShapeRenderer : IDrawable, IDisposable
    {
        #region Private Data Members

        private bool _disposed = false;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ShapeRenderer"/> class.
        /// </summary>
        /// <param name="shape">The shape.</param>
        public ShapeRenderer(ShapeView shape)
        {            
        }

        #endregion

        #region IDrawable Members

        /// <summary>
        /// Renders this object.
        /// </summary>
        public virtual void Draw(RenderTarget renderTarget2D)
        {
            
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {

                }
                _disposed = true;
            }
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="ShapeRenderer"/> class.
        /// </summary>
        ~ShapeRenderer()
        {
            Dispose(false);
        }

        #endregion
    }
}
