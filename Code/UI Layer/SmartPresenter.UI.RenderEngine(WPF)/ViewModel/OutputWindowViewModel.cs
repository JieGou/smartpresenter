using Microsoft.Practices.Prism.Mvvm;
using SmartPresenter.UI.Controls.SlideShow;
using SmartPresenter.UI.Controls.ViewModel;
using System;

namespace SmartPresenter.UI.RenderEngine.WPF.ViewModel
{
    /// <summary>
    /// Class for ViewModel of Output Window.
    /// </summary>
    public class OutputWindowViewModel : BindableBase
    {
        #region Private Data Members

        private SlideView _currentSlide;
        private SlideView _previousSlide;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="OutputWindowViewModel"/> class.
        /// </summary>
        public OutputWindowViewModel()
        {
            SlideShowManager.Instance.CurrentSlideUpdated += CurrentSlideUpdated;
        }

        #endregion

        #region Properties

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
                if (_currentSlide == null)
                {
                    _currentSlide = SlideShowManager.Instance.CurrentSlide;
                }
                return _currentSlide;
            }
            set
            {
                PreviousSlide = _currentSlide;
                _currentSlide = value;
                OnPropertyChanged("CurrentSlide");
            }
        }

        /// <summary>
        /// Gets or sets the previous slide.
        /// </summary>
        /// <value>
        /// The previous slide.
        /// </value>
        public SlideView PreviousSlide
        {
            get
            {
                return _previousSlide;
            }
            set
            {
                _previousSlide = value;
                OnPropertyChanged("PreviousSlide");
            }
        }

        #endregion

        #region Commands



        #region Command Handlers



        #endregion

        #endregion

        #region Methods

        #region Public Methods



        #endregion

        #region Private Methods

        /// <summary>
        /// Currents the slide updated.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void CurrentSlideUpdated(object sender, EventArgs e)
        {
            CurrentSlide = SlideShowManager.Instance.CurrentSlide;
        }

        #endregion

        #endregion
    }
}
