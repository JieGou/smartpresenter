using SmartPresenter.UI.Controls.ViewModel;
using SmartPresenter.UI.RenderEngine.DirectX.Entities;
using System;
using System.Timers;

namespace SmartPresenter.UI.RenderEngine.DirectX
{
    /// <summary>
    /// Application's Main Renderer.
    /// </summary>
    public class Renderer
    {
        #region Constants

        /// <summary>
        /// The render interval(frame rate)
        /// </summary>
        private int Render_Interval = 60;

        #endregion

        #region Private Data Members

        private static volatile Renderer _instance;
        private static Object _lockObject = new Object();

        private SlideView _currentSlide;
        private SlideRenderer _slideRenderer;
        private IntPtr _displayWindowHandle;
        private Timer _renderTimer;

        #endregion

        #region Private Constructor

        /// <summary>
        /// Prevents a default instance of the <see cref="Renderer"/> class from being created.
        /// </summary>
        private Renderer()
        {
            _renderTimer = new Timer(Render_Interval);
            _renderTimer.Elapsed += RenderTimer_Elapsed;
        }        

        #endregion

        #region Properties

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static Renderer Instance
        {
            get
            {
                if(_instance == null)
                {
                    lock(_lockObject)
                    {
                        if(_instance == null)
                        {
                            _instance = new Renderer();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// Gets or sets the current slide.
        /// </summary>
        /// <value>
        /// The current slide.
        /// </value>
        public SlideView CurrentSlide
        {
            get
            {
                return _currentSlide;
            }
            set
            {
                _currentSlide = value;
                if (value != null)
                {
                    if (DisplayWindowHandle.ToInt32() > 0)
                    {
                        if (_slideRenderer != null)
                        {
                            _slideRenderer.Dispose();
                            _slideRenderer = null;
                        }
                        _slideRenderer = new SlideRenderer(CurrentSlide, DisplayWindowHandle);
                    }
                }
            }
        }

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
                _displayWindowHandle = value;
                if (value != null)
                {
                    if (_displayWindowHandle.ToInt32() > 0)
                    {
                        if (_slideRenderer != null)
                        {
                            _slideRenderer.Dispose();
                            _slideRenderer = null;
                        }
                        _slideRenderer = new SlideRenderer(CurrentSlide, value);
                    }
                }
            }
        }

        #endregion

        #region Methods

        #region Private Methods

        /// <summary>
        /// Handles the Elapsed event of the RenderTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ElapsedEventArgs"/> instance containing the event data.</param>
        private void RenderTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            RenderLoop();
        }

        /// <summary>
        /// Render loop of application. All rendering logic goes here.
        /// </summary>
        private void RenderLoop()
        {
            if (_slideRenderer != null)
            {
                _slideRenderer.Render();
            }
        }

        #endregion

        #region Public Methods
       
        /// <summary>
        /// Starts the render loop.
        /// </summary>
        public void Start()
        {
            _renderTimer.Start();
        }

        /// <summary>
        /// Stops the render loop.
        /// </summary>
        public void Stop()
        {
            _renderTimer.Stop();
        }

        #endregion

        #endregion
    }
}
