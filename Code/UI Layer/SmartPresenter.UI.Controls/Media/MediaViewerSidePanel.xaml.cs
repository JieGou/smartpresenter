using SmartPresenter.UI.Common;
using SmartPresenter.UI.Controls.ViewModel;
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace SmartPresenter.UI.Controls.Media
{
    /// <summary>
    /// Interaction logic for MediaViewerSidePanel.xaml
    /// </summary>
    [ViewExport(RegionName = RegionNames.SideMediaContainerRegion)]
    public partial class MediaViewerSidePanel : UserControl
    {
        public MediaViewerSidePanel()
        {
            InitializeComponent();
        }

        [Import]
        MediaViewerSidePanelViewModel ViewModel
        {
            get
            {
                return (MediaViewerSidePanelViewModel)this.DataContext;
            }
            set
            {
                this.DataContext = value;
            }
        }
    }
}
