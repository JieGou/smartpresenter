using SmartPresenter.BO.Common.Entities;
using SmartPresenter.BO.Common.Enums;
using SmartPresenter.Common.Enums;
using System;
using System.ComponentModel;

namespace SmartPresenter.UI.Controls.ViewModel
{
    /// <summary>
    /// A class for Audio object UI.
    /// </summary>
    [DisplayName("Audio")]
    public class AudioView : ShapeView
    {
        #region Private Data Members

        private Audio _audio;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioView"/> class.
        /// </summary>
        /// <param name="audio">The audio.</param>
        public AudioView(Audio audio)
            : base(audio)
        {
            _audio = audio;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioView"/> class.
        /// </summary>
        /// <param name="audio">The audio.</param>
        public AudioView(Shape audio)
            : base(audio)
        {
            _audio = (Audio)audio;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public override ElementType Type
        {
            get
            {
                return ElementType.Audio;
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
                return _audio.Volume;
            }
            set
            {
                _audio.Volume = value;
                OnPropertyChanged("Volume");
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
                return _audio.Duration;
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
                return _audio.IsPlaying;
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
                return _audio.State;
            }
        }

        /// <summary>
        /// Gets the current position.
        /// </summary>
        /// <value>
        /// The current position.
        /// </value>
        [Category("Media")]
        public TimeSpan CurrentPosition
        {
            get
            {
                return _audio.CurrentPosition;
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
                return _audio.IsMuted;
            }
            set
            {
            }
        }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        [Category("File")]
        public string Path
        {
            get
            {
                return _audio.Path;
            }
            set
            {
                _audio.Path = value;
                OnPropertyChanged("Path");
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
            return (Audio)_audio.Clone();
        }

        /// <summary>
        /// Clones the internal.
        /// </summary>
        /// <returns></returns>
        protected override object CloneInternal()
        {
            ShapeView elementView = new AudioView(this.GetInnerObjectCloned());

            return elementView;
        }

        #endregion

    }
}
