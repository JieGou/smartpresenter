using SmartPresenter.UI.Controls.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace SmartPresenter.UI.Controls.Settings
{
    /// <summary>
    /// Interaction logic for SocialSettingsView.xaml
    /// </summary>
    public partial class SocialSettingsView : UserControl
    {
        public SocialSettingsView()
        {
            InitializeComponent();
            this.Loaded += SocialSettingsView_Loaded;
        }

        private void SocialSettingsView_Loaded(object sender, RoutedEventArgs e)
        {
            FacebookSettingsViewModel _facebookSettingsViewModel = new FacebookSettingsViewModel();
            FacebookTab.DataContext = _facebookSettingsViewModel;
            _facebookSettingsViewModel.WebBrowser = webBrowser;
        }
    }
}
