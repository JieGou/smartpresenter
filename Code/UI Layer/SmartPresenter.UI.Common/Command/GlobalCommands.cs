using System.Windows.Input;

namespace SmartPresenter.UI.Common
{
    /// <summary>
    /// Editor Commands.
    /// </summary>
    public static class EditorCommands
    {
        #region Globally Accesible Commands

        public static ICommand DeleteAllCommand { get; set; }
        public static ICommand BringForwardCommand { get; set; }
        public static ICommand BringToFrontCommand { get; set; }
        public static ICommand SendBackwardCommand { get; set; }
        public static ICommand SendToBackCommand { get; set; }
        public static ICommand UnSelectAllCommand { get; set; }
        public static ICommand InsertSquareCommand { get; set; }
        public static ICommand InsertRectangleCommand { get; set; }
        public static ICommand InsertCircleCommand { get; set; }
        public static ICommand InsertEllipseCommand { get; set; }
        public static ICommand InsertTriangleCommand { get; set; }
        public static ICommand InsertPolygonCommand { get; set; }
        public static ICommand InsertImageCommand { get; set; }
        public static ICommand InsertVideoCommand { get; set; }
        public static ICommand InsertAudioCommand { get; set; }
        public static ICommand InsertTextCommand { get; set; }

        #endregion

        #region Static Constructor

        static EditorCommands()
        {
            DeleteAllCommand = new RoutedUICommand();
            BringForwardCommand = new RoutedUICommand();
            BringToFrontCommand = new RoutedUICommand();
            SendBackwardCommand = new RoutedUICommand();
            SendToBackCommand = new RoutedUICommand();
            UnSelectAllCommand = new RoutedUICommand();
            InsertSquareCommand = new RoutedUICommand();
            InsertRectangleCommand = new RoutedUICommand();
            InsertCircleCommand = new RoutedUICommand();
            InsertEllipseCommand = new RoutedUICommand();
            InsertTriangleCommand = new RoutedUICommand();
            InsertPolygonCommand = new RoutedUICommand();
            InsertImageCommand = new RoutedUICommand();
            InsertVideoCommand = new RoutedUICommand();
            InsertTextCommand = new RoutedUICommand();
            InsertAudioCommand = new RoutedUICommand();
        }

        #endregion
    }

    /// <summary>
    /// Slide Show Commands.
    /// </summary>
    public static class SlideShowCommands
    {
        public static ICommand StartSlideShowCommand { get; set; }
        public static ICommand PauseSlideShowCommand { get; set; }
        public static ICommand StopSlideShowCommand { get; set; }

        static SlideShowCommands()
        {
            StartSlideShowCommand = new RoutedUICommand();
            PauseSlideShowCommand = new RoutedUICommand();
            StopSlideShowCommand = new RoutedUICommand();
        }
    }
}
