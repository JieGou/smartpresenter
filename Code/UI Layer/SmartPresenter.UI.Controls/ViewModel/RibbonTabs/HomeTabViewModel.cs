using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;
using SmartPresenter.BO.Common.Enums;
using SmartPresenter.Common.Enums;
using SmartPresenter.UI.Common;
using SmartPresenter.UI.Controls.Events;
using System;
using System.Windows.Input;
using System.Windows.Media;

namespace SmartPresenter.UI.Controls.ViewModel
{
    /// <summary>
    /// Class for View Model of Home Tab.
    /// </summary>
    public class HomeTabViewModel : BindableBase
    {

        #region Private Data Members

        private FontFamily _selectedFontFamily;
        private int _selectedFontSize;
        private Brush _selectedTextColor;

        #endregion

        #region Contsructor

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeTabViewModel"/> class.
        /// </summary>
        public HomeTabViewModel()
        {
            Initialize();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the selected font family.
        /// </summary>
        /// <value>
        /// The selected font family.
        /// </value>
        public FontFamily SelectedFontFamily
        {
            get
            {
                return _selectedFontFamily;
            }
            set
            {
                _selectedFontFamily = value;
                OnPropertyChanged("SelectedFontFamily");
                EventAggregator.GetEvent<FontFamilyUpdatedEvent>().Publish(_selectedFontFamily);
            }
        }

        /// <summary>
        /// Gets or sets the size of the selected font.
        /// </summary>
        /// <value>
        /// The size of the selected font.
        /// </value>
        public int SelectedFontSize
        {
            get
            {
                return _selectedFontSize;
            }
            set
            {
                _selectedFontSize = value;
                OnPropertyChanged("SelectedFontSize");
                EventAggregator.GetEvent<FontSizeUpdatedEvent>().Publish(_selectedFontSize);
            }
        }

        /// <summary>
        /// Gets or sets the color of the selected.
        /// </summary>
        /// <value>
        /// The color of the selected.
        /// </value>
        public Brush SelectedTextColor
        {
            get
            {
                return _selectedTextColor;
            }
            set
            {
                _selectedTextColor = value;
                EventAggregator.GetEvent<SelectedTextColorChangedEvent>().Publish(_selectedTextColor);
                OnPropertyChanged("SelectedColor");
            }
        }

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
        /// Gets or sets the command bindings.
        /// </summary>
        /// <value>
        /// The command bindings.
        /// </value>
        public CommandBindingCollection CommandBindings { get; set; }

        #endregion

        #region Commands

        /// <summary>
        /// Gets the font size increased command.
        /// </summary>
        /// <value>
        /// The font size increased command.
        /// </value>
        public DelegateCommand FontSizeIncreasedCommand { get; private set; }

        /// <summary>
        /// Gets the font size decreased command.
        /// </summary>
        /// <value>
        /// The font size decreased command.
        /// </value>
        public DelegateCommand FontSizeDecreasedCommand { get; private set; }

        /// <summary>
        /// Gets the font bold command.
        /// </summary>
        /// <value>
        /// The font bold command.
        /// </value>
        public DelegateCommand FontBoldCommand { get; private set; }

        /// <summary>
        /// Gets the font italic command.
        /// </summary>
        /// <value>
        /// The font italic command.
        /// </value>
        public DelegateCommand FontItalicCommand { get; private set; }

        /// <summary>
        /// Gets the font underline command.
        /// </summary>
        /// <value>
        /// The font underline command.
        /// </value>
        public DelegateCommand FontUnderlineCommand { get; private set; }

        /// <summary>
        /// Gets the font strikeout command.
        /// </summary>
        /// <value>
        /// The font strikeout command.
        /// </value>
        public DelegateCommand FontStrikeoutCommand { get; private set; }

        /// <summary>
        /// Gets the change text alignment command.
        /// </summary>
        /// <value>
        /// The change text alignment command.
        /// </value>
        public DelegateCommand<Object> ChangeTextAlignmentCommand { get; private set; }

        /// <summary>
        /// Gets the change text vertical alignment command.
        /// </summary>
        /// <value>
        /// The change text vertical alignment command.
        /// </value>
        public DelegateCommand<Object> ChangeTextVerticalAlignmentCommand { get; private set; }

        /// <summary>
        /// Gets the change case command.
        /// </summary>
        /// <value>
        /// The change case command.
        /// </value>
        public DelegateCommand<string> ChangeCaseCommand { get; private set; }

        #region Command Handlers

        /// <summary>
        /// Handles the Executed event of the EditorCommands_InsertSquareCommand control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void EditorCommands_InsertSquareCommand_Executed(object sender, ExecutedRoutedEventArgs args)
        {
            EventAggregator.GetEvent<InsertObjectInEditorEvent>().Publish(ElementType.Square);
        }
        /// <summary>
        /// Handles the Executed event of the EditorCommands_InsertRectangleCommand control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void EditorCommands_InsertRectangleCommand_Executed(object sender, ExecutedRoutedEventArgs args)
        {
            EventAggregator.GetEvent<InsertObjectInEditorEvent>().Publish(ElementType.Rectangle);
        }
        /// <summary>
        /// Handles the Executed event of the EditorCommands_InsertCircleCommand control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void EditorCommands_InsertCircleCommand_Executed(object sender, ExecutedRoutedEventArgs args)
        {
            EventAggregator.GetEvent<InsertObjectInEditorEvent>().Publish(ElementType.Circle);
        }
        /// <summary>
        /// Handles the Executed event of the EditorCommands_InsertEllipseCommand control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void EditorCommands_InsertEllipseCommand_Executed(object sender, ExecutedRoutedEventArgs args)
        {
            EventAggregator.GetEvent<InsertObjectInEditorEvent>().Publish(ElementType.Ellipse);
        }
        /// <summary>
        /// Handles the Executed event of the EditorCommands_InsertTriangleCommand control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void EditorCommands_InsertTriangleCommand_Executed(object sender, ExecutedRoutedEventArgs args)
        {
            EventAggregator.GetEvent<InsertObjectInEditorEvent>().Publish(ElementType.Triangle);
        }
        /// <summary>
        /// Handles the Executed event of the EditorCommands_InsertPolygonCommand control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void EditorCommands_InsertPolygonCommand_Executed(object sender, ExecutedRoutedEventArgs args)
        {
            EventAggregator.GetEvent<InsertObjectInEditorEvent>().Publish(ElementType.Polygon);
        }

        /// <summary>
        /// Handles the Executed event of the EditorCommands_InsertImageCommand control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void EditorCommands_InsertImageCommand_Executed(object sender, ExecutedRoutedEventArgs args)
        {
            EventAggregator.GetEvent<InsertObjectInEditorEvent>().Publish(ElementType.Image);
        }

        /// <summary>
        /// Handles the Executed event of the EditorCommands_InsertVideoCommand control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void EditorCommands_InsertVideoCommand_Executed(object sender, ExecutedRoutedEventArgs args)
        {
            EventAggregator.GetEvent<InsertObjectInEditorEvent>().Publish(ElementType.Video);
        }

        private void EditorCommands_InsertAudioCommand_Executed(object sender, ExecutedRoutedEventArgs args)
        {
            EventAggregator.GetEvent<InsertObjectInEditorEvent>().Publish(ElementType.Audio);
        }

        /// <summary>
        /// Handles the Executed event of the EditorCommands_InsertTextCommand control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="ExecutedRoutedEventArgs"/> instance containing the event data.</param>
        private void EditorCommands_InsertTextCommand_Executed(object sender, ExecutedRoutedEventArgs args)
        {
            EventAggregator.GetEvent<InsertObjectInEditorEvent>().Publish(ElementType.Text);
        }

        /// <summary>
        /// Handles the CanExecuted event of the EditorCommands control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The <see cref="CanExecuteRoutedEventArgs"/> instance containing the event data.</param>
        private void EditorCommands_CanExecuted(object sender, CanExecuteRoutedEventArgs args)
        {
            args.CanExecute = true;
        }

        /// <summary>
        /// Fonts the size increased command_ executed.
        /// </summary>
        private void FontSizeIncreasedCommand_Executed()
        {
            EventAggregator.GetEvent<FontSizeIncreasedEvent>().Publish(4);
        }

        /// <summary>
        /// Fonts the size decreased command_ executed.
        /// </summary>
        private void FontSizeDecreasedCommand_Executed()
        {
            EventAggregator.GetEvent<FontSizeDecreasedEvent>().Publish(4);
        }

        /// <summary>
        /// Fonts the bold command_ executed.
        /// </summary>
        private void FontBoldCommand_Executed()
        {
            EventAggregator.GetEvent<FontBoldAppliedEvent>().Publish(null);
        }

        /// <summary>
        /// Fonts the italic command_ executed.
        /// </summary>
        private void FontItalicCommand_Executed()
        {
            EventAggregator.GetEvent<FontItalicAppliedEvent>().Publish(null);
        }

        /// <summary>
        /// Fonts the underline command_ executed.
        /// </summary>
        private void FontUnderlineCommand_Executed()
        {
            EventAggregator.GetEvent<FontUnderlineAppliedEvent>().Publish(null);
        }

        /// <summary>
        /// Fonts the strikeout command_ executed.
        /// </summary>
        private void FontStrikeoutCommand_Executed()
        {
            EventAggregator.GetEvent<FontStrikeoutEvent>().Publish(null);
        }

        /// <summary>
        /// Changes the text alignment command_ executed.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void ChangeTextAlignmentCommand_Executed(Object parameter)
        {
            System.Windows.TextAlignment textAlignment = (System.Windows.TextAlignment)parameter;
            EventAggregator.GetEvent<TextAlignmentChangedEvent>().Publish(textAlignment);
        }

        /// <summary>
        /// Changes the text vertical alignment command_ executed.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void ChangeTextVerticalAlignmentCommand_Executed(Object parameter)
        {
            System.Windows.VerticalAlignment verticalAlignment = (System.Windows.VerticalAlignment)parameter;
            EventAggregator.GetEvent<VerticalTextAlignmentChangedEvent>().Publish(verticalAlignment);
        }

        /// <summary>
        /// Changes the case command_ executed.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void ChangeCaseCommand_Executed(string parameter)
        {
            EventAggregator.GetEvent<TextCaseChangedEvent>().Publish(parameter);
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
            CreateCommandBindings();
            CreateCommands();
        }

        /// <summary>
        /// Creates the command bindings.
        /// </summary>
        private void CreateCommandBindings()
        {
            CommandBindings = new CommandBindingCollection();

            this.CommandBindings.Add(new CommandBinding(EditorCommands.InsertSquareCommand, EditorCommands_InsertSquareCommand_Executed, EditorCommands_CanExecuted));
            this.CommandBindings.Add(new CommandBinding(EditorCommands.InsertCircleCommand, EditorCommands_InsertCircleCommand_Executed, EditorCommands_CanExecuted));
            this.CommandBindings.Add(new CommandBinding(EditorCommands.InsertEllipseCommand, EditorCommands_InsertEllipseCommand_Executed, EditorCommands_CanExecuted));
            this.CommandBindings.Add(new CommandBinding(EditorCommands.InsertPolygonCommand, EditorCommands_InsertPolygonCommand_Executed, EditorCommands_CanExecuted));
            this.CommandBindings.Add(new CommandBinding(EditorCommands.InsertRectangleCommand, EditorCommands_InsertRectangleCommand_Executed, EditorCommands_CanExecuted));
            this.CommandBindings.Add(new CommandBinding(EditorCommands.InsertTriangleCommand, EditorCommands_InsertTriangleCommand_Executed, EditorCommands_CanExecuted));
            this.CommandBindings.Add(new CommandBinding(EditorCommands.InsertImageCommand, EditorCommands_InsertImageCommand_Executed, EditorCommands_CanExecuted));
            this.CommandBindings.Add(new CommandBinding(EditorCommands.InsertVideoCommand, EditorCommands_InsertVideoCommand_Executed, EditorCommands_CanExecuted));
            this.CommandBindings.Add(new CommandBinding(EditorCommands.InsertAudioCommand, EditorCommands_InsertAudioCommand_Executed, EditorCommands_CanExecuted));
            this.CommandBindings.Add(new CommandBinding(EditorCommands.InsertTextCommand, EditorCommands_InsertTextCommand_Executed, EditorCommands_CanExecuted));
        }

        /// <summary>
        /// Creates the commands.
        /// </summary>
        private void CreateCommands()
        {
            FontSizeIncreasedCommand = new DelegateCommand(FontSizeIncreasedCommand_Executed);
            FontSizeDecreasedCommand = new DelegateCommand(FontSizeDecreasedCommand_Executed);
            FontBoldCommand = new DelegateCommand(FontBoldCommand_Executed);
            FontItalicCommand = new DelegateCommand(FontItalicCommand_Executed);
            FontUnderlineCommand = new DelegateCommand(FontUnderlineCommand_Executed);
            FontStrikeoutCommand = new DelegateCommand(FontStrikeoutCommand_Executed);
            ChangeTextAlignmentCommand = new DelegateCommand<Object>(ChangeTextAlignmentCommand_Executed);
            ChangeTextVerticalAlignmentCommand = new DelegateCommand<Object>(ChangeTextVerticalAlignmentCommand_Executed);
            ChangeCaseCommand = new DelegateCommand<string>(ChangeCaseCommand_Executed);
        }

        #endregion

        #region Public Methods



        #endregion

        #endregion

        #region Events



        #endregion

    }
}
