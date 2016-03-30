using SmartPresenter.BO.Common;
using SmartPresenter.UI.Common;
using SmartPresenter.UI.Common.Enums;
using SmartPresenter.UI.Controls.ViewModel;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SmartPresenter.UI.Controls.Slide
{
    /// <summary>
    /// Interaction logic for SlideViewer.xaml
    /// </summary>
    [ViewExport(RegionName = RegionNames.ContentRegion)]
    public partial class SlideViewer : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SlideViewer"/> class.
        /// </summary>
        public SlideViewer()
        {
            InitializeComponent();
            this.DataContextChanged += SlideViewer_DataContextChanged;
        }

        /// <summary>
        /// Handles the DataContextChanged event of the SlideViewer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        void SlideViewer_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Initialize();
        }

        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        /// <value>
        /// The view model.
        /// </value>
        [Import]
        SlideViewerViewModel ViewModel
        {
            get
            {
                return (SlideViewerViewModel)this.DataContext;
            }
            set
            {
                this.DataContext = value;
            }
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            this.CommandBindings.AddRange(ViewModel.CommandBindings);

            this.InputBindings.Add(new KeyBinding(ViewModel.ChangeViewModeCommand, Key.E, ModifierKeys.Control)
            {
                CommandParameter = ViewModel.Mode == SlideViewerMode.Editor ? SlideViewerMode.Thumbnail : SlideViewerMode.Editor
            });
        }
    }
}
