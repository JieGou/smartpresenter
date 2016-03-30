using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SmartPresenter.BO.UndoRedo
{
    public class UndoRedoPropertyChangedCommand : IUndoRedo
    {
        #region IUndoRedo Members

        #region Methods
        public void Undo()
        {
            PropertyInfo info = State.Target.GetType().GetProperty(State.PropertyName);
            info.SetValue(State.Target, ((UndoRedoPropertyChangedEventArgs)State).OldValue, null);    
        }

        public void Redo()
        {
            PropertyInfo info = State.Target.GetType().GetProperty(State.PropertyName);
            info.SetValue(State.Target, ((UndoRedoPropertyChangedEventArgs)State).NewValue, null);
        }

        #endregion

        #region Properties

        public UndoRedoEventArgs State { get; set; }

        #endregion

        #endregion

        
    }
}
