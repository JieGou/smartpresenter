using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SmartPresenter.Common.Test
{
    /// <summary>
    /// Helper Test class to add additional functionality along with Assert Class.
    /// </summary>
    public static class AssertHelper
    {
        /// <summary>
        /// Assertion for a method throwing exception. This will fail if method does not throw specified exception.
        /// </summary>
        /// <typeparam name="exception"></typeparam>
        /// <param name="method"></param>
        public static void AssertThrows<exception>(Action method) where exception : Exception
        {
            try
            {
                method.Invoke();
            }
            catch (exception)
            {
                return; // Expected exception.
            }
            catch (Exception ex)
            {
                Assert.Fail("Wrong exception thrown: " + ex.Message);
            }
            Assert.Fail("No exception thrown");
        }
    }
}
