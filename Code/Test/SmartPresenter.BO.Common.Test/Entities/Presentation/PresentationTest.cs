using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartPresenter.BO.Common.Entities;
using SmartPresenter.BO.Common.Interfaces;
using System.Configuration;

namespace SmartPresenter.BO.Common.Test.Entities
{
    [TestClass]
    public class PresentationTest
    {
        private IPresentation GetSamplePresentation()
        {
            IPresentationFactory presentationFactory = new PresentationFactory();
            IPresentation presentation = presentationFactory.CreatePresentation();
            return presentation;
        }

        private ISlide GetSampleSlide()
        {
            ISlideFactory slideFactory = new SlideFactory();
            ISlide slide = slideFactory.CreateSlide();
            return slide;
        }

        [TestMethod]
        public void AddNewSlideTest()
        {
            IPresentation presentation = GetSamplePresentation();
            int oldCount = presentation.Slides.Count;

            presentation.AddNewSlide();

            Assert.IsTrue(presentation.Slides.Count == (oldCount + 1));
        }

        [TestMethod]
        public void MarkDirtyTest()
        {
            IPresentation presentation = GetSamplePresentation();

            presentation.MarkDirty();

            Assert.IsTrue(presentation.IsDirty);
        }

        [TestMethod]
        public void SaveTest()
        {
            string testFolderPath = ConfigurationManager.AppSettings["TestDataFolder"];
            string path = System.IO.Path.Combine(testFolderPath, "SamplePresentation.spd");

            IPresentation presentation = GetSamplePresentation();
            presentation.Name = "SamplePresentation.spd";
            presentation.ParentLibraryLocation = testFolderPath;

            presentation.MarkDirty();
            presentation.Save();
        }

        [TestMethod]
        public void SaveToTest()
        {
            string testFolderPath = ConfigurationManager.AppSettings["TestDataFolder"];
            string path = System.IO.Path.Combine(testFolderPath, "SamplePresentation.spd");

            IPresentation presentation = GetSamplePresentation();

            presentation.MarkDirty();
            presentation.Save(path);
        }

        [TestMethod]
        public void LoadTest()
        {
            string testFolderPath = ConfigurationManager.AppSettings["TestDataFolder"];
            string path = System.IO.Path.Combine(testFolderPath, "SamplePresentation.spd");

            IPresentation presentation = GetSamplePresentation();

            presentation.MarkDirty();
            presentation.Save(path);

            IPresentation loadedPresentation = Presentation.Load(path);

            Assert.IsTrue(presentation.Equals(loadedPresentation));
        }
    }
}
