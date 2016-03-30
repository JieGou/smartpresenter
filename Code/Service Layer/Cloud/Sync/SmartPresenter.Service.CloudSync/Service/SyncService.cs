using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace SmartPresenter.Service.LocalSync
{
    public abstract class SyncService : ISyncService
    {
        #region Private Data Members

        private volatile bool _isUploading;
        private volatile bool _isDownloading;
        private ReaderWriterLockSlim _uploadLock = new ReaderWriterLockSlim();
        private ReaderWriterLockSlim _downloadLock = new ReaderWriterLockSlim();
        private ConcurrentQueue<TransferEntity> _uploadQueue = new ConcurrentQueue<TransferEntity>();
        private ConcurrentQueue<TransferEntity> _downloadQueue = new ConcurrentQueue<TransferEntity>();
        private IStore _store;

        #endregion

        #region Constructor

        public SyncService(IStore store)
        {
            _store = store;
        }

        #endregion

        #region Methods

        #region Private Methods

        private void Upload()
        {
            _uploadLock.EnterWriteLock();
            _isUploading = true;
            _uploadLock.ExitWriteLock();
            try
            {
                TransferEntity dataItem;
                if (_uploadQueue.TryDequeue(out dataItem) == true)
                {
                    _store.StoreFile(dataItem);
                }
            }
            finally
            {
                _uploadLock.EnterWriteLock();
                _isUploading = false;
                _uploadLock.ExitWriteLock();
            }
        }

        private void Download()
        {
            _downloadLock.EnterWriteLock();
            _isDownloading = true;
            _downloadLock.ExitWriteLock();
            try
            {
                TransferEntity dataItem;
                if (_downloadQueue.TryDequeue(out dataItem) == true)
                {
                    _store.RetrieveFile(dataItem);
                }
            }
            finally
            {
                _downloadLock.EnterWriteLock();
                _isDownloading = false;
                _downloadLock.ExitWriteLock();
            }
        }

        #endregion

        #region ISyncService Members

        public void UploadFile(Guid userId, Guid libraryId, Guid fileId, string fileName, SyncFileType fileType)
        {
            _uploadQueue.Enqueue(new TransferEntity(userId, libraryId, fileId, fileName, fileType));
            _uploadLock.EnterReadLock();
            try
            {
                if (_isUploading == false)
                {
                    Task.Factory.StartNew(() =>
                    {
                        Upload();
                    });
                }
            }
            finally
            {
                _uploadLock.ExitReadLock();
            }
        }

        public void DownloadFile(Guid userId, Guid libraryId, Guid fileId, string fileName, SyncFileType fileType)
        {
            _downloadQueue.Enqueue(new TransferEntity(userId, libraryId, fileId, fileName, fileType));
            _downloadLock.EnterReadLock();
            try
            {
                if (_isDownloading == false)
                {
                    Task.Factory.StartNew(() =>
                    {
                        Download();
                    });
                }
            }
            finally
            {
                _downloadLock.ExitReadLock();
            }
        }

        #endregion

        #endregion        
    }
}