using System;
using System.Xml;
using System.Xml.Schema;
using SmartPresenter.Common.Extensions;
using SmartPresenter.Common.Enums;

namespace SmartPresenter.Data.Entities
{
    /// <summary>
    /// An interface for all media objects like shapes, images, video etc
    /// </summary>
    [Serializable]
    public abstract class ShapeDTO
    {
        #region Private Data Members        

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ShapeDTO"/> class.
        /// </summary>
        public ShapeDTO()
            : this(0, 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShapeDTO"/> class.
        /// </summary>
        public ShapeDTO(int width, int height)
        {
            Width = width;
            Height = height;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the unique identifier.
        /// </summary>
        /// <value>
        /// The unique identifier.
        /// </value>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the opacity.
        /// </summary>
        /// <value>
        /// The opacity.
        /// </value>
        public double Opacity { get; set; }

        /// <summary>
        /// Gets or sets the border thickness of object.
        /// </summary>
        /// <value>
        /// The border thickness of object.
        /// </value>
        public double StrokeThickness { get; set; }
        /// <summary>
        /// Gets or sets the color of the border of object.
        /// </summary>
        /// <value>
        /// The color of the border of object.
        /// </value>
        public string Stroke { get; set; }
        /// <summary>
        /// Gets or sets the back color of object.
        /// </summary>
        /// <value>
        /// The back color of the object.
        /// </value>
        public string Background { get; set; }
        /// <summary>
        /// Gets or sets the shadow of object.
        /// </summary>
        /// <value>
        /// The shadow of object.
        /// </value>
        public string Shadow { get; set; }

        /// <summary>
        /// Gets or sets the x cordinate of object.
        /// </summary>
        /// <value>
        /// The x cordinate.
        /// </value>
        public int X { get; set; }
        /// <summary>
        /// Gets or sets the y cordinate of object.
        /// </summary>
        /// <value>
        /// The y cordinate.
        /// </value>
        public int Y { get; set; }
        /// <summary>
        /// Gets or sets the width of object.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public virtual int Width { get; set; }
        /// <summary>
        /// Gets or sets the height of object.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public virtual int Height { get; set; }

        /// <summary>
        /// Gets or sets if the object is horizontally flipped.
        /// </summary>
        public bool IsFlipHorizontal { get; set; }
        /// <summary>
        /// Gets or sets if the objects is vertically flipped.
        /// </summary>
        public bool IsFlipVertical { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is enabled; otherwise, <c>false</c>.
        /// </value>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public ElementType Type { get; }

        /// <summary>
        /// Gets or sets the rotation angle.
        /// </summary>
        /// <value>
        /// The rotation angle.
        /// </value>
        //public int RotationAngle { get; set; }

        /// <summary>
        /// Gets or sets the transformations.
        /// </summary>
        /// <value>
        /// The transformations.
        /// </value>
        public string Transform { get; set; }

        /// <summary>
        /// Gets or sets the parent slide.
        /// </summary>
        /// <value>
        /// The parent slide.
        /// </value>
        public SlideDTO ParentSlide { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Reads the XML internal.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        protected virtual void ReadXmlInternal(XmlReader reader)
        {
            if (reader.AttributeCount == 0)
            {
                reader.Read();
            }
            ReadObject(reader);
        }

        /// <summary>
        /// Reads the object.
        /// </summary>
        /// <param name="reader">The reader.</param>
        protected virtual void ReadObject(XmlReader reader)
        {
            ReadCommonAttributes(reader);
            ReadBackground(reader);
            ReadStroke(reader);
            ReadShadow(reader);
            ReadTransform(reader);
        }

        /// <summary>
        /// Reads the shadow.
        /// </summary>
        /// <param name="reader">The reader.</param>
        protected void ReadShadow(XmlReader reader)
        {
            //reader.Read();
            if (reader.Name.Equals("Shadow"))
            {
                this.Shadow = reader.ReadInnerXml();
            }
        }

        /// <summary>
        /// Reads the border.
        /// </summary>
        /// <param name="reader">The reader.</param>
        protected void ReadStroke(XmlReader reader)
        {
            //reader.Read();
            if (reader.Name.Equals("Stroke"))
            {
                int opacity = reader["Opacity"].ToInt();
                this.Stroke = reader.ReadInnerXml();
                //this.Stroke.Opacity = opacity;
            }
        }

        /// <summary>
        /// Reads the background.
        /// </summary>
        /// <param name="reader">The reader.</param>
        protected void ReadBackground(XmlReader reader)
        {
            reader.Read();
            if (reader.Name.Equals("Background"))
            {
                this.Background = reader.ReadInnerXml();
            }
        }

        /// <summary>
        /// Reads the transform.
        /// </summary>
        /// <param name="reader">The reader.</param>
        protected void ReadTransform(XmlReader reader)
        {
            //reader.Read();
            if (reader.Name.Equals("Transform"))
            {
                this.Transform = reader.ReadInnerXml();
            }
        }

        /// <summary>
        /// Reads the common attributes.
        /// </summary>
        /// <param name="reader">The reader.</param>
        protected virtual void ReadCommonAttributes(XmlReader reader)
        {
            this.Id = reader["Id"].ToGuid();
            this.X = reader["X"].ToInt();
            this.Y = reader["Y"].ToInt();
            this.Height = reader["Height"].ToInt();
            this.Width = reader["Width"].ToInt();
            this.StrokeThickness = reader["StrokeThickness"].ToDouble();
            //this.RotationAngle = reader["RotationAngle"].ToInt();
            this.IsEnabled = reader["Enabled"].ToBool();
            this.Opacity = reader["Opacity"].ToDouble();
        }

        /// <summary>
        /// Writes the XML internal.
        /// </summary>
        /// <param name="writer">The writer.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        protected virtual void WriteXmlInternal(XmlWriter writer)
        {
            WriteObject(writer);
        }

        /// <summary>
        /// Writes the object.
        /// </summary>
        /// <param name="writer">The writer.</param>
        protected virtual void WriteObject(XmlWriter writer)
        {
            // Write Common shape attributes.
            WriteCommonAttributes(writer);
            // Write BackgroundColor sub shape.
            WriteBackground(writer);
            // Write BorderColor sub shape.
            WriteStroke(writer);
            // Write Shadow sub shape.
            WriteShadow(writer);
            // Write Transform sub shape.
            WriteTransform(writer);
        }

        /// <summary>
        /// Writes the shadow.
        /// </summary>
        /// <param name="writer">The writer.</param>
        protected void WriteShadow(XmlWriter writer)
        {
            writer.WriteStartElement("Shadow");
            writer.WriteRaw(this.Shadow);
            writer.WriteEndElement();
        }

        /// <summary>
        /// Writes the border.
        /// </summary>
        /// <param name="writer">The writer.</param>
        protected void WriteStroke(XmlWriter writer)
        {
            writer.WriteStartElement("Stroke");
            writer.WriteAttributeString("Opacity", this.Stroke);
            writer.WriteRaw(this.Stroke);
            writer.WriteEndElement();
        }

        /// <summary>
        /// Writes the background.
        /// </summary>
        /// <param name="writer">The writer.</param>
        protected void WriteBackground(XmlWriter writer)
        {
            writer.WriteStartElement("Background");
            writer.WriteRaw(this.Background);
            writer.WriteEndElement();
        }

        /// <summary>
        /// Writes the transformations.
        /// </summary>
        /// <param name="writer">The writer.</param>
        protected void WriteTransform(XmlWriter writer)
        {
            writer.WriteStartElement("Transform");
            writer.WriteRaw(this.Transform);
            writer.WriteEndElement();
        }

        /// <summary>
        /// Writes the common attributes.
        /// </summary>
        /// <param name="writer">The writer.</param>
        protected virtual void WriteCommonAttributes(XmlWriter writer)
        {
            writer.WriteAttributeString("Id", Id.ToString());
            writer.WriteAttributeString("X", this.X < 0 ? "0" : this.X.ToString());
            writer.WriteAttributeString("Y", this.Y < 0 ? "0" : this.Y.ToString());
            writer.WriteAttributeString("Height", this.Height < 0 ? "0" : this.Height.ToString());
            writer.WriteAttributeString("Width", this.Width < 0 ? "0" : this.Width.ToString());
            writer.WriteAttributeString("StrokeThickness", this.StrokeThickness.ToString());
            //writer.WriteAttributeString("RotationAngle", this.RotationAngle < 0 ? "0" : this.RotationAngle.ToString());
            writer.WriteAttributeString("Enabled", this.IsEnabled.ToString());
            writer.WriteAttributeString("Opacity", this.Opacity.ToString());
        }

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
            ReadXmlInternal(reader);
        }

        /// <summary>
        /// Converts an object into its XML representation.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> stream to which the object is serialized.</param>
        public void WriteXml(XmlWriter writer)
        {
            WriteXmlInternal(writer);
        }

        #endregion

        #endregion
    }
}
