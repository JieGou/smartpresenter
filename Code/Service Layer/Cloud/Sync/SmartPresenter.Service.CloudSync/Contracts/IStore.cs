using System;

namespace SmartPresenter.Service.LocalSync
{
    public interface IStore
    {
        #region Methods

        void StoreFile(TransferEntity transferEntity);
        
        byte[] RetrieveFile(TransferEntity transferEntity);

        #endregion
    }
}
