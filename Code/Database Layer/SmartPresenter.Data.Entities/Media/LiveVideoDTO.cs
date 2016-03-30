using SmartPresenter.Common.Enums;
using System;
using System.Xml;

namespace SmartPresenter.Data.Entities
{
    /// <summary>
    /// Class representing a live feed video coming from a camera or some input source.
    /// </summary>
    [Serializable]
    public class LiveVideoDTO : RectangleDTO
    {
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
            if (this.Type == ElementType.LiveVideo)
            {
                // Attributes
                if (reader.Name.Equals("LiveVideo"))
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
            if (this.Type == ElementType.LiveVideo)
            {
                // Write Shape shape.
                writer.WriteStartElement("LiveVideo");
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
