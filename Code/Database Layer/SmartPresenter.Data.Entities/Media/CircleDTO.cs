using SmartPresenter.Common.Enums;
using System;
using System.Xml;
using SmartPresenter.Common.Extensions;

namespace SmartPresenter.Data.Entities
{
    /// <summary>
    /// A class representing a Circle.
    /// </summary>
    [Serializable]
    public class CircleDTO : ShapeDTO
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="CircleDTO"/> class.
        /// </summary>
        public CircleDTO()
            : base()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="CircleDTO"/> class.
        /// </summary>
        /// <param name="radius">The radius.</param>
        public CircleDTO(int radius)
            : base(radius * 2, radius * 2)
        {
            Radius = radius;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the radius of Circle.
        /// </summary>
        /// <value>
        /// The radius of Circle.
        /// </value>
        public int Radius { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Reads the XML internal.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override void ReadXmlInternal(XmlReader reader)
        {
            if (this.Type == ElementType.Circle)
            {
                // Attributes
                if (reader.Name.Equals("Circle"))
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
        /// Reads the common attributes.
        /// </summary>
        /// <param name="reader">The reader.</param>
        protected override void ReadCommonAttributes(XmlReader reader)
        {
            base.ReadCommonAttributes(reader);
            this.Radius = reader["Radius"].ToInt();
        }

        /// <summary>
        /// Writes the XML internal.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override void WriteXmlInternal(XmlWriter writer)
        {
            if (this.Type == ElementType.Circle)
            {
                // Write Shape shape.
                writer.WriteStartElement("Circle");
                base.WriteXmlInternal(writer);
                writer.WriteEndElement();
            }
            else
            {
                base.WriteXmlInternal(writer);
            }
        }

        /// <summary>
        /// Writes the common attributes.
        /// </summary>
        /// <param name="writer">The writer.</param>
        protected override void WriteCommonAttributes(XmlWriter writer)
        {
            base.WriteCommonAttributes(writer);
            writer.WriteAttributeString("Radius", Radius.ToString());
        }

        #endregion
    }
}
