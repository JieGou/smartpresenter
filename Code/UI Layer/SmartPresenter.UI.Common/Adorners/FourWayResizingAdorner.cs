using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SmartPresenter.UI.Common.Adorners
{
    /// <summary>
    /// A Resizing adorner with four pick points.
    /// </summary>
    public class FourWayResizingAdorner : Adorner
    {
        // Resizing adorner uses Thumbs for visual elements.  
        // The Thumbs have built-in mouse input handling.
        Thumb _topLeft, _topRight, _bottomLeft, _bottomRight;
        Rectangle _border;

        // To store and manage the adorner's visual children.
        VisualCollection _visualChildren;

        // Initialize the ResizingAdorner.
        public FourWayResizingAdorner(UIElement adornedElement)
            : base(adornedElement)
        {
            _visualChildren = new VisualCollection(this);

            // Call a helper method to initialize the Thumbs
            // with a customized cursors.
            _topLeft = BuildAdornerCorner(Cursors.SizeNWSE);
            _topRight = BuildAdornerCorner(Cursors.SizeNESW);
            _bottomLeft = BuildAdornerCorner(Cursors.SizeNESW);
            _bottomRight = BuildAdornerCorner(Cursors.SizeNWSE);
            _border = new Rectangle() { Stroke = Brushes.Blue, StrokeThickness = 1.5, StrokeDashArray = { 6, 3 } };
            _visualChildren.Add(_border);

            // Add handlers for resizing.
            _bottomLeft.DragDelta += new DragDeltaEventHandler(HandleBottomLeft);
            _bottomRight.DragDelta += new DragDeltaEventHandler(HandleBottomRight);
            _topLeft.DragDelta += new DragDeltaEventHandler(HandleTopLeft);
            _topRight.DragDelta += new DragDeltaEventHandler(HandleTopRight);
        }

        // Handler for resizing from the bottom-right.
        void HandleBottomRight(object sender, DragDeltaEventArgs args)
        {
            FrameworkElement adornedElement = this.AdornedElement as FrameworkElement;
            Thumb hitThumb = sender as Thumb;

            if (adornedElement == null || hitThumb == null) return;
            FrameworkElement parentElement = adornedElement.Parent as FrameworkElement;

            // Ensure that the Width and Height are properly initialized after the resize.
            EnforceSize(adornedElement);

            // Change the size by the amount the user drags the mouse, as long as it's larger 
            // than the width or height of an adorner, respectively.
            double change = Math.Max(args.HorizontalChange, args.VerticalChange);
            adornedElement.Width = Math.Max(adornedElement.Width + change, hitThumb.DesiredSize.Width);
            adornedElement.Height = Math.Max(args.VerticalChange + change, hitThumb.DesiredSize.Height);
        }

        // Handler for resizing from the bottom-left.
        void HandleBottomLeft(object sender, DragDeltaEventArgs args)
        {
            FrameworkElement adornedElement = AdornedElement as FrameworkElement;
            Thumb hitThumb = sender as Thumb;

            if (adornedElement == null || hitThumb == null) return;

            // Ensure that the Width and Height are properly initialized after the resize.
            EnforceSize(adornedElement);

            // Change the size by the amount the user drags the mouse, as long as it's larger 
            // than the width or height of an adorner, respectively.
            double change = Math.Max(args.HorizontalChange, args.VerticalChange);
            adornedElement.Width = Math.Max(adornedElement.Width - args.HorizontalChange, hitThumb.DesiredSize.Width);
            double x = Canvas.GetLeft(adornedElement) + change;
            double y = Canvas.GetTop(adornedElement) + change;
            Canvas.SetLeft(adornedElement, x < 0 ? 0 : x);
            Canvas.SetTop(adornedElement, y < 0 ? 0 : y);
        }

        // Handler for resizing from the top-right.
        void HandleTopRight(object sender, DragDeltaEventArgs args)
        {
            FrameworkElement adornedElement = this.AdornedElement as FrameworkElement;
            Thumb hitThumb = sender as Thumb;

            if (adornedElement == null || hitThumb == null) return;
            FrameworkElement parentElement = adornedElement.Parent as FrameworkElement;

            // Ensure that the Width and Height are properly initialized after the resize.
            EnforceSize(adornedElement);

            // Change the size by the amount the user drags the mouse, as long as it's larger 
            // than the width or height of an adorner, respectively.
            adornedElement.Width = Math.Max(adornedElement.Width + args.HorizontalChange, hitThumb.DesiredSize.Width);
            double y = Canvas.GetTop(adornedElement) + args.VerticalChange;
            Canvas.SetTop(adornedElement, y < 0 ? 0 : y);
            adornedElement.Height = Math.Max(adornedElement.Height - args.VerticalChange, hitThumb.DesiredSize.Height);
        }

        // Handler for resizing from the top-left.
        void HandleTopLeft(object sender, DragDeltaEventArgs args)
        {
            FrameworkElement adornedElement = AdornedElement as FrameworkElement;
            Thumb hitThumb = sender as Thumb;

            if (adornedElement == null || hitThumb == null) return;

            // Ensure that the Width and Height are properly initialized after the resize.
            EnforceSize(adornedElement);

            // Change the size by the amount the user drags the mouse, as long as it's larger 
            // than the width or height of an adorner, respectively.

            double x = Canvas.GetLeft(adornedElement) + args.HorizontalChange;
            double y = Canvas.GetTop(adornedElement) + args.VerticalChange;
            Canvas.SetLeft(adornedElement, x < 0 ? 0 : x);
            Canvas.SetTop(adornedElement, y < 0 ? 0 : y);
            adornedElement.Height = Math.Max(adornedElement.Height - args.VerticalChange, hitThumb.DesiredSize.Height);
            adornedElement.Width = Math.Max(adornedElement.Width - args.HorizontalChange, hitThumb.DesiredSize.Width);
        }

        // Arrange the Adorners.
        protected override Size ArrangeOverride(Size finalSize)
        {
            // desiredWidth and desiredHeight are the width and height of the shape that's being adorned.  
            // These will be used to place the ResizingAdorner at the corners of the adorned shape.  
            double desiredWidth = AdornedElement.DesiredSize.Width;
            double desiredHeight = AdornedElement.DesiredSize.Height;
            // adornerWidth & adornerHeight are used for placement as well.
            double adornerWidth = this.DesiredSize.Width;
            double adornerHeight = this.DesiredSize.Height;

            _topLeft.Arrange(new Rect(-adornerWidth / 2, -adornerHeight / 2, adornerWidth, adornerHeight));
            _topRight.Arrange(new Rect(desiredWidth - adornerWidth / 2, -adornerHeight / 2, adornerWidth, adornerHeight));
            _bottomLeft.Arrange(new Rect(-adornerWidth / 2, desiredHeight - adornerHeight / 2, adornerWidth, adornerHeight));
            _bottomRight.Arrange(new Rect(desiredWidth - adornerWidth / 2, desiredHeight - adornerHeight / 2, adornerWidth, adornerHeight));

            _border.Arrange(new Rect(-2, -2, desiredWidth + 4, desiredHeight + 4));

            // Return the final size.
            return finalSize;
        }

        // Helper method to instantiate the corner Thumbs, set the Cursor property, 
        // set some appearance properties, and add the elements to the visual tree.
        Thumb BuildAdornerCorner(Cursor customizedCursor)
        {
            Thumb thumb = new Thumb();

            // Set some arbitrary visual characteristics.
            thumb.Cursor = customizedCursor;
            thumb.Height = thumb.Width = 12;
            thumb.Opacity = 1;
            thumb.Background = new SolidColorBrush(Colors.Blue);
            thumb.BorderBrush = Brushes.Black;
            thumb.BorderThickness = new Thickness(1);

            _visualChildren.Add(thumb);

            return thumb;
        }

        // This method ensures that the Widths and Heights are initialized.  Sizing to content produces
        // Width and Height values of Double.NaN.  Because this Adorner explicitly resizes, the Width and Height
        // need to be set first.  It also sets the maximum size of the adorned shape.
        void EnforceSize(FrameworkElement adornedElement)
        {
            if (adornedElement.Width.Equals(Double.NaN))
                adornedElement.Width = adornedElement.DesiredSize.Width;
            if (adornedElement.Height.Equals(Double.NaN))
                adornedElement.Height = adornedElement.DesiredSize.Height;

            FrameworkElement parent = adornedElement.Parent as FrameworkElement;
            if (parent != null)
            {
                adornedElement.MaxHeight = parent.ActualHeight;
                adornedElement.MaxWidth = parent.ActualWidth;
            }
        }
        // Override the VisualChildrenCount and GetVisualChild properties to interface with 
        // the adorner's visual collection.
        protected override int VisualChildrenCount { get { return _visualChildren.Count; } }
        protected override Visual GetVisualChild(int index) { return _visualChildren[index]; }
    }
}
