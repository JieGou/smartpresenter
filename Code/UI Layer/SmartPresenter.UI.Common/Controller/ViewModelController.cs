using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SmartPresenter.UI.Common.Controller
{
    public sealed class ViewModelController
    {
        #region Private Data Members

        private static volatile ViewModelController _instance;
        private static object _lockObject = new Object();
        private Dictionary<Type> _viewModels = new Dictionary<Type>();

        #endregion

        #region Constructor

        private ViewModelController()
        {

        }

        #endregion

        #region Singleton Implementation

        public static ViewModelController Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObject)
                    {
                        if (_instance == null)
                        {
                            _instance = new ViewModelController();
                        }
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Registers listeners to be notified for propertychanged.
        /// </summary>
        /// <param name="viewModel"></param>
        public void Register(IListener viewModel)
        {
            if (_viewModels.ContainsKey(viewModel.GetType()) == false)
            {
                _viewModels.Add(viewModel.GetType(), viewModel);
            }
        }

        /// <summary>
        /// Notifies all the listners.
        /// </summary>
        /// <param name="type">Source of action</param>
        /// <param name="propertyName">property that was changed.</param>
        /// <param name="value">New value of property changed.</param>
        public void Notify(Type type, string propertyName, object value)
        {
            foreach (IListener viewModel in _viewModels.Values)
            {
                if (type != viewModel.GetType())
                {
                    viewModel.Listen(type, propertyName, value);
                }
            }
        }

        /// <summary>
        /// Ask for the value of a specific property on a specific object.
        /// </summary>
        /// <param name="type">The type of object whom we are asking for a property value.</param>
        /// <param name="propertyName">The name of the property whose value is required.</param>
        /// <returns></returns>
        public Object Ask(Type type, string propertyName)
        {
            Object propertyValue = null;

            foreach (IListener viewModel in _viewModels.Values)
            {
                if (type == viewModel.GetType())
                {
                    PropertyInfo property = type.GetProperty(propertyName);
                    if (property != null)
                    {
                        propertyValue = property.GetValue(viewModel);
                    }
                    break;
                }
            }

            return propertyValue;
        }

        #endregion
    }
}
