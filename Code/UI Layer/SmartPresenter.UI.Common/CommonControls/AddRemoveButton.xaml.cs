using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SmartPresenter.UI.Common
{
    /// <summary>
    /// Interaction logic for AddRemoveButton.xaml
    /// </summary>
    public partial class AddRemoveButton : UserControl
    {
        public AddRemoveButton()
        {
            InitializeComponent();
        }

        #region Dependency Properties

        public static readonly DependencyProperty AddCommandProperty = DependencyProperty.Register("AddCommand", typeof(ICommand), typeof(AddRemoveButton), new PropertyMetadata());
        public static readonly DependencyProperty RemoveCommandProperty = DependencyProperty.Register("RemoveCommand", typeof(ICommand), typeof(AddRemoveButton), new PropertyMetadata());
        
        public static readonly DependencyProperty AddCommandParameterProperty = DependencyProperty.Register("AddCommandParameter", typeof(Object), typeof(AddRemoveButton), new PropertyMetadata());
        public static readonly DependencyProperty RemoveCommandParameterProperty = DependencyProperty.Register("RemoveCommandParameter", typeof(Object), typeof(AddRemoveButton), new PropertyMetadata());

        #endregion

        #region Properties

        public ICommand AddCommand
        {
            get
            {
                return (ICommand)GetValue(AddCommandProperty);
            }
            set
            {
                SetValue(AddCommandProperty, value);
            }
        }

        public ICommand RemoveCommand
        {
            get
            {
                return (ICommand)GetValue(RemoveCommandProperty);
            }
            set
            {
                SetValue(RemoveCommandProperty, value);
            }
        }

        public Object AddCommandParameter
        {
            get
            {
                return (Object)GetValue(AddCommandParameterProperty);
            }
            set
            {
                SetValue(AddCommandParameterProperty, value);
            }
        }

        public Object RemoveCommandParameter
        {
            get
            {
                return (Object)GetValue(RemoveCommandParameterProperty);
            }
            set
            {
                SetValue(RemoveCommandParameterProperty, value);
            }
        }  

        #endregion
    }
}
