using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using SmartPresenter.UI.Controls.ViewModel;
using System;
using System.Windows.Controls;

namespace SmartPresenter.UI.Controls.Popups
{
    /// <summary>
    /// Interaction logic for HotKeyPopup.xaml
    /// </summary>
    public partial class HotKeyPopup : UserControl, IInteractionRequestAware
    {
        private HotKeyPopupViewModel _viewModel;

        public HotKeyPopup()
        {
            InitializeComponent();
            DataContext = _viewModel = new HotKeyPopupViewModel();
        }

        /// <summary>
        /// Gets or sets the finish interaction.
        /// </summary>
        /// <value>
        /// The finish interaction.
        /// </value>
        public Action FinishInteraction
        {
            get
            {
                return _viewModel.FinishInteraction;
            }
            set
            {
                _viewModel.FinishInteraction = value;
            }
        }

        /// <summary>
        /// Gets or sets the notification.
        /// </summary>
        /// <value>
        /// The notification.
        /// </value>
        public INotification Notification { get; set; }
    }
}
