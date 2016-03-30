using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SmartPresenter.Common.Test
{
    [TestClass]
    public class KnownFileTypesTests
    {
        /// <summary>
        /// Method to test "IsSmartPresenterDocFile" method.
        /// </summary>
        [TestMethod]        
        public void IsSmartPresenterDocFileTest()
        {
            // Arrange.
            string fileName1 = "TestFileName.spd";
            string fileName2 = "TestFileName.txt";
            string fileName3 = "";

            // Act.

            bool result1 = KnownFileTypes.Instance.IsSmartPresenterDocFile(fileName1);
            bool result2 = KnownFileTypes.Instance.IsSmartPresenterDocFile(fileName2);

            // Assert.

            // Assert true in case of valid file name.
            Assert.IsTrue(result1);
            // Assert false in case if invalid file name.
            Assert.IsFalse(result2);
            // Assert throws exception in case if invalid input.
            AssertHelper.AssertThrows<ArgumentException>(() =>
                {
                    KnownFileTypes.Instance.IsSmartPresenterDocFile(fileName3);
                });
        }
    }
}
