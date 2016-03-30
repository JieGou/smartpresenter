
namespace SmartPresenter.Data.Common
{
    /// <summary>
    /// Interface for serialization family of classes.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISerializer<T>
    {
        #region Methods

        /// <summary>
        /// Serializes an object to a specified location.
        /// </summary>
        /// <param name="dataObject">Object to be serialized.</param>
        /// <param name="path">location where object will be saved</param>
        bool Serialize(T dataObject, string path);
        /// <summary>
        /// Deserializes specified object to specified location using XmlSerializer.
        /// </summary>
        /// <param name="path">location where object will be saved</param>
        /// <returns>Object loaded from specified location</returns>
        T Deserialize(string path);

        #endregion
    }
}
