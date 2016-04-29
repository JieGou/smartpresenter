using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using SmartPresenter.Common.Extensions;

namespace SmartPresenter.Data.Entities
{
    public class SlideDTO
    {
        #region Private Data Members

        

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Slide"/> class.
        /// </summary>
        public SlideDTO()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; protected set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has been changed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance was changes; otherwise, <c>false</c>.
        /// </value>
        public bool IsDirty { get; private set; }

        /// <summary>
        /// Gets or sets the hot key.
        /// </summary>
        /// <value>The hot key.</value>
        public char? HotKey { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>The notes.</value>
        public string Notes { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>The label.</value>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this slide is enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if this slide is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Gets or sets the presentation ID.
        /// </summary>
        /// <value>The presentation ID.</value>
        public PresentationDTO ParentPresentation { get; set; }

        /// <summary>
        /// Gets the type of the slide.
        /// </summary>
        /// <value>The type of the slide.</value>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public int Height { get; set; }

        /// <summary>
        /// Gets or sets the color of the background of slide.
        /// </summary>
        /// <value>
        /// The color of the background of slide.
        /// </value>
        public string Background { get; set; }

        /// <summary>
        /// Gets or sets the slide number.
        /// </summary>
        /// <value>The slide number.</value>
        public int SlideNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [loop to first].
        /// </summary>
        /// <value><c>true</c> if [loop to first]; otherwise, <c>false</c>.</value>
        public bool LoopToFirst { get; set; }

        /// <summary>
        /// Gets or sets the delay before next slide.
        /// </summary>
        /// <value>
        /// The delay before next slide.
        /// </value>
        public TimeSpan DelayBeforeNextSlide { get; set; }

        /// <summary>
        /// Gets or sets the elements of slide.
        /// </summary>
        /// <value>
        /// The elements of slide.
        /// </value>
        public ObservableCollection<ShapeDTO> Elements { get; set; }

        /// <summary>
        /// Gets or sets the transition data.
        /// </summary>
        /// <value>
        /// The transition data.
        /// </value>
        public string TransitionType { get; set; }

        public double TransitionDuration { get; set; }        

        #endregion

        #region Methods


        /// <summary>
        /// This method is reserved and should not be used. When implementing the IXmlSerializable interface, you should return null (Nothing in Visual Basic) from this method, and instead, if specifying a custom schema is required, apply the <see cref="T:System.Xml.Serialization.XmlSchemaProviderAttribute" /> to the class.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Xml.Schema.XmlSchema" /> that describes the XML representation of the object that is produced by the <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)" /> method and consumed by the <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)" /> method.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// Generates an object from its XML representation.
        /// </summary>
        /// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> stream from which the object is deserialized.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void ReadXml(XmlReader reader)
        {
            // Attributes
            this.Id = reader["Id"].ToGuid();
            if (string.IsNullOrEmpty(reader["HotKey"]))
            {
                this.HotKey = null;
            }
            else
            {
                this.HotKey = reader["HotKey"].ToChar();
            }
            this.DelayBeforeNextSlide = reader["DelayBeforeNextSlide"].ToTimeSpan();
            this.Height = reader["Height"].ToInt();
            this.Width = reader["Width"].ToInt();
            this.LoopToFirst = reader["LoopToFirst"].ToBool();
            this.IsEnabled = reader["Enabled"].ToBool();
            this.Notes = reader["Notes"].ToSafeString();
            this.Label = reader["Label"].ToSafeString();
            this.SlideNumber = reader["SlideNumber"].ToInt();

            reader.Read();
            if (reader.Name.Equals("Background"))
            {
                this.Background = reader.ReadInnerXml();
            }

            if (reader.Name.Equals("Transition"))
            {
                this.TransitionType = reader["Type"];                
                this.TransitionDuration = reader["Duration"].ToDouble();                
                reader.Read();
            }

            // Slides
            this.Elements.Clear();
            ReadElements(reader).ForEach(item => this.Elements.Add(item));

            reader.Read();
        }

        /// <summary>
        /// Reads the slides.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns></returns>
        private static ObservableCollection<ShapeDTO> ReadElements(XmlReader reader)
        {
            ObservableCollection<ShapeDTO> elements = new ObservableCollection<ShapeDTO>();
            var slidesReader = reader;
            if (!reader.IsEmptyElement && reader.Name.Equals("Slide"))
            {
                slidesReader = reader.ReadSubtree();
                while (reader.Name.Equals("Elements") == false)
                {
                    slidesReader.Read();
                }
            }
            if (reader.Name.Equals("Elements"))
            {
                DefaultShapeFactory shapeFactory = new DefaultShapeFactory();
                ShapeDTO shape = null;
                while (slidesReader.IsEmptyElement == false && slidesReader.Read() && reader.Name.Equals("Elements") == false)
                {
                    switch (slidesReader.Name)
                    {
                        case "Rectangle":
                        case "Square":
                        case "Circle":
                        case "Ellipse":
                        case "Text":
                        case "Image":
                        case "Audio":
                        case "Video":
                            shape = shapeFactory.CreateElement(slidesReader.Name);
                            break;
                        default:
                            slidesReader.Skip();
                            break;
                    }
                    if (shape != null)
                    {
                        shape.ReadXml(slidesReader);
                        elements.Add(shape);
                    }
                }
            }

            return elements;
        }

        /// <summary>
        /// Converts an object into its XML representation.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> stream to which the object is serialized.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("Slide");

            writer.WriteAttributeString("Id", Id.ToString());
            writer.WriteAttributeString("HotKey", this.HotKey == null ? "" : this.HotKey.ToString());
            writer.WriteAttributeString("DelayBeforeNextSlide", this.DelayBeforeNextSlide < TimeSpan.FromSeconds(0) ? TimeSpan.FromSeconds(0).ToString() : this.DelayBeforeNextSlide.ToString());
            writer.WriteAttributeString("Height", this.Height < 0 ? "0" : this.Height.ToString());
            writer.WriteAttributeString("Width", this.Width < 0 ? "0" : this.Width.ToString());
            writer.WriteAttributeString("Enabled", this.IsEnabled.ToString());
            writer.WriteAttributeString("LoopToFirst", this.LoopToFirst.ToString());
            writer.WriteAttributeString("Notes", this.Notes.ToString());
            writer.WriteAttributeString("Label", this.Label.ToString());
            writer.WriteAttributeString("SlideNumber", this.SlideNumber.ToString());

            writer.WriteStartElement("Background");
            writer.WriteRaw(this.Background);
            writer.WriteEndElement();

            writer.WriteStartElement("Transition");
            writer.WriteAttributeString("Type", this.TransitionType.ToString());
            writer.WriteAttributeString("Duration", this.TransitionDuration.ToString());
            writer.WriteEndElement();

            writer.WriteStartElement("Elements");
            this.Elements.ForEach(shape => shape.WriteXml(writer));
            writer.WriteEndElement();

            writer.WriteEndElement();
        }

        #endregion

    }
}
