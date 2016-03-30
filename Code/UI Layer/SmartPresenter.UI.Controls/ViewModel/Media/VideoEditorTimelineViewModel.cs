using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;
using SmartPresenter.UI.Controls.Events;
using SmartPresenter.UI.Controls.ViewModel.Media;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace SmartPresenter.UI.Controls.ViewModel
{
    public class VideoEditorTimelineViewModel : BindableBase
    {
        #region Private Data Members

        private Point _startPoint;
        private bool _isDragging;

        #endregion

        #region Constructor

        public VideoEditorTimelineViewModel()
        {
            Initialize();
        }

        #endregion

        #region Properties

        public ObservableCollection<VideoEditorImageOverlayTrackItem> ImageOverlays { get; set; }
        public ObservableCollection<VideoEditorVideoOverlayTrackItem> VideoOverlays { get; set; }
        public ObservableCollection<VideoEditorTextOverlayTrackItem> TextOverlays { get; set; }
        public ObservableCollection<VideoEditorAudioOverlayTrackItem> AudioOverlays { get; set; }

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

        internal Grid MainGrid { get; set; }

        #endregion

        #region Commands

        public DelegateCommand<DragEventArgs> ImageOverlayPreviewDropCommand { get; private set; }
        public DelegateCommand<DragEventArgs> VideoOverlayPreviewDropCommand { get; private set; }
        public DelegateCommand<MouseEventArgs> OverlayPreviewMouseLeftButtonDownCommand { get; private set; }
        public DelegateCommand<MouseEventArgs> OverlayPreviewMouseLeftButtonUpCommand { get; private set; }
        public DelegateCommand<MouseEventArgs> OverlayPreviewMouseMoveCommand { get; private set; }
        public DelegateCommand<DragDeltaEventArgs> DragDeltaXCommand { get; private set; }
        public DelegateCommand<DragDeltaEventArgs> DragDeltaWidthCommand { get; private set; }

        #region Command Handlers

        private void ImageOverlayPreviewDropCommand_Executed(DragEventArgs e)
        {
            if (e.Data.GetDataPresent("DragDropItemsControl"))
            {
                ImageView image = e.Data.GetData("DragDropItemsControl") as ImageView;
                if (image != null)
                {
                    ImageOverlays.Add(new VideoEditorImageOverlayTrackItem() { X = 0, Width = (int)MainGrid.ActualWidth });
                }
                image.Width = 200;
                image.Height = 150;
                image.X = 200;
                image.Y = 200;
                EventAggregator.GetEvent<OverlayAddedEvent>().Publish(image);
            }
        }

        private void VideoOverlayPreviewDropCommand_Executed(DragEventArgs e)
        {
            if (e.Data.GetDataPresent("DragDropItemsControl"))
            {
                VideoView video = e.Data.GetData("DragDropItemsControl") as VideoView;
                if (video != null)
                {
                    VideoOverlays.Add(new VideoEditorVideoOverlayTrackItem() { X = 0, Width = (int)MainGrid.ActualWidth });
                }
                video.Width = 200;
                video.Height = 150;
                video.X = 200;
                video.Y = 200;
                EventAggregator.GetEvent<OverlayAddedEvent>().Publish(video);
            }
        }

        private void OverlayPreviewMouseLeftButtonDown_Executed(MouseEventArgs e)
        {
            _startPoint = e.GetPosition(e.Source as IInputElement);
            _isDragging = true;
        }

        private void OverlayPreviewMouseLeftButtonUp_Executed(MouseEventArgs e)
        {
            _startPoint = new Point();
            _isDragging = false;
        }

        private void OverlayPreviewMouseMove_Executed(MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && _isDragging)
            {
                Point currentPoint = e.GetPosition(e.Source as IInputElement);
                double xDistance = currentPoint.X - _startPoint.X;
                VideoEditorOverlayTrackItem overlayItem = (e.Source as FrameworkElement).DataContext as VideoEditorOverlayTrackItem;
                overlayItem.X += (int)xDistance;
                overlayItem.X = overlayItem.X < 0 ? 0 : overlayItem.X;
            }
        }

        private void DragDeltaXCommand_Executed(DragDeltaEventArgs e)
        {
            VideoEditorOverlayTrackItem overlayItem = (e.Source as FrameworkElement).DataContext as VideoEditorOverlayTrackItem;
            overlayItem.X += (int)e.HorizontalChange;
            overlayItem.X = overlayItem.X < 0 ? 0 : overlayItem.X;
            if (overlayItem.X > 0)
            {
                overlayItem.Width -= (int)e.HorizontalChange;
            }
        }

        private void DragDeltaWidthCommand_Executed(DragDeltaEventArgs e)
        {
            VideoEditorOverlayTrackItem overlayItem = (e.Source as FrameworkElement).DataContext as VideoEditorOverlayTrackItem;
            overlayItem.Width += (int)e.HorizontalChange;
        }

        #endregion

        #endregion

        #region Private Methods

        private void CreateCommands()
        {
            ImageOverlayPreviewDropCommand = new DelegateCommand<DragEventArgs>(ImageOverlayPreviewDropCommand_Executed);
            VideoOverlayPreviewDropCommand = new DelegateCommand<DragEventArgs>(VideoOverlayPreviewDropCommand_Executed);
            OverlayPreviewMouseLeftButtonDownCommand = new DelegateCommand<MouseEventArgs>(OverlayPreviewMouseLeftButtonDown_Executed);
            OverlayPreviewMouseLeftButtonUpCommand = new DelegateCommand<MouseEventArgs>(OverlayPreviewMouseLeftButtonUp_Executed);
            OverlayPreviewMouseMoveCommand = new DelegateCommand<MouseEventArgs>(OverlayPreviewMouseMove_Executed);
            DragDeltaXCommand = new DelegateCommand<DragDeltaEventArgs>(DragDeltaXCommand_Executed);
            DragDeltaWidthCommand = new DelegateCommand<DragDeltaEventArgs>(DragDeltaWidthCommand_Executed);
        }

        private void Initialize()
        {
            CreateCommands();

            ImageOverlays = new ObservableCollection<VideoEditorImageOverlayTrackItem>();
            VideoOverlays = new ObservableCollection<VideoEditorVideoOverlayTrackItem>();
            TextOverlays = new ObservableCollection<VideoEditorTextOverlayTrackItem>();
            AudioOverlays = new ObservableCollection<VideoEditorAudioOverlayTrackItem>();
        }

        #endregion
    }
}
