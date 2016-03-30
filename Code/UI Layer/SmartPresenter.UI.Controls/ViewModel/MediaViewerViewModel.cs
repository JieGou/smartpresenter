using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Win32;
using SmartPresenter.BO.Common;
using SmartPresenter.BO.Common.Entities;
using SmartPresenter.BO.Common.Enums;
using SmartPresenter.BO.Common.Interfaces;
using SmartPresenter.Common;
using SmartPresenter.Common.Enums;
using SmartPresenter.Common.Logger;
using SmartPresenter.UI.Common;
using SmartPresenter.UI.Common.Enums;
using SmartPresenter.UI.Common.Interfaces;
using SmartPresenter.UI.Controls.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SmartPresenter.UI.Controls.ViewModel
{
    /// <summary>
    /// View Model class for MediaViewer
    /// </summary>
    [Export]
    public class MediaViewerViewModel : BindableBase
    {
        #region Private Data Members

        private ObservableCollection<MediaView> _mediaItems = new ObservableCollection<MediaView>();

        private MediaViewerMode _mode;

        private MediaLibrary _mediaLibrary;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaViewerViewModel"/> class.
        /// </summary>
        public MediaViewerViewModel()
        {
            Initialize();
        }

        #endregion

        #region Commands

        /// <summary>
        /// Command to change the view mode on viewer.
        /// </summary>
        public DelegateCommand<Object> ChangeViewModeCommand { get; set; }

        /// <summary>
        /// Gets or sets the add media command.
        /// </summary>
        /// <value>
        /// The add media command.
        /// </value>
        public DelegateCommand AddMediaCommand { get; set; }

        /// <summary>
        /// Gets or sets the edit media command.
        /// </summary>
        /// <value>
        /// The edit media command.
        /// </value>
        public DelegateCommand<Object> EditMediaCommand { get; set; }

        #region Command Handlers

        /// <summary>
        /// Handler for ChangeViewModeCommand.
        /// </summary>
        /// <param name="parameter"></param>
        private void ChangeViewModeCommand_Executed(Object parameter)
        {
            if (parameter != null && string.IsNullOrEmpty(parameter.ToString()) == false)
            {
                switch (parameter.ToString())
                {
                    case "Thumbnail":
                        Mode = MediaViewerMode.Thumbnail;
                        break;
                    case "Details":
                        Mode = MediaViewerMode.Details;
                        break;
                }
            }
        }

        /// <summary>
        /// Adds the media command_ executed.
        /// </summary>
        private void AddMediaCommand_Executed()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Filter = "Image files (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png | Video files(*.wmv, *.mov, *.mpg)|*.wmv;*.mov;*.mpg";
            fileDialog.ShowDialog();
            if (fileDialog.FileNames != null && fileDialog.FileNames.Count() > 0)
            {
                IShapeFactory imageFactory = new ImageFactory();
                IShapeFactory videoFactory = new VideoFactory();
                List<string> filesCouldNotBeImported = new List<string>();
                ObservableCollection<MediaView> mediaItems = new ObservableCollection<MediaView>();

                Parallel.ForEach(fileDialog.FileNames, file =>
                {
                    try
                    {
                        if (MediaHelper.IsValidImageFile(file))
                        {
                            ImageView imageView = new ImageView(imageFactory.CreateElement(file));
                            UIInteractionService.InvokeOnUIThread(() =>
                                {
                                    MediaItems.Add(imageView);
                                });
                        }
                        else if (MediaHelper.IsValidVideoFile(file))
                        {
                            VideoView videoView = new VideoView(videoFactory.CreateElement(file));
                            UIInteractionService.InvokeOnUIThread(() =>
                            {
                                MediaItems.Add(videoView);
                            });
                        }
                    }
                    catch (ArgumentException)
                    {
                        filesCouldNotBeImported.Add(file);
                    }
                });
                if (filesCouldNotBeImported.Count > 0)
                {
                    string errorMessage = "Following files could not be imported:" + Environment.NewLine;
                    int index = 1;
                    filesCouldNotBeImported.ForEach(file =>
                        {
                            errorMessage = errorMessage + index.ToString() + ". " + file + Environment.NewLine;
                            index++;
                        });
                    UIInteractionService.ShowMessageBox("Error in import", errorMessage, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        /// <summary>
        /// Edits the media command_ executed.
        /// </summary>
        private void EditMediaCommand_Executed(Object paramater)
        {
            NavigationParameters navigationParameters = new NavigationParameters();
            navigationParameters.Add("SelectedMedia", paramater);
            if (paramater is VideoView)
            {
                this.RegionManager.RequestNavigate(RegionNames.ContentRegion, new Uri("VideoEditorView", UriKind.Relative), navigationParameters);
            }
            else
            {
                this.RegionManager.RequestNavigate(RegionNames.ContentRegion, new Uri("ImageEditorView", UriKind.Relative), navigationParameters);
            }
        }

        #endregion

        #endregion

        #region Properties

        /// <summary>
        /// Collection of media items.
        /// </summary>
        public ObservableCollection<MediaView> MediaItems
        {
            get
            {
                return _mediaItems;
            }
            set
            {
                _mediaItems = value;
                OnPropertyChanged("MediaItems");
                EventAggregator.GetEvent<MediaViewerItemsUpdatedEvent>().Publish(MediaItems);
            }
        }

        /// <summary>
        /// Viewer Mode.
        /// </summary>
        public MediaViewerMode Mode
        {
            get
            {
                return _mode;
            }
            set
            {
                _mode = value;
                OnPropertyChanged("Mode");
            }
        }

        /// <summary>
        /// Number of total items in Media Gallery.
        /// </summary>
        public int TotalCount
        {
            get
            {
                return MediaItems.Count;
            }
        }

        /// <summary>
        /// Number of total images in Media Gallery.
        /// </summary>
        public int ImageCount
        {
            get
            {
                return MediaItems.Where(item => item.Type == ElementType.Image).Count();
            }
        }

        /// <summary>
        /// Number of total videos in Media Gallery.
        /// </summary>
        public int VideoCount
        {
            get
            {
                return MediaItems.Where(item => item.Type == ElementType.Video).Count();
            }
        }

        /// <summary>
        /// Gets the notification request.
        /// </summary>
        /// <value>
        /// The notification request.
        /// </value>
        public InteractionRequest<INotification> NotificationRequest { get; private set; }

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
        /// Gets the region manager.
        /// </summary>
        /// <value>
        /// The region manager.
        /// </value>
        private IRegionManager RegionManager
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IRegionManager>();
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

        #endregion

        #region Methods

        #region Public Methods

        #endregion

        #region Private Methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            Mode = MediaViewerMode.Thumbnail;
            this.NotificationRequest = new InteractionRequest<INotification>();

            CreateCommands();

            LoadData();
            MediaItems.CollectionChanged += MediaItems_CollectionChanged;
            // In case of Unit Testing it will be null.
            if (Application.Current != null)
            {
                Application.Current.Dispatcher.ShutdownStarted += Dispatcher_ShutdownStarted;
            }
            EventAggregator.GetEvent<MediaViewerItemsAskedEvent>().Subscribe(NotifyMediaViewerItemsUpdated);
        }

        /// <summary>
        /// Handles the CollectionChanged event of the MediaItems control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        private void MediaItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (MediaView item in e.NewItems)
                {
                    if (item.Type == ElementType.Image)
                    {
                        _mediaLibrary.Items.Add((Image)item.GetInnerObject());
                    }
                    if (item.Type == ElementType.Video)
                    {
                        _mediaLibrary.Items.Add((Video)item.GetInnerObject());
                    }
                }
            }
            EventAggregator.GetEvent<MediaViewerItemsUpdatedEvent>().Publish(MediaItems);
        }

        /// <summary>
        /// Creates the commands.
        /// </summary>
        private void CreateCommands()
        {
            ChangeViewModeCommand = new DelegateCommand<Object>(ChangeViewModeCommand_Executed);
            AddMediaCommand = new DelegateCommand(AddMediaCommand_Executed);
            EditMediaCommand = new DelegateCommand<Object>(EditMediaCommand_Executed);
        }

        /// <summary>
        /// Loads the data.
        /// </summary>
        private void LoadData()
        {
            if (ApplicationSettings.Instance.ActiveUserAccount.ActiveProfile.GeneralSettings.SelectedMediaLibrary != null)
            {
                _mediaLibrary = ApplicationSettings.Instance.ActiveUserAccount.ActiveProfile.GeneralSettings.SelectedMediaLibrary;
            }
            else
            {
                _mediaLibrary = new MediaLibrary(ApplicationSettings.Instance.ActiveUserAccount.ActiveProfile.MediaLibrariesFolderPath) { Name = "Default" };
                ApplicationSettings.Instance.ActiveUserAccount.ActiveProfile.GeneralSettings.MediaLibraries.Add(_mediaLibrary);
                ApplicationSettings.Instance.ActiveUserAccount.ActiveProfile.GeneralSettings.SelectedMediaLibrary = _mediaLibrary;
            }
            foreach (Image image in _mediaLibrary.Items)
            {
                if (image.Type == ElementType.Image)
                {
                    MediaItems.Add(new ImageView(image));
                }
                if (image.Type == ElementType.Video)
                {
                    MediaItems.Add(new VideoView(image));
                }
            }
        }

        private void Save()
        {
            Logger.LogEntry();

            if (ApplicationSettings.Instance.ActiveUserAccount.ActiveProfile.GeneralSettings.SelectedMediaLibrary != _mediaLibrary)
            {
                ApplicationSettings.Instance.ActiveUserAccount.ActiveProfile.GeneralSettings.SelectedMediaLibrary = _mediaLibrary;
            }   
            foreach(MediaLibrary mediaLibrary in ApplicationSettings.Instance.ActiveUserAccount.ActiveProfile.GeneralSettings.MediaLibraries)
            {
                mediaLibrary.Save();
            }         

            Logger.LogExit();
        }

        /// <summary>
        /// Saves the settings.
        /// </summary>
        private static void SaveSettings()
        {
            ApplicationSettings.Instance.ActiveUserAccount.ActiveProfile.Save();
        }

        /// <summary>
        /// Handles the ShutdownStarted event of the Dispatcher control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Dispatcher_ShutdownStarted(object sender, EventArgs e)
        {
            UpdateBusinessObject();
        }

        /// <summary>
        /// Updates the business object.
        /// </summary>
        public void UpdateBusinessObject()
        {
            Save();
            SaveSettings();
        }

        /// <summary>
        /// Notifies the media viewer items updated.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void NotifyMediaViewerItemsUpdated(Object parameter)
        {
            EventAggregator.GetEvent<MediaViewerItemsUpdatedEvent>().Publish(MediaItems);
        }

        #endregion

        #endregion

    }
}
