using Microsoft.Expression.Encoder;
using SmartPresenter.BO.Common.Enums;
using SmartPresenter.BO.Common.Interfaces;
using SmartPresenter.Common;
using SmartPresenter.Common.Enums;
using SmartPresenter.Common.Extensions;
using SmartPresenter.Common.Logger;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Xml;

namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// class representing a video object.
    /// </summary>
    [Serializable]
    public class Video : Image, IMedia, IFileSource, INotifyPropertyChanged
    {
        #region Private Data Members

        private string _thumbnailPath;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Video"/> class.
        /// </summary>
        public Video()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Video"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public Video(string path)
            : base(path)
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    string thumbnailPath = System.IO.Path.Combine(ApplicationSettings.Instance.ActiveUserAccount.ActiveProfile.MediaThumbnailsPath, System.IO.Path.GetFileNameWithoutExtension(this.Path) + ".jpg");

                    if (MediaHelper.IsValidVideoFile(this.Path))
                    {
                        AudioVideoFile avFile = new AudioVideoFile(this.Path);
                        Bitmap bitmap = avFile.GetThumbnail(TimeSpan.FromSeconds(5), avFile.VideoStreams[0].VideoSize);

                        MediaHelper.SaveBitmapToFile(bitmap, thumbnailPath);
                        this.ThumbnailPath = thumbnailPath;
                    }
                }
                catch(Exception e)
                {
                    Logger.LogMsg.Error(string.Format("Could not create thumbnail for file {0}", path), e);
                }
            });
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the frames per second.
        /// </summary>
        /// <value>
        /// The frames per second.
        /// </value>
        public int FramesPerSecond { get; set; }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public override ElementType Type
        {
            get { return ElementType.Video; }
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
                return _thumbnailPath;
            }
            set
            {
                if (File.Exists(value) == false)
                {
                    //throw new FileNotFoundException("Specified media file does not exist", value);
                }
                _thumbnailPath = value;
                OnPropertyChanged("ThumbnailPath");
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Renders this instance.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void Render()
        {
            throw new NotImplementedException();
        }        

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region IMedia Members

        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        /// <value>
        /// The volume.
        /// </value>
        public int Volume { get; set; }

        /// <summary>
        /// Gets the duration.
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        public TimeSpan Duration { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is playing.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is playing; otherwise, <c>false</c>.
        /// </value>
        public bool IsPlaying { get; private set; }

        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public MediaState State { get; private set; }

        /// <summary>
        /// Gets the current position.
        /// </summary>
        /// <value>
        /// The current position.
        /// </value>
        public TimeSpan CurrentPosition { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this instance is muted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is muted; otherwise, <c>false</c>.
        /// </value>
        public bool IsMuted { get; private set; }

        /// <summary>
        /// Plays this media.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Play()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Pauses this media.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Pause()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Stops this media.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Stop()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Resumes this media.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Resume()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Forwards this media.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Forward()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reverses this media.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Reverse()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Seeks to the specified position.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Seek(TimeSpan position)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Mutes this media.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Mute()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Unmute this media.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void UnMute()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Volumes up.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void VolumeUp()
        {
            Volume += Constants.Default_Volume_Change;
        }

        /// <summary>
        /// Volumes down.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void VolumeDown()
        {
            Volume -= Constants.Default_Volume_Change;
        }

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

    }
}
