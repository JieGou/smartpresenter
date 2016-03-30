using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace SmartPresenter.UI.Common.Controls
{
    /// <summary>
    /// Interaction logic for PropertyGrid.xaml
    /// </summary>
    public partial class PropertyGrid : UserControl, INotifyPropertyChanged
    {
        #region Private Data Members

        ObservableCollection<PropertyData> _properties = new ObservableCollection<PropertyData>();

        #endregion

        #region Constructor

        public PropertyGrid()
        {
            InitializeComponent();
            Initialize();
        }

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty DisplayNameProperty = DependencyProperty.Register("DisplayName", typeof(String), typeof(PropertyGrid), new FrameworkPropertyMetadata());
        public String DisplayName
        {
            get { return (String)GetValue(DisplayNameProperty); }
            set { SetValue(DisplayNameProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(Object), typeof(PropertyGrid), new FrameworkPropertyMetadata(SelectedItemChangedCallback));
        public Object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        #endregion

        #region Properties

        public ObservableCollection<PropertyData> Properties
        {
            get
            {
                return _properties;
            }
            set
            {
                _properties = value;
                OnPropertyChanged("Properties");
            }
        }

        #endregion

        #region Methods

        #region Private Methods

        private void Initialize()
        {
            DataContext = this;
        }

        private static void SelectedItemChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            Object selectedItem = e.NewValue;
            PropertyGrid propertyGrid = sender as PropertyGrid;
            if (selectedItem != null && propertyGrid != null)
            {
                propertyGrid.Properties.Clear();
                Type type = selectedItem.GetType();
                List<PropertyData> properties = new List<PropertyData>();
                foreach (PropertyInfo propInfo in type.GetProperties())
                {
                    properties.Add(new PropertyData()
                    {
                        SelectedItem = selectedItem,
                        Name = propInfo.Name,
                        Type = propInfo.PropertyType,
                        IsReadOnly = !propInfo.CanWrite,
                    });
                }
                propertyGrid.Properties = new ObservableCollection<PropertyData>(properties.OrderBy(p => p.Name));
            }
        }

        #endregion

        #endregion

        #region INotifyPropertyChanged

        #region Events

        /// <summary>
        /// PropertyChanged Event to notify any changes to UI.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion        

        #region Public Methods

        /// <summary>
        /// Method which fires PropertyChanged.
        /// </summary>
        /// <param name="propertyName"></param>
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #endregion
    }

    public class PropertyData : ObservableObject
    {
        #region Private Data Members

        private Object _selectedItem;

        private String _name;

        private Type _type;

        private bool _isReadOnly;

        private List<PropertyInfo> _properties = new List<PropertyInfo>();

        #endregion

        #region Properties

        public Object SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                if (_selectedItem is ObservableObject)
                {
                    ((ObservableObject)_selectedItem).PropertyChanged -= PropertyData_PropertyChanged;
                }
                _selectedItem = value;
                OnPropertyChanged("SelectedItem");
                if (value is ObservableObject)
                {
                    ((ObservableObject)value).PropertyChanged += PropertyData_PropertyChanged;
                }
                Type type = value.GetType();
                foreach (PropertyInfo propInfo in type.GetProperties())
                {
                    _properties.Add(propInfo);
                }
            }
        }

        private void PropertyData_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals(this.Name))
            {
                Type type = this.SelectedItem.GetType();
                foreach (PropertyInfo propInfo in type.GetProperties())
                {
                    if (propInfo.Name.Equals(e.PropertyName))
                    {
                        (this.SelectedItem as ObservableObject).PropertyChanged -= PropertyData_PropertyChanged;
                        this.Value = propInfo.GetValue(this.SelectedItem);
                        (this.SelectedItem as ObservableObject).PropertyChanged += PropertyData_PropertyChanged;
                    }
                }
            }
        }

        public String Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public Type Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
                OnPropertyChanged("Type");
            }
        }

        public Object Value
        {
            get
            {
                Type type = SelectedItem.GetType();
                PropertyInfo property = type.GetProperty(this.Name);
                return property.GetValue(SelectedItem);
            }
            set
            {
                if (SelectedItem != null)
                {
                    (this.SelectedItem as ObservableObject).PropertyChanged -= PropertyData_PropertyChanged;
                    Type type = SelectedItem.GetType();
                    PropertyInfo property = type.GetProperty(this.Name);
                    if (property.PropertyType == typeof(int))
                    {
                        property.SetValue(SelectedItem, Convert.ToInt32(value));
                    }
                    else if (property.PropertyType == typeof(float))
                    {
                        property.SetValue(SelectedItem, Convert.ToSingle(value));
                    }
                    else if (property.PropertyType == typeof(double))
                    {
                        property.SetValue(SelectedItem, Convert.ToDouble(value));
                    }
                    else if (property.PropertyType == typeof(Thickness))
                    {
                        property.SetValue(SelectedItem, (Thickness)value);
                    }
                    else if (property.PropertyType == typeof(Brush))
                    {
                        if (value is Brush)
                        {
                            property.SetValue(SelectedItem, (Brush)value);
                        }
                        else if (value is Color)
                        {
                            property.SetValue(SelectedItem, new SolidColorBrush((Color)value));
                        }
                    }
                    else if (property.PropertyType == typeof(TextAlignment))
                    {
                        TextAlignment textAlignment;
                        Enum.TryParse(value.ToString(), out textAlignment);
                        property.SetValue(SelectedItem, textAlignment);
                    }
                    OnPropertyChanged("Value");
                    (this.SelectedItem as ObservableObject).PropertyChanged += PropertyData_PropertyChanged;
                }
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return _isReadOnly;
            }
            set
            {
                _isReadOnly = value;
                OnPropertyChanged("IsReadOnly");
            }
        }

        #endregion
    }
}
