using SmartPresenter.UI.Common;
using SmartPresenter.UI.Controls.ViewModel;
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace SmartPresenter.UI.Controls
{
    /// <summary>
    /// Interaction logic for SlidesTab.xaml
    /// </summary>
    [ViewExport(RegionName = RegionNames.SlidesTabRegion)]
    public sealed partial class SlidesTab : UserControl
    {
        /// <summary>
        /// Creates a new Slide Tab.
        /// </summary>
        public SlidesTab()
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
        SlidesTabViewModel ViewModel
        {
            set
            {
                this.DataContext = value;
            }
        }
    }
}
