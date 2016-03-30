using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SmartPresenter.UI.RenderEngine.WPF
{
    /// <summary>
    /// Interaction logic for VideoView.xaml
    /// </summary>
    public partial class VideoView : UserControl
    {
        public static readonly DependencyProperty ContextProperty = DependencyProperty.Register("Context", typeof(SmartPresenter.UI.Controls.ViewModel.VideoView), typeof(VideoView), new PropertyMetadata(ContextPropertyChangedCallback));

        public SmartPresenter.UI.Controls.ViewModel.VideoView Context
        {
            get { return (SmartPresenter.UI.Controls.ViewModel.VideoView)this.GetValue(ContextProperty); }
            set { this.SetValue(ContextProperty, value); }
        }

        public VideoView()
        {
            InitializeComponent();            
        }

        private static void ContextPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SmartPresenter.UI.Controls.ViewModel.VideoView value = e.NewValue as SmartPresenter.UI.Controls.ViewModel.VideoView;
            if (value != null)
            {
                VideoView videoView = d as VideoView;
                if (videoView != null)
                {
                    videoView.videoDrawing.Rect = new Rect(value.X, value.Y, value.Width, value.Height);
                    MediaTimeline mediaTimeline = new MediaTimeline(new Uri(value.Path));
                    videoView.videoPlayer.Clock = mediaTimeline.CreateClock();
                    videoView.videoPlayer.Clock.Controller.Begin();
                }
            }
        }  
    }    
}
