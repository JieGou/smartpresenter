using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPresenter.BO.UndoRedo
{
    public class UndoRedoEventArgs
    {
        #region Properties

        public Object Target { get; set; }
        public string PropertyName { get; set; }

        #endregion
    }
}
