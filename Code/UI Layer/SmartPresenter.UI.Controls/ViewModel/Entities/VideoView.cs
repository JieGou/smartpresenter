using Microsoft.Practices.ServiceLocation;
using SmartPresenter.BO.Common.Entities;
using SmartPresenter.BO.Common.Enums;
using SmartPresenter.UI.Common.Interfaces;
using System;
using System.ComponentModel;

namespace SmartPresenter.UI.Controls.ViewModel
{
    /// <summary>
    /// A class for Video Object UI.
    /// </summary>
    [DisplayName("Video")]
    public class VideoView : MediaView
    {

        #region Private Data Members

        private Video _video;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoView"/> class.
        /// </summary>
        /// <param name="mediaItem">The media item.</param>
        public VideoView(Image mediaItem)
            : base(mediaItem)
        {
            _video = mediaItem as Video;
            _video.PropertyChanged += Video_PropertyChanged;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoView"/> class.
        /// </summary>
        /// <param name="mediaItem">The media item.</param>
        public VideoView(Shape mediaItem)
            : base(mediaItem)
        {
            _video = mediaItem as Video;
            _video.PropertyChanged += Video_PropertyChanged;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the UI interaction service.
        /// </summary>
        /// <value>
        /// The UI interaction service.
        /// </value>
        private IInteractionService UIInteractionService
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IInteractionService>();
            }
        }

        /// <summary>
        /// Gets or sets the frames per second.
        /// </summary>
        /// <value>
        /// The frames per second.
        /// </value>
        [DisplayName("Frames Per Second")]
        [Category("Media")]
        public int FramesPerSecond
        {
            get
            {
                return _video.FramesPerSecond;
            }
            set
            {
                _video.FramesPerSecond = value;
                OnPropertyChanged(() => this.FramesPerSecond);
            }
        }

        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        /// <value>
        /// The volume.
        /// </value>
        [Category("Media")]
        public int Volume
        {
            get
            {
                return _video.Volume;
            }
            set
            {
                _video.Volume = value;
                OnPropertyChanged(() => this.Volume);
            }
        }
        /// <summary>
        /// Gets the duration.
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        [Category("Media")]
        public TimeSpan Duration
        {
            get
            {
                return _video.Duration;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is playing.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is playing; otherwise, <c>false</c>.
        /// </value>
        [Category("Media")]
        public bool IsPlaying
        {
            get
            {
                return _video.IsPlaying;
            }
            set
            {
            }
        }

        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        [Category("Media")]
        public MediaState State
        {
            get
            {
                return _video.State;
            }
        }

        /// <summary>
        /// Gets the current position.
        /// </summary>
        /// <value>
        /// The current position.
        /// </value>
        [DisplayName("Current Position")]
        [Category("Media")]
        public TimeSpan CurrentPosition
        {
            get
            {
                return _video.CurrentPosition;
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is muted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is muted; otherwise, <c>false</c>.
        /// </value>
        [Category("Media")]
        public bool IsMuted
        {
            get
            {
                return _video.IsMuted;
            }
            set
            {
            }
        }

        /// <summary>
        /// Gets or sets the thumbnail path.
        /// </summary>
        /// <value>
        /// The thumbnail path.
        /// </value>
        public string ThumbnailPath
        {
            get
            {
                return _video.ThumbnailPath;
            }
            set
            {
                //if (_video.ThumbnailPath != value)
                {
                    _video.ThumbnailPath = value;
                    UIInteractionService.InvokeOnUIThread(() =>
                        {
                            OnPropertyChanged("ThumbnailPath");
                        });
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the inner object.
        /// </summary>
        /// <returns></returns>
        protected internal override Shape GetInnerObjectCloned()
        {
            return (Video)_video.Clone();
        }

        /// <summary>
        /// Clones the internal.
        /// </summary>
        /// <returns></returns>
        protected override object CloneInternal()
        {
            ShapeView elementView = new VideoView(this.GetInnerObjectCloned());

            return elementView;
        }

        /// <summary>
        /// Handles the PropertyChanged event of the Video control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void Video_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "ThumbnailPath":
                    UIInteractionService.InvokeOnUIThread(() =>
                    {
                        OnPropertyChanged("ThumbnailPath");
                    });
                    break;
            }
        }

        #endregion
    }
}
