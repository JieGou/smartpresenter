using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace SmartPresenter.UI.Controls.Media
{
    /// <summary>
    /// Interaction logic for ImageEditor.xaml
    /// </summary>
    [Export("ImageEditorView")]
    public partial class ImageEditorView : UserControl
    {
        public ImageEditorView()
        {
            InitializeComponent();
        }
    }
}
