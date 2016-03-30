
using Microsoft.Expression.Encoder.Profiles;
using System;
using System.Collections.Generic;
using TagLib;
namespace SmartPresenter.Common.Media
{
    /// <summary>
    /// A class to hold audio properties of a media files.
    /// </summary>
    public class AudioProperties
    {
        #region Private Data Members



        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioProperties"/> class.
        /// </summary>
        public AudioProperties()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioProperties"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public AudioProperties(string fileName)
        {
            FileName = fileName;
            Initialize();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; internal set; }

        /// <summary>
        /// Gets the bits per sample.
        /// </summary>
        /// <value>
        /// The bits per sample.
        /// </value>
        public int BitsPerSample { get; internal set; }

        /// <summary>
        /// Gets the channels.
        /// </summary>
        /// <value>
        /// The channels.
        /// </value>
        public int Channels { get; internal set; }

        /// <summary>
        /// Gets the duration.
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        public TimeSpan Duration { get; internal set; }

        /// <summary>
        /// Gets the size of the sample.
        /// </summary>
        /// <value>
        /// The size of the sample.
        /// </value>
        public int SampleSize { get; internal set; }

        /// <summary>
        /// Gets the name of the stream.
        /// </summary>
        /// <value>
        /// The name of the stream.
        /// </value>
        public string StreamName { get; internal set; }

        /// <summary>
        /// Gets the bitrate.
        /// </summary>
        /// <value>
        /// The bitrate.
        /// </value>
        public Bitrate Bitrate { get; internal set; }

        /// <summary>
        /// Gets the codec.
        /// </summary>
        /// <value>
        /// The codec.
        /// </value>
        public AudioCodec Codec { get; internal set; }

        /// <summary>
        /// Gets the album.
        /// </summary>
        /// <value>
        /// The album.
        /// </value>
        public string Album { get; internal set; }

        /// <summary>
        /// Gets the album artists.
        /// </summary>
        /// <value>
        /// The album artists.
        /// </value>
        public List<string> AlbumArtists { get; internal set; }

        /// <summary>
        /// Gets the composers.
        /// </summary>
        /// <value>
        /// The composers.
        /// </value>
        public List<string> Composers { get; internal set; }

        /// <summary>
        /// Gets the genres.
        /// </summary>
        /// <value>
        /// The genres.
        /// </value>
        public List<string> Genres { get; internal set; }

        /// <summary>
        /// Gets the performers.
        /// </summary>
        /// <value>
        /// The performers.
        /// </value>
        public List<string> Performers { get; internal set; }

        /// <summary>
        /// Gets the pictures.
        /// </summary>
        /// <value>
        /// The pictures.
        /// </value>
        public List<IPicture> Pictures { get; internal set; }

        /// <summary>
        /// Gets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value>
        public string Comment { get; internal set; }

        /// <summary>
        /// Gets the lyrics.
        /// </summary>
        /// <value>
        /// The lyrics.
        /// </value>
        public string Lyrics { get; internal set; }

        /// <summary>
        /// Gets the beats per minute.
        /// </summary>
        /// <value>
        /// The beats per minute.
        /// </value>
        public uint BeatsPerMinute { get; internal set; }

        /// <summary>
        /// Gets the track.
        /// </summary>
        /// <value>
        /// The track.
        /// </value>
        public uint Track { get; internal set; }

        /// <summary>
        /// Gets the track count.
        /// </summary>
        /// <value>
        /// The track count.
        /// </value>
        public uint TrackCount { get; internal set; }

        /// <summary>
        /// Gets the year.
        /// </summary>
        /// <value>
        /// The year.
        /// </value>
        public uint Year { get; internal set; }

        #endregion

        #region Methods

        #region Private Methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            Microsoft.Expression.Encoder.MediaItem mediaItem = new Microsoft.Expression.Encoder.MediaItem(FileName);
            TagLib.File fileTagInfo = TagLib.File.Create(FileName);

            BitsPerSample = mediaItem.SourceAudioProfile.BitsPerSample;
            Channels = mediaItem.SourceAudioProfile.Channels;
            Duration = mediaItem.MainMediaFile.AudioStreams[0].Duration;
            SampleSize = mediaItem.MainMediaFile.AudioStreams[0].SampleSize;
            StreamName = mediaItem.MainMediaFile.AudioStreams[0].StreamName;
            Bitrate = mediaItem.SourceAudioProfile.Bitrate;
            Codec = mediaItem.SourceAudioProfile.Codec;
            Album = fileTagInfo.Tag.Album;
        }

        #endregion

        #region Public Methods



        #endregion


        #endregion
    }
}
