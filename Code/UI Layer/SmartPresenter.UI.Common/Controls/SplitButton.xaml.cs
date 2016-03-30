using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SmartPresenter.UI.Common
{
    /// <summary>
    /// Interaction logic for SplitButton.xaml
    /// </summary>
    public sealed partial class SplitButton : UserControl
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SplitButton"/> class.
        /// </summary>
        public SplitButton()
        {
            InitializeComponent();
            DataContext = this;
        }

        #endregion

        #region Dependency Properties

        /// <summary>
        /// The command Dependency property
        /// </summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(SplitButton), new PropertyMetadata());
        /// <summary>
        /// The command parameter Dependency property
        /// </summary>
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(Object), typeof(SplitButton), new PropertyMetadata());

        #endregion

        #region Commands

        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        /// <value>
        /// The command.
        /// </value>
        public ICommand Command
        {
            get
            {
                return (ICommand)GetValue(CommandProperty);
            }
            set
            {
                SetValue(CommandProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the command parameter.
        /// </summary>
        /// <value>
        /// The command parameter.
        /// </value>
        public Object CommandParameter
        {
            get
            {
                return (Object)GetValue(CommandParameterProperty);
            }
            set
            {
                SetValue(CommandParameterProperty, value);
            }
        }

        #endregion

        #region Private Methods

        // To open the contextmenu on click. Tag attribute brings the PlacementTarget through bindings so that we can usee correct DataContext for ContextMenu.
        private void DropDown_Click(object sender, RoutedEventArgs e)
        {
            Button dropDownButton = ((Button)sender);
            if (dropDownButton.ContextMenu != null)
            {
                dropDownButton.ContextMenu.IsOpen = true;
                dropDownButton.ContextMenu.PlacementTarget = (UIElement)dropDownButton.Tag;
            }
        }

        #endregion

    }
}
