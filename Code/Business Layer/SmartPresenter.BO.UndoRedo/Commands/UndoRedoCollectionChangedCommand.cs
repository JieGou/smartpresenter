using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPresenter.BO.UndoRedo
{
    public class UndoRedoCollectionChangedCommand : IUndoRedo
    {
        #region IUndoRedo Members

        #region Methods
        public void Undo()
        {
            UndoRedoCollectionChangedEventArgs args = State as UndoRedoCollectionChangedEventArgs;
            switch(args.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    UndoAdd();
                    break;
                case NotifyCollectionChangedAction.Remove:
                    UndoRemove();
                    break;
            }
        }

        public void Redo()
        {
            UndoRedoCollectionChangedEventArgs args = State as UndoRedoCollectionChangedEventArgs;
            switch(args.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    RedoAdd();
                    break;
                case NotifyCollectionChangedAction.Remove:
                    RedoRemove();
                    break;
            }
        }

        #endregion

        #region Properties

        public UndoRedoEventArgs State { get; set; }

        #endregion

        #endregion

        #region Private Methods

        private void UndoAdd()
        {
            UndoRedoCollectionChangedEventArgs args = State as UndoRedoCollectionChangedEventArgs;
            foreach (Object item in args.NewItems)
            {
                args.Collection.Remove(item);
            }            
        }

        private void UndoRemove()
        {
            UndoRedoCollectionChangedEventArgs args = State as UndoRedoCollectionChangedEventArgs;
            foreach (Object item in args.OldItems)
            {
                args.Collection.Add(item);
            }
        }

        private void RedoAdd()
        {
            UndoRedoCollectionChangedEventArgs args = State as UndoRedoCollectionChangedEventArgs;
            foreach (Object item in args.NewItems)
            {
                args.Collection.Add(item);
            }
        }

        private void RedoRemove()
        {
            UndoRedoCollectionChangedEventArgs args = State as UndoRedoCollectionChangedEventArgs;
            foreach (Object item in args.OldItems)
            {
                args.Collection.Remove(item);
            }
        }

        #endregion
    }
}
