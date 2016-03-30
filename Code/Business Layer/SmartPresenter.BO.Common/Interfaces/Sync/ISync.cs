namespace SmartPresenter.BO.Common.Interfaces.Sync
{	
	public interface ISync  : ISyncDown, ISyncUp
	{
		void SyncAll();

		void SyncAudioLibrary();

		void SyncMediaLibrary();

		void SyncPresentationLibrary();

		void SyncSettings();

		void SyncAllUp();

		void SyncAllDown();

	}
}

