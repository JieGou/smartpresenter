using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Win32;
using SmartPresenter.BO.Common.Entities;
using SmartPresenter.BO.Common.Enums;
using SmartPresenter.BO.Common.Interfaces;
using SmartPresenter.BO.Common.Transitions;
using SmartPresenter.BO.UndoRedo;
using SmartPresenter.Common;
using SmartPresenter.Common.Enums;
using SmartPresenter.UI.Common;
using SmartPresenter.UI.Common.Interfaces;
using SmartPresenter.UI.Controls.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SmartPresenter.UI.Controls.ViewModel
{
    /// <summary>
    /// View Model class for Slide Editor.
    /// </summary>
    [CLSCompliant(false)]
    public class SlideEditorViewModel : BindableBase
    {

        #region Private Data Members

        private SlideView _selectedSlide;
        private ShapeView _selectedElement;
        private ObservableCollection<ShapeView> _selectedElements = new ObservableCollection<ShapeView>();
        private Stack<Object> _clipBoard = new Stack<Object>();

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SlideEditorViewModel"/> class.
        /// </summary>
        public SlideEditorViewModel()
        {
            Initialize();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Selected Slide for editing.
        /// </summary>
        public SlideView SelectedSlide
        {
            get
            {
                return _selectedSlide;
            }
            set
            {

                _selectedSlide = value;
                OnPropertyChanged("SelectedSlide");
            }
        }

        /// <summary>
        /// Selected shape on editor.
        /// </summary>
        public ShapeView SelectedElement
        {
            get
            {
                return _selectedElement;
            }
            set
            {
                if (_selectedElement is TextView)
                {
                    ((TextView)SelectedElement).IsInEditingMode = false;
                }
                if (_selectedElement != value)
                {
                    try
                    {
                        OnPropertyChanged("SelectedElement");
                        UpdateAdornerOnSelectedElement(value);
                    }
                    finally
                    {
                        _selectedElement = value;
                        OnPropertyChanged("SelectedElement");
                    }
                }
            }
        }

        /// <summary>
        /// List of selected elements on editor.
        /// </summary>
        public ReadOnlyObservableCollection<ShapeView> SelectedElements
        {
            get
            {
                return new ReadOnlyObservableCollection<ShapeView>(_selectedElements);
            }
        }

        /// <summary>
        /// Gets or sets the add adorner to item.
        /// </summary>
        /// <value>
        /// The add adorner to item.
        /// </value>
        public Action<int> AddAdornerToItem { get; set; }

        /// <summary>
        /// Gets or sets the remove adorner from item.
        /// </summary>
        /// <value>
        /// The remove adorner from item.
        /// </value>
        public Action<int> RemoveAdornerFromItem { get; set; }

        /// <summary>
        /// Gets or sets the remove adorner from all items.
        /// </summary>
        /// <value>
        /// The remove adorner from all items.
        /// </value>
        public Action RemoveAdornerFromAllItems { get; set; }

        /// <summary>
        /// Gets or sets the command bindings.
        /// </summary>
        /// <value>
        /// The command bindings.
        /// </value>
        public CommandBindingCollection CommandBindings { get; set; }

        /// <summary>
        /// Gets or sets the editing canvas.
        /// </summary>
        /// <value>
        /// The editing canvas.
        /// </value>
        public Canvas EditingCanvas { get; set; }

        /// <summary>
        /// Gets the event aggregator.
        /// </summary>
        /// <value>
        /// The event aggregator.
        /// </value>
        private IEventAggregator EventAggregator
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IEventAggregator>();
            }
        }

        #endregion

        #region Commands

        public DelegateCommand<SelectionChangedEventArgs> ShapeSelectionChangedCommand { get; private set; }

        public DelegateCommand<MouseButtonEventArgs> RemoveSelectionCommand { get; private set; }

        #region Command Handlers

        /// <summary>
        /// Shapes the selection changed command_ executed.
        /// </summary>
        /// <param name="args">The <see cref="SelectionChangedEventArgs"/> instance containing the event data.</param>
        private void ShapeSelectionChangedCommand_Executed(SelectionChangedEventArgs args)
        {
            foreach (var item in args.AddedItems)
            {
                if (item is ShapeView)
                {
                    AddToSelectedItems(item as ShapeView);
                }
            }
            foreach (var item in args.RemovedItems)
            {
                if (item is ShapeView)
                {
                    RemoveFromSelectedItems(item as ShapeView);
                }
            }
        }

        /// <summary>
        /// Removes the selection command_ executed.
        /// </summary>
        /// <param name="args">The <see cref="MouseButtonEventArgs"/> instance containing the event data.</param>
        private void RemoveSelectionCommand_Executed(MouseButtonEventArgs args)
        {
            if (SelectedElement is TextView)
            {
                ((TextView)SelectedElement).IsInEditingMode = false;
            }
            SelectedElement = null;
        }

        /// <summary>
        /// Handles the Executed event of the ApplicationCommands_Cut control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void ApplicationCommands_Cut_Executed(object sender, ExecutedRoutedEventArgs args)
        {
            _clipBoard.Clear();
            if (this.SelectedElements != null && this.SelectedElements.Count > 0)
            {
                this.SelectedElements.ToList().ForEach(item =>
                {
                    _clipBoard.Push(item);
                    this.SelectedSlide.RemoveElement(item);
                });
                _selectedElements.Clear();
            }
        }

        /// <summary>
        /// Handles the Executed event of the ApplicationCommands_Copy control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void ApplicationCommands_Copy_Executed(object sender, ExecutedRoutedEventArgs args)
        {
            _clipBoard.Clear();
            if (this.SelectedElements != null && this.SelectedElements.Count > 0)
            {
                this.SelectedElements.ToList().ForEach(item =>
                {
                    _clipBoard.Push(item);
                });
            }
        }

        /// <summary>
        /// Handles the Executed event of the ApplicationCommands_Paste control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void ApplicationCommands_Paste_Executed(object sender, ExecutedRoutedEventArgs args)
        {
            _selectedElements.Clear();
            RemoveAdornerFromAllItems();
            while (_clipBoard.Count > 0)
            {
                // Clone element.
                ShapeView copiedElement = (ShapeView)_clipBoard.Pop();
                ShapeView cloneElement = (ShapeView)copiedElement.Clone();
                // Set new positions for added element.
                cloneElement.X += 10;
                cloneElement.Y += 10;
                // Add new element to slide.
                this.SelectedSlide.AddElement(cloneElement);
                ShapeView newlyAddedElement = this.SelectedSlide.Elements.Last();
                // Add new element to selection.
                _selectedElements.Add(newlyAddedElement);
                // Add adorner to new element.
                //AddAdornerToItem(this.SelectedSlide.Elements.Count - 1);
            }
        }

        private void ApplicationCommands_Undo_Executed(object sender, ExecutedRoutedEventArgs args)
        {
            UndoRedoManager.Instance.Undo();
        }

        private void ApplicationCommands_Redo_Executed(object sender, ExecutedRoutedEventArgs args)
        {
            UndoRedoManager.Instance.Redo();
        }

        /// <summary>
        /// Handles the Executed event of the ApplicationCommands_Delete control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void ApplicationCommands_Delete_Executed(object sender, ExecutedRoutedEventArgs args)
        {
            if (this.SelectedElements != null && this.SelectedElements.Count > 0)
            {
                this.SelectedElements.ToList().ForEach(item =>
                {
                    this.SelectedSlide.RemoveElement(item);
                });
            }
        }

        /// <summary>
        /// Handles the Executed event of the ApplicationCommands_SelectAll control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void ApplicationCommands_SelectAll_Executed(object sender, ExecutedRoutedEventArgs args)
        {
            if (this.SelectedSlide != null && this.SelectedSlide.Elements != null && this.SelectedSlide.Elements.Count > 0)
            {
                this.SelectedSlide.Elements.ToList().ForEach(item =>
                {
                    item.IsSelected = true;
                });
            }
        }

        /// <summary>
        /// Handles the Executed event of the EditorCommands_UnSelectAllCommand control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void EditorCommands_UnSelectAllCommand_Executed(object sender, ExecutedRoutedEventArgs args)
        {
            if (this.SelectedSlide != null && this.SelectedSlide.Elements != null && this.SelectedSlide.Elements.Count > 0)
            {
                this.SelectedSlide.Elements.ToList().ForEach(item =>
                {
                    item.IsSelected = false;
                });
            }
            SelectedElement = null;
            _selectedElements.Clear();
        }

        /// <summary>
        /// Handles the Executed event of the EditorCommands_DeleteAllCommand control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void EditorCommands_DeleteAllCommand_Executed(object sender, ExecutedRoutedEventArgs args)
        {
            if (this.SelectedSlide != null && this.SelectedSlide.Elements != null && this.SelectedSlide.Elements.Count > 0)
            {
                var copyOfSlideElements = this.SelectedSlide.Elements.ToList();
                copyOfSlideElements.ToList().ForEach(item =>
                {
                    this.SelectedSlide.RemoveElement(item);
                });
            }
            SelectedElement = null;
            _selectedElements.Clear();
        }

        /// <summary>
        /// Handles the Executed event of the EditorCommands_BringForwardCommand control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void EditorCommands_BringForwardCommand_Executed(object sender, ExecutedRoutedEventArgs args)
        {
            foreach (ShapeView shapeView in this.SelectedElements.ToList())
            {
                int oldIndex = this.SelectedSlide.Elements.IndexOf(shapeView);
                int newIndex = oldIndex + 1;
                newIndex = newIndex > this.SelectedSlide.Elements.Count ? this.SelectedSlide.Elements.Count : newIndex;
                this.SelectedSlide.MoveElement(oldIndex, newIndex);
            }
        }

        /// <summary>
        /// Handles the Executed event of the EditorCommands_BringToFrontCommand control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void EditorCommands_BringToFrontCommand_Executed(object sender, ExecutedRoutedEventArgs args)
        {
            foreach (ShapeView shapeView in this.SelectedElements.ToList())
            {
                int oldIndex = this.SelectedSlide.Elements.IndexOf(shapeView);
                int newIndex = this.SelectedSlide.Elements.Count - 1;
                newIndex = newIndex > this.SelectedSlide.Elements.Count ? this.SelectedSlide.Elements.Count : newIndex;
                this.SelectedSlide.MoveElement(oldIndex, newIndex);
            }
        }

        /// <summary>
        /// Handles the Executed event of the EditorCommands_SendBackwardCommand control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void EditorCommands_SendBackwardCommand_Executed(object sender, ExecutedRoutedEventArgs args)
        {
            foreach (ShapeView shapeView in this.SelectedElements.ToList())
            {
                int oldIndex = this.SelectedSlide.Elements.IndexOf(shapeView);
                int newIndex = oldIndex - 1;
                newIndex = newIndex < 0 ? 0 : newIndex;
                this.SelectedSlide.MoveElement(oldIndex, newIndex);
            }
        }

        /// <summary>
        /// Handles the Executed event of the EditorCommands_SendToBackCommand control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void EditorCommands_SendToBackCommand_Executed(object sender, ExecutedRoutedEventArgs args)
        {
            foreach (ShapeView shapeView in this.SelectedElements.ToList())
            {
                int oldIndex = this.SelectedSlide.Elements.IndexOf(shapeView);
                int newIndex = 0;
                this.SelectedSlide.MoveElement(oldIndex, newIndex);
            }
        }

        /// <summary>
        /// Handles the CanExecuted event of the ApplicationCommands_CutCopy control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="CanExecuteRoutedEventArgs"/> instance containing the event data.</param>
        private void ApplicationCommands_Cut_Copy_Delete_CanExecuted(object sender, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = _selectedElements.Count > 0 ? true : false;
        }

        /// <summary>
        /// Handles the CanExecuted event of the ApplicationCommands_Paste control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="CanExecuteRoutedEventArgs"/> instance containing the event data.</param>
        private void ApplicationCommands_Paste_CanExecuted(object sender, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = _clipBoard.Count > 0 ? true : false;
        }

        /// <summary>
        /// Handles the CanExecuted event of the ApplicationCommands control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="CanExecuteRoutedEventArgs"/> instance containing the event data.</param>
        private void ApplicationCommands_CanExecuted(object sender, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = true;
        }

        #endregion

        #endregion

        #region Methods

        #region Private Methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            CommandBindings = new CommandBindingCollection();
            CreateCommands();

            InitializeEvents();

            EventAggregator.GetEvent<SelectedPresentationAskedEvent>().Publish(null);
            EventAggregator.GetEvent<SelectedSlideAskedEvent>().Publish(null);

            _selectedElements.CollectionChanged += SelectedElements_CollectionChanged;
        }

        /// <summary>
        /// Initializes the events.
        /// </summary>
        private void InitializeEvents()
        {
            EventAggregator.GetEvent<SelectedSlideChangedEvent>().Subscribe(UpdateSelectedSlide);
            EventAggregator.GetEvent<InsertObjectInEditorEvent>().Subscribe(InsertObjectInEditor);
            EventAggregator.GetEvent<FontFamilyUpdatedEvent>().Subscribe(FontFamilyUpdated);
            EventAggregator.GetEvent<FontSizeUpdatedEvent>().Subscribe(FontSizeUpdated);
            EventAggregator.GetEvent<FontSizeIncreasedEvent>().Subscribe(FontSizeIncreased);
            EventAggregator.GetEvent<FontSizeDecreasedEvent>().Subscribe(FontSizeDecreased);
            EventAggregator.GetEvent<FontBoldAppliedEvent>().Subscribe(FontBoldApplied);
            EventAggregator.GetEvent<FontItalicAppliedEvent>().Subscribe(FontItalicApplied);
            EventAggregator.GetEvent<FontUnderlineAppliedEvent>().Subscribe(FontUnderlineApplied);
            EventAggregator.GetEvent<FontStrikeoutEvent>().Subscribe(FontStrikeoutApplied);
            EventAggregator.GetEvent<TextAlignmentChangedEvent>().Subscribe(TextAlignmentChanged);
            EventAggregator.GetEvent<VerticalTextAlignmentChangedEvent>().Subscribe(VerticalTextAlignmentChanged);
            EventAggregator.GetEvent<SelectedTextColorChangedEvent>().Subscribe(SelectedTextColorChanged);
            EventAggregator.GetEvent<TextCaseChangedEvent>().Subscribe(TextCaseChanged);
            EventAggregator.GetEvent<TransitionChangedEvent>().Subscribe(TransitionChanged);
        }

        /// <summary>
        /// Creates the commands.
        /// </summary>
        private void CreateCommands()
        {
            ShapeSelectionChangedCommand = new DelegateCommand<SelectionChangedEventArgs>(ShapeSelectionChangedCommand_Executed);
            RemoveSelectionCommand = new DelegateCommand<MouseButtonEventArgs>(RemoveSelectionCommand_Executed);

            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Cut, ApplicationCommands_Cut_Executed, ApplicationCommands_Cut_Copy_Delete_CanExecuted));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Copy, ApplicationCommands_Copy_Executed, ApplicationCommands_Cut_Copy_Delete_CanExecuted));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, ApplicationCommands_Paste_Executed, ApplicationCommands_Paste_CanExecuted));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Delete, ApplicationCommands_Delete_Executed, ApplicationCommands_Cut_Copy_Delete_CanExecuted));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.SelectAll, ApplicationCommands_SelectAll_Executed, ApplicationCommands_CanExecuted));
            this.CommandBindings.Add(new CommandBinding(EditorCommands.UnSelectAllCommand, EditorCommands_UnSelectAllCommand_Executed, ApplicationCommands_CanExecuted));
            this.CommandBindings.Add(new CommandBinding(EditorCommands.DeleteAllCommand, EditorCommands_DeleteAllCommand_Executed, ApplicationCommands_Cut_Copy_Delete_CanExecuted));
            this.CommandBindings.Add(new CommandBinding(EditorCommands.BringForwardCommand, EditorCommands_BringForwardCommand_Executed, ApplicationCommands_CanExecuted));
            this.CommandBindings.Add(new CommandBinding(EditorCommands.BringToFrontCommand, EditorCommands_BringToFrontCommand_Executed, ApplicationCommands_CanExecuted));
            this.CommandBindings.Add(new CommandBinding(EditorCommands.SendBackwardCommand, EditorCommands_SendBackwardCommand_Executed, ApplicationCommands_CanExecuted));
            this.CommandBindings.Add(new CommandBinding(EditorCommands.SendToBackCommand, EditorCommands_SendToBackCommand_Executed, ApplicationCommands_CanExecuted));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Undo, ApplicationCommands_Undo_Executed, ApplicationCommands_CanExecuted));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Redo, ApplicationCommands_Redo_Executed, ApplicationCommands_CanExecuted));
        }

        /// <summary>
        /// Transitions the data changed.
        /// </summary>
        /// <param name="Transition">The transition data.</param>
        private void TransitionChanged(Transition Transition)
        {
            SelectedSlide.Transition = Transition;
        }

        /// <summary>
        /// Updates the selected slide.
        /// </summary>
        /// <param name="newSlideView">The new slide view.</param>
        private void UpdateSelectedSlide(SlideView newSlideView)
        {
            EventAggregator.GetEvent<SelectedSlideChangedEvent>().Unsubscribe(UpdateSelectedSlide);
            SelectedSlide = newSlideView as SlideView ?? SelectedSlide;
            EventAggregator.GetEvent<SelectedSlideChangedEvent>().Subscribe(UpdateSelectedSlide);
        }

        /// <summary>
        /// Handles the CollectionChanged event of the SelectedElements control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        private void SelectedElements_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (var item in e.NewItems)
                {
                    if (item is ShapeView)
                    {
                        int index = SelectedSlide.Elements.IndexOf(item as ShapeView);
                        if (index >= 0)
                        {
                            _selectedElements.CollectionChanged -= SelectedElements_CollectionChanged;
                            AddAdornerToItem(index);
                            _selectedElements.CollectionChanged += SelectedElements_CollectionChanged;
                        }
                    }
                }
            }
            if (e.OldItems != null)
            {
                foreach (var item in e.OldItems)
                {
                    if (item is ShapeView)
                    {
                        int index = SelectedSlide.Elements.IndexOf(item as ShapeView);
                        if (index >= 0)
                        {
                            RemoveAdornerFromItem(index);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Adds to selected items.
        /// </summary>
        /// <param name="shape">The shape.</param>
        internal void AddToSelectedItems(ShapeView shape)
        {
            _selectedElements.Add(shape);
        }

        /// <summary>
        /// Removes from selected items.
        /// </summary>
        /// <param name="shape">The shape.</param>
        internal void RemoveFromSelectedItems(ShapeView shape)
        {
            _selectedElements.Remove(shape);
        }

        /// <summary>
        /// Inserts the object in editor.
        /// </summary>
        /// <param name="elementType">Type of the element.</param>
        private void InsertObjectInEditor(ElementType elementType)
        {
            IShapeFactory factory;
            int x = (int)(EditingCanvas.ActualWidth / 2);
            int y = (int)(EditingCanvas.ActualHeight / 2);
            switch (elementType)
            {
                case ElementType.Square:
                    factory = new SquareFactory();
                    Square square = (Square)factory.CreateElement();
                    square.X = x - square.Width / 2;
                    square.Y = y - square.Height / 2;
                    SelectedSlide.AddElement(square);
                    break;
                case ElementType.Rectangle:
                    factory = new RectangleFactory();
                    Rectangle rectangle = (Rectangle)factory.CreateElement();
                    rectangle.X = x - rectangle.Width / 2;
                    rectangle.Y = y - rectangle.Height / 2;
                    SelectedSlide.AddElement(rectangle);
                    break;
                case ElementType.Circle:
                    factory = new CircleFactory();
                    Circle circle = (Circle)factory.CreateElement();
                    circle.X = x - circle.Width / 2;
                    circle.Y = y - circle.Height / 2;
                    SelectedSlide.AddElement(circle);
                    break;
                case ElementType.Ellipse:
                    factory = new EllipseFactory();
                    Ellipse ellipse = (Ellipse)factory.CreateElement();
                    ellipse.X = x - ellipse.Width / 2;
                    ellipse.Y = y - ellipse.Height / 2;
                    SelectedSlide.AddElement(ellipse);
                    break;
                case ElementType.Triangle:
                    break;
                case ElementType.Polygon:
                    break;
                case ElementType.Image:
                    InsertImage();
                    break;
                case ElementType.Video:
                    InsertVideo();
                    break;
                case ElementType.Audio:
                    InsertAudio();
                    break;
                case ElementType.Text:
                    factory = new TextFactory();
                    Text text = (Text)factory.CreateElement();
                    text.X = x - text.Width / 2;
                    text.Y = y - text.Height / 2;
                    SelectedSlide.AddElement(text);
                    break;
            }

            this.RemoveAdornerFromAllItems();
            this.AddAdornerToItem(this.SelectedSlide.Elements.Count - 1);
        }

        /// <summary>
        /// Inserts an image onto slide.
        /// </summary>
        private void InsertImage()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Filter = "Image files (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png";
            fileDialog.ShowDialog();
            if (fileDialog.FileNames != null && fileDialog.FileNames.Count() > 0)
            {
                IShapeFactory imageFactory = new ImageFactory();
                fileDialog.FileNames.ToList().ForEach(file =>
                    {
                        try
                        {
                            if (MediaHelper.IsValidImageFile(file))
                            {
                                SmartPresenter.BO.Common.Entities.Image image = (SmartPresenter.BO.Common.Entities.Image)imageFactory.CreateElement(file);
                                image.X = (int)((EditingCanvas.ActualWidth - 100) / 2);
                                image.Y = (int)((EditingCanvas.ActualHeight - 100) / 2);
                                SelectedSlide.AddElement(image);
                            }
                        }
                        catch (ArgumentException)
                        {
                            ServiceLocator.Current.GetInstance<IInteractionService>().ShowMessageBox("Error adding Image", "Unexpected error occured while importing Image.", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    });
            }
        }

        /// <summary>
        /// Inserts a video object onto slide.
        /// </summary>
        private void InsertVideo()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Filter = "Video files(*.wmv, *.mov, *.mpg, *.mp4)|*.wmv;*.mov;*.mpg;*.mp4";
            fileDialog.ShowDialog();
            if (fileDialog.FileNames != null && fileDialog.FileNames.Count() > 0)
            {
                IShapeFactory videoFactory = new VideoFactory();
                fileDialog.FileNames.ToList().ForEach(file =>
                {
                    try
                    {
                        if (MediaHelper.IsValidVideoFile(file))
                        {
                            SmartPresenter.BO.Common.Entities.Video video = (SmartPresenter.BO.Common.Entities.Video)videoFactory.CreateElement(file);
                            video.X = (int)((EditingCanvas.ActualWidth - 100) / 2);
                            video.Y = (int)((EditingCanvas.ActualHeight - 100) / 2);
                            SelectedSlide.AddElement(video);
                        }
                    }
                    catch (ArgumentException)
                    {
                        ServiceLocator.Current.GetInstance<IInteractionService>().ShowMessageBox("Error adding Video", "Unexpected error occured while importing Video.", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                });
            }
        }

        /// <summary>
        /// Inserts an audio object onto slide.
        /// </summary>
        private void InsertAudio()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Filter = "Audio files(*.wma, *.mp3)|*.wma;*.mp3";
            fileDialog.ShowDialog();
            if (fileDialog.FileNames != null && fileDialog.FileNames.Count() > 0)
            {
                IShapeFactory audioFactory = new AudioFactory();
                fileDialog.FileNames.ToList().ForEach(file =>
                {
                    try
                    {
                        if (MediaHelper.IsValidAudioFile(file))
                        {
                            SmartPresenter.BO.Common.Entities.Audio audio = (SmartPresenter.BO.Common.Entities.Audio)audioFactory.CreateElement(file);
                            audio.X = (int)((EditingCanvas.ActualWidth - 100) / 2);
                            audio.Y = (int)((EditingCanvas.ActualHeight - 100) / 2);
                            SelectedSlide.AddElement(audio);
                        }
                    }
                    catch (ArgumentException)
                    {
                        ServiceLocator.Current.GetInstance<IInteractionService>().ShowMessageBox("Error adding Audio", "Unexpected error occured while importing Audio.", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                });
            }
        }

        private void FontFamilyUpdated(FontFamily fontFamily)
        {
            if (fontFamily != null)
            {
                TextView textElement = SelectedElement as TextView;
                if (textElement != null)
                {
                    textElement.FontFamily = fontFamily;
                }
            }
        }

        private void FontSizeUpdated(int fontSize)
        {
            TextView textElement = SelectedElement as TextView;
            if (textElement != null)
            {
                textElement.FontSize = fontSize;
            }
        }

        private void FontSizeIncreased(int fontSize)
        {
            TextView textElement = SelectedElement as TextView;
            if (textElement != null)
            {
                textElement.FontSize += fontSize;
            }
        }

        private void FontSizeDecreased(int fontSize)
        {
            TextView textElement = SelectedElement as TextView;
            if (textElement != null)
            {
                textElement.FontSize -= fontSize;
            }
        }

        private void FontBoldApplied(Object parameter)
        {
            TextView textElement = SelectedElement as TextView;
            if (textElement != null)
            {
                textElement.IsBold = !textElement.IsBold;
            }
        }

        private void FontItalicApplied(Object parameter)
        {
            TextView textElement = SelectedElement as TextView;
            if (textElement != null)
            {
                textElement.IsItalic = !textElement.IsItalic;
            }
        }

        private void FontUnderlineApplied(Object parameter)
        {
            TextView textElement = SelectedElement as TextView;
            if (textElement != null)
            {
                textElement.IsUnderline = !textElement.IsUnderline;
            }
        }

        private void FontStrikeoutApplied(Object parameter)
        {
            TextView textElement = SelectedElement as TextView;
            if (textElement != null)
            {
                textElement.IsStrikeout = !textElement.IsStrikeout;
            }
        }

        private void TextAlignmentChanged(System.Windows.TextAlignment textAlignment)
        {
            TextView textElement = SelectedElement as TextView;
            if (textElement != null)
            {
                textElement.TextAlignment = textAlignment;
            }
        }

        private void VerticalTextAlignmentChanged(System.Windows.VerticalAlignment verticalAlignment)
        {
            TextView textElement = SelectedElement as TextView;
            if (textElement != null)
            {
                textElement.VerticalAlignment = verticalAlignment;
            }
        }

        private void SelectedTextColorChanged(Brush color)
        {
            TextView textElement = SelectedElement as TextView;
            if (textElement != null)
            {
                textElement.Color = color;
            }
        }

        private void TextCaseChanged(string parameter)
        {
            TextView textElement = SelectedElement as TextView;
            if (textElement != null)
            {
                textElement.ChangeCase(parameter);
            }
        }

        /// <summary>
        /// Updates the adorner on selected element.
        /// </summary>
        /// <param name="newSelectedElement">The new selected element.</param>
        private void UpdateAdornerOnSelectedElement(ShapeView newSelectedElement)
        {
            if (newSelectedElement != null)
            {
                int index;
                if (_selectedElement != null)
                {
                    index = SelectedSlide.Elements.IndexOf(_selectedElement);
                    if (index >= 0)
                    {
                        RemoveAdornerFromItem(index);
                    }
                }
                index = SelectedSlide.Elements.IndexOf(newSelectedElement);
                if (index >= 0)
                {
                    AddAdornerToItem(index);
                }
            }
            else
            {
                RemoveAdornerFromAllItems();
            }
        }

        #endregion

        #endregion

    }
}
