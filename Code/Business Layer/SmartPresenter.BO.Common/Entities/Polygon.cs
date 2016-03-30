using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using SmartPresenter.BO.Common.Enums;
using System.Windows.Media;
using System.Windows;
using SmartPresenter.Common.Extensions;

namespace SmartPresenter.BO.Common.Entities
{
    [Serializable]
    public class Polygon : Element
    {
        #region Constructor

        public Polygon()
        {
            Initialize();
        }

        #endregion


        #region Properties

        public PointCollection Points { get; set; }

        public override ElementType Type
        {
            get
            {
                return ElementType.Polygon;
            }
        }

        #endregion

        #region Methods

        public override void Render()
        {
            throw new NotImplementedException();
        }

        protected override void ReadXmlInternal(XmlReader reader)
        {
            // Attributes
            if (reader.Name.Equals("Polygon"))
            {
                ReadCommonAttributes(reader);
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

        protected virtual void ReadCommonAttributes(XmlReader reader)
        {
            this.Id = reader["Id"].ToGuid();
            string[] thicknessArray = reader["BorderThickness"].ToSafeString().Split(',');
            this.BorderThickness = new Thickness(thicknessArray[0].ToInt(), thicknessArray[1].ToInt(), thicknessArray[2].ToInt(), thicknessArray[3].ToInt());
            this.IsEnabled = reader["Enabled"].ToBool();
        }

        protected override void WriteXmlInternal(XmlWriter writer)
        {
            // Write Polygon element.
            writer.WriteStartElement("Polygon");

            WriteCommonAttributes(writer);

            writer.WriteEndElement();
        }

        protected virtual void WriteCommonAttributes(XmlWriter writer)
        {
            writer.WriteAttributeString("Id", Id.ToString());
            writer.WriteAttributeString("BorderThickness", string.Format("{0},{1},{2},{3}", this.BorderThickness.Left, this.BorderThickness.Top, this.BorderThickness.Right, this.BorderThickness.Bottom));
            writer.WriteAttributeString("Enabled", this.IsEnabled.ToString());

            writer.WriteStartElement("Points");

            foreach (Point point in this.Points)
            {
                writer.WriteStartElement("Point");
                writer.WriteAttributeString("X", point.X.ToString());
                writer.WriteAttributeString("Y", point.Y.ToString());
                writer.WriteEndElement();
            }

            writer.WriteEndElement();
        }

        private void Initialize()
        {
            this.Points = new PointCollection();
        }

        #endregion
    }
}
