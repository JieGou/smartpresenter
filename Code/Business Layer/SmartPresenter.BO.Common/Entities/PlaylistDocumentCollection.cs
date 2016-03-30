using SmartPresenter.Common;
using SmartPresenter.Data.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SmartPresenter.BO.Common.Entities
{
    [DataContract(Namespace="")]
    public class PlaylistDocumentCollection
    {
        #region Constants

        private const string Library_Playlist_Path = "Library.spp";

        #endregion

        #region Private Data Members

        // Singleton instance.
        private static PlaylistDocumentCollection _instance;
        // Synchronization lock.
        private static volatile Object _lockObject = new Object();

        #endregion

        #region Constructor

        private PlaylistDocumentCollection()
        {
            PlaylistDocuments = new List<PlaylistDocument>();
        }

        #endregion

        #region Singleton Implementation

        /// <summary>
        /// Single Instance of GeneralSettings.
        /// </summary>
        public static PlaylistDocumentCollection Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObject)
                    {
                        // Try to load the last saved object from Disk.
                        if (_instance == null)
                        {
                            _instance = Load();
                        }
                        // If Can't load then create a new one.
                        if (_instance == null)
                        {
                            _instance = new PlaylistDocumentCollection();
                        }
                    }
                }

                return _instance;
            }
        }

        #endregion

        #region Properties

        [DataMember]
        public List<PlaylistDocument> PlaylistDocuments { get; set; }        

        #endregion

        #region Methods

        private static PlaylistDocumentCollection Load()
        {
            Serializer<PlaylistDocumentCollection> serializer = new Serializer<PlaylistDocumentCollection>();
            PlaylistDocumentCollection documentCollection = null;
            try
            {
                documentCollection = serializer.Load(Path.Combine(ApplicationData.Instance.DocumentPlaylistsFolderPath, Library_Playlist_Path));
            }
            catch (Exception ex)
            {
                documentCollection = new PlaylistDocumentCollection();
            }

            return documentCollection;
        }

        public void Save()
        {
            Serializer<PlaylistDocumentCollection> serializer = new Serializer<PlaylistDocumentCollection>();

            serializer.Save(this, Path.Combine(ApplicationData.Instance.DocumentPlaylistsFolderPath, Library_Playlist_Path));          
        }

        #endregion

    }
}
