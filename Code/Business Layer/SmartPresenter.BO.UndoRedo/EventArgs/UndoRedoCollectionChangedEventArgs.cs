using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPresenter.BO.UndoRedo
{
    public class UndoRedoCollectionChangedEventArgs : UndoRedoEventArgs
    {
        #region Constructor

        public UndoRedoCollectionChangedEventArgs()
        {

        }

        public UndoRedoCollectionChangedEventArgs(Object target, string propertyName, IList collection, NotifyCollectionChangedEventArgs args)
        {
            Target = target;
            PropertyName = propertyName;
            Collection = collection;
            Action = args.Action;
            OldItems = args.OldItems;
            NewItems = args.NewItems;
        }

        public UndoRedoCollectionChangedEventArgs(Object target, string propertyName, IList collection, NotifyCollectionChangedAction action, IList oldItems, IList newItems)
        {
            Target = target;
            PropertyName = propertyName;
            Collection = collection;
            Action = action;
            OldItems = oldItems;
            NewItems = newItems;
        }

        #endregion

        #region Properties

        public IList Collection { get; set; }
        public NotifyCollectionChangedAction Action { get; set; }
        public IList OldItems { get; set; }
        public IList NewItems { get; set; }

        #endregion
    }
}
