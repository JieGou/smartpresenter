using System;

namespace SmartPresenter.Data.Entities
{
    /// <summary>
    /// A Font object for text objects.
    /// </summary>
    [Serializable]
    public class Font
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the Font class.
        /// </summary>
        public Font()
        {
            this.Name = string.Empty;
            this.Size = 0;
            this.Style = string.Empty;
            this.IsStrikeout = false;
            this.IsUnderline = false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the style.
        /// </summary>
        /// <value>
        /// The style.
        /// </value>
        public string Style { get; set; }
        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>
        /// The size.
        /// </value>
        public int Size { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is strikeout.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is strikeout; otherwise, <c>false</c>.
        /// </value>
        public bool IsStrikeout { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is underline.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is underline; otherwise, <c>false</c>.
        /// </value>
        public bool IsUnderline { get; set; }

        /// <summary>
        /// Gets or sets the color of font.
        /// </summary>
        /// <value>
        /// The font color.
        /// </value>
        public string Color { get; set; }

        #endregion
    }
}
