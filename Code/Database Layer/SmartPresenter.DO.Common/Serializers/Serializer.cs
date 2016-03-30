
using System;
namespace SmartPresenter.Data.Common
{
    /// <summary>
    /// Class responsible for managing serialization/deserialization of objects.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Serializer<T>
    {
        #region Private Data Members

        /// <summary>
        /// Serializer which can be either XML or DataContract at runtime.
        /// </summary>
        private ISerializer<T> _serializer;
        private XmlSerializer<T> _xmlSerializer = new XmlSerializer<T>();
        private DataContractSerializer<T> _dataContractSeriallizer = new DataContractSerializer<T>();

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a new instance of Serializer.
        /// </summary>
        public Serializer()
        {
            _serializer = _xmlSerializer;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Serializes an object to specified loaction.
        /// </summary>
        /// <param name="dataObject">Object to be serialized.</param>
        /// <param name="path">location where object will be saved after serialization.</param>
        public bool Save(T dataObject, string path)
        {
            try
            {
                _serializer.Serialize(dataObject, path);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Deserializes an object from a specified location.
        /// </summary>
        /// <param name="path">location from where object will be loaded.</param>
        /// <returns></returns>
        public T Load(string path)
        {
            return _serializer.Deserialize(path);
        }

        /// <summary>
        /// Switches to XMLSerializer at runtime.
        /// </summary>
        public void SwitchToXmlSerialization()
        {
            _serializer = _xmlSerializer;
        }

        /// <summary>
        /// Switches to DataContractSerializer at runtime.
        /// </summary>
        public void SwitchToDataContractSerialization()
        {
            _serializer = _dataContractSeriallizer;
        }

        #endregion
    }
}
