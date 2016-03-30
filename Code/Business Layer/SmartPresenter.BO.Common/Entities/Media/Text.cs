using SmartPresenter.BO.Common.Enums;
using SmartPresenter.Common.Enums;
using SmartPresenter.Common.Extensions;
using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using System.Xml;

namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// Class representing a text object.
    /// </summary>
    [Serializable]
    public class Text : Rectangle
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Text"/> class.
        /// </summary>
        public Text()
        {
            Initialize();
        }

        #endregion

        #region Private Data Members

        private System.Windows.TextAlignment _alignment;
        private VerticalAlignment _verticalAlignment;
        [NonSerialized]
        private Brush _color;
        [NonSerialized]
        private FlowDocument _content;
        private string _textContent;
        private string _rtftContent;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public override ElementType Type
        {
            get { return ElementType.Text; }
        }

        /// <summary>
        /// Gets or sets the font.
        /// </summary>
        /// <value>
        /// The font.
        /// </value>
        public Font Font { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is bold.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is bold; otherwise, <c>false</c>.
        /// </value>
        public bool IsBold { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is strikeout.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is strikeout; otherwise, <c>false</c>.
        /// </value>
        public bool IsStrikeout { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is italic.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is italic; otherwise, <c>false</c>.
        /// </value>
        public bool IsItalic { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is underline.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is underline; otherwise, <c>false</c>.
        /// </value>
        public bool IsUnderline { get; set; }
        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        public Brush Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
            }
        }

        /// <summary>
        /// Gets or sets the alignment.
        /// </summary>
        /// <value>
        /// The alignment.
        /// </value>
        public System.Windows.TextAlignment Alignment
        {
            get
            {
                return _alignment;
            }
            set
            {
                _alignment = value;
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
                return _verticalAlignment;
            }
            set
            {
                _verticalAlignment = value;
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
                return _textContent;
            }
            set
            {
                _textContent = value;
            }
        }

        /// <summary>
        /// Gets or sets the content of the RTF.
        /// </summary>
        /// <value>
        /// The content of the RTF.
        /// </value>
        public string RTFContent
        {
            get
            {
                return _rtftContent;
            }
            set
            {
                _rtftContent = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Renders an object.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void Render()
        {
            throw new NotImplementedException();
        }        

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            this.Font = new Font();
            this.Alignment = System.Windows.TextAlignment.Left;
            this.Color = new SolidColorBrush(Colors.White);
            this.RTFContent = this.TextContent = "Insert some text.";
        }

        #endregion
    }
}
