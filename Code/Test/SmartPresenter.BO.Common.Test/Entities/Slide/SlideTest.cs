using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartPresenter.BO.Common.Entities;
using SmartPresenter.BO.Common.Interfaces;
using System;
using System.Xml.Schema;

namespace SmartPresenter.BO.Common.Test.Entities.Slide
{
    [TestClass]
    public class SlideTest
    {
        private Shape GetSampleElement()
        {
            int index = new Random().Next(2);
            IShapeFactory elementFactory = null;
            switch (index)
            {
                case 0:
                    elementFactory = new ImageFactory();
                    break;
                case 1:
                    elementFactory = new VideoFactory();
                    break;
                default:
                    elementFactory = new RectangleFactory();
                    break;
            }
            Shape shape = elementFactory.CreateElement();
            return shape;
        }

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
        public void MarkDirtyTest()
        {
            ISlide slide = GetSampleSlide();
            IPresentation presentation = GetSamplePresentation();
            presentation.Slides.Add(slide);

            slide.MarkDirty();

            Assert.IsTrue(slide.IsDirty);
            Assert.IsTrue(slide.ParentPresentation.IsDirty);
        }

        [TestMethod]
        public void AddElementTest()
        {
            ISlide slide = GetSampleSlide();
            int oldCount = slide.Elements.Count;

            slide.AddElement(GetSampleElement());

            Assert.IsTrue(slide.Elements.Count == (oldCount + 1));
        }

        [TestMethod]
        public void SlideCloneTest()
        {
            ISlide slide = GetSampleSlide();
            ISlide clonedSlide = (ISlide)slide.Clone();

            Assert.AreEqual(slide, clonedSlide);
        }
    }
}
