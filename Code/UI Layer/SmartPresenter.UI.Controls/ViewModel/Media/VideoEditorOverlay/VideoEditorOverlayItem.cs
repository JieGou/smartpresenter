
using Microsoft.Practices.Prism.Mvvm;
using SmartPresenter.UI.Common;
using System;
using System.Globalization;
namespace SmartPresenter.UI.Controls.ViewModel.Media
{
    public abstract class VideoEditorOverlayTrackItem : BindableBase
    {
        #region Private Data Members

        private int _x;
        private int _width;
        private TimeSpan _startTime;
        private TimeSpan _endTime;
        private TimeSpanToDoubleConverter _converter = new TimeSpanToDoubleConverter();

        #endregion

        #region Constructor



        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        /// <value>
        /// The x.
        /// </value>
        public int X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
                OnPropertyChanged("X");
                StartTime = (TimeSpan)_converter.Convert(value, typeof(double), null, CultureInfo.CurrentCulture);
            }
        }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public int Width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
                OnPropertyChanged("Width");
            }
        }

        /// <summary>
        /// Gets or sets the start time.
        /// </summary>
        /// <value>
        /// The start time.
        /// </value>
        public TimeSpan StartTime
        {
            get
            {
                return _startTime;
            }
            set
            {
                _startTime = value;
                OnPropertyChanged("StartTime");
            }
        }

        /// <summary>
        /// Gets or sets the end time.
        /// </summary>
        /// <value>
        /// The end time.
        /// </value>
        public TimeSpan EndTime
        {
            get
            {
                return _endTime;
            }
            set
            {
                _endTime = value;
                OnPropertyChanged("EndTime");
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
