using SmartPresenter.UI.Common;
using System.Windows.Controls;

namespace SmartPresenter.UI.Main
{
    /// <summary>
    /// Interaction logic for MainPanel.xaml
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainRegion)]
    public partial class MainPanel : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainPanel"/> class.
        /// </summary>
        public MainPanel()
        {
            InitializeComponent();
        }
    }
}
