using Microsoft.Practices.Prism.Commands;
using SmartPresenter.BO.Common;
using SmartPresenter.BO.Common.Entities;
using SmartPresenter.BO.Common.Enums;
using SmartPresenter.Common.Enums;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace SmartPresenter.UI.Controls.ViewModel
{
    /// <summary>
    /// A class for Text Object UI.
    /// </summary>
    [DisplayName("Text")]
    public class TextView : RectangleView
    {

        #region Private Data Members

        private Text _text;
        private FlowDocument _content;
        private FlowDocument _viewerContent;
        private FlowDocument _outputContent;
        private bool _isInEditingMode;
        private FontFamily _fontFamily;
        private double _fontSize;
        private FontWeight _fontWeight;
        private bool _isValidRTF;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TextView"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        public TextView(Text text)
            : base(text)
        {
            _text = text;
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextView"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        public TextView(Shape text)
            : base(text)
        {
            _text = text as Text;
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextView"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        public TextView(Rectangle text)
            : base(text)
        {
            _text = text as Text;
            Initialize();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the font family.
        /// </summary>
        /// <value>
        /// The font family.
        /// </value>
        [Category("Text")]
        public FontFamily FontFamily
        {
            get
            {
                return _fontFamily;
            }
            set
            {
                _fontFamily = value;
                new TextRange(_content.ContentStart, _content.ContentEnd).ApplyPropertyValue(FlowDocument.FontFamilyProperty, value);
                Refresh();
                OnPropertyChanged("FontFamily");
            }
        }

        /// <summary>
        /// Gets or sets the size of the font.
        /// </summary>
        /// <value>
        /// The size of the font.
        /// </value>
        [Category("Text")]
        public double FontSize
        {
            get
            {
                return _fontSize;
            }
            set
            {
                _fontSize = value;
                new TextRange(_content.ContentStart, _content.ContentEnd).ApplyPropertyValue(FlowDocument.FontSizeProperty, value);
                Refresh();
                OnPropertyChanged("FontSize");
            }
        }

        /// <summary>
        /// Gets or sets the font weight.
        /// </summary>
        /// <value>
        /// The font weight.
        /// </value>
        [Category("Text")]
        public FontWeight FontWeight
        {
            get
            {
                return _fontWeight;
            }
            set
            {
                _fontWeight = value;
                OnPropertyChanged("FontWeight");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is bold.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is bold; otherwise, <c>false</c>.
        /// </value>
        [Category("Text")]
        public bool IsBold
        {
            get
            {
                return _text.IsBold;
            }
            set
            {
                _text.IsBold = value;
                new TextRange(_content.ContentStart, _content.ContentEnd).ApplyPropertyValue(FlowDocument.FontWeightProperty, value == true ? FontWeights.Bold : FontWeights.Normal);
                Refresh();
                OnPropertyChanged("IsBold");
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is italic.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is italic; otherwise, <c>false</c>.
        /// </value>
        [Category("Text")]
        public bool IsItalic
        {
            get
            {
                return _text.IsItalic;
            }
            set
            {
                _text.IsItalic = value;
                new TextRange(_content.ContentStart, _content.ContentEnd).ApplyPropertyValue(FlowDocument.FontStyleProperty, value == true ? FontStyles.Italic : FontStyles.Normal);
                Refresh();
                OnPropertyChanged("IsItalic");
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is underline.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is underline; otherwise, <c>false</c>.
        /// </value>
        [Category("Text")]
        public bool IsUnderline
        {
            get
            {
                return _text.IsUnderline;
            }
            set
            {
                _text.IsUnderline = value;
                foreach (Block block in _content.Blocks)
                {
                    if (block is Paragraph)
                    {
                        if (value == true)
                        {
                            ((Paragraph)block).TextDecorations.Add(TextDecorations.Underline);
                        }
                        else
                        {
                            ((Paragraph)block).TextDecorations.Remove(TextDecorations.Underline[0]);
                        }
                    }
                }
                Refresh();
                OnPropertyChanged("IsUnderline");
            }
        }
        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        [Category("Brush")]
        public Brush Color
        {
            get
            {
                return _text.Color;
            }
            set
            {
                _text.Color = value;
                new TextRange(_content.ContentStart, _content.ContentEnd).ApplyPropertyValue(FlowDocument.ForegroundProperty, value);
                OnPropertyChanged("Color");
                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the alignment.
        /// </summary>
        /// <value>
        /// The alignment.
        /// </value>
        [Category("Text")]
        public System.Windows.TextAlignment TextAlignment
        {
            get
            {
                return _text.Alignment;
            }
            set
            {
                _text.Alignment = value;
                new TextRange(_content.ContentStart, _content.ContentEnd).ApplyPropertyValue(FlowDocument.TextAlignmentProperty, value);
                Refresh();
                OnPropertyChanged("Alignment");
            }
        }

        /// <summary>
        /// Gets or sets the vertical alignment.
        /// </summary>
        /// <value>
        /// The vertical alignment.
        /// </value>
        public VerticalAlignment VerticalAlignment
        {
            get
            {
                return _text.VerticalAlignment;
            }
            set
            {
                _text.VerticalAlignment = value;
                OnPropertyChanged("VerticalAlignment");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is strikeout.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is strikeout; otherwise, <c>false</c>.
        /// </value>
        public bool IsStrikeout
        {
            get
            {
                return _text.IsStrikeout;
            }
            set
            {
                _text.IsStrikeout = value;
                foreach (Block block in _content.Blocks)
                {
                    if (block is Paragraph)
                    {
                        if (value == true)
                        {
                            ((Paragraph)block).TextDecorations.Add(TextDecorations.Strikethrough);
                        }
                        else
                        {
                            ((Paragraph)block).TextDecorations.Remove(TextDecorations.Strikethrough[0]);
                        }
                    }
                }
                Refresh();
                OnPropertyChanged("IsStrikeout");
            }
        }

        /// <summary>
        /// Gets or sets the text content.
        /// </summary>
        /// <value>
        /// The text content.
        /// </value>
        [Category("Text")]
        public string RTFContent
        {
            get
            {
                return _text.RTFContent;
            }
            set
            {
                _text.RTFContent = value;
                _isValidRTF = false;
                OnPropertyChanged("RTFContent");
                OnPropertyChanged("Content");
            }
        }

        /// <summary>
        /// Gets or sets the text content of the text object.
        /// </summary>
        /// <value>
        /// The text content.
        /// </value>
        public string TextContent
        {
            get
            {
                _text.TextContent = FlowDocumentHelper.GetPlainText(_content);
                return _text.TextContent;
            }
        }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        [Category("Text")]
        public FlowDocument Content
        {
            get
            {
                if (_content == null || _isValidRTF == false)
                {
                    _content = new FlowDocument();
                    if (string.IsNullOrEmpty(RTFContent) == false)
                    {
                        TextRange textRange = new TextRange(_content.ContentStart, _content.ContentEnd);
                        using (MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(RTFContent)))
                        {
                            textRange.Load(stream, System.Windows.DataFormats.Rtf);
                        }
                    }
                    _isValidRTF = true;
                }
                return _content;
            }
            set
            {
                _content = value;
            }
        }

        /// <summary>
        /// Gets or sets the content of the viewer.
        /// </summary>
        /// <value>
        /// The content of the viewer.
        /// </value>
        [Category("Text")]
        public FlowDocument ViewerContent
        {
            get
            {
                _viewerContent = CloneFlowDocument();
                double fontSize = this.FontSize * ZoomRatio / 6;
                new TextRange(_viewerContent.ContentStart, _viewerContent.ContentEnd).ApplyPropertyValue(FlowDocument.FontSizeProperty, fontSize);
                return _viewerContent;
            }
            set
            {
                _viewerContent = value;
            }
        }

        /// <summary>
        /// Gets or sets the content of the output.
        /// </summary>
        /// <value>
        /// The content of the output.
        /// </value>
        [Category("Text")]
        public FlowDocument OutputContent
        {
            get
            {
                _outputContent = CloneFlowDocument();
                return _outputContent;
            }
            set
            {
                _outputContent = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is in editing mode.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is in editing mode; otherwise, <c>false</c>.
        /// </value>
        public bool IsInEditingMode
        {
            get
            {
                return _isInEditingMode;
            }
            set
            {
                _isInEditingMode = value;
                OnPropertyChanged(() => IsInEditingMode);
            }
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public override ElementType Type
        {
            get
            {
                return ElementType.Text;
            }
        }

        #endregion

        #region Commands

        public DelegateCommand<MouseButtonEventArgs> InitiateEditModeCommand { get; set; }
        public DelegateCommand InitiateViewModeCommand { get; set; }

        #region Command Handlers

        /// <summary>
        /// Initiates the edit mode command_ executed.
        /// </summary>
        private void InitiateEditModeCommand_Executed(MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                IsInEditingMode = true;
            }
        }

        /// <summary>
        /// Initiates the view mode command_ executed.
        /// </summary>
        private void InitiateViewModeCommand_Executed()
        {
            IsInEditingMode = false;
        }

        #endregion

        #endregion

        #region Methods

        #region Private Methods

        /// <summary>
        /// Creates the commands.
        /// </summary>
        private void CreateCommands()
        {
            InitiateEditModeCommand = new DelegateCommand<MouseButtonEventArgs>(InitiateEditModeCommand_Executed);
            InitiateViewModeCommand = new DelegateCommand(InitiateViewModeCommand_Executed);
        }

        private FlowDocument CloneFlowDocument()
        {
            FlowDocument clonedFlowDocument = new FlowDocument();
            if (Content != null)
            {
                TextRange range = new TextRange(Content.ContentStart, Content.ContentEnd);
                MemoryStream stream = new MemoryStream();
                System.Windows.Markup.XamlWriter.Save(range, stream);
                range.Save(stream, DataFormats.XamlPackage);
                TextRange range2 = new TextRange(clonedFlowDocument.ContentEnd, clonedFlowDocument.ContentEnd);
                range2.Load(stream, DataFormats.XamlPackage);
            }
            return clonedFlowDocument;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            CreateCommands();

            this._fontSize = 40;
            this._fontFamily = new FontFamily("Times New Roman");

            this.PropertyChanged += TextView_PropertyChanged;
        }

        private void TextView_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "FontFamily":
                case "FontSize":
                    Refresh();
                    break;
            }
        }

        #endregion

        #region Public Methods

        private string GetRTF()
        {
            string rtf = string.Empty;
            if (_content != null)
            {
                var textRange = new TextRange(_content.ContentStart, _content.ContentEnd);

                if (textRange.CanSave(DataFormats.Rtf))
                {
                    using (var stream = new MemoryStream())
                    {
                        textRange.Save(stream, DataFormats.Rtf);
                        stream.Seek(0, SeekOrigin.Begin);
                        using (StreamReader streamReader = new StreamReader(stream))
                        {
                            rtf = streamReader.ReadToEnd();
                        }
                    }
                }
            }
            return rtf;
        }

        private void Refresh()
        {
            RTFContent = GetRTF();
        }

        internal void ChangeCase(string parameter)
        {
            TextRange textRange = new TextRange(_content.ContentStart, _content.ContentEnd);
            switch (parameter)
            {
                case "Sentence":
                    break;
                case "Lowercase":
                    textRange.Text = textRange.Text.ToLower();
                    break;
                case "Uppercase":
                    textRange.Text = textRange.Text.ToUpper();
                    break;
                case "Capitalize":
                    break;
                case "Toggle":
                    break;
            }
        }

        /// <summary>
        /// Gets the inner object.
        /// </summary>
        /// <returns></returns>
        protected internal override Shape GetInnerObjectCloned()
        {
            return (Text)_text.Clone();
        }

        /// <summary>
        /// Clones the internal.
        /// </summary>
        /// <returns></returns>
        protected override object CloneInternal()
        {
            ShapeView elementView = new TextView(this.GetInnerObjectCloned());

            return elementView;
        }

        #endregion

        #endregion

    }
}
