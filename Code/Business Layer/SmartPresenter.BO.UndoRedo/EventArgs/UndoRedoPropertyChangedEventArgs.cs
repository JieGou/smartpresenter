using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPresenter.BO.UndoRedo
{
    public class UndoRedoPropertyChangedEventArgs : UndoRedoEventArgs
    {
        #region Constructor

        public UndoRedoPropertyChangedEventArgs()
        {

        }

        public UndoRedoPropertyChangedEventArgs(Object target, string propertyName, Object oldValue, Object newValue)
        {
            Target = target;
            PropertyName = propertyName;
            OldValue = oldValue;
            NewValue = newValue;
        }

        #endregion

        #region Properties

        public Object OldValue { get; set; }
        public Object NewValue { get; set; }

        #endregion
    }
}
