using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPresenter.BO.UndoRedo
{
    public delegate void UndoRedoEventHandler(Object sender, UndoRedoEventArgs args);
    public interface IUndoRedoCapable
    {
        #region Events

        event UndoRedoEventHandler UndoRedoPropertyChanged;
        
        #endregion
    }
}
