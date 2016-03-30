using System;

namespace SmartPresenter.Service.LocalSync
{
    public class TransferEntity
    {
        #region Constructor

        internal TransferEntity(Guid userId, Guid libraryId, Guid fileId, string fileName, SyncFileType fileType)
        {
            UserId = userId;
            LibraryId = libraryId;
            FileId = fileId;
            FileName = fileName;
            FileType = fileType;
        }

        #endregion

        #region Properties

        public Guid UserId { get; set; }
        public Guid LibraryId { get; set; }
        public Guid FileId { get; set; }
        public string FileName { get; set; }
        public SyncFileType FileType { get; set; }

        #endregion
    }
}