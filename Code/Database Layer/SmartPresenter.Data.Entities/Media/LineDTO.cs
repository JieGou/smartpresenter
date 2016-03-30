using SmartPresenter.Common.Enums;
using System.Xml;
using SmartPresenter.Common.Extensions;

namespace SmartPresenter.Data.Entities
{
    /// <summary>
    /// A class to represent Line object.
    /// </summary>
    public class LineDTO : ShapeDTO
    {
        #region Properties

        /// <summary>
        /// Gets or sets the x1 cordinate.
        /// </summary>
        /// <value>
        /// The x1 cordinate.
        /// </value>
        public double X1 { get; set; }

        /// <summary>
        /// Gets or sets the x2 cordinate.
        /// </summary>
        /// <value>
        /// The x2 cordinate.
        /// </value>
        public double X2 { get; set; }

        /// <summary>
        /// Gets or sets the y1 cordinate.
        /// </summary>
        /// <value>
        /// The y1 cordinate.
        /// </value>
        public double Y1 { get; set; }

        /// <summary>
        /// Gets or sets the y2 cordinate.
        /// </summary>
        /// <value>
        /// The y2 cordinate.
        /// </value>
        public double Y2 { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Reads the XML internal.
        /// </summary>
        /// <param name="reader">The reader.</param>
        protected override void ReadXmlInternal(XmlReader reader)
        {
            if (this.Type == ElementType.Line)
            {
                // Attributes
                if (reader.Name.Equals("Line"))
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
            if (this.Type == ElementType.Line)
            {
                // Write Shape shape.
                writer.WriteStartElement("Line");
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
            this.X1 = reader["X1"].ToInt();
            this.Y1 = reader["Y1"].ToInt();
            this.X2 = reader["X2"].ToInt();
            this.Y2 = reader["Y2"].ToInt();
        }

        /// <summary>
        /// Writes the common attributes.
        /// </summary>
        /// <param name="writer">The writer.</param>
        protected override void WriteCommonAttributes(XmlWriter writer)
        {
            base.WriteCommonAttributes(writer);
            writer.WriteAttributeString("X1", X1.ToString());
            writer.WriteAttributeString("Y1", Y1.ToString());
            writer.WriteAttributeString("X2", X2.ToString());
            writer.WriteAttributeString("Y2", Y2.ToString());
        }

        #endregion
    }
}
