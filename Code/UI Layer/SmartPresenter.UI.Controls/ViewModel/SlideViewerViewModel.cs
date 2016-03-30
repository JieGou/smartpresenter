using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;
using SmartPresenter.BO.Common.Transitions;
using SmartPresenter.BO.UndoRedo;
using SmartPresenter.UI.Common;
using SmartPresenter.UI.Common.Enums;
using SmartPresenter.UI.Controls.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace SmartPresenter.UI.Controls.ViewModel
{
    /// <summary>
    /// View Model class for SlideViewer
    /// </summary>
    [Export]
    public class SlideViewerViewModel : BindableBase
    {
        #region Private Data Members

        private PresentationView _selectedPresentation;
        private SlideView _selectedSlide;
        private SlideView _activeSlide;
        private ObservableCollection<SlideView> _selectedSlides = new ObservableCollection<SlideView>();
        private SlideViewerMode _mode;
        private Stack<Object> _clipBoard = new Stack<Object>();
        private int _zoomRatio;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SlideViewerViewModel"/> class.
        /// </summary>
        public SlideViewerViewModel()
        {
            Initialize();
        }

        #endregion

        #region Commands

        /// <summary>
        /// Command to add new slide.
        /// </summary>
        public DelegateCommand<Object> AddSlideCommand { get; set; }

        /// <summary>
        /// Command to change the view mode on viewer.
        /// </summary>
        public DelegateCommand<Object> ChangeViewModeCommand { get; set; }

        /// <summary>
        /// Gets or sets the disable slide toggle command.
        /// </summary>
        /// <value>
        /// The disable slide toggle command.
        /// </value>
        public DelegateCommand<Object> DisableSlideToggleCommand { get; set; }

        /// <summary>
        /// Gets or sets the loop to first command.
        /// </summary>
        /// <value>
        /// The loop to first command.
        /// </value>
        public DelegateCommand<Object> LoopToFirstCommand { get; set; }

        /// <summary>
        /// Gets or sets the quick edit command.
        /// </summary>
        /// <value>
        /// The quick edit command.
        /// </value>
        public DelegateCommand<Object> QuickEditCommand { get; set; }

        /// <summary>
        /// Gets or sets the hot key command.
        /// </summary>
        /// <value>
        /// The hot key command.
        /// </value>
        public DelegateCommand<Object> HotKeyCommand { get; set; }

        /// <summary>
        /// Gets or sets the next slide timer command.
        /// </summary>
        /// <value>
        /// The next slide timer command.
        /// </value>
        public DelegateCommand<Object> NextSlideTimerCommand { get; set; }

        /// <summary>
        /// Gets or sets the slide selection changed command.
        /// </summary>
        /// <value>
        /// The slide selection changed command.
        /// </value>
        public DelegateCommand<SelectionChangedEventArgs> SlideSelectionChangedCommand { get; set; }

        #region Command Handlers

        /// <summary>
        /// Handler for AddSlideCommand.
        /// </summary>
        /// <param name="parameter"></param>
        private void AddSlideCommand_Executed(Object parameter)
        {
            if (SelectedPresentation != null)
            {
                SelectedPresentation.AddNewSlide();
            }
        }

        /// <summary>
        /// Handler for ChangeViewModeCommand.
        /// </summary>
        /// <param name="parameter"></param>
        private void ChangeViewModeCommand_Executed(Object parameter)
        {
            if (parameter != null && string.IsNullOrEmpty(parameter.ToString()) == false)
            {
                switch (parameter.ToString())
                {
                    case "Thumbnail":
                        Mode = SlideViewerMode.Thumbnail;
                        break;
                    case "Details":
                        Mode = SlideViewerMode.Details;
                        break;
                    case "Editor":
                        Mode = SlideViewerMode.Editor;
                        break;
                }
            }
        }

        /// <summary>
        /// Disables the slide.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void DisableSlideToggleCommand_Executed(Object parameter)
        {
            SlideView slide = parameter as SlideView;
            if (slide != null)
            {
                slide.IsEnabled = !slide.IsEnabled;
            }
        }

        /// <summary>
        /// Loops to first toggle command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void LoopToFirstCommand_Executed(Object parameter)
        {
            SlideView slide = parameter as SlideView;
            if (slide != null)
            {
                slide.LoopToFirst = !slide.LoopToFirst;
            }
        }

        /// <summary>
        /// Quick edit command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void QuickEditCommand_Executed(Object parameter)
        {
            RaiseQuickEditNotificationRequest();
        }

        /// <summary>
        /// Hots key command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void HotKeyCommand_Executed(Object parameter)
        {
            RaiseHotKeyNotificationRequest();
        }

        /// <summary>
        /// Next slide timer command.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void NextSlideTimerCommand_Executed(Object parameter)
        {
            RaiseNextSlideTimerNotificationRequest();
        }

        /// <summary>
        /// Slides the selection changed command_ executed.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void SlideSelectionChangedCommand_Executed(SelectionChangedEventArgs args)
        {
            foreach (var item in args.AddedItems)
            {
                if (item is SlideView)
                {
                    AddToSelectedItems(item as SlideView);
                }
            }
            foreach (var item in args.RemovedItems)
            {
                if (item is SlideView)
                {
                    RemoveFromSelectedItems(item as SlideView);
                }
            }
        }
        /// <summary>
        /// Handles the Executed event of the ApplicationCommands_Cut control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void ApplicationCommands_Cut_Executed(object sender, ExecutedRoutedEventArgs args)
        {
            _clipBoard.Clear();
            if (this.SelectedSlides != null && this.SelectedSlides.Count > 0)
            {
                ObservableCollection<SlideView> copyOfSlides = new ObservableCollection<SlideView>();
                foreach (SlideView slideView in this.SelectedSlides)
                {
                    copyOfSlides.Add((SlideView)slideView.Clone());
                }
                this.SelectedSlides.ToList().ForEach(item =>
                {
                    this.SelectedPresentation.RemoveSlide(item);
                });
                _selectedSlides.Clear();
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
            if (this.SelectedSlide != null && this.SelectedSlides.Count > 0)
            {
                ObservableCollection<SlideView> copyOfSlides = new ObservableCollection<SlideView>();
                foreach (SlideView slideView in this.SelectedSlides)
                {
                    copyOfSlides.Add((SlideView)slideView.Clone());
                }
                _clipBoard.Push(copyOfSlides);
            }
        }

        /// <summary>
        /// Handles the Executed event of the ApplicationCommands_Paste control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void ApplicationCommands_Paste_Executed(object sender, ExecutedRoutedEventArgs args)
        {
            if (_clipBoard.Count > 0)
            {
                var copiedSlides = _clipBoard.Pop();
                ObservableCollection<SlideView> slides = copiedSlides as ObservableCollection<SlideView>;
                if (slides != null)
                {
                    foreach (SlideView slideView in slides)
                    {
                        this.SelectedPresentation.AddSlide(slideView);
                    }
                }
                else
                {
                    SlideView slide = (SlideView)copiedSlides;
                    this.SelectedPresentation.AddSlide(slide);
                }
                _selectedSlides.Clear();
            }
        }
        /// <summary>
        /// Handles the Executed event of the ApplicationCommands_Delete control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void ApplicationCommands_Delete_Executed(object sender, ExecutedRoutedEventArgs args)
        {
            foreach (SlideView slide in this.SelectedSlides.ToList())
            {
                this.SelectedPresentation.RemoveSlide(this.SelectedPresentation.Slides.FirstOrDefault(item => item.GetInnerObject().Id.Equals(slide.GetInnerObject().Id)));
            }
            _selectedSlides.Clear();
        }
        /// <summary>
        /// Handles the Executed event of the ApplicationCommands_SelectAll control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void ApplicationCommands_SelectAll_Executed(object sender, ExecutedRoutedEventArgs args)
        {

        }
        /// <summary>
        /// Handles the Executed event of the EditorCommands_UnSelectAllCommand control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void EditorCommands_UnSelectAllCommand_Executed(object sender, ExecutedRoutedEventArgs args)
        {

        }
        /// <summary>
        /// Handles the Executed event of the EditorCommands_DeleteAllCommand control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void EditorCommands_DeleteAllCommand_Executed(object sender, ExecutedRoutedEventArgs args)
        {
            foreach (SlideView slide in this.SelectedPresentation.Slides.ToList())
            {
                this.SelectedPresentation.RemoveSlide(this.SelectedPresentation.Slides.FirstOrDefault(item => item.GetInnerObject().Id.Equals(slide.GetInnerObject().Id)));
            }
            _selectedSlides.Clear();
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

        #region Properties

        /// <summary>
        /// Currently selected presentation in slide viewer.
        /// </summary>
        public PresentationView SelectedPresentation
        {
            get
            {
                return _selectedPresentation;
            }
            set
            {
                _selectedPresentation = value;
                OnPropertyChanged("SelectedPresentation");
                OnPropertyChanged("SelectedPresentation.Slides");
            }
        }

        /// <summary>
        /// Currently selected slide on slideviewer.
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
                EventAggregator.GetEvent<SelectedSlideChangedEvent>().Publish(value);
            }
        }

        /// <summary>
        /// Gets or sets the active slide.
        /// </summary>
        /// <value>
        /// The active slide.
        /// </value>
        public SlideView ActiveSlide
        {
            get
            {
                return _activeSlide;
            }
            set
            {
                _activeSlide = value;
                OnPropertyChanged("ActiveSlide");
            }
        }

        /// <summary>
        /// Currently selected slides on slideviewer.
        /// </summary>
        public ObservableCollection<SlideView> SelectedSlides
        {
            get
            {
                return _selectedSlides;
            }
        }

        /// <summary>
        /// Viewer Mode.
        /// </summary>
        public SlideViewerMode Mode
        {
            get
            {
                return _mode;
            }
            set
            {
                _mode = value;
                OnPropertyChanged("Mode");
            }
        }

        /// <summary>
        /// Gets or sets the zoom ratio.
        /// </summary>
        /// <value>
        /// The zoom ratio.
        /// </value>
        public int ZoomRatio
        {
            get
            {
                return _zoomRatio;
            }
            set
            {
                _zoomRatio = value;
                OnPropertyChanged("ZoomRatio");
                SetZoomRatioToPresentation(value);
            }
        }

        /// <summary>
        /// Gets or sets the command bindings.
        /// </summary>
        /// <value>
        /// The command bindings.
        /// </value>
        public CommandBindingCollection CommandBindings { get; set; }

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

        /// <summary>
        /// Gets the quick edit notification request.
        /// </summary>
        /// <value>
        /// The quick edit notification request.
        /// </value>
        public InteractionRequest<INotification> QuickEditNotificationRequest { get; private set; }

        /// <summary>
        /// Gets the hot key notification request.
        /// </summary>
        /// <value>
        /// The hot key notification request.
        /// </value>
        public InteractionRequest<INotification> HotKeyNotificationRequest { get; private set; }

        /// <summary>
        /// Gets the next slide timer notification request.
        /// </summary>
        /// <value>
        /// The next slide timer notification request.
        /// </value>
        public InteractionRequest<INotification> NextSlideTimerNotificationRequest { get; private set; }

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

            this.ZoomRatio = 1;
            Mode = SlideViewerMode.Thumbnail;
        }

        /// <summary>
        /// Creates the commands.
        /// </summary>
        private void CreateCommands()
        {
            AddSlideCommand = new DelegateCommand<Object>(AddSlideCommand_Executed);
            ChangeViewModeCommand = new DelegateCommand<Object>(ChangeViewModeCommand_Executed);
            DisableSlideToggleCommand = new DelegateCommand<object>(DisableSlideToggleCommand_Executed);
            LoopToFirstCommand = new DelegateCommand<object>(LoopToFirstCommand_Executed);
            QuickEditCommand = new DelegateCommand<object>(QuickEditCommand_Executed);
            HotKeyCommand = new DelegateCommand<object>(HotKeyCommand_Executed);
            NextSlideTimerCommand = new DelegateCommand<object>(NextSlideTimerCommand_Executed);
            SlideSelectionChangedCommand = new DelegateCommand<SelectionChangedEventArgs>(SlideSelectionChangedCommand_Executed);


            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Cut, ApplicationCommands_Cut_Executed, ApplicationCommands_CanExecuted));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Copy, ApplicationCommands_Copy_Executed, ApplicationCommands_CanExecuted));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, ApplicationCommands_Paste_Executed, ApplicationCommands_CanExecuted));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Delete, ApplicationCommands_Delete_Executed, ApplicationCommands_CanExecuted));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.SelectAll, ApplicationCommands_SelectAll_Executed, ApplicationCommands_CanExecuted));
            this.CommandBindings.Add(new CommandBinding(EditorCommands.UnSelectAllCommand, EditorCommands_UnSelectAllCommand_Executed, ApplicationCommands_CanExecuted));
            this.CommandBindings.Add(new CommandBinding(EditorCommands.DeleteAllCommand, EditorCommands_DeleteAllCommand_Executed, ApplicationCommands_CanExecuted));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Undo, ApplicationCommands_Undo_Executed, ApplicationCommands_CanExecuted));
            this.CommandBindings.Add(new CommandBinding(ApplicationCommands.Redo, ApplicationCommands_Redo_Executed, ApplicationCommands_CanExecuted));

            QuickEditNotificationRequest = new InteractionRequest<INotification>();
            HotKeyNotificationRequest = new InteractionRequest<INotification>();
            NextSlideTimerNotificationRequest = new InteractionRequest<INotification>();
        }

        /// <summary>
        /// Initializes the events.
        /// </summary>
        private void InitializeEvents()
        {
            EventAggregator.GetEvent<SelectedPresentationChangedEvent>().Subscribe(UpdateSelectedPresentation);
            EventAggregator.GetEvent<NextSlideTimerUpdatedEvent>().Subscribe(UpdateNextSlideTimer);
            EventAggregator.GetEvent<HotKeyUpdatedEvent>().Subscribe(UpdateHotKey);
            EventAggregator.GetEvent<TransitionChangedEvent>().Subscribe(TransitionChanged);
        }

        /// <summary>
        /// Sets the zoom ratio to presentation.
        /// </summary>
        /// <param name="zoomValue">The zoom value.</param>
        private void SetZoomRatioToPresentation(int zoomValue)
        {
            if (this.SelectedPresentation != null)
            {
                this.SelectedPresentation.SetZoomRatioToSlides(zoomValue);
            }
        }

        /// <summary>
        /// Raises the quick edit notification request.
        /// </summary>
        private void RaiseQuickEditNotificationRequest()
        {
            this.QuickEditNotificationRequest.Raise(
               new Notification { Content = "Notification Message", Title = "Notification", AreMinMaxButtonEnabled = false });
        }

        /// <summary>
        /// Raises the hot key notification request.
        /// </summary>
        private void RaiseHotKeyNotificationRequest()
        {
            this.HotKeyNotificationRequest.Raise(
               new Notification { Content = "Notification Message", Title = "Hot Key", AreMinMaxButtonEnabled = false });
        }

        /// <summary>
        /// Raises the next slide timer notification request.
        /// </summary>
        private void RaiseNextSlideTimerNotificationRequest()
        {
            this.NextSlideTimerNotificationRequest.Raise(
               new Notification { Content = "Notification Message", Title = "Next Slide Timer", AreMinMaxButtonEnabled = false });
        }

        /// <summary>
        /// Updates the selected presentation.
        /// </summary>
        /// <param name="newPresentationView">The new presentation view.</param>
        private void UpdateSelectedPresentation(PresentationView newPresentationView)
        {
            SelectedPresentation = newPresentationView ?? SelectedPresentation;
        }

        /// <summary>
        /// Updates the next slide timer.
        /// </summary>
        /// <param name="slideTimer">The slide timer.</param>
        private void UpdateNextSlideTimer(SlideTimer slideTimer)
        {
            foreach (SlideView slide in this.SelectedSlides)
            {
                slide.DelayBeforeNextSlide = TimeSpan.FromSeconds(slideTimer.NextSlideDelay);
                slide.LoopToFirst = slideTimer.LoopToFirst;
            }
        }

        /// <summary>
        /// Updates the hot key.
        /// </summary>
        /// <param name="hotKey">The hot key.</param>
        private void UpdateHotKey(Object hotKey)
        {
            this.SelectedSlide.HotKey = (char)hotKey;
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
        /// Adds to selected items.
        /// </summary>
        /// <param name="slideView">The shape.</param>
        internal void AddToSelectedItems(SlideView slideView)
        {
            _selectedSlides.Add(slideView);
        }

        /// <summary>
        /// Removes from selected items.
        /// </summary>
        /// <param name="shape">The shape.</param>
        internal void RemoveFromSelectedItems(SlideView slideView)
        {
            _selectedSlides.Remove(slideView);
        }

        #endregion

        #region Public Methods


        #endregion

        #endregion
    }
}
