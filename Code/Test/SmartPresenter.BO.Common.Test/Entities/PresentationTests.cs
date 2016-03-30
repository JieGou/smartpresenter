using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartPresenter.BO.Common.Entities;
using SmartPresenter.BO.Common.Interfaces;
using SmartPresenter.BO.Common;
using SmartPresenter.Common;

namespace SmartPresenter.BO.Common.Test
{
    [TestClass]
    public class PresentationTests
    {
        [TestMethod]
        public void CreateNewPresentationTest()
        {
            // Arrange
            IPresentationFactory presentationFactory = new PresentationFactory();

            // Act
            IPresentation presentation = presentationFactory.CreatePresentation();

            // Assert
            Assert.AreEqual(presentation.Name, Constants.Default_Presentation_Name);
        }
    }
}
