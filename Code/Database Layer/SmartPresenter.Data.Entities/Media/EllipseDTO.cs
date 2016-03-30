using SmartPresenter.Common.Enums;
using System;
using System.Xml;

namespace SmartPresenter.Data.Entities
{
    /// <summary>
    /// A class representing Ellipse object.
    /// </summary>
    [Serializable]
    public class EllipseDTO : ShapeDTO
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="EllipseDTO"/> class.
        /// </summary>
        public EllipseDTO()
            : base()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="EllipseDTO"/> class.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public EllipseDTO(int width, int height)
            : base(width, height)
        {

        }

        #endregion

        #region Properties

        #endregion

        #region Methods

        /// <summary>
        /// Reads the XML internal.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override void ReadXmlInternal(XmlReader reader)
        {
            if (this.Type == ElementType.Ellipse)
            {
                // Attributes
                if (reader.Name.Equals("Ellipse"))
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
        /// <exception cref="System.NotImplementedException"></exception>
        protected override void WriteXmlInternal(XmlWriter writer)
        {
            if (this.Type == ElementType.Ellipse)
            {
                // Write Shape shape.
                writer.WriteStartElement("Ellipse");
                base.WriteXmlInternal(writer);
                writer.WriteEndElement();
            }
            else
            {
                base.WriteXmlInternal(writer);
            }
        }

        #endregion
    }
}
