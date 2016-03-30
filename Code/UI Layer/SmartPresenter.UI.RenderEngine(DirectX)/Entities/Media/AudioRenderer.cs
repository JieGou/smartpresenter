
using SharpDX.Direct2D1;
using SmartPresenter.UI.Controls.ViewModel;
namespace SmartPresenter.UI.RenderEngine.DirectX.Entities
{
    /// <summary>
    /// A Renderer to render audio objects.
    /// </summary>
    class AudioRenderer : ShapeRenderer
    {
        #region Private Data Members

        private bool _disposed = false;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioRenderer"/> class.
        /// </summary>
        /// <param name="audio">The audio.</param>
        public AudioRenderer(AudioView audio) : base(audio)
        {

        }

        #endregion

        #region IRenderer Members

        /// <summary>
        /// Renders this object.
        /// </summary>
        public override void Draw(RenderTarget renderTarget2D)
        {
            base.Draw(renderTarget2D);
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
