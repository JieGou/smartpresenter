using SmartPresenter.UI.Common;
using SmartPresenter.UI.Controls.ViewModel;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;

namespace SmartPresenter.UI.Controls
{
    /// <summary>
    /// Interaction logic for LibraryTab.xaml
    /// </summary>
    [ViewExport(RegionName = RegionNames.LibraryTabRegion)]
    public sealed partial class LibraryTab : UserControl
    {
        /// <summary>
        /// Creates a new LibraryTab.
        /// </summary>
        public LibraryTab()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        /// <value>
        /// The view model.
        /// </value>
        [Import]
        LibraryTabViewModel ViewModel
        {
            get
            {
                return (LibraryTabViewModel)this.DataContext;
            }
            set
            {
                this.DataContext = value;
            }
        }

        /// <summary>
        /// Handles the Unloaded event of the LibraryTab control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void LibraryTab_Unloaded(object sender, RoutedEventArgs e)
        {
            if (ViewModel != null)
            {
                ViewModel.UpdateBusinessObject();
            }
        }
    }
}
