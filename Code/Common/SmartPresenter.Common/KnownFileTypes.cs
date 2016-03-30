using System;
using System.IO;

namespace SmartPresenter.Common
{
    /// <summary>
    /// Class to find out/examine types known to SmartPresenter.
    /// </summary>
    public sealed class KnownFileTypes
    {
        #region Constants

        private readonly string SMART_PRESENTER_DOCUMENT_EXT = ".spd";

        #endregion

        #region Private Members

        // Single Instance.
        private static KnownFileTypes _instance;

        // Synchronization lock.
        private static volatile object _lockObject = new object();

        #endregion

        #region Constructor

        /// <summary>
        /// Creates an object of KnownFileTypes.
        /// </summary>
        private KnownFileTypes()
        {
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Single Instance.
        /// </summary>
        public static KnownFileTypes Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObject)
                    {
                        if (_instance == null)
                        {
                            _instance = new KnownFileTypes();
                        }
                    }
                }

                return _instance;
            }
        }

        /// <summary>
        /// Default extension for SmartPresenter Documents.
        /// </summary>
        public string DocumentExtension
        {
            get
            {
                return SMART_PRESENTER_DOCUMENT_EXT;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// To Check if a file is SmartPresenter Document.
        /// </summary>
        /// <param name="fileName">file to be tested</param>
        /// <returns>true if file is a SmartPresenter Document, false otherwise</returns>
        public bool IsSmartPresenterDocFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException("Argument \"fileName\" is null or empty");
            }

            fileName = Path.GetFileName(fileName);
            if (fileName.EndsWith(SMART_PRESENTER_DOCUMENT_EXT, System.StringComparison.Ordinal))
                return true;
            return false;
        }

        #endregion
    }
}
