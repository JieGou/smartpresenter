using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Xml;
using System.Text;

namespace SmartPresenter.Data.Entities.Test
{
    [TestClass]
    public class SlideDTOTest
    {
        [TestMethod]
        public void ReadXmlTest()
        {
        }

        [TestMethod]
        private void ReadElementsTest()
        {
        }

        [TestMethod]
        public void WriteXmlTest()
        {
            SlideDTO actualSlideDTO = new SlideDTO()
            {
                Background = "",
                DelayBeforeNextSlide = TimeSpan.FromSeconds(4),
                Height = 720,
                Width = 1280,
                HotKey = 'H',
                IsEnabled = true,
                Label = "Test Slide",
                LoopToFirst = true,
                Notes = "Sample Notes",
                SlideNumber = 10,
                Type = "Presentation",
                TransitionDuration = 5.0,
                TransitionType = "None",

            };

            XmlWriterSettings writerSettings = new XmlWriterSettings();
            writerSettings.Indent = true;
            StringBuilder output = new StringBuilder();

            using (XmlWriter writer = XmlWriter.Create(output, writerSettings))
            {
                actualSlideDTO.WriteXml(writer);
                writer.Flush();
            }

            SlideDTO expectedSlideDTO = new SlideDTO();
            using (XmlReader reader = XmlReader.Create(new StringReader(output.ToString())))
            {
                reader.Read();
                while (reader.Name.Equals("Slide") == false)
                {
                    if (reader.Read() == false) break;
                }
                expectedSlideDTO.ReadXml(reader);
            }

            Assert.AreEqual(actualSlideDTO, expectedSlideDTO);
        }
    }
}
