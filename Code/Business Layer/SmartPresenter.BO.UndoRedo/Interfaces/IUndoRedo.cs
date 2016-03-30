using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPresenter.BO.UndoRedo
{
    public interface IUndoRedo
    {
        #region Properties

        UndoRedoEventArgs State { get; set; }

        #endregion

        #region Methods

        void Undo();
        void Redo();

        #endregion
    }
}
