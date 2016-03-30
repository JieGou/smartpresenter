using SmartPresenter.Common.Enums;
using System;
using System.Xml;

namespace SmartPresenter.Data.Entities
{
    /// <summary>
    /// A class to represent a Square object.
    /// </summary>
    [Serializable]
    public class SquareDTO : RectangleDTO
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SquareDTO"/> class.
        /// </summary>
        public SquareDTO()
            : base()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SquareDTO"/> class.
        /// </summary>
        /// <param name="length">The length.</param>
        public SquareDTO(int length)
            : base(length, length)
        {

        }

        #endregion        

        #region Method

        #region Public Methods

        /// <summary>
        /// Reads the XML internal.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override void ReadXmlInternal(XmlReader reader)
        {
            if (this.Type == ElementType.Square)
            {
                // Attributes
                if (reader.Name.Equals("Square"))
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
            if (this.Type == ElementType.Square)
            {
                // Write Shape shape.
                writer.WriteStartElement("Square");
                base.WriteXmlInternal(writer);
                writer.WriteEndElement();
            }
            else
            {
                base.WriteXmlInternal(writer);
            }
        }

        #endregion

        #endregion
    }
}
