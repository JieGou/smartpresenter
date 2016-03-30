using System;
using System.ComponentModel;

namespace SmartPresenter.UI.Common
{
    /// <summary>
    /// This class is meant to serve as base class for property change notifications.
    /// </summary>
    [Serializable]
    [Obsolete("This class is obsolete, Use BindableBase from Prism instead.")]
    public abstract class ObservableObject : INotifyPropertyChanged
    {
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
    }
}
