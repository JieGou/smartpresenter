﻿using SmartPresenter.Common.Enums;
using System;
using System.Xml;
using SmartPresenter.Common.Extensions;

namespace SmartPresenter.Data.Entities
{
    /// <summary>
    /// Class representing an image object.
    /// </summary>
    [Serializable]
    public class ImageDTO : RectangleDTO
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageDTO"/> class.
        /// </summary>
        public ImageDTO()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageDTO"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public ImageDTO(string path)
        {
            Path = path;
        }

        #endregion

        #region Properties        

        /// <summary>
        /// Gets or sets the path of Image.
        /// </summary>
        /// <value>
        /// The path of Image.
        /// </value>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the stretch.
        /// </summary>
        /// <value>
        /// The stretch.
        /// </value>
        public string Stretch { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Reads the XML internal.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        protected override void ReadXmlInternal(XmlReader reader)
        {
            if (this.Type == ElementType.Image)
            {
                // Attributes
                if (reader.Name.Equals("Image"))
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
            if (this.Type == ElementType.Image)
            {
                // Write Image shape.
                writer.WriteStartElement("Image");
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
            this.Path = reader["Path"].ToSafeString();
            string stretchString = reader["Stretch"].ToSafeString();
            //Stretch stretch;
            //Enum.TryParse(stretchString, out stretch);
            this.Stretch = stretchString;
        }

        /// <summary>
        /// Writes the common attributes.
        /// </summary>
        /// <param name="writer">The writer.</param>
        protected override void WriteCommonAttributes(XmlWriter writer)
        {
            base.WriteCommonAttributes(writer);
            writer.WriteAttributeString("Path", Path.ToString());
            writer.WriteAttributeString("Stretch", this.Stretch.ToString());
        }

        #endregion

    }
}
