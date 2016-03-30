using SmartPresenter.UI.Common;
using System.Windows.Controls;

namespace SmartPresenter.UI.Controls
{
    /// <summary>
    /// Interaction logic for DocumentViewer.xaml
    /// </summary>
    [ViewExport(RegionName = RegionNames.DocumentContainerRegion)]
    public sealed partial class DocumentContainer : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentContainer"/> class.
        /// </summary>
        public DocumentContainer()
        {
            InitializeComponent();
        }
    }
}
