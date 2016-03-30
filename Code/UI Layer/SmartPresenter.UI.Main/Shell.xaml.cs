using SmartPresenter.UI.Common;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Input;

namespace SmartPresenter.UI.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    [Export(typeof(Shell))]
    public partial class Shell : Window
    {
        public Shell()
        {
            InitializeComponent();
            this.DataContextChanged += Shell_DataContextChanged;
        }

        private void Shell_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.InputBindings.Add(new KeyBinding(SlideShowCommands.StartSlideShowCommand, Key.F5, ModifierKeys.None));
            this.CommandBindings.AddRange(ViewModel.CommandBindings);
        }

        /// <summary>
        /// Sets the view model for Shell.
        /// </summary>
        /// <value>
        /// The view model of Shell.
        /// </value>
        [Import]
        ShellViewModel ViewModel
        {
            get
            {
                return (ShellViewModel)this.DataContext;
            }
            set
            {
                this.DataContext = value;
            }
        }
    }
}
