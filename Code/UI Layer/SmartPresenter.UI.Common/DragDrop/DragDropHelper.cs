using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace SmartPresenter.UI.Common.DragDrop
{
    public class DragDropHelper
    {
        // source and target
        private DataFormat format = DataFormats.GetDataFormat("DragDropItemsControl");
        private Point initialMousePosition;
        private Vector initialMouseOffset;
        private object draggedData;
        private DraggedAdorner mainWindowDraggedAdorner;
        private DraggedAdorner controlDraggedAdorner;
        private InsertionAdorner insertionAdorner;
        private Window topWindow;
        // source
        private FrameworkElement sourceItemContainer;
        // target
        private FrameworkElement targetItemContainer;
        private bool hasVerticalOrientation;
        private int insertionIndex;
        private bool isInFirstHalf;
        // singleton
        private static DragDropHelper instance;
        private static DragDropHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DragDropHelper();
                }
                return instance;
            }
        }

        public static bool GetIsDragSource(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsDragSourceProperty);
        }

        public static void SetIsDragSource(DependencyObject obj, bool value)
        {
            obj.SetValue(IsDragSourceProperty, value);
        }

        public static readonly DependencyProperty IsDragSourceProperty =
            DependencyProperty.RegisterAttached("IsDragSource", typeof(bool), typeof(DragDropHelper), new UIPropertyMetadata(false, IsDragSourceChanged));


        public static bool GetIsDropTarget(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsDropTargetProperty);
        }

        public static void SetIsDropTarget(DependencyObject obj, bool value)
        {
            obj.SetValue(IsDropTargetProperty, value);
        }

        public static readonly DependencyProperty IsDropTargetProperty =
            DependencyProperty.RegisterAttached("IsDropTarget", typeof(bool), typeof(DragDropHelper), new UIPropertyMetadata(false, IsDropTargetChanged));

        public static DataTemplate GetDragDropTemplate(DependencyObject obj)
        {
            return (DataTemplate)obj.GetValue(DragDropTemplateProperty);
        }

        public static void SetDragDropTemplate(DependencyObject obj, DataTemplate value)
        {
            obj.SetValue(DragDropTemplateProperty, value);
        }

        public static readonly DependencyProperty DragDropTemplateProperty =
            DependencyProperty.RegisterAttached("DragDropTemplate", typeof(DataTemplate), typeof(DragDropHelper), new UIPropertyMetadata(null));

        private static void IsDragSourceChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var dragSource = obj as FrameworkElement;
            if (dragSource != null)
            {
                if (Object.Equals(e.NewValue, true))
                {
                    dragSource.PreviewMouseLeftButtonDown += Instance.DragSource_PreviewMouseLeftButtonDown;
                    dragSource.PreviewMouseLeftButtonUp += Instance.DragSource_PreviewMouseLeftButtonUp;
                    dragSource.PreviewMouseMove += Instance.DragSource_PreviewMouseMove;
                }
                else
                {
                    dragSource.PreviewMouseLeftButtonDown -= Instance.DragSource_PreviewMouseLeftButtonDown;
                    dragSource.PreviewMouseLeftButtonUp -= Instance.DragSource_PreviewMouseLeftButtonUp;
                    dragSource.PreviewMouseMove -= Instance.DragSource_PreviewMouseMove;
                }
            }
        }

        private static void IsDropTargetChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var dropTarget = obj as FrameworkElement;
            if (dropTarget != null)
            {
                if (Object.Equals(e.NewValue, true))
                {
                    dropTarget.AllowDrop = true;
                    dropTarget.PreviewDrop += Instance.DropTarget_PreviewDrop;
                    dropTarget.PreviewDragEnter += Instance.DropTarget_PreviewDragEnter;
                    dropTarget.PreviewDragOver += Instance.DropTarget_PreviewDragOver;
                    dropTarget.PreviewDragLeave += Instance.DropTarget_PreviewDragLeave;
                }
                else
                {
                    dropTarget.AllowDrop = false;
                    dropTarget.PreviewDrop -= Instance.DropTarget_PreviewDrop;
                    dropTarget.PreviewDragEnter -= Instance.DropTarget_PreviewDragEnter;
                    dropTarget.PreviewDragOver -= Instance.DropTarget_PreviewDragOver;
                    dropTarget.PreviewDragLeave -= Instance.DropTarget_PreviewDragLeave;
                }
            }
        }

        // DragSource

        private void DragSource_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.sourceItemContainer = (FrameworkElement)sender;
            Visual visual = e.OriginalSource as Visual;

            this.topWindow = Window.GetWindow(this.sourceItemContainer);
            this.initialMousePosition = e.GetPosition(this.topWindow);

            if (this.sourceItemContainer != null)
            {
                if (this.sourceItemContainer.DataContext != null)
                {
                    this.draggedData = this.sourceItemContainer.DataContext;
                }
                else
                {
                    this.draggedData = this.sourceItemContainer;
                }
            }
        }

        // Drag = mouse down + move by a certain amount
        private void DragSource_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (this.draggedData != null)
            {
                // Only drag when user moved the mouse by a reasonable amount.
                if (Utilities.IsMovementBigEnough(this.initialMousePosition, e.GetPosition(this.topWindow)))
                {
                    this.initialMouseOffset = this.initialMousePosition - this.sourceItemContainer.TranslatePoint(new Point(0, 0), this.topWindow);

                    DataObject data = new DataObject(this.format.Name, this.draggedData);

                    // Adding events to the window to make sure dragged adorner comes up when mouse is not over a drop target.
                    bool previousAllowDrop = this.topWindow.AllowDrop;
                    this.topWindow.AllowDrop = true;
                    this.topWindow.DragEnter += TopWindow_DragEnter;
                    this.topWindow.DragOver += TopWindow_DragOver;
                    this.topWindow.DragLeave += TopWindow_DragLeave;

                    DragDropEffects effects = System.Windows.DragDrop.DoDragDrop((DependencyObject)sender, data, DragDropEffects.Move);

                    // Without this call, there would be a bug in the following scenario: Click on a data item, and drag
                    // the mouse very fast outside of the window. When doing this really fast, for some reason I don't get 
                    // the Window leave event, and the dragged adorner is left behind.
                    // With this call, the dragged adorner will disappear when we release the mouse outside of the window,
                    // which is when the DoDragDrop synchronous method returns.
                    RemoveDraggedAdorner();

                    this.topWindow.AllowDrop = previousAllowDrop;
                    this.topWindow.DragEnter -= TopWindow_DragEnter;
                    this.topWindow.DragOver -= TopWindow_DragOver;
                    this.topWindow.DragLeave -= TopWindow_DragLeave;

                    this.draggedData = null;
                }
            }
        }

        private void DragSource_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.draggedData = null;
        }

        // DropTarget

        private void DropTarget_PreviewDragEnter(object sender, DragEventArgs e)
        {
            this.targetItemContainer = (FrameworkElement)sender;
            object draggedItem = e.Data.GetData(this.format.Name);

            DecideDropTarget(e);
            if (draggedItem != null)
            {
                // Dragged Adorner is created on the first enter only.
                ShowDraggedAdorner(e.GetPosition(this.topWindow));
                CreateInsertionAdorner();
            }
            e.Handled = true;
        }

        private void DropTarget_PreviewDragOver(object sender, DragEventArgs e)
        {
            object draggedItem = e.Data.GetData(this.format.Name);

            DecideDropTarget(e);
            if (draggedItem != null)
            {
                // Dragged Adorner is only updated here - it has already been created in DragEnter.
                ShowDraggedAdorner(e.GetPosition(this.topWindow));
                UpdateInsertionAdornerPosition();
            }
            e.Handled = true;
        }

        private void DropTarget_PreviewDrop(object sender, DragEventArgs e)
        {
            object draggedItem = e.Data.GetData(this.format.Name);

            if (draggedItem != null)
            {
                RemoveDraggedAdorner();
                RemoveInsertionAdorner();
            }
            //e.Handled = true;
        }

        private void DropTarget_PreviewDragLeave(object sender, DragEventArgs e)
        {
            // Dragged Adorner is only created once on DragEnter + every time we enter the window. 
            // It's only removed once on the DragDrop, and every time we leave the window. (so no need to remove it here)
            object draggedItem = e.Data.GetData(this.format.Name);

            if (draggedItem != null)
            {
                RemoveInsertionAdorner();
            }
            e.Handled = true;
        }

        // If the types of the dragged data and FrameworkElement's source are compatible, 
        // there are 3 situations to have into account when deciding the drop target:
        // 1. mouse is over an items container
        // 2. mouse is over the empty part of an FrameworkElement, but FrameworkElement is not empty
        // 3. mouse is over an empty FrameworkElement.
        // The goal of this method is to decide on the values of the following properties: 
        // targetItemContainer, insertionIndex and isInFirstHalf.
        private void DecideDropTarget(DragEventArgs e)
        {
            //ItemsControl targetItemsControl = targetItemContainer as ItemsControl;
            //if (targetItemsControl != null)
            //{
            //    int targetItemsControlCount = targetItemsControl.Items.Count;
            //    object draggedItem = e.Data.GetData(this.format.Name);

            //    if (IsDropDataTypeAllowed(draggedItem))
            //    {
            //        if (targetItemsControlCount > 0)
            //        {
            //            this.hasVerticalOrientation = Utilities.HasVerticalOrientation(targetItemsControl.ItemContainerGenerator.ContainerFromIndex(0) as FrameworkElement);
            //            this.targetItemContainer = targetItemsControl.ContainerFromElement((DependencyObject)e.OriginalSource) as FrameworkElement;

            //            if (this.targetItemContainer != null)
            //            {
            //                Point positionRelativeToItemContainer = e.GetPosition(this.targetItemContainer);
            //                this.isInFirstHalf = Utilities.IsInFirstHalf(this.targetItemContainer, positionRelativeToItemContainer, this.hasVerticalOrientation);
            //                this.insertionIndex = targetItemsControl.ItemContainerGenerator.IndexFromContainer(this.targetItemContainer);

            //                if (!this.isInFirstHalf)
            //                {
            //                    this.insertionIndex++;
            //                }
            //            }
            //            else
            //            {
            //                this.targetItemContainer = targetItemsControl.ItemContainerGenerator.ContainerFromIndex(targetItemsControlCount - 1) as FrameworkElement;
            //                this.isInFirstHalf = false;
            //                this.insertionIndex = targetItemsControlCount;
            //            }
            //        }
            //        else
            //        {
            //            this.targetItemContainer = null;
            //            this.insertionIndex = 0;
            //        }
            //    }
            //    else
            //    {
            //        this.targetItemContainer = null;
            //        this.insertionIndex = -1;
            //        e.Effects = DragDropEffects.None;
            //    }
            //}
        }

        // Can the dragged data be added to the destination collection?
        // It can if destination is bound to IList<allowed type>, IList or not data bound.
        private bool IsDropDataTypeAllowed(object draggedItem)
        {
            bool isDropDataTypeAllowed = true;
            //IEnumerable collectionSource = this.targetItemsControl.ItemsSource;
            //if (draggedItem != null)
            //{
            //    if (collectionSource != null)
            //    {
            //        Type draggedType = draggedItem.GetType();
            //        Type collectionType = collectionSource.GetType();

            //        Type genericIListType = collectionType.GetInterface("IList`1");
            //        if (genericIListType != null)
            //        {
            //            Type[] genericArguments = genericIListType.GetGenericArguments();
            //            isDropDataTypeAllowed = genericArguments[0].IsAssignableFrom(draggedType);
            //        }
            //        else if (typeof(IList).IsAssignableFrom(collectionType))
            //        {
            //            isDropDataTypeAllowed = true;
            //        }
            //        else
            //        {
            //            isDropDataTypeAllowed = false;
            //        }
            //    }
            //    else // the FrameworkElement's ItemsSource is not data bound.
            //    {
            //        isDropDataTypeAllowed = true;
            //    }
            //}
            //else
            //{
            //    isDropDataTypeAllowed = false;
            //}
            return isDropDataTypeAllowed;
        }

        // Window

        private void TopWindow_DragEnter(object sender, DragEventArgs e)
        {
            ShowDraggedAdorner(e.GetPosition(this.topWindow));
            e.Effects = DragDropEffects.None;
            e.Handled = true;
        }

        private void TopWindow_DragOver(object sender, DragEventArgs e)
        {
            ShowDraggedAdorner(e.GetPosition(this.topWindow));
            e.Effects = DragDropEffects.None;
            e.Handled = true;
        }

        private void TopWindow_DragLeave(object sender, DragEventArgs e)
        {
            RemoveDraggedAdorner();
            e.Handled = true;
        }

        // Adorners

        // Creates or updates the dragged Adorner. 
        private void ShowDraggedAdorner(Point currentPosition)
        {
            if (this.mainWindowDraggedAdorner == null)
            {
                var adornerLayer = AdornerLayer.GetAdornerLayer(Application.Current.MainWindow.Content as UIElement);
                this.mainWindowDraggedAdorner = new DraggedAdorner(this.draggedData, GetDragDropTemplate(this.sourceItemContainer), Application.Current.MainWindow.Content as UIElement, adornerLayer);
            }
            //if (this.controlDraggedAdorner == null)
            //{
            //    var adornerLayer = AdornerLayer.GetAdornerLayer(this.sourceItemContainer);
            //    this.controlDraggedAdorner = new DraggedAdorner(this.draggedData, GetDragDropTemplate(this.sourceItemContainer), this.sourceItemContainer, adornerLayer);
            //}
            this.mainWindowDraggedAdorner.SetPosition(currentPosition.X, currentPosition.Y);
            //this.controlDraggedAdorner.SetPosition(currentPosition.X - this.initialMousePosition.X + this.initialMouseOffset.X, currentPosition.Y - this.initialMousePosition.Y + this.initialMouseOffset.Y);
        }

        private void RemoveDraggedAdorner()
        {
            if (this.mainWindowDraggedAdorner != null)
            {
                this.mainWindowDraggedAdorner.Detach();
                this.mainWindowDraggedAdorner = null;
            }

            //if (this.controlDraggedAdorner != null)
            //{
            //    this.controlDraggedAdorner.Detach();
            //    this.controlDraggedAdorner = null;
            //}
        }

        private void CreateInsertionAdorner()
        {
            if (this.targetItemContainer != null)
            {
                // Here, I need to get adorner layer from targetItemContainer and not targetItemsControl. 
                // This way I get the AdornerLayer within ScrollContentPresenter, and not the one under AdornerDecorator (Snoop is awesome).
                // If I used targetItemsControl, the adorner would hang out of FrameworkElement when there's a horizontal scroll bar.
                var adornerLayer = AdornerLayer.GetAdornerLayer(this.targetItemContainer);
                this.insertionAdorner = new InsertionAdorner(this.hasVerticalOrientation, this.isInFirstHalf, this.targetItemContainer, adornerLayer);
            }
        }

        private void UpdateInsertionAdornerPosition()
        {
            if (this.insertionAdorner != null)
            {
                this.insertionAdorner.IsInFirstHalf = this.isInFirstHalf;
                this.insertionAdorner.InvalidateVisual();
            }
        }

        private void RemoveInsertionAdorner()
        {
            if (this.insertionAdorner != null)
            {
                this.insertionAdorner.Detach();
                this.insertionAdorner = null;
            }
        }
    }
}
