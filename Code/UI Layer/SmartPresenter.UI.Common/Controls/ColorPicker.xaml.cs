using SmartPresenter.UI.Common.ViewModel;
using System.Windows.Controls;

namespace SmartPresenter.UI.Common.Controls
{
    /// <summary>
    /// Interaction logic for ColorPicker.xaml
    /// </summary>
    public partial class ColorPicker : UserControl
    {
        private ColorPickerViewModel _viewModel;

        public ColorPicker()
        {
            InitializeComponent();

            DataContext = _viewModel = new ColorPickerViewModel();
        }
    }
}
