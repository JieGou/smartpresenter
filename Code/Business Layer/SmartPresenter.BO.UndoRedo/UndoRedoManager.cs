using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SmartPresenter.BO.UndoRedo
{
    public sealed class UndoRedoManager
    {
        #region Private Data Members

        private static volatile UndoRedoManager _instance;
        private static Object _lockObject = new Object();
        private List<IUndoRedoCapable> _registeredObjects = new List<IUndoRedoCapable>();
        private Stack<IUndoRedo> _undoStack = new Stack<IUndoRedo>();
        private Stack<IUndoRedo> _redoStack = new Stack<IUndoRedo>();
        private bool _isNestedUndoRedoInProgress;

        #endregion

        #region Constructor

        private UndoRedoManager()
        {

        }

        #endregion

        #region Properties

        public static UndoRedoManager Instance
        {
            get
            {
                if(_instance == null)
                {
                    lock (_lockObject)
                    {
                        if (_instance == null)
                        {
                            _instance = new UndoRedoManager();
                        }
                    }
                }
                return _instance;
            }
        }

        #endregion

        #region Methods

        #region Public Methods

        public void Register(IUndoRedoCapable objectToBeRegistered)
        {
            objectToBeRegistered.UndoRedoPropertyChanged += ObjectToBeRegistered_UndoRedoPropertyChanged;
            _registeredObjects.Add(objectToBeRegistered);
        }

        public void UnRegisterAll()
        {
            foreach(IUndoRedoCapable obj in _registeredObjects)
            {
                obj.UndoRedoPropertyChanged -= ObjectToBeRegistered_UndoRedoPropertyChanged;
            }
        }

        public void Undo()
        {
            if(_undoStack.Count > 0)
            {
                IUndoRedo command = _undoStack.Pop();
                ((IUndoRedoCapable)command.State.Target).UndoRedoPropertyChanged -= ObjectToBeRegistered_UndoRedoPropertyChanged;
                command.Undo();
                ((IUndoRedoCapable)command.State.Target).UndoRedoPropertyChanged += ObjectToBeRegistered_UndoRedoPropertyChanged;
                _redoStack.Push(command);
            }
        }

        public void Redo()
        {
            if(_redoStack.Count > 0)
            {
                IUndoRedo command = _redoStack.Pop();
                ((IUndoRedoCapable)command.State.Target).UndoRedoPropertyChanged -= ObjectToBeRegistered_UndoRedoPropertyChanged;
                command.Redo();
                ((IUndoRedoCapable)command.State.Target).UndoRedoPropertyChanged += ObjectToBeRegistered_UndoRedoPropertyChanged;
            }
        }

        public void StartNestedUndo()
        {            
            _isNestedUndoRedoInProgress = true;
        }

        public void StopNestedUndo()
        {
            _isNestedUndoRedoInProgress = false;
        }

        #endregion

        #region Private Methods

        private void ObjectToBeRegistered_UndoRedoPropertyChanged(object sender, UndoRedoEventArgs args)
        {
            IUndoRedo undoRedoCommand = null;
            if (args is UndoRedoPropertyChangedEventArgs)
            {
                undoRedoCommand = new UndoRedoPropertyChangedCommand();                
            }
            else if(args is UndoRedoCollectionChangedEventArgs)
            {
                undoRedoCommand = new UndoRedoCollectionChangedCommand();                
            }
            undoRedoCommand.State = args;

            if (_isNestedUndoRedoInProgress)
            {
                UndoRedoNestedCommand nestedCommand = null;
                if (_undoStack.Count > 0)
                {
                    nestedCommand = _undoStack.Pop() as UndoRedoNestedCommand;
                    if (nestedCommand != null && nestedCommand.State.Target.Equals(args.Target) == false)
                    {
                        _undoStack.Push(nestedCommand);
                        nestedCommand = null;
                    }
                }
                if (nestedCommand == null)
                {
                    nestedCommand = new UndoRedoNestedCommand();
                }
                nestedCommand.State = args;
                if (nestedCommand.Commands.ContainsKey(args.PropertyName) == false)
                {
                    nestedCommand.Commands[args.PropertyName] = new IUndoRedo[2];
                    nestedCommand.Commands[args.PropertyName][0] = undoRedoCommand;
                }
                else
                {
                    nestedCommand.Commands[args.PropertyName][1] = undoRedoCommand;
                }
                _undoStack.Push(nestedCommand);
            }
            else
            {
                _undoStack.Push(undoRedoCommand);
            }
        }

        #endregion

        #endregion
    }
}
