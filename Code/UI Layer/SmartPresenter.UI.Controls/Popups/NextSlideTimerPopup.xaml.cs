using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using SmartPresenter.UI.Controls.ViewModel;
using System;
using System.Windows.Controls;

namespace SmartPresenter.UI.Controls.Popups
{
    /// <summary>
    /// Interaction logic for NextSlideTimerPopup.xaml
    /// </summary>
    public partial class NextSlideTimerPopup : UserControl, IInteractionRequestAware
    {
        private NextSlideTimerPopupViewModel _viewModel;

        public NextSlideTimerPopup()
        {
            InitializeComponent();

            DataContext = _viewModel = new NextSlideTimerPopupViewModel();
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
