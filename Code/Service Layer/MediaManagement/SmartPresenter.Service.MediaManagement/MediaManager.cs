using SmartPresenter.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SmartPresenter.Service.MediaManagement
{
    public static class MediaManager
    {
        #region Data Members

        private static SynchronizedCollection<string> _allMediaFiles = new SynchronizedCollection<string>();

        #endregion

        #region Methods

        #region Public Static Methods

        public static SynchronizedCollection<string> GetAllMediaFiles()
        {
            return _allMediaFiles;
        }

        public static void CreateMediaFilesList()
        {
            foreach (string drive in Environment.GetLogicalDrives())
            {
                ProcessDirectory(drive);
            }
        }

        #endregion

        #region Private Static Methods

        private static void ProcessDirectory(string directory)
        {
            try
            {
                string[] allFiles = Directory.GetFiles(directory).Where(f => MediaHelper.ValidVideoExtensios.Contains(new FileInfo(f).Extension.ToLower())).ToArray();
                foreach (string file in allFiles)
                {
                    _allMediaFiles.Add(file);
                }

                string[] allDirectories = Directory.GetDirectories(directory);
                foreach (string dir in allDirectories)
                {
                    ProcessDirectory(dir);
                }
            }
            catch (Exception e)
            {
                
            }
        }

        #endregion

        #endregion
    }
}
