using Microsoft.Practices.Prism.Regions;
using SmartPresenter.BO.Media;
using SmartPresenter.UI.Common;
using SmartPresenter.UI.Common.Adorners;
using SmartPresenter.UI.Controls.ViewModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace SmartPresenter.UI.Controls.Media
{
    /// <summary>
    /// Interaction logic for VideoEditor.xaml
    /// </summary>    
    [Export("VideoEditorView")]
    public partial class VideoEditorView : UserControl, INavigationAware
    {
        #region Private Data Members

        private Canvas _canvas;
        private Point _startPoint;
        private bool _isDragStarted;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="VideoEditorView"/> class.
        /// </summary>
        public VideoEditorView()
        {
            InitializeComponent();
            this.Loaded += VideoEditorView_Loaded;
            this.DataContextChanged += VideoEditorView_DataContextChanged;
            //MediaProperties vidProps = new MediaProperties(@"C:\Users\Vibhore\Videos\Wildlife.wmv");
        }

        #endregion

        #region Properties

        /// <summary>
        /// Sets the view model for Shell.
        /// </summary>
        /// <value>
        /// The view model of Shell.
        /// </value>
        [Import]
        VideoEditorViewModel ViewModel
        {
            get
            {
                return (VideoEditorViewModel)this.DataContext;
            }
            set
            {
                this.DataContext = value;
            }
        }

        #endregion

        #region Methods

        #region Public Methods

        /// <summary>
        /// Determines whether [is navigation target] [the specified navigation context].
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        /// <returns></returns>
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return false;
        }

        /// <summary>
        /// Called when [navigated from].
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        /// <summary>
        /// Called when [navigated to].
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            VideoView videoView = navigationContext.Parameters["SelectedMedia"] as VideoView;
            ViewModel.SelectedVideo = videoView;
        }

        #endregion

        #region Private Methods

        private void VideoEditorView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ViewModel.AddAdornerToItem = AddAdornerToItem;
            ViewModel.RemoveAdornerFromItem = RemoveAdornerFromItem;
            ViewModel.RemoveAdornerFromAllItems = RemoveAdornerFromAllItems;
            ViewModel.EditingCanvas = _canvas = VisualTreeAssitant.FindElementInVisualTree<Canvas>(editorListBox);
        }

        /// <summary>
        /// Handles the Loaded event of the VideoEditorView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void VideoEditorView_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.MediaElement = mediaElement;
            ViewModel.ProgressSlider = progressSlider;

        }

        /// <summary>
        /// Completes drag and drop. Clears the state, reset all object to their previous state.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Shape_MouseUp(object sender, MouseEventArgs e)
        {
            if (_isDragStarted)
            {
                var dragElement = sender as FrameworkElement;
                //dragElement.ReleaseMouseCapture();
                dragElement.Cursor = Cursors.Arrow;
                _startPoint = new Point();
                foreach (ListBoxItem item in _canvas.Children)
                {
                    item.IsHitTestVisible = true;
                }
                _isDragStarted = false;
            }
        }

        /// <summary>
        /// Starts drag and drop.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Shape_MouseDown(object sender, MouseEventArgs e)
        {
            if (_canvas == null)
            {
                _canvas = VisualTreeAssitant.FindElementInVisualTree<Canvas>(editorListBox);
            }
            var dragElement = sender as FrameworkElement;
            ShapeView elementView = dragElement.DataContext as ShapeView;
            _startPoint = e.GetPosition(dragElement);
            //dragElement.CaptureMouse();
            dragElement.Cursor = Cursors.SizeAll;
            foreach (ListBoxItem item in _canvas.Children)
            {
                if (item.Equals(dragElement) == false)
                {
                    item.IsHitTestVisible = false;
                }
            }
            _isDragStarted = true;
        }

        /// <summary>
        /// Moves object as cursor is moved.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Shape_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var dragElement = sender as FrameworkElement;
                Point currentPoint = e.GetPosition(_canvas);
                Canvas.SetLeft(dragElement, currentPoint.X - _startPoint.X);
                Canvas.SetTop(dragElement, currentPoint.Y - _startPoint.Y);
            }
        }

        /// <summary>
        /// Handles the KeyUp event of the Element control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void Shape_KeyDown(object sender, KeyEventArgs e)
        {
            ShapeView shape = ((FrameworkElement)sender).DataContext as ShapeView;
            if (shape != null)
            {
                int change = 1;
                if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                {
                    change = 10;
                }

                switch (e.Key)
                {
                    case Key.Up:
                        shape.Y -= change;
                        break;
                    case Key.Down:
                        shape.Y += change;
                        break;
                    case Key.Left:
                        shape.X -= change;
                        break;
                    case Key.Right:
                        shape.X += change;
                        break;
                }
            }
            e.Handled = true;
        }

        /// <summary>
        /// Adds the adorner to item.
        /// </summary>
        /// <param name="index">The index.</param>
        private void AddAdornerToItem(int index)
        {
            ListBoxItem listBoxItem = (ListBoxItem)editorListBox.ItemContainerGenerator.ContainerFromIndex(index);
            if (listBoxItem != null)
            {
                AdornerLayer myAdornerLayer = AdornerLayer.GetAdornerLayer(listBoxItem);
                Adorner adorner = null;
                if (listBoxItem.DataContext is CircleView)
                {
                    adorner = new FourWayResizingAdorner(listBoxItem);
                }
                else
                {
                    adorner = new ResizingAdorner(listBoxItem);
                }
                myAdornerLayer.Add(adorner);
            }
        }

        /// <summary>
        /// Removes the adorner from all items.
        /// </summary>
        private void RemoveAdornerFromAllItems()
        {
            for (int index = 0; index < editorListBox.Items.Count; ++index)
            {
                ListBoxItem contentPresenter = (ListBoxItem)editorListBox.ItemContainerGenerator.ContainerFromIndex(index);
                if (contentPresenter != null)
                {
                    AdornerLayer myAdornerLayer = AdornerLayer.GetAdornerLayer(contentPresenter);
                    Adorner[] adornersToRemove = myAdornerLayer.GetAdorners(contentPresenter);
                    if (adornersToRemove != null)
                    {
                        adornersToRemove.ToList().ForEach(adorner => myAdornerLayer.Remove(adorner));
                    }
                }
            }
        }

        /// <summary>
        /// Removes the adorner from item.
        /// </summary>
        /// <param name="index">The index.</param>
        private void RemoveAdornerFromItem(int index)
        {
            ListBoxItem contentPresenter = (ListBoxItem)editorListBox.ItemContainerGenerator.ContainerFromIndex(index);
            if (contentPresenter != null)
            {
                AdornerLayer myAdornerLayer = AdornerLayer.GetAdornerLayer(contentPresenter);
                Adorner[] adornersToRemove = myAdornerLayer.GetAdorners(contentPresenter);
                if (adornersToRemove != null)
                {
                    adornersToRemove.ToList().ForEach(adorner => myAdornerLayer.Remove(adorner));
                }
            }
        }

        #endregion

        #endregion

    }
}
