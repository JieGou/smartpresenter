using SmartPresenter.BO.Common.Enums;
using SmartPresenter.Common.Interfaces;
using System;

namespace SmartPresenter.BO.Common.Interfaces
{
    /// <summary>
    /// Interface for objects that support media playback.
    /// </summary>
    public interface IMedia : IEntity
    {

        #region Properties

        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        /// <value>
        /// The volume.
        /// </value>
        int Volume { get; set; }

        /// <summary>
        /// Gets the duration.
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        TimeSpan Duration { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is playing.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is playing; otherwise, <c>false</c>.
        /// </value>
        bool IsPlaying { get; }

        /// <summary>
        /// Gets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        MediaState State { get; }

        /// <summary>
        /// Gets the current position.
        /// </summary>
        /// <value>
        /// The current position.
        /// </value>
        TimeSpan CurrentPosition { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is muted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is muted; otherwise, <c>false</c>.
        /// </value>
        bool IsMuted { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Plays this media.
        /// </summary>
        void Play();
        /// <summary>
        /// Pauses this media.
        /// </summary>
        void Pause();
        /// <summary>
        /// Stops this media.
        /// </summary>
        void Stop();
        /// <summary>
        /// Resumes this media.
        /// </summary>
        void Resume();
        /// <summary>
        /// Forwards this media.
        /// </summary>
        void Forward();
        /// <summary>
        /// Reverses this media.
        /// </summary>
        void Reverse();
        /// <summary>
        /// Seeks to the specified position.
        /// </summary>
        /// <param name="position">The position.</param>
        void Seek(TimeSpan position);
        /// <summary>
        /// Mutes this media.
        /// </summary>
        void Mute();
        /// <summary>
        /// Unmute this media.
        /// </summary>
        void UnMute();
        /// <summary>
        /// Volumes up.
        /// </summary>
        void VolumeUp();
        /// <summary>
        /// Volumes down.
        /// </summary>
        void VolumeDown();

        #endregion
    }
}
