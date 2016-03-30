using SmartPresenter.Common.Enums;
using System;
using System.Xml;
using SmartPresenter.Common.Extensions;

namespace SmartPresenter.Data.Entities
{
    /// <summary>
    /// Class representing a text object.
    /// </summary>
    [Serializable]
    public class TextDTO : RectangleDTO
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="TextDTO"/> class.
        /// </summary>
        public TextDTO()
        {            
        }

        #endregion
        
        #region Properties

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
        public string Color { get; set; }

        /// <summary>
        /// Gets or sets the alignment.
        /// </summary>
        /// <value>
        /// The alignment.
        /// </value>
        public string Alignment { get; set; }

        /// <summary>
        /// Gets or sets the vertical alignment.
        /// </summary>
        /// <value>
        /// The vertical alignment.
        /// </value>
        public string VerticalAlignment { get; set; }

        /// <summary>
        /// Gets or sets the text content of the text object.
        /// </summary>
        /// <value>
        /// The text content.
        /// </value>
        public string TextContent { get; set; }

        /// <summary>
        /// Gets or sets the content of the RTF.
        /// </summary>
        /// <value>
        /// The content of the RTF.
        /// </value>
        public string RTFContent { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Reads the XML internal.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override void ReadXmlInternal(XmlReader reader)
        {
            if (this.Type == ElementType.Text)
            {
                // Attributes
                if (reader.Name.Equals("Text"))
                {
                    base.ReadXmlInternal(reader);
                }
            }
            else
            {
                base.ReadXmlInternal(reader);
            }
        }

        /// <summary>
        /// Reads the object.
        /// </summary>
        /// <param name="reader">The reader.</param>
        protected override void ReadObject(XmlReader reader)
        {
            base.ReadObject(reader);
            ReadTextColor(reader);
            ReadFont(reader);
            reader.Read();
            if (reader.Name.Equals("RTF"))
            {
                this.RTFContent = reader.ReadInnerXml();
            }
            //reader.Read();
            if (reader.Name.Equals("Text"))
            {
                this.TextContent = reader.ReadInnerXml();
            }
        }

        /// <summary>
        /// Reads the common attributes.
        /// </summary>
        /// <param name="reader">The reader.</param>
        protected override void ReadCommonAttributes(XmlReader reader)
        {
            base.ReadCommonAttributes(reader);
            this.IsBold = reader["Bold"].ToBool();
            this.IsItalic = reader["Italic"].ToBool();
            this.IsUnderline = reader["Underline"].ToBool();
            this.Alignment = reader["Alignment"].ToSafeString();
            //Enum.TryParse(alignmentString, out this._alignment);
        }

        /// <summary>
        /// Reads the color of the text.
        /// </summary>
        /// <param name="reader">The reader.</param>
        private void ReadTextColor(XmlReader reader)
        {
            //reader.Read();
            if (reader.Name.Equals("TextColor"))
            {
                int opacity = reader["Opacity"].ToInt();
                this.Color = reader.ReadInnerXml();
                //this.Color.Opacity = opacity;
            }
        }

        /// <summary>
        /// Reads the font.
        /// </summary>
        /// <param name="reader">The reader.</param>
        private void ReadFont(XmlReader reader)
        {
            //reader.Read();
            if (reader.Name.Equals("Font"))
            {
                this.Font = new Font()
                {
                    Name = reader["Name"].ToSafeString(),
                    Size = reader["Size"].ToInt(),
                    Style = reader["Style"].ToSafeString(),
                    IsStrikeout = reader["Strikeout"].ToBool(),
                };
            }
        }

        /// <summary>
        /// Writes the XML internal.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override void WriteXmlInternal(XmlWriter writer)
        {
            if (this.Type == ElementType.Text)
            {
                // Write Shape shape.
                writer.WriteStartElement("Text");
                base.WriteXmlInternal(writer);
                writer.WriteStartElement("RTF");
                writer.WriteRaw(this.RTFContent);
                writer.WriteEndElement();
                writer.WriteStartElement("Text");
                writer.WriteRaw(this.TextContent);
                writer.WriteEndElement();
                writer.WriteEndElement();
            }
        }

        /// <summary>
        /// Writes the object.
        /// </summary>
        /// <param name="writer">The writer.</param>
        protected override void WriteObject(XmlWriter writer)
        {
            base.WriteObject(writer);
            // Write Color sub shape.
            WriteTextColor(writer);
            // Write Font sub shape.
            WriteFont(writer);
        }

        /// <summary>
        /// Writes the common attributes.
        /// </summary>
        /// <param name="writer">The writer.</param>
        protected override void WriteCommonAttributes(XmlWriter writer)
        {
            base.WriteCommonAttributes(writer);
            writer.WriteAttributeString("Bold", this.IsBold.ToString());
            writer.WriteAttributeString("Italic", this.IsItalic.ToString());
            writer.WriteAttributeString("Underline", this.IsUnderline.ToString());
            writer.WriteAttributeString("Alignment", this.Alignment.ToString());
        }

        /// <summary>
        /// Writes the font.
        /// </summary>
        /// <param name="writer">The writer.</param>
        private void WriteFont(XmlWriter writer)
        {
            writer.WriteStartElement("Font");
            writer.WriteAttributeString("Name", this.Font.Name.ToString());
            writer.WriteAttributeString("Size", this.Font.Size.ToString());
            writer.WriteAttributeString("Style", this.Font.Style.ToString());
            writer.WriteAttributeString("Strikeout", this.Font.IsStrikeout.ToString());
            writer.WriteEndElement();
        }

        /// <summary>
        /// Writes the color of the text.
        /// </summary>
        /// <param name="writer">The writer.</param>
        private void WriteTextColor(XmlWriter writer)
        {
            writer.WriteStartElement("TextColor");
            //writer.WriteAttributeString("Opacity", this.Color.Opacity.ToString());
            //writer.WriteRaw(this.Color.SaveToString());
            writer.WriteEndElement();
        }

        #endregion
    }
}
