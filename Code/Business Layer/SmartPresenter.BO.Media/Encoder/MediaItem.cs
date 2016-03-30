
using SmartPresenter.Common.Enums;
using SmartPresenter.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using SmartPresenter.Common.Media;

namespace SmartPresenter.BO.Media.Encoder
{
    /// <summary>
    /// A class for media item that can be encoded.
    /// </summary>
    public class MediaItem
    {
        #region Constants

        private const string FFMPEG = "ffmpeg.exe";
        public const string Input_File = " -i \"{0}\"";
        public const string Output_File = " \"{0}\"";
        public const string Codec_Copy_Video = " -c:v copy";
        public const string Codec_Copy_Audio = " -c:a copy";
        public const string Start_Time = " -ss {0}:{1}:{2}.{3}";
        public const string End_Time = " -t {0}:{1}:{2}.{3}";
        public const string Audio_Bitrate = " -ab {0}k";
        public const string Volume = " -af \"volume={0}\"";
        public const string Crop = " -filter:v \"crop={0}:{1}:{2}:{3}\"";
        public const string SpeedRatio = " -filter:v \"setpts={0}*PTS\"";
        public const string Crop_And_SpeedRatio = " -filter:v \"crop={0}:{1}:{2}:{3}\" \"setpts={4}*PTS\"";
        public const string Quality = " -qscale {0}";

        public const double Min_Volume_Percentage = -10.0;
        public const double Max_Volume_Percentage = 10.0;

        private string Title_String = "Title";
        private string Description_String = "Description";
        private string Copyright_String = "Copyright";

        #endregion

        #region Private Data Members

        Microsoft.Expression.Encoder.MediaItem _mediaItem;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaItem"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public MediaItem(string fileName)
        {
            FileName = fileName;
            Initialize();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the source audio profile.
        /// </summary>
        /// <value>
        /// The source audio profile.
        /// </value>
        public AudioProfile SourceAudioProfile { get; private set; }

        /// <summary>
        /// Gets or sets the target audio profile.
        /// </summary>
        /// <value>
        /// The target audio profile.
        /// </value>
        public AudioProfile TargetAudioProfile { get; set; }

        /// <summary>
        /// Gets the source video profile.
        /// </summary>
        /// <value>
        /// The source video profile.
        /// </value>
        public VideoProfile SourceVideoProfile { get; private set; }

        /// <summary>
        /// Gets or sets the target video profile.
        /// </summary>
        /// <value>
        /// The target video profile.
        /// </value>
        public VideoProfile TargetVideoProfile { get; set; }

        /// <summary>
        /// Gets or sets the markers.
        /// </summary>
        /// <value>
        /// The markers.
        /// </value>
        public List<Marker> Markers { get; set; }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// Gets or sets the output path.
        /// </summary>
        /// <value>
        /// The output path.
        /// </value>
        public string OutputPath { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the copyright.
        /// </summary>
        /// <value>
        /// The copyright.
        /// </value>
        public string Copyright { get; set; }

        #endregion

        #region Methods

        #region Private Methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            _mediaItem = new Microsoft.Expression.Encoder.MediaItem(FileName);
            TagLib.File fileTagInfo = TagLib.File.Create(FileName);
            // Read source audio profile.
            SourceAudioProfile = new AudioProfile();
            TargetAudioProfile = new AudioProfile();
            if (_mediaItem.SourceAudioProfile != null)
            {
                SourceAudioProfile.BitsPerSample = _mediaItem.SourceAudioProfile.BitsPerSample;
                SourceAudioProfile.Channels = _mediaItem.SourceAudioProfile.Channels;

                TargetAudioProfile.BitsPerSample = _mediaItem.SourceAudioProfile.BitsPerSample;
                TargetAudioProfile.Channels = _mediaItem.SourceAudioProfile.Channels;
            }
            SourceAudioProfile.BitRate = fileTagInfo.Properties.AudioBitrate;
            TargetAudioProfile.BitRate = fileTagInfo.Properties.AudioBitrate;

            // Read source video profile.
            SourceVideoProfile = new VideoProfile();
            TargetVideoProfile = new VideoProfile();
            if (_mediaItem.SourceVideoProfile != null)
            {
                SourceVideoProfile.AspectRatio = _mediaItem.SourceVideoProfile.ActualAspectRatio;
                SourceVideoProfile.AutoFit = _mediaItem.SourceVideoProfile.AutoFit;
                SourceVideoProfile.VideoSize = _mediaItem.SourceVideoProfile.Size;
                SourceVideoProfile.CropRect = new CropRect(_mediaItem.CropRect.X, _mediaItem.CropRect.Y, _mediaItem.CropRect.Width, _mediaItem.CropRect.Height);

                TargetVideoProfile.AspectRatio = _mediaItem.SourceVideoProfile.ActualAspectRatio;
                TargetVideoProfile.AutoFit = _mediaItem.SourceVideoProfile.AutoFit;
                TargetVideoProfile.VideoSize = _mediaItem.SourceVideoProfile.Size;
                TargetVideoProfile.CropRect = new CropRect(_mediaItem.CropRect.X, _mediaItem.CropRect.Y, _mediaItem.CropRect.Width, _mediaItem.CropRect.Height);
            }
            SourceVideoProfile.FrameRate = _mediaItem.OriginalFrameRate;
            TargetVideoProfile.FrameRate = _mediaItem.OriginalFrameRate;
            SourceVideoProfile.VideoFormat = FileFormatHelper.GetVideoFormat(_mediaItem.SourceFileName);
            TargetVideoProfile.VideoFormat = FileFormatHelper.GetVideoFormat(_mediaItem.SourceFileName);

            Duration = _mediaItem.FileDuration;

            Title = _mediaItem.Metadata[Title_String];
            Description = _mediaItem.Metadata[Description_String];
            Copyright = _mediaItem.Metadata[Copyright_String];

            Markers = new List<Marker>();

            OutputPath = @"B:\Video\";
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Encodes this job.
        /// </summary>
        public void Encode()
        {
            if (MediaHelper.IsValidVideoFile(FileName) || MediaHelper.IsValidAudioFile(FileName))
            {
                string outputFileName = Path.GetFileName(this.FileName).Insert(Path.GetFileName(this.FileName).LastIndexOf("."), "_temp");
                string extension = FileFormatHelper.GetExtensionForVideoFormat(this.TargetVideoProfile.VideoFormat);
                outputFileName = outputFileName.Substring(0, outputFileName.LastIndexOf(".")) + extension;

                double speedRatio = TargetVideoProfile.FrameRate / SourceVideoProfile.FrameRate;

                string command = string.Format(Input_File, this.FileName);
                if (SourceVideoProfile.CropRect.Equals(TargetVideoProfile.CropRect) == false && (int)SourceVideoProfile.FrameRate != (int)TargetVideoProfile.FrameRate)
                {
                    command = command + string.Format(Crop_And_SpeedRatio, TargetVideoProfile.CropRect.Width, TargetVideoProfile.CropRect.Height, TargetVideoProfile.CropRect.X, TargetVideoProfile.CropRect.Y, speedRatio);
                }
                else if (SourceVideoProfile.CropRect.Equals(TargetVideoProfile.CropRect) == false)
                {
                    command = command + string.Format(Crop, TargetVideoProfile.CropRect.Width, TargetVideoProfile.CropRect.Height, TargetVideoProfile.CropRect.X, TargetVideoProfile.CropRect.Y);
                }
                else if ((int)SourceVideoProfile.FrameRate != (int)TargetVideoProfile.FrameRate)
                {
                    command = command + string.Format(SpeedRatio, speedRatio);
                }

                if (TargetAudioProfile.AudioGainLevel >= -5 && TargetAudioProfile.AudioGainLevel <= 5 && TargetAudioProfile.AudioGainLevel != 0)
                {
                    command = command + string.Format(Volume, TargetAudioProfile.AudioGainLevel);
                }

                if (TargetAudioProfile.BitRate != SourceAudioProfile.BitRate)
                {
                    command = command + string.Format(Audio_Bitrate, TargetAudioProfile.BitRate);
                }

                command = command + string.Format(Quality, 0);

                command = command + string.Format(Output_File, OutputPath + outputFileName);

                RunProcess(command);
            }
        }

        /// <summary>
        /// Clips the video clip by specified start time and end time.
        /// </summary>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        public void Clip(TimeSpan startTime, TimeSpan endTime)
        {
            if (MediaHelper.IsValidVideoFile(FileName) || MediaHelper.IsValidAudioFile(FileName))
            {
                string outputFileName = Path.GetFileName(this.FileName).Insert(Path.GetFileName(this.FileName).LastIndexOf("."), "_temp");
                string cutVideoCommand = string.Format(Start_Time, startTime.Hours, startTime.Minutes, startTime.Seconds, startTime.Milliseconds) + string.Format(Input_File, this.FileName) + Codec_Copy_Video + Codec_Copy_Audio + string.Format(End_Time, endTime.Hours, endTime.Minutes, endTime.Seconds, endTime.Milliseconds) + string.Format(Output_File, OutputPath + outputFileName);

                RunProcess(cutVideoCommand);
            }
        }

        /// <summary>
        /// Converts the specified source format.
        /// </summary>
        /// <param name="sourceFormat">The source format.</param>
        /// <param name="tragetFormat">The traget format.</param>
        public void ConvertVideoFormat(VideoFormat targetVideoFormat)
        {
            if (MediaHelper.IsValidVideoFile(FileName))
            {
                string outputFileName = Path.GetFileName(this.FileName).Insert(Path.GetFileName(this.FileName).LastIndexOf("."), "_temp");
                outputFileName = outputFileName.Substring(0, outputFileName.LastIndexOf(".")) + FileFormatHelper.GetExtensionForVideoFormat(targetVideoFormat);
                string convertVideoFormatCommand = string.Format(string.Format(Input_File, this.FileName)) + Codec_Copy_Video + Codec_Copy_Audio + string.Format(Output_File, OutputPath + outputFileName);

                RunProcess(convertVideoFormatCommand);
            }
        }

        /// <summary>
        /// Converts the audio format.
        /// </summary>
        /// <param name="targetAudioFormat">The target audio format.</param>
        public void ConvertAudioFormat(AudioFormat targetAudioFormat)
        {
            if (MediaHelper.IsValidAudioFile(FileName))
            {
                string outputFileName = Path.GetFileName(this.FileName).Insert(Path.GetFileName(this.FileName).LastIndexOf("."), "_temp");
                outputFileName = outputFileName.Substring(0, outputFileName.LastIndexOf(".")) + FileFormatHelper.GetExtensionForAudioFormat(targetAudioFormat);
                string convertAudioCommand = string.Format(string.Format(Input_File, this.FileName)) + string.Format(Audio_Bitrate, this.SourceAudioProfile.BitRate) + string.Format(Output_File, OutputPath + outputFileName);

                RunProcess(convertAudioCommand);
            }
        }

        /// <summary>
        /// Changes the volume level.
        /// </summary>
        /// <param name="percentageChange">The percentage change.</param>
        public void ChangeVolumeLevel(double percentageChange)
        {
            if ((MediaHelper.IsValidAudioFile(FileName) || MediaHelper.IsValidVideoFile(FileName)) && percentageChange >= Min_Volume_Percentage && percentageChange <= Max_Volume_Percentage)
            {
                string outputFileName = Path.GetFileName(this.FileName).Insert(Path.GetFileName(this.FileName).LastIndexOf("."), "_temp");
                string changeVolumeCommand = string.Format(string.Format(Input_File, this.FileName)) + string.Format(Volume, percentageChange) + string.Format(Output_File, OutputPath + outputFileName);

                RunProcess(changeVolumeCommand);
            }
        }

        /// <summary>
        /// Crops the video.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="top">The top.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public void CropVideo(int left, int top, int width, int height)
        {
            if (MediaHelper.IsValidVideoFile(FileName))
            {
                string outputFileName = Path.GetFileName(this.FileName).Insert(Path.GetFileName(this.FileName).LastIndexOf("."), "_temp");
                string cropVideoCommand = string.Format(string.Format(Input_File, this.FileName)) + string.Format(Crop, width, height, left, top) + Codec_Copy_Video + Codec_Copy_Audio + string.Format(Output_File, OutputPath + outputFileName);

                RunProcess(cropVideoCommand);
            }
        }

        /// <summary>
        /// Runs the process.
        /// </summary>
        /// <param name="convertAudioCommand">The convert audio command.</param>
        private static void RunProcess(string convertAudioCommand)
        {
            ProcessStartInfo info = new ProcessStartInfo(Directory.GetCurrentDirectory() + @"\" + FFMPEG, convertAudioCommand);
            Process encodingProcess = Process.Start(info);
            encodingProcess.WaitForExit();
        }

        #endregion

        #endregion
    }

    public class CropRect
    {
        public CropRect(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public override bool Equals(object obj)
        {
            CropRect cropRect = obj as CropRect;
            return X == cropRect.X && Y == cropRect.Y && Width == cropRect.Width && Height == cropRect.Height;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
