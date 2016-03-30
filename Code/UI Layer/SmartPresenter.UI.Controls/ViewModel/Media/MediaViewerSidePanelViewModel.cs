using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;
using SmartPresenter.BO.Common;
using SmartPresenter.BO.Common.Entities;
using SmartPresenter.BO.Common.Enums;
using SmartPresenter.Common.Enums;
using SmartPresenter.UI.Controls.Events;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;

namespace SmartPresenter.UI.Controls.ViewModel
{
    [Export]
    public class MediaViewerSidePanelViewModel : BindableBase
    {
        #region Private Data Members

        private bool _showImages = true;
        private bool _showVideos = true;
        private ObservableCollection<MediaView> _items;
        private bool _areItemsAvailable = false;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaViewerSidePanelViewModel"/> class.
        /// </summary>
        public MediaViewerSidePanelViewModel()
        {
            Initialize();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public ObservableCollection<MediaView> Items
        {
            get
            {
                if (_areItemsAvailable == false)
                {
                    EventAggregator.GetEvent<MediaViewerItemsAskedEvent>().Publish(null);
                }
                _areItemsAvailable = false;
                return _items;
            }
        }

        /// <summary>
        /// Gets or sets the filtered items.
        /// </summary>
        /// <value>
        /// The filtered items.
        /// </value>
        public ObservableCollection<MediaView> FilteredItems
        {
            get
            {
                if (ShowImages == true && ShowVideos == false)
                {
                    return new ObservableCollection<MediaView>(Items.OfType<ImageView>());
                }
                if (ShowImages == false && ShowVideos == true)
                {
                    return new ObservableCollection<MediaView>(Items.OfType<VideoView>());
                }
                if (ShowImages == false && ShowVideos == false)
                {
                    return new ObservableCollection<MediaView>();
                }
                return Items;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show images].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show images]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowImages
        {
            get
            {
                return _showImages;
            }
            set
            {
                _showImages = value;
                FilteredItems.Clear();
                Items.OfType<ImageView>().ToList().ForEach((item =>
                {
                    FilteredItems.Add(item);
                }));
                OnPropertyChanged("ShowImages");
                OnPropertyChanged("Items");
                OnPropertyChanged("FilteredItems");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [show videos].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show videos]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowVideos
        {
            get
            {
                return _showVideos;
            }
            set
            {
                _showVideos = value;
                FilteredItems.Clear();
                Items.OfType<VideoView>().ToList().ForEach((item =>
                {
                    FilteredItems.Add(item);
                }));
                OnPropertyChanged("ShowVideos");
                OnPropertyChanged("Items");
                OnPropertyChanged("FilteredItems");
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

        #region Private Methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            EventAggregator.GetEvent<MediaViewerItemsUpdatedEvent>().Subscribe(MediaViewerItemsUpdatedEvent);
            if (ApplicationSettings.Instance.ActiveUserAccount.ActiveProfile.GeneralSettings.SelectedMediaLibrary != null)
            {
                foreach (Image image in ApplicationSettings.Instance.ActiveUserAccount.ActiveProfile.GeneralSettings.SelectedMediaLibrary.Items)
                {
                    if (image.Type == ElementType.Image)
                    {
                        Items.Add(new ImageView(image));
                    }
                    else if (image.Type == ElementType.Video)
                    {
                        Items.Add(new VideoView(image));
                    }
                }
            }
            Items.ToList().ForEach((item =>
                {
                    FilteredItems.Add(item);
                }));
        }

        #endregion

        #region Public Methods



        #endregion

        #region Private Methods

        /// <summary>
        /// Medias the viewer items updated event.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void MediaViewerItemsUpdatedEvent(Object parameter)
        {
            try
            {
                ObservableCollection<MediaView> mediaItems = parameter as ObservableCollection<MediaView>;
                if (parameter != null)
                {
                    _items = new ObservableCollection<MediaView>(mediaItems);
                }
                else
                {
                    _items = new ObservableCollection<MediaView>();
                }
            }
            finally
            {
                _areItemsAvailable = true;
                OnPropertyChanged("Items");
                OnPropertyChanged("FilteredItems");
            }
        }

        #endregion

        #endregion
    }
}
