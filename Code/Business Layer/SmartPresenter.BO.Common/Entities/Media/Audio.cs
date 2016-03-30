using SmartPresenter.BO.Common.Enums;
using SmartPresenter.BO.Common.Interfaces;
using SmartPresenter.Common;
using SmartPresenter.Common.Enums;
using SmartPresenter.Common.Extensions;
using System;
using System.IO;
using System.Xml;

namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// A class representing an audio object.
    /// </summary>
    [Serializable]
    public class Audio : Shape, IMedia, IFileSource
    {
        #region Private Data Members



        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Audio"/> class.
        /// </summary>
        public Audio() : this(string.Empty)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Audio"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public Audio(string path)
        {
            if(File.Exists(path) == false)
            {
                throw new FileNotFoundException("Specified Audio file does not exist", path);
            }
            this.Path = path;
            Initialize();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the path of Image.
        /// </summary>
        /// <value>
        /// The path of Image.
        /// </value>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public override ElementType Type
        {
            get { return ElementType.Audio; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Renders an object.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void Render()
        {
            throw new NotImplementedException();
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

        private void Initialize()
        {
            IsPlaying = IsMuted = true;
        }        

        #endregion

    }
}
