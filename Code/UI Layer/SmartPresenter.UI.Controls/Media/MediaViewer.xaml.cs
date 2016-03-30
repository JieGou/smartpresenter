using SmartPresenter.UI.Common;
using SmartPresenter.UI.Controls.ViewModel;
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace SmartPresenter.UI.Controls.Media
{
    /// <summary>
    /// Interaction logic for MediaViewer.xaml
    /// </summary>
    [ViewExport(RegionName = RegionNames.MediaViewerRegion)]
    public partial class MediaViewer : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaViewer"/> class.
        /// </summary>
        public MediaViewer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets the view model.
        /// </summary>
        /// <value>
        /// The view model.
        /// </value>
        [Import]
        MediaViewerViewModel ViewModel
        {
            set
            {
                this.DataContext = value;
            }
        }
    }
}
