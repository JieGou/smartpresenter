using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SmartPresenter.UI.Common
{
    /// <summary>
    /// Interaction logic for SplitButton.xaml
    /// </summary>
    public partial class SplitButton : UserControl
    {
        #region Constructor

        public SplitButton()
        {
            InitializeComponent();
            DataContext = this;
        }

        #endregion  

        #region Dependency Properties

        public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(SplitButton), new PropertyMetadata());
        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(Object), typeof(SplitButton), new PropertyMetadata());

        #endregion

        #region

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

        private void DropDown_Click(object sender, RoutedEventArgs e)
        {
            Button dropDownButton = ((Button)sender);
            if (dropDownButton.ContextMenu != null)
            {
                dropDownButton.ContextMenu.IsOpen = true;
                dropDownButton.ContextMenu.PlacementTarget = (UIElement)dropDownButton.Tag;
            }
        }
    }
}
