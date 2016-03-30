using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SmartPresenter.UI.Common
{
    /// <summary>
    /// Interaction logic for AddRemoveButton.xaml
    /// </summary>
    public sealed partial class AddRemoveButton : UserControl
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AddRemoveButton"/> class.
        /// </summary>
        public AddRemoveButton()
        {
            InitializeComponent();
        }

        #endregion

        #region Dependency Properties

        /// <summary>
        /// The add command Dependency property
        /// </summary>
        public static readonly DependencyProperty AddCommandProperty = DependencyProperty.Register("AddCommand", typeof(ICommand), typeof(AddRemoveButton), new PropertyMetadata());
        /// <summary>
        /// The remove command Dependency property
        /// </summary>
        public static readonly DependencyProperty RemoveCommandProperty = DependencyProperty.Register("RemoveCommand", typeof(ICommand), typeof(AddRemoveButton), new PropertyMetadata());
        /// <summary>
        /// The add command parameter Dependency property
        /// </summary>
        public static readonly DependencyProperty AddCommandParameterProperty = DependencyProperty.Register("AddCommandParameter", typeof(Object), typeof(AddRemoveButton), new PropertyMetadata());
        /// <summary>
        /// The remove command parameter Dependency property
        /// </summary>
        public static readonly DependencyProperty RemoveCommandParameterProperty = DependencyProperty.Register("RemoveCommandParameter", typeof(Object), typeof(AddRemoveButton), new PropertyMetadata());

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the add command.
        /// </summary>
        /// <value>
        /// The add command.
        /// </value>
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

        /// <summary>
        /// Gets or sets the remove command.
        /// </summary>
        /// <value>
        /// The remove command.
        /// </value>
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

        /// <summary>
        /// Gets or sets the add command parameter.
        /// </summary>
        /// <value>
        /// The add command parameter.
        /// </value>
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

        /// <summary>
        /// Gets or sets the remove command parameter.
        /// </summary>
        /// <value>
        /// The remove command parameter.
        /// </value>
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
