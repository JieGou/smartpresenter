using SmartPresenter.UI.Common;
using SmartPresenter.UI.Controls.ViewModel;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;

namespace SmartPresenter.UI.Controls
{
    /// <summary>
    /// Interaction logic for RibbonControl.xaml
    /// </summary>
    [ViewExport(RegionName = RegionNames.MainToolBarRegion)]
    public sealed partial class RibbonControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RibbonControl"/> class.
        /// </summary>
        public RibbonControl()
        {
            InitializeComponent();
            this.DataContextChanged += RibbonControl_DataContextChanged;
            Initialize();
        }

        /// <summary>
        /// Handles the DataContextChanged event of the RibbonControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private void RibbonControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.CommandBindings.AddRange(ViewModel.CommandBindings);
        }

        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        /// <value>
        /// The view model.
        /// </value>
        [Import]
        RibbonControlViewModel ViewModel
        {
            get
            {
                return (RibbonControlViewModel)this.DataContext;
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

        }
    }
}
