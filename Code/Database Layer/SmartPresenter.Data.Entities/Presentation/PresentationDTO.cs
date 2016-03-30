using SmartPresenter.Common.Interfaces;
using SmartPresenter.Common.Logger;
using SmartPresenter.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using SmartPresenter.Common;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SmartPresenter.Data.Entities
{
    public sealed class PresentationDTO : IEntity, IXmlSerializable
    {
        #region Private Data Members        

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Presentation"/> class.
        /// </summary>
        public PresentationDTO()
        {
        }

        public PresentationDTO(Guid id)
        {
            Id = id;
        }

        #endregion

        #region Public Properties        

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has been changed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance was changes; otherwise, <c>false</c>.
        /// </value>
        public bool IsDirty { get; private set; }

        /// <summary>
        /// Gets or sets the name of presentation item.
        /// </summary>
        /// <value>
        /// The name of presentation item.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets the path of presentation item.
        /// </summary>
        /// <value>
        /// The path of presentation item.
        /// </value>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the width of presentation.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the height of presentation.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public int Height { get; set; }

        /// <summary>
        /// Type of Presentation.
        /// </summary>        
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the slides of this presentation.
        /// </summary>
        /// <value>
        /// The slides of this presentation.
        /// </value>
        public ObservableCollection<SlideDTO> Slides { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the parent library location.
        /// </summary>
        /// <value>
        /// The parent library location.
        /// </value>
        public string ParentLibraryLocation { get; set; }

        public object Clone()
        {
            Logger.LogEntry();

            PresentationDTO clonedPresentation = new PresentationDTO();

            try
            {
                using (var stream = new MemoryStream())
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(stream, this);
                    stream.Position = 0;

                    clonedPresentation = (PresentationDTO)binaryFormatter.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                Logger.LogMsg.Error(ex.Message, ex);
            }

            Logger.LogExit();

            return clonedPresentation;
        }

        #endregion

        #region Methods

        #region IXmlSerializable Members

        /// <summary>
        /// This method is reserved and should not be used. When implementing the IXmlSerializable interface, you should return null (Nothing in Visual Basic) from this method, and instead, if specifying a custom schema is required, apply the <see cref="T:System.Xml.Serialization.XmlSchemaProviderAttribute" /> to the class.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Xml.Schema.XmlSchema" /> that describes the XML representation of the object that is produced by the <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)" /> method and consumed by the <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)" /> method.
        /// </returns>
        public XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// Generates an object from its XML representation.
        /// </summary>
        /// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> stream from which the object is deserialized.</param>
        public void ReadXml(XmlReader reader)
        {
            Logger.LogEntry();

            reader.ReadStartElement();

            // Attributes
            this.Id = reader["Id"].ToGuid();
            this.Name = reader["Name"].ToSafeString();
            this.Category = string.IsNullOrEmpty(reader["Category"]) ? Constants.Default_Presentation_Category : reader["Category"].ToSafeString();
            this.Path = reader["Path"].ToSafeString();
            this.ParentLibraryLocation = reader["ParentLibraryLocation"].ToSafeString();

            // Slides
            reader.Read();
            ReadSlides(reader).ForEach(item => this.Slides.Add(item));

            Logger.LogExit();
        }

        /// <summary>
        /// Reads the slides.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        private static ObservableCollection<SlideDTO> ReadSlides(XmlReader reader)
        {
            Logger.LogEntry();

            ObservableCollection<SlideDTO> slides = new ObservableCollection<SlideDTO>();

            if (!reader.IsEmptyElement && reader.Name.Equals("Slides"))
            {
                var slidesReader = reader.ReadSubtree();
                slidesReader.Read(); // start reading the subtree
                slidesReader.Read(); // go to first display shape node
                while (slidesReader.IsStartElement())
                {
                    switch (reader.Name)
                    {
                        case "Slide":
                            SlideDTO slide = new SlideDTO();
                            slide.ReadXml(slidesReader);
                            slides.Add(slide);
                            break;
                        default:
                            slidesReader.Skip();
                            break;
                    }
                    slidesReader.Read();
                }
            }

            Logger.LogExit();

            return slides;
        }

        /// <summary>
        /// Converts an object into its XML representation.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> stream to which the object is serialized.</param>
        public void WriteXml(XmlWriter writer)
        {
            Logger.LogEntry();

            writer.WriteStartElement("Presentation");

            // Attributes
            writer.WriteAttributeString("Id", Id.ToString());
            writer.WriteAttributeString("Name", Name ?? string.Empty);
            writer.WriteAttributeString("Category", Category ?? Constants.Default_Presentation_Category);
            writer.WriteAttributeString("Path", Path ?? string.Empty);
            writer.WriteAttributeString("ParentLibraryLocation", ParentLibraryLocation ?? string.Empty);

            // Slides
            writer.WriteStartElement("Slides");
            this.Slides.ForEach(slide => slide.WriteXml(writer));
            writer.WriteEndElement();

            writer.WriteEndElement();

            Logger.LogExit();
        }

        #endregion

        #endregion
    }
}
