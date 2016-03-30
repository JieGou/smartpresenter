using SmartPresenter.BO.Common.Entities;
using SmartPresenter.BO.Common.Interfaces;
using SmartPresenter.Common;
using SmartPresenter.Common.Extensions;
using SmartPresenter.Common.Logger;
using SmartPresenter.Data.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using System.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SmartPresenter.BO.Common
{
    /// <summary>
    /// Class to store General Application settings.
    /// </summary>
    [DataContract]
    public sealed class GeneralSettings : SettingsBase<GeneralSettings>, IXmlSerializable
    {
        #region Private Data Members
        
        #endregion

        #region Public Constructor

        /// <summary>
        /// Creates an instance of class.
        /// </summary>
        public GeneralSettings()
        {
            Initialize();
        }

        #endregion

        #region Public Propreties      

        /// <summary>
        /// List of Libraries.
        /// </summary>
        [DataMember]
        public List<PresentationLibrary> PresentationLibraries { get; set; }

        /// <summary>
        /// Gets or sets the selected presentation library.
        /// </summary>
        /// <value>
        /// The selected presentation library.
        /// </value>
        [DataMember]
        public PresentationLibrary SelectedPresentationLibrary { get; set; }

        /// <summary>
        /// Gets or sets the media libraries.
        /// </summary>
        /// <value>
        /// The media libraries.
        /// </value>
        [DataMember]
        public List<MediaLibrary> MediaLibraries { get; set; }

        /// <summary>
        /// Gets or sets the selected media library.
        /// </summary>
        /// <value>
        /// The selected media library.
        /// </value>
        [DataMember]
        public MediaLibrary SelectedMediaLibrary { get; set; }

        /// <summary>
        /// Gets or sets the audio libraries.
        /// </summary>
        /// <value>
        /// The audio libraries.
        /// </value>
        public List<AudioLibrary> AudioLibraries { get; set; }

        /// <summary>
        /// Gets or sets the selected audio library.
        /// </summary>
        /// <value>
        /// The selected audio library.
        /// </value>
        public AudioLibrary SelectedAudioLibrary { get; set; }

        #endregion        

        #region Methods

        #region Private Methods

        /// <summary>
        /// Initializes GeneralSettings object.
        /// </summary>
        private void Initialize()
        {
            Logger.LogEntry();

            PresentationLibraries = new List<PresentationLibrary>();
            MediaLibraries = new List<MediaLibrary>();
            AudioLibraries = new List<AudioLibrary>();

            Logger.LogExit();
        }

        #endregion

        #region Public Methods

        public override void Save(string path)
        {
            Logger.LogEntry();

            Serializer<GeneralSettings> serializer = new Serializer<GeneralSettings>();
            serializer.Save(this, path);

            Logger.LogExit();
        }

        public override GeneralSettings Load(string path)
        {
            Logger.LogEntry();

            try
            {
                Serializer<GeneralSettings> serializer = new Serializer<GeneralSettings>();
                GeneralSettings generalSettings = serializer.Load(path);
                if (Verify(generalSettings) == false)
                {
                    generalSettings = new GeneralSettings();
                }
                return generalSettings;                
            }
            catch (FileNotFoundException)
            {
                return null;
            }
            finally
            {                
                Logger.LogExit();
            }
        }

        /// <summary>
        /// Verifies the specified general settings are valid or not.
        /// </summary>
        /// <param name="generalSettings">The general settings.</param>
        /// <returns></returns>
        private static bool Verify(GeneralSettings generalSettings)
        {
            Logger.LogEntry();

            bool result = true;

            //if (generalSettings != null && generalSettings.PresentationLibrary != null)
            //{
            //    List<PresentationLibrary> itemsToBeRemoved = new List<PresentationLibrary>();
            //    foreach (PresentationLibrary library in generalSettings.PresentationLibrary)
            //    {
            //        if (String.IsNullOrEmpty(library.Location) == true || Directory.Exists(library.Location) == false)
            //        {
            //            itemsToBeRemoved.Add(library);
            //        }
            //    }

            //    itemsToBeRemoved.ForEach(item => generalSettings.PresentationLibrary.Remove(item));
            //    result = true;
            //}

            Logger.LogExit();

            return result;
        }

        #endregion        

        #region IXmlSerializable Members

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            Logger.LogEntry();

            reader.Read();

            if (reader.Name.Equals("PresentationLibraries"))
            {
                reader.Read();
                while (reader.Name.Equals("PresentationLibrary"))
                {
                    Guid Id = reader["Id"].ToGuid();
                    string name = reader["Name"].ToSafeString();
                    string location = reader["Location"].ToSafeString();
                    this.PresentationLibraries.Add(new PresentationLibrary(location, name));
                    reader.Read();
                }
            }

            if (reader.Name.Equals("MediaLibraries") == false)
            {
                reader.Read();

                if (reader.Name.Equals("SelectedPresentationLibrary"))
                {
                    string name = reader["Name"].ToSafeString();
                    string location = reader["Location"].ToSafeString();
                    SelectedPresentationLibrary = this.PresentationLibraries.FirstOrDefault(l => l.Name.Equals(name) && l.Location.Equals(location));                   
                }

                reader.Read();
            }

            if (reader.Name.Equals("MediaLibraries"))
            {
                reader.Read();
                while (reader.Name.Equals("MediaLibrary"))
                {
                    Guid id = reader["Id"].ToGuid();
                    string name = reader["Name"].ToSafeString();
                    string location = reader["Location"].ToSafeString();
                    string path = System.IO.Path.Combine(location, name);
                    this.MediaLibraries.Add(MediaLibrary.Load(path));
                    reader.Read();
                }
            }

            if (reader.Name.Equals("AudioLibraries") == false)
            {
                reader.Read();
                if (reader.Name.Equals("SelectedMediaLibrary"))
                {
                    Guid id = reader["Id"].ToGuid();
                    string name = reader["Name"].ToSafeString();
                    string location = reader["Location"].ToSafeString();
                    string path = System.IO.Path.Combine(location, name);
                    SelectedMediaLibrary = this.MediaLibraries.FirstOrDefault(l => l.Name.Equals(name) && l.Location.Equals(location));
                }

                reader.Read();
            }

            if (reader.Name.Equals("AudioLibraries"))
            {
                reader.Read();
                while (reader.Name.Equals("AudioLibrary"))
                {
                    Guid id = reader["Id"].ToGuid();
                    string name = reader["Name"].ToSafeString();
                    this.AudioLibraries.Add(AudioLibrary.Load(name));
                    reader.Read();
                }
            }

            reader.Read();
            if (reader.Name.Equals("SelectedAudioLibrary"))
            {
                Guid id = reader["Id"].ToGuid();
                string name = reader["Name"].ToSafeString();
                SelectedAudioLibrary = AudioLibrary.Load(name);
            }

            reader.Read();

            Logger.LogExit();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("PresentationLibraries");
            foreach (PresentationLibrary library in this.PresentationLibraries)
            {
                writer.WriteStartElement("PresentationLibrary");
                writer.WriteAttributeString("Id", library.Id.ToString());
                writer.WriteAttributeString("Name", library.Name);
                writer.WriteAttributeString("Location", library.Location);
                writer.WriteAttributeString("ItemCount", library.Items.Count.ToString());
                writer.WriteAttributeString("PlaylistCount", library.Playlists.Count.ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            if (SelectedPresentationLibrary != null)
            {
                writer.WriteStartElement("SelectedPresentationLibrary");
                writer.WriteAttributeString("Id", SelectedPresentationLibrary.Id.ToString());
                writer.WriteAttributeString("Name", SelectedPresentationLibrary.Name);
                writer.WriteAttributeString("Location", SelectedPresentationLibrary.Location);
                writer.WriteEndElement();
            }

            writer.WriteStartElement("MediaLibraries");
            foreach (MediaLibrary library in this.MediaLibraries)
            {
                writer.WriteStartElement("MediaLibrary");
                writer.WriteAttributeString("Id", library.Id.ToString());
                writer.WriteAttributeString("Name", library.Name);
                writer.WriteAttributeString("Location", library.Location);
                writer.WriteAttributeString("ItemCount", library.Items.Count.ToString());
                writer.WriteAttributeString("PlaylistCount", library.Playlists.Count.ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            if (SelectedMediaLibrary != null)
            {
                writer.WriteStartElement("SelectedMediaLibrary");
                writer.WriteAttributeString("Id", SelectedMediaLibrary.Id.ToString());
                writer.WriteAttributeString("Name", SelectedMediaLibrary.Name);
                writer.WriteAttributeString("Location", SelectedMediaLibrary.Location);
                writer.WriteEndElement();
            }

            writer.WriteStartElement("AudioLibraries");
            foreach (AudioLibrary library in this.AudioLibraries)
            {
                writer.WriteStartElement("AudioLibrary");
                writer.WriteAttributeString("Id", library.Id.ToString());
                writer.WriteAttributeString("Name", library.Name);
                writer.WriteAttributeString("ItemCount", library.Items.Count.ToString());
                writer.WriteAttributeString("PlaylistCount", library.Playlists.Count.ToString());
                writer.WriteEndElement();
            }
            writer.WriteEndElement();

            if (SelectedAudioLibrary != null)
            {
                writer.WriteStartElement("SelectedAudioLibrary");
                writer.WriteAttributeString("Id", SelectedAudioLibrary.Id.ToString());
                writer.WriteAttributeString("Name", SelectedAudioLibrary.Name);
                writer.WriteEndElement();
            }

        }

        #endregion

        #endregion
        
    }
}
