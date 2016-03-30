using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using SmartPresenter.BO.Common.Enums;
using SmartPresenter.UI.Common;
using SmartPresenter.UI.Controls.SlideShow;

namespace SmartPresenter.UI.Controls.ViewModel
{
    /// <summary>
    /// View Model class for SlideShow tab.
    /// </summary>
    public class SlideShowTabViewModel : BindableBase
    {
        #region Private Data Members



        #endregion

        #region Contsructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SlideShowTabViewModel"/> class.
        /// </summary>
        public SlideShowTabViewModel()
        {
            Initialize();
        }

        #endregion

        #region Properties



        #endregion

        #region Commands

        /// <summary>
        /// Gets the slide show from current slide command.
        /// </summary>
        /// <value>
        /// The slide show from current slide command.
        /// </value>
        public DelegateCommand SlideShowFromCurrentSlideCommand { get; private set; }

        /// <summary>
        /// Gets the slide show from first slide command.
        /// </summary>
        /// <value>
        /// The slide show from first slide command.
        /// </value>
        public DelegateCommand SlideShowFromFirstSlideCommand { get; private set; }

        #region Command Handlers

        /// <summary>
        /// Slides the show from current slide command_ executed.
        /// </summary>
        private void SlideShowFromCurrentSlideCommand_Executed()
        {
            SlideShowManager.Instance.SlideShowStartMode = SlideShowStartMode.FromCurrent;
            SlideShowCommands.StartSlideShowCommand.Execute(null);
        }

        /// <summary>
        /// Slides the show from first slide command_ executed.
        /// </summary>
        private void SlideShowFromFirstSlideCommand_Executed()
        {
            SlideShowManager.Instance.SlideShowStartMode = SlideShowStartMode.FromFirst;
            SlideShowCommands.StartSlideShowCommand.Execute(null);
        }

        #endregion

        #endregion

        #region Methods

        #region Private Methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            CreateCommands();
        }

        /// <summary>
        /// Creates the commands.
        /// </summary>
        private void CreateCommands()
        {
            SlideShowFromFirstSlideCommand = new DelegateCommand(SlideShowFromFirstSlideCommand_Executed);
            SlideShowFromCurrentSlideCommand = new DelegateCommand(SlideShowFromCurrentSlideCommand_Executed);
        }

        #endregion

        #region Public Methods



        #endregion

        #endregion

        #region Events



        #endregion

    }
}
