using SmartPresenter.Common.Enums;
using System;
using System.Xml;
using SmartPresenter.Common.Extensions;
using System.Drawing;
using System.Collections.Generic;

namespace SmartPresenter.Data.Entities
{
    /// <summary>
    /// A class representing Polygon object.
    /// </summary>
    [Serializable]
    public class PolygonDTO : ShapeDTO
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="PolygonDTO"/> class.
        /// </summary>
        public PolygonDTO()
        {            
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the points.
        /// </summary>
        /// <value>
        /// The points.
        /// </value>
        public List<Point> Points { get; set; }

        /// <summary>
        /// Gets or sets the fill rule.
        /// </summary>
        /// <value>
        /// The fill rule.
        /// </value>
        public string FillRule { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Reads the XML internal.
        /// </summary>
        /// <param name="reader">The reader.</param>
        protected override void ReadXmlInternal(XmlReader reader)
        {
            if (this.Type == ElementType.Polygon)
            {
                // Attributes
                if (reader.Name.Equals("Polygon"))
                {
                    base.ReadXmlInternal(reader);
                    reader.Read();
                    if (reader.Name.Equals("Points"))
                    {
                        reader.Read();
                        if (reader.Name.Equals("Point"))
                        {
                            Point point = new Point();

                            point.X = reader["X"].ToInt();
                            point.Y = reader["Y"].ToInt();

                            this.Points.Add(point);
                        }
                    }
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
            if (this.Type == ElementType.Polygon)
            {
                // Write Polygon shape.
                writer.WriteStartElement("Polygon");
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
