using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPresenter.BO.UndoRedo
{
    public interface IUndoRedo
    {
        #region Methods

        void Undo();
        void Redo();

        #endregion
    }
}
