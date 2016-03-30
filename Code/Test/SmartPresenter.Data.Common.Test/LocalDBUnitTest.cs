using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartPresenter.Data.Common.Local;

namespace SmartPresenter.Data.Common.Test
{
    [TestClass]
    public class LocalDBUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            LocalDB localDB = new LocalDB();
            localDB.OpenConnection();
        }
    }
}
