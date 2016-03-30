using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DirectWrite;
using SharpDX.DXGI;
using SmartPresenter.BO.Common;
using System;

namespace SmartPresenter.UI.RenderEngine.DirectX
{
    /// <summary>
    /// A base class which contains core rendering fucntionality, actual rendering logic will come from objects being rendered.
    /// </summary>
    class BaseRenderer : IRenderer, IDisposable
    {
        #region Private Data Members

        private bool _disposed = false;
        private IntPtr _displayWindowHandle;

        SharpDX.Direct3D11.Device _device;
        SwapChain _swapChain;
        Texture2D _backBuffer = null;
        RenderTargetView _backBufferView = null;
        RenderTarget _renderTarget2D = null;
        
        TextLayout _textLayout;
        SharpDX.Direct2D1.SolidColorBrush _textColorBrush;

        int _outputX;
        int _outputY;
        int _outputWidth;
        int _outputHeight;

        #endregion

        #region Constructor

        public BaseRenderer()
        {
            Initialize();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the display window handle.
        /// </summary>
        /// <value>
        /// The display window handle.
        /// </value>
        internal IntPtr DisplayWindowHandle
        {
            get
            {
                return _displayWindowHandle;
            }
            set
            {
                if (_displayWindowHandle != value)
                {
                    _displayWindowHandle = value;
                    InitializeDevice();
                }
            }
        }

        #endregion

        #region Methods

        #region Private Methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            _outputX = ApplicationSettings.Instance.ActiveUserAccount.ActiveProfile.DisplaySettings.OutputX;
            _outputY = ApplicationSettings.Instance.ActiveUserAccount.ActiveProfile.DisplaySettings.OutputY;
            _outputWidth = ApplicationSettings.Instance.ActiveUserAccount.ActiveProfile.DisplaySettings.OutputWidth;
            _outputHeight = ApplicationSettings.Instance.ActiveUserAccount.ActiveProfile.DisplaySettings.OutputHeight;

            if (DisplayWindowHandle.ToInt32() > 0)
            {
                InitializeDevice();
            }
        }

        /// <summary>
        /// Initializes DirectX objects.
        /// </summary>
        private void InitializeDevice()
        {
            try
            {
                // Clean up.
                UnInitializeDevice();

                // Initialize SwapChain.
                SwapChainDescription swapChainDescription = new SwapChainDescription()
                {
                    BufferCount = 1,
                    ModeDescription = new ModeDescription(_outputWidth, _outputHeight, new Rational(60, 1), Format.R8G8B8A8_UNorm),
                    IsWindowed = true,
                    OutputHandle = DisplayWindowHandle,
                    SampleDescription = new SampleDescription(1, 0),
                    SwapEffect = SwapEffect.Discard,
                    Usage = Usage.RenderTargetOutput
                };

                // Create Device and SwapChain.
                SharpDX.Direct3D11.Device.CreateWithSwapChain(DriverType.Hardware,
                    DeviceCreationFlags.BgraSupport, new[] { SharpDX.Direct3D.FeatureLevel.Level_10_0 }, swapChainDescription, out _device, out _swapChain);

                // Associate the Render Widnow to Render Engine.
                SharpDX.DXGI.Factory factory = _swapChain.GetParent<SharpDX.DXGI.Factory>();
                factory.MakeWindowAssociation(DisplayWindowHandle, WindowAssociationFlags.IgnoreAll);

                // Create Back Buffer and Back Buffer View.
                _backBuffer = Texture2D.FromSwapChain<Texture2D>(_swapChain, 0);
                _backBufferView = new RenderTargetView(_device, _backBuffer);

                SharpDX.Direct2D1.Factory factory2D = new SharpDX.Direct2D1.Factory();
                using (Surface surface = _backBuffer.QueryInterface<Surface>())
                {
                    _renderTarget2D = new RenderTarget(factory2D, surface, new RenderTargetProperties(new SharpDX.Direct2D1.PixelFormat(Format.Unknown, AlphaMode.Premultiplied)));
                }
                _renderTarget2D.AntialiasMode = AntialiasMode.PerPrimitive;

                _textLayout = CreateLogoText();
            }
            catch(Exception e)
            {

            }
        }

        /// <summary>
        /// Creates the logo text.
        /// </summary>
        /// <returns></returns>
        private TextLayout CreateLogoText()
        {
            SharpDX.DirectWrite.Factory factoryDWrite = new SharpDX.DirectWrite.Factory();

            TextFormat textFormat = new TextFormat(factoryDWrite, "Times New Roman", 64) { TextAlignment = SharpDX.DirectWrite.TextAlignment.Center, ParagraphAlignment = ParagraphAlignment.Center };
            TextLayout textLayout = new TextLayout(factoryDWrite, "SmartPresenter - Your Intelligent Presenter", textFormat, 1600, 900);
            _textColorBrush = new SharpDX.Direct2D1.SolidColorBrush(_renderTarget2D, SharpDX.Color.Green);
            _renderTarget2D.TextAntialiasMode = TextAntialiasMode.Cleartype;

            return textLayout;
        }

        /// <summary>
        /// Disposes all DirectX objects.
        /// </summary>
        private void UnInitializeDevice()
        {
            Utilities.Dispose(ref _backBuffer);
            Utilities.Dispose(ref _backBufferView);
            Utilities.Dispose(ref _renderTarget2D);
            Utilities.Dispose(ref _swapChain);
            Utilities.Dispose(ref _device);

            Utilities.Dispose(ref _textLayout);
            Utilities.Dispose(ref _textColorBrush);
        }

        #endregion

        #region Public Methods

        #region IRenderer Members

        /// <summary>
        /// Renders this object.
        /// </summary>
        public void Render()
        {
            BeginDraw();
            Draw(_renderTarget2D);
            EndDraw();
        }

        #endregion

        /// <summary>
        /// Begins the draw operation, initialize objects.
        /// </summary>
        protected virtual void BeginDraw()
        {            
            _device.ImmediateContext.Rasterizer.SetViewport(new Viewport(_outputX, _outputY, _outputWidth, _outputHeight));
            _device.ImmediateContext.OutputMerger.SetTargets(_backBufferView);

            _renderTarget2D.BeginDraw();
            _renderTarget2D.Clear(Color4.White);
        }

        /// <summary>
        /// Draws this object.
        /// </summary>
        protected virtual void Draw(RenderTarget renderTarget2D)
        {
            DrawLogo();

            renderTarget2D.DrawRectangle(new SharpDX.RectangleF(200, 200, 500, 500), new SolidColorBrush(renderTarget2D, Color4.Black));
            renderTarget2D.FillRectangle(new SharpDX.RectangleF(200, 200, 500, 500), new SolidColorBrush(renderTarget2D, new Color4(0, 0, 255, 255)));
        }        

        /// <summary>
        /// Ends the draw operation and send the object to be presented to screen.
        /// </summary>
        protected virtual void EndDraw()
        {
            _renderTarget2D.EndDraw();

            _swapChain.Present(0, PresentFlags.None);
        }

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
            if (_disposed == false)
            {
                if (disposing)
                {
                    UnInitializeDevice();
                }
            }
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="BaseRenderer"/> class.
        /// </summary>
        ~BaseRenderer()
        {
            Dispose(false);
        }

        #endregion

        #endregion

        #region Private Methods

        /// <summary>
        /// Draws the logo.
        /// </summary>
        private void DrawLogo()
        {
            _renderTarget2D.DrawTextLayout(new Vector2(0, 0), _textLayout, _textColorBrush, DrawTextOptions.None);
        }

        #endregion

        #endregion
    }
}
