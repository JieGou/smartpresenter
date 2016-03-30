using SmartPresenter.UI.Controls.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;

namespace SmartPresenter.UI.Controls.Media
{
    /// <summary>
    /// Interaction logic for VideoEditorTimeline.xaml
    /// </summary>
    public partial class VideoEditorTimeline : UserControl
    {
        #region Private Data Members

        private VideoEditorTimelineViewModel _viewModel;

        #endregion

        #region Constructor

        public VideoEditorTimeline()
        {
            InitializeComponent();
            DataContextChanged += VideoEditorTimeline_DataContextChanged;
            DataContext = _viewModel = new VideoEditorTimelineViewModel();
        }

        #endregion

        #region Dependency Properties

        public TimeSpan TimelinePosition
        {
            get { return (TimeSpan)this.GetValue(TimelinePositionProperty); }
            set { this.SetValue(TimelinePositionProperty, value); }
        }

        public static readonly DependencyProperty TimelinePositionProperty = DependencyProperty.Register("TimelinePosition", typeof(TimeSpan), typeof(VideoEditorTimeline), new PropertyMetadata(TimeSpan.FromSeconds(0)));

        #endregion

        #region Methods

        #region Private Methods

        private void VideoEditorTimeline_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            _viewModel.MainGrid = mainGrid;
        }

        #endregion

        #endregion

    }
}
