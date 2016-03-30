using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;
using SmartPresenter.BO.Common.Enums;
using SmartPresenter.BO.Media.Encoder;
using SmartPresenter.BO.Media.Video;
using SmartPresenter.UI.Common;
using SmartPresenter.UI.Controls.Events;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SmartPresenter.UI.Controls.ViewModel
{
    /// <summary>
    /// View Model class for Video Editor.
    /// </summary>
    [Export]
    public class VideoEditorViewModel : BindableBase
    {
        #region Private Data Members

        /// <summary>
        /// The selected video
        /// </summary>
        private VideoView _selectedVideo;

        private DispatcherTimer _playbackTimer = new DispatcherTimer(DispatcherPriority.Render);

        private int _playbackProgress;

        private MediaElement _mediaElement;

        private PlaybackState _playbackState;

        private double _volume;

        private ObservableCollection<BookMark> _bookMarks = new ObservableCollection<BookMark>();

        private MediaItem _mediaItem;

        private AudioChannel _audioChannelForEncoding;

        private ObservableCollection<ShapeView> _overlays;

        private ShapeView _selectedOverlay;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoEditorViewModel"/> class.
        /// </summary>
        public VideoEditorViewModel()
        {
            Initialize();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the selected video.
        /// </summary>
        /// <value>
        /// The selected video.
        /// </value>
        public VideoView SelectedVideo
        {
            get
            {
                return _selectedVideo;
            }
            set
            {
                _selectedVideo = value;
                OnPropertyChanged("SelectedVideo");
                Task.Factory.StartNew(() => { MediaItem = new MediaItem(value.Path); });
            }
        }

        /// <summary>
        /// Gets or sets the media item.
        /// </summary>
        /// <value>
        /// The media item.
        /// </value>
        public MediaItem MediaItem
        {
            get
            {
                return _mediaItem;
            }
            set
            {
                _mediaItem = value;
                OnPropertyChanged("DisplayName");
                OnPropertyChanged("FileSize");
                OnPropertyChanged("FrameRate");
                OnPropertyChanged("AspectRatio");
                OnPropertyChanged("Duration");
                OnPropertyChanged("VideoSize");
                OnPropertyChanged("FileExtension");
                OnPropertyChanged("Title");
                OnPropertyChanged("Description");
                OnPropertyChanged("Copyright");
            }
        }

        /// <summary>
        /// Gets or sets the media element.
        /// </summary>
        /// <value>
        /// The media element.
        /// </value>
        public MediaElement MediaElement
        {
            get
            {
                return _mediaElement;
            }
            set
            {
                if (_mediaElement != null)
                {
                    _mediaElement.MediaEnded -= MediaElement_MediaEnded;
                    _mediaElement.MediaOpened -= MediaElement_MediaOpened;
                }
                _mediaElement = value;
                if (_mediaElement != null)
                {
                    _mediaElement.MediaEnded += MediaElement_MediaEnded;
                    _mediaElement.MediaOpened += MediaElement_MediaOpened;
                    _mediaElement.Volume = Volume;
                }
                OnPropertyChanged("MediaElement");
                OnPropertyChanged("PlaybackDuration");
                OnPropertyChanged("Volume");
            }
        }

        /// <summary>
        /// Gets or sets the progress slider.
        /// </summary>
        /// <value>
        /// The progress slider.
        /// </value>
        public Slider ProgressSlider { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is playing.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is playing; otherwise, <c>false</c>.
        /// </value>
        public PlaybackState PlaybackState
        {
            get
            {
                return _playbackState;
            }
            set
            {
                _playbackState = value;
                OnPropertyChanged("PlaybackState");
            }
        }

        /// <summary>
        /// Gets or sets the playback progress.
        /// </summary>
        /// <value>
        /// The playback progress.
        /// </value>
        public int PlaybackProgress
        {
            get
            {
                return _playbackProgress;
            }
            set
            {
                _playbackProgress = value;
                OnPropertyChanged("PlaybackProgress");
            }
        }

        /// <summary>
        /// Gets or sets the duration of the playback.
        /// </summary>
        /// <value>
        /// The duration of the playback.
        /// </value>
        public int PlaybackDuration
        {
            get
            {
                if (_mediaElement != null && _mediaElement.NaturalDuration.HasTimeSpan)
                {
                    return (int)_mediaElement.NaturalDuration.TimeSpan.TotalSeconds;
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                if (_mediaElement != null)
                {
                    _mediaElement.Position = TimeSpan.FromSeconds(value);
                }
            }
        }

        public ObservableCollection<BookMark> BookMarks
        {
            get
            {
                return _bookMarks;
            }
            set
            {
                _bookMarks = value;
                OnPropertyChanged("BookMarks");
            }
        }

        /// <summary>
        /// Gets or sets the volume.
        /// </summary>
        /// <value>
        /// The volume.
        /// </value>
        public double Volume
        {
            get
            {
                return _volume;
            }
            set
            {
                if (value < 0.0)
                {
                    _volume = 0.0;
                }
                else if (value > 1.0)
                {
                    _volume = 1.0;
                }
                else
                {
                    _volume = value;
                }
                if (MediaElement != null)
                {
                    MediaElement.Volume = _volume;
                }
                OnPropertyChanged("Volume");
            }
        }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName
        {
            get
            {
                if (_mediaItem != null)
                {
                    return _mediaItem.FileName;
                }
                return "N/A";
            }
            set { }
        }

        /// <summary>
        /// Gets the size of the file.
        /// </summary>
        /// <value>
        /// The size of the file.
        /// </value>
        public string FileSize
        {
            get
            {
                FileInfo fileInfo = new FileInfo(SelectedVideo.Path);
                if (fileInfo.Length < 1024)
                {
                    return string.Format(Thread.CurrentThread.CurrentUICulture, "{0} B", fileInfo.Length);
                }
                else if (fileInfo.Length < (1024 * 1024))
                {
                    return string.Format(Thread.CurrentThread.CurrentUICulture, "{0} KB", fileInfo.Length / 1024);
                }
                else if (fileInfo.Length < (1024 * 1024 * 1024))
                {
                    return string.Format(Thread.CurrentThread.CurrentUICulture, "{0} MB", fileInfo.Length / (1024 * 1024));
                }
                return "N/A";
            }
        }

        /// <summary>
        /// Gets the frame rate.
        /// </summary>
        /// <value>
        /// The frame rate.
        /// </value>
        public string FrameRate
        {
            get
            {
                if (_mediaItem != null)
                {
                    return string.Format("{0} FPS", _mediaItem.SourceVideoProfile.FrameRate.ToString("00.00"));
                }
                return "N/A";
            }
        }

        /// <summary>
        /// Gets the aspect ratio.
        /// </summary>
        /// <value>
        /// The aspect ratio.
        /// </value>
        public string AspectRatio
        {
            get
            {
                if (_mediaItem != null)
                {
                    return string.Format("{0} : {1}", _mediaItem.SourceVideoProfile.AspectRatio.Width, _mediaItem.SourceVideoProfile.AspectRatio.Height);
                }
                return "N/A";
            }
        }

        /// <summary>
        /// Gets the duration.
        /// </summary>
        /// <value>
        /// The duration.
        /// </value>
        public string Duration
        {
            get
            {
                if (_mediaItem != null)
                {
                    return string.Format("{0}:{1}:{2} hrs", _mediaItem.Duration.Hours.ToString("00"), _mediaItem.Duration.Minutes.ToString("00"), _mediaItem.Duration.Seconds.ToString("00"));
                }
                return "N/A";
            }
        }

        /// <summary>
        /// Gets the size of the video.
        /// </summary>
        /// <value>
        /// The size of the video.
        /// </value>
        public string VideoSize
        {
            get
            {
                if (_mediaItem != null)
                {
                    return string.Format("{0} x {1}", _mediaItem.SourceVideoProfile.VideoSize.Width, _mediaItem.SourceVideoProfile.VideoSize.Height);
                }
                return "N/A";
            }
        }

        /// <summary>
        /// Gets the file extension.
        /// </summary>
        /// <value>
        /// The file extension.
        /// </value>
        public string FileExtension
        {
            get
            {
                if (_mediaItem != null)
                {
                    return Path.GetExtension(SelectedVideo.FileName);
                }
                return "N/A";
            }
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title
        {
            get
            {
                if (_mediaItem != null && string.IsNullOrEmpty(_mediaItem.Copyright) == false)
                {
                    return _mediaItem.Title;
                }
                return "N/A";
            }
        }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description
        {
            get
            {
                if (_mediaItem != null && string.IsNullOrEmpty(_mediaItem.Copyright) == false)
                {
                    return _mediaItem.Description;
                }
                return "N/A";
            }
        }

        /// <summary>
        /// Gets or sets the copyright.
        /// </summary>
        /// <value>
        /// The copyright.
        /// </value>
        public string Copyright
        {
            get
            {
                if (_mediaItem != null && string.IsNullOrEmpty(_mediaItem.Copyright) == false)
                {
                    return _mediaItem.Copyright;
                }
                return "N/A";
            }
        }

        /// <summary>
        /// Gets or sets the audio channel for encoding.
        /// </summary>
        /// <value>
        /// The audio channel for encoding.
        /// </value>
        public AudioChannel AudioChannelForEncoding
        {
            get
            {
                return _audioChannelForEncoding;
            }
            set
            {
                _audioChannelForEncoding = value;
                OnPropertyChanged("AudioChannelForEncoding");
            }
        }

        /// <summary>
        /// Gets or sets the overlays.
        /// </summary>
        /// <value>
        /// The overlays.
        /// </value>
        public ObservableCollection<ShapeView> Overlays
        {
            get
            {
                return _overlays;
            }
            set
            {
                _overlays = value;
                OnPropertyChanged("Overlays");
            }
        }

        /// <summary>
        /// Gets or sets the selected overlay.
        /// </summary>
        /// <value>
        /// The selected overlay.
        /// </value>
        public ShapeView SelectedOverlay
        {
            get
            {
                return _selectedOverlay;
            }
            set
            {
                if (_selectedOverlay != value)
                {
                    try
                    {
                        OnPropertyChanged("SelectedOverlay");
                        UpdateAdornerOnSelectedElement(value);
                    }
                    finally
                    {
                        _selectedOverlay = value;
                        OnPropertyChanged("SelectedOverlay");
                    }
                }
            }
        }

        /// <summary>
        /// Gets the event aggregator.
        /// </summary>
        /// <value>
        /// The event aggregator.
        /// </value>
        private IEventAggregator EventAggregator
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IEventAggregator>();
            }
        }

        /// <summary>
        /// Gets or sets the add adorner to item.
        /// </summary>
        /// <value>
        /// The add adorner to item.
        /// </value>
        public Action<int> AddAdornerToItem { get; set; }

        /// <summary>
        /// Gets or sets the remove adorner from item.
        /// </summary>
        /// <value>
        /// The remove adorner from item.
        /// </value>
        public Action<int> RemoveAdornerFromItem { get; set; }

        /// <summary>
        /// Gets or sets the remove adorner from all items.
        /// </summary>
        /// <value>
        /// The remove adorner from all items.
        /// </value>
        public Action RemoveAdornerFromAllItems { get; set; }

        /// <summary>
        /// Gets or sets the editing canvas.
        /// </summary>
        /// <value>
        /// The editing canvas.
        /// </value>
        public Canvas EditingCanvas { get; set; }

        #endregion

        #region Commands

        /// <summary>
        /// Gets the play pause command.
        /// </summary>
        /// <value>
        /// The play pause command.
        /// </value>
        public DelegateCommand PlayPauseCommand { get; private set; }

        /// <summary>
        /// Gets the go to begin command.
        /// </summary>
        /// <value>
        /// The go to begin command.
        /// </value>
        public DelegateCommand GoToBeginCommand { get; private set; }

        /// <summary>
        /// Gets the rewind command.
        /// </summary>
        /// <value>
        /// The rewind command.
        /// </value>
        public DelegateCommand RewindCommand { get; private set; }

        /// <summary>
        /// Gets the forward command.
        /// </summary>
        /// <value>
        /// The forward command.
        /// </value>
        public DelegateCommand ForwardCommand { get; private set; }

        /// <summary>
        /// Gets the go to end command.
        /// </summary>
        /// <value>
        /// The go to end command.
        /// </value>
        public DelegateCommand GoToEndCommand { get; private set; }

        /// <summary>
        /// Gets the mark begin command.
        /// </summary>
        /// <value>
        /// The mark begin command.
        /// </value>
        public DelegateCommand MarkBeginCommand { get; private set; }

        /// <summary>
        /// Gets the mark end command.
        /// </summary>
        /// <value>
        /// The mark end command.
        /// </value>
        public DelegateCommand MarkEndCommand { get; private set; }

        /// <summary>
        /// Gets the seek command.
        /// </summary>
        /// <value>
        /// The seek command.
        /// </value>
        public DelegateCommand SeekCommand { get; private set; }

        /// <summary>
        /// Gets the stop command.
        /// </summary>
        /// <value>
        /// The stop command.
        /// </value>
        public DelegateCommand StopCommand { get; private set; }

        /// <summary>
        /// Gets the mute command.
        /// </summary>
        /// <value>
        /// The mute command.
        /// </value>
        public DelegateCommand MuteCommand { get; private set; }

        /// <summary>
        /// Gets the cut command.
        /// </summary>
        /// <value>
        /// The cut command.
        /// </value>
        public DelegateCommand CutCommand { get; private set; }

        /// <summary>
        /// Gets the book mark command.
        /// </summary>
        /// <value>
        /// The book mark command.
        /// </value>
        public DelegateCommand EncodeCommand { get; private set; }

        #region Command Handlers

        /// <summary>
        /// Plays the pause command_ executed.
        /// </summary>
        private void PlayPauseCommand_Executed()
        {
            if (PlaybackState == PlaybackState.Playing)
            {
                MediaElement.Pause();
                PlaybackState = PlaybackState.Paused;
            }
            else if (PlaybackState == PlaybackState.Paused)
            {
                //this.MediaElement.Opacity = 1;
                MediaElement.Play();
                PlaybackState = PlaybackState.Playing;
            }
            else if (PlaybackState == PlaybackState.Stopped)
            {
                GoToBeginCommand_Executed();
                MediaElement.Play();
                PlaybackState = PlaybackState.Playing;
            }
        }

        /// <summary>
        /// Goes to begin command_ executed.
        /// </summary>
        private void GoToBeginCommand_Executed()
        {
            MediaElement.Position = TimeSpan.FromSeconds(0);
            MediaElement.Pause();
            PlaybackProgress = 0;
        }

        /// <summary>
        /// Rewinds the command_ executed.
        /// </summary>
        private void RewindCommand_Executed()
        {
            MediaElement.Position -= TimeSpan.FromSeconds(1);
            PlaybackProgress--;
        }

        /// <summary>
        /// Forwards the command_ executed.
        /// </summary>
        private void ForwardCommand_Executed()
        {
            MediaElement.Position += TimeSpan.FromSeconds(1);
            PlaybackProgress++;
        }

        /// <summary>
        /// Goes to end command_ executed.
        /// </summary>
        private void GoToEndCommand_Executed()
        {
            PlaybackProgress = PlaybackDuration;
            MediaElement.Stop();
            PlaybackState = PlaybackState.Stopped;
        }

        /// <summary>
        /// Marks the begin command_ executed.
        /// </summary>
        private void MarkBeginCommand_Executed()
        {
            if (this.MediaElement != null && this.MediaElement.NaturalDuration.HasTimeSpan)
            {
                double currentPosition = this.MediaElement.Position.TotalSeconds;
                double totalDuration = this.MediaElement.NaturalDuration.TimeSpan.TotalSeconds;

                double totalWidth = ProgressSlider.ActualWidth;
                double margin = totalWidth * currentPosition / totalDuration;

                BookMark bookMark = new BookMark(this.MediaElement.Position) { Margin = new System.Windows.Thickness(margin, -4, 0, 0), Type = BookMarkType.In };
                this.BookMarks.Add(bookMark);
            }
        }

        /// <summary>
        /// Marks the end command_ executed.
        /// </summary>
        private void MarkEndCommand_Executed()
        {
            if (this.MediaElement != null && this.MediaElement.NaturalDuration.HasTimeSpan)
            {
                double currentPosition = this.MediaElement.Position.TotalSeconds;
                double totalDuration = this.MediaElement.NaturalDuration.TimeSpan.TotalSeconds;

                double totalWidth = ProgressSlider.ActualWidth;
                double margin = totalWidth * currentPosition / totalDuration;

                BookMark bookMark = new BookMark(this.MediaElement.Position) { Margin = new System.Windows.Thickness(margin, -4, 0, 0), Type = BookMarkType.Out };
                this.BookMarks.Add(bookMark);
            }
        }

        /// <summary>
        /// Seeks the command_ executed.
        /// </summary>
        private void SeekCommand_Executed()
        {
            _playbackTimer.Stop();
            MediaElement.Position = TimeSpan.FromSeconds((int)ProgressSlider.Value);
            PlaybackProgress = (int)ProgressSlider.Value;
            _playbackTimer.Start();
        }

        /// <summary>
        /// Stops the command_ executed.
        /// </summary>
        private void StopCommand_Executed()
        {
            MediaElement.Stop();
            PlaybackProgress = PlaybackDuration;
        }

        /// <summary>
        /// Mutes the command_ executed.
        /// </summary>
        private void MuteCommand_Executed()
        {
            MediaElement.IsMuted = !MediaElement.IsMuted;
        }

        /// <summary>
        /// Cuts the command_ executed.
        /// </summary>
        private void CutCommand_Executed()
        {
            VideoEditor videoEditor = new VideoEditor(@"C:\Users\Public\Videos\Sample Videos\Wildlife.wmv");

            TimeSpan startTime = new TimeSpan(0, 0, 05);
            TimeSpan endTime = new TimeSpan(0, 0, 20);

            videoEditor.ClipVideo(startTime, endTime);
        }

        /// <summary>
        /// Books the mark command_ executed.
        /// </summary>
        private void EncodeCommand_Executed()
        {
            if (MediaItem != null)
            {
                MediaItem.Encode();
            }
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

            _playbackTimer.Interval = TimeSpan.FromSeconds(1);
            _playbackTimer.Tick += PlaybackTimer_Tick;

            PlaybackState = PlaybackState.Stopped;
            Volume = 0.8;

            Overlays = new ObservableCollection<ShapeView>();
            EventAggregator.GetEvent<OverlayAddedEvent>().Subscribe(OverlayAdded);
        }

        /// <summary>
        /// Creates the commands.
        /// </summary>
        private void CreateCommands()
        {
            PlayPauseCommand = new DelegateCommand(PlayPauseCommand_Executed);
            GoToBeginCommand = new DelegateCommand(GoToBeginCommand_Executed);
            GoToEndCommand = new DelegateCommand(GoToEndCommand_Executed);
            RewindCommand = new DelegateCommand(RewindCommand_Executed);
            ForwardCommand = new DelegateCommand(ForwardCommand_Executed);
            MarkBeginCommand = new DelegateCommand(MarkBeginCommand_Executed);
            MarkEndCommand = new DelegateCommand(MarkEndCommand_Executed);
            SeekCommand = new DelegateCommand(SeekCommand_Executed);
            StopCommand = new DelegateCommand(StopCommand_Executed);
            MuteCommand = new DelegateCommand(MuteCommand_Executed);
            CutCommand = new DelegateCommand(CutCommand_Executed);
            EncodeCommand = new DelegateCommand(EncodeCommand_Executed);
        }

        /// <summary>
        /// Overlays the added.
        /// </summary>
        /// <param name="overlay">The overlay.</param>
        private void OverlayAdded(ShapeView overlay)
        {
            Overlays.Add(overlay);
        }

        /// <summary>
        /// Handles the Tick event of the PlaybackTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void PlaybackTimer_Tick(object sender, EventArgs e)
        {
            if (PlaybackState == PlaybackState.Playing)
            {
                PlaybackProgress++;
            }
        }

        /// <summary>
        /// Handles the MediaOpened event of the MediaElement control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void MediaElement_MediaOpened(object sender, System.Windows.RoutedEventArgs e)
        {
            _playbackTimer.Start();
            PlaybackProgress = 0;
            PlaybackState = PlaybackState.Playing;
            OnPropertyChanged("PlaybackDuration");
        }

        /// <summary>
        /// Handles the MediaEnded event of the MediaElement control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void MediaElement_MediaEnded(object sender, System.Windows.RoutedEventArgs e)
        {
            _playbackTimer.Stop();
            PlaybackState = PlaybackState.Stopped;
        }

        /// <summary>
        /// Updates the adorner on selected element.
        /// </summary>
        /// <param name="newSelectedElement">The new selected element.</param>
        private void UpdateAdornerOnSelectedElement(ShapeView newSelectedElement)
        {
            if (newSelectedElement != null)
            {
                int index;
                if (_selectedOverlay != null)
                {
                    index = Overlays.IndexOf(_selectedOverlay);
                    if (index >= 0)
                    {
                        RemoveAdornerFromItem(index);
                    }
                }
                index = Overlays.IndexOf(newSelectedElement);
                if (index >= 0)
                {
                    AddAdornerToItem(index);
                }
            }
            else
            {
                RemoveAdornerFromAllItems();
            }
        }

        #endregion

        #region Public Methods



        #endregion

        #endregion
    }
}
