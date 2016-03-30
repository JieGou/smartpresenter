using SmartPresenter.UI.Controls.SlideShow;
using SmartPresenter.UI.RenderEngine.DirectX.ViewModel;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace SmartPresenter.UI.RenderEngine.DirectX
{
    /// <summary>
    /// Interaction logic for OutputWindow.xaml
    /// </summary>
    public partial class OutputWindow : Window
    {
        #region Private Data Members

        /// <summary>
        /// The View Model
        /// </summary>
        private OutputWindowViewModel _viewModel;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="OutputWindow"/> class.
        /// </summary>
        public OutputWindow()
        {
            InitializeComponent();
            DataContext = _viewModel = new OutputWindowViewModel();
            Initialize();
        }

        #endregion

        #region Methods

        #region Private Methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            Loaded += OutputWindow_Loaded;
        }

        /// <summary>
        /// Handles the Activated event of the OutputWindow control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OutputWindow_Loaded(object sender, EventArgs e)
        {
            SetHandle(new WindowInteropHelper(this).Handle);
        }        

        /// <summary>
        /// Handles the PreviewKeyDown event of the Window control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    SlideShowManager.Instance.StopSlideShow();
                    this.Hide();
                    break;
                case Key.Left:
                case Key.Up:
                    SlideShowManager.Instance.PlayPreviousSlide();
                    break;
                case Key.Right:
                case Key.Down:
                    SlideShowManager.Instance.PlayNextSlide();
                    break;
            }
        }

        internal void SetHandle(IntPtr handle)
        {
            _viewModel.SetHandle(handle);   
        }


        #endregion

        #endregion
    }
}
