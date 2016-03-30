using SmartPresenter.Common.Logger;
using System;
using System.IO;
using System.Xml.Serialization;

namespace SmartPresenter.Data.Common
{
    /// <summary>
    /// Class for serializing/deserializing objects using XmlSerializer.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class XmlSerializer<T> : ISerializer<T>
    {
        #region Public Methods

        /// <summary>
        /// Serializes specified object to specified location using XmlSerializer.
        /// </summary>
        /// <param name="dataObject">Object to be serialized</param>
        /// <param name="path">location where object will be saved</param>
        public bool Serialize(T dataObject, string path)
        {
            Logger.LogEntry();

            if (dataObject == null)
            {
                throw new ArgumentNullException("dataObject", "Value to be saved can't be null");
            }

            try
            {
                using (FileStream writer = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                    namespaces.Add(String.Empty, String.Empty);
                    xmlSerializer.Serialize(writer, dataObject, namespaces);
                }
                Logger.LogExit();
                return true;
            }
            catch (Exception ex)
            {
                Logger.LogMsg.Error("Error serializing file : " + Path.GetFileNameWithoutExtension(path), ex);
                Logger.LogExit();
                return false;
            }
        }

        /// <summary>
        /// Deserializes specified object to specified location using XmlSerializer.
        /// </summary>
        /// <param name="path">location where object will be saved</param>
        /// <returns>Object loaded from specified location</returns>
        public T Deserialize(string path)
        {
            Logger.LogEntry();

            if (File.Exists(path) == false)
            {
                throw new FileNotFoundException("File does not exist", Path.GetFileName(path));
            }

            T dataObject = default(T);
            try
            {
                using (FileStream stream = new FileStream(path, FileMode.Open))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                    dataObject = (T)xmlSerializer.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                Logger.LogMsg.Error("Error de-serializing file : " + Path.GetFileNameWithoutExtension(path), ex);
            }
            Logger.LogExit();
            return dataObject;
        }

        #endregion
    }
}
