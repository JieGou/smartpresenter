
namespace SmartPresenter.UI.Controls.ViewModel.Media
{
    public class VideoEditorImageOverlayTrackItem : VideoEditorOverlayTrackItem
    {
        #region Private Data Members

        private string _path;

        #endregion

        #region Constructor



        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        public string Path
        {
            get
            {
                return _path;
            }
            set
            {
                _path = value;
                OnPropertyChanged("Path");
            }
        }

        #endregion

        #region Methods

        #region Private Methods



        #endregion

        #region Public Methods



        #endregion

        #endregion
    }
}
