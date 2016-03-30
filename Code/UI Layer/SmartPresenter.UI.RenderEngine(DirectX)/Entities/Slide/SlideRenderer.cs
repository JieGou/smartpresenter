
using SharpDX.Direct2D1;
using SmartPresenter.UI.Controls.ViewModel;
using System;
using System.Collections.Generic;
namespace SmartPresenter.UI.RenderEngine.DirectX.Entities
{
    /// <summary>
    /// A Renderer to render slide objects.
    /// </summary>
    class SlideRenderer : BaseRenderer
    {
        #region Private Data Members

        private bool _disposed = false;
        private List<IDrawable> _elements = new List<IDrawable>();

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SlideRenderer"/> class.
        /// </summary>
        /// <param name="slide">The slide.</param>
        public SlideRenderer(SlideView slide, IntPtr displayWindowHandle)
        {
            DisplayWindowHandle = displayWindowHandle;
            IDrawableFactory rendererFactory = new ShapeRendererFactory();
            foreach(ShapeView shape in slide.Elements)
            {
                _elements.Add(rendererFactory.CreateDrawable(shape));
            }
        }

        #endregion        

        #region Methods

        #region Private Methods

        

        #endregion

        #region Public Methods

        #region IRenderer Members

        /// <summary>
        /// Renders this object.
        /// </summary>
        protected override void Draw(RenderTarget renderTarget2D)
        {
            base.Draw(renderTarget2D);

            foreach (IDrawable element in _elements)
            {
                element.Draw(renderTarget2D);
            }
        }

        #endregion

        #region IDisposable Members
        

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (_disposed == false)
            {
                if (disposing)
                {

                }
                _disposed = true;
            }
            base.Dispose(disposing);
        }

        #endregion

        #endregion

        #endregion


    }
}
