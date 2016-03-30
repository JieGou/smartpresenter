using SmartPresenter.Common.Enums;
using System;
using System.Xml;
using SmartPresenter.Common.Extensions;

namespace SmartPresenter.Data.Entities
{
    /// <summary>
    /// A clas to represent Rectangle.
    /// </summary>
    [Serializable]
    public class RectangleDTO : ShapeDTO
    {

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="RectangleDTO"/> class.
        /// </summary>
        public RectangleDTO()
            : this(0, 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RectangleDTO"/> class.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public RectangleDTO(int width, int height) : base(width, height)
        {
            Width = width;
            Height = height;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the corner radius of object.
        /// </summary>
        /// <value>
        /// The corner radius of object.
        /// </value>
        public int CornerRadius { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Reads the XML internal.
        /// </summary>
        /// <param name="reader">The reader.</param>
        protected override void ReadXmlInternal(XmlReader reader)
        {
            if (this.Type == ElementType.Rectangle)
            {
                // Attributes
                if (reader.Name.Equals("Rectangle"))
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
        /// Writes the XML internal.
        /// </summary>
        /// <param name="writer">The writer.</param>
        protected override void WriteXmlInternal(XmlWriter writer)
        {
            if (this.Type == ElementType.Rectangle)
            {
                // Write Shape shape.
                writer.WriteStartElement("Rectangle");
                base.WriteXmlInternal(writer);
                writer.WriteEndElement();
            }
            else
            {
                base.WriteXmlInternal(writer);
            }
        }

        /// <summary>
        /// Reads the common attributes.
        /// </summary>
        /// <param name="reader">The reader.</param>
        protected override void ReadCommonAttributes(XmlReader reader)
        {
            base.ReadCommonAttributes(reader);
            this.CornerRadius = reader["CornerRadius"].ToInt();
        }

        /// <summary>
        /// Writes the common attributes.
        /// </summary>
        /// <param name="writer">The writer.</param>
        protected override void WriteCommonAttributes(XmlWriter writer)
        {
            base.WriteCommonAttributes(writer);
            writer.WriteAttributeString("CornerRadius", this.CornerRadius.ToString());
        }

        #endregion

    }
}
