using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPresenter.BO.UndoRedo
{
    public class UndoRedoNestedCommand : IUndoRedo
    {
        #region Constructor

        public UndoRedoNestedCommand()
        {
            Commands = new Dictionary<string, IUndoRedo[]>();
        }        

        #endregion

        #region IUndoRedo Members
        public UndoRedoEventArgs State { get; set; }

        public Dictionary<string, IUndoRedo[]> Commands { get; set; }

        public void Undo()
        {
            foreach (IUndoRedo[] commands in Commands.Values)
            {
                commands[0].Undo();
            }
            
        }

        public void Redo()
        {
            foreach (IUndoRedo[] commands in Commands.Values)
            {
                commands[1].Redo();
            }
        }

        #endregion
    }
}
