using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using SmartPresenter.BO.Common.Interfaces.Sync;
using SmartPresenter.BO.Common.Sync;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPresenter.UI.Controls.ViewModel
{
    [CLSCompliant(false)]
    [Export]
    public class SyncSettingsViewModel : BindableBase
    {
        #region Constructor

        public SyncSettingsViewModel()
        {
            Initialize();
        }

        #endregion

        #region Commands

        public DelegateCommand<Object> SyncCommand { get; set; }

        #region Command Handlers

        private void SyncCommand_Executed(Object parameter)
        {
            ISync syncServer = LocalSyncServer.Instance;
            syncServer.SyncAll();
        }

        #endregion

        #endregion

        #region Methods

        #region Private Methods

        private void Initialize()
        {
            SyncCommand = new DelegateCommand<object>(SyncCommand_Executed);
        }

        #endregion

        #endregion
    }
}
