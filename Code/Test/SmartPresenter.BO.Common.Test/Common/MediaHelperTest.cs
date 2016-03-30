using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartPresenter.Common;
using System;

namespace SmartPresenter.BO.Common.Test.Common
{
    [TestClass]
    public class MediaHelperTest
    {
        [TestMethod]
        public void IsValidImageFileTest()
        {
            string[] ValidImageExtensios = { ".jpg", ".jpeg", ".png", ".bmp", ".gif" };

            int index = new Random().Next(ValidImageExtensios.Length);

            Assert.IsTrue(MediaHelper.IsValidImageFile(ValidImageExtensios[index]));
            Assert.IsFalse(MediaHelper.IsValidImageFile("Some Random Text"));

            try
            {
                MediaHelper.IsValidImageFile("");
                Assert.Fail();
            }
            catch (ArgumentException)
            {
            }
        }

        [TestMethod]
        public void IsValidVideoFileTest()
        {
            string[] ValidVideoExtensios = { ".mpg", ".mov", ".wmv" };
            int index = new Random().Next(ValidVideoExtensios.Length);

            Assert.IsTrue(MediaHelper.IsValidVideoFile(ValidVideoExtensios[index]));
            Assert.IsFalse(MediaHelper.IsValidVideoFile("Some Random Text"));

            try
            {
                MediaHelper.IsValidImageFile("");
                Assert.Fail();
            }
            catch (ArgumentException)
            {
            }
        }
    }
}
