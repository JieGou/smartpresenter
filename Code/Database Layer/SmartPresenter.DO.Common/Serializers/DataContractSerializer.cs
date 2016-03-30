using SmartPresenter.Common.Logger;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Security;
using System.ServiceModel;

namespace SmartPresenter.Data.Common
{
    /// <summary>
    /// Class for serializing/deserializing objects using DataContractSerializer.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataContractSerializer<T> : ISerializer<T>
    {
        #region Public Methods

        /// <summary>
        /// Serializes specified object to specified location using DataContractSerializer.
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
                    DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                    serializer.WriteObject(writer, dataObject);
                }
                return true;
            }
            catch (Exception ex)
            {
                if (ex is ArgumentNullException || ex is ArgumentException || ex is NotSupportedException || ex is FileNotFoundException || ex is IOException || ex is SecurityException
                    || ex is DirectoryNotFoundException || ex is UnauthorizedAccessException || ex is PathTooLongException || ex is ArgumentOutOfRangeException)
                {
                    Logger.LogMsg.Error("file error", ex);
                }
                else if (ex is InvalidDataContractException || ex is SerializationException || ex is QuotaExceededException)
                {
                    Logger.LogMsg.Error("serialization error", ex);
                }
                return false;
            }
            finally
            {
                Logger.LogExit();
            }
        }

        /// <summary>
        /// Deserializes specified object to specified location using DataContractSerializer.
        /// </summary>
        /// <param name="path">location where object will be saved</param>
        /// <returns>Object loaded from specified location</returns>
        public T Deserialize(string path)
        {
            Logger.LogEntry();

            T dataObject = default(T);

            if (File.Exists(path) == false)
            {
                throw new FileNotFoundException("File does not exist", Path.GetFileName(path));
            }

            try
            {
                using (FileStream reader = new FileStream(path, FileMode.Open, FileAccess.Read))
                {
                    DataContractSerializer serializer = new DataContractSerializer(typeof(T));
                    dataObject = (T)serializer.ReadObject(reader);
                }
            }
            catch (Exception ex)
            {
                Logger.LogMsg.Error("file does not exist", ex);
            }
            finally
            {
                Logger.LogExit();
            }
            return dataObject;
        }

        #endregion
    }
}
