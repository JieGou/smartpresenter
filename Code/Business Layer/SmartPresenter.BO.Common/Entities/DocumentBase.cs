using System;
using System.IO;
using System.Runtime.Serialization;
namespace SmartPresenter.BO.Common
{
    /// <summary>
    /// Base class for all kind of documents/presentations.
    /// </summary>    
    [DataContract(Namespace = "")]
    public abstract class DocumentBase
    {
        #region Constants

        private const string DEFAULT_DOCUMENT_EXTENSION = ".spd";

        #endregion

        #region Private Data Members        

        #endregion

        #region Constructore

        public DocumentBase()
        {
            ID = Guid.NewGuid();
        }

        #endregion

        #region Properteis
        Guid id;
        [DataMember(Name = "ID")]
        public Guid ID
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }

        /// <summary>
        /// Name of the document.
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Path of file.
        /// </summary>                 
        public string Path
        {
            get
            {
                return System.IO.Path.Combine(ParentLibraryLocation, string.Concat(ID, DEFAULT_DOCUMENT_EXTENSION));                
            }
        }

        /// <summary>
        /// Parent Library of the presentation.
        /// </summary>
        [DataMember]
        public string ParentLibraryLocation
        {
            get;
            set;
        }
        /// <summary>
        /// Type of the document.
        /// </summary>
        public abstract DocumentType DocumentType { get; }

        /// <summary>
        /// Category for a document like Home, College, Office Presentation.
        /// </summary>
        [DataMember]
        public string Category { get; set; }

        #endregion

        #region Abstract Methods

        public abstract void Save();

        #endregion
    }
}
