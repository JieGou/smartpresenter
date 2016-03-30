using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartPresenter.BO.Common.UserAccounts;
using System.IO;
using SmartPresenter.Common;
using SmartPresenter.BO.Common.Interfaces;

namespace SmartPresenter.BO.Common.Test.Settings
{
    [TestClass]
    public class ApplicationSettingsTests
    {
        [TestMethod]
        public void ApplicationSettingsTest()
        {
            // Arrange

            // Act
            ApplicationSettings applicationSettings = ApplicationSettings.Instance;

            // Assert
            Assert.IsNotNull(applicationSettings.UserAccounts);
            Assert.AreEqual(applicationSettings.UserAccounts.Count, 0);    
        }

        [TestMethod]
        public void LoadTest()
        {
            // Arrange
            ApplicationSettings.Instance.UserAccounts.Add(new CloudUserAccount());
            ApplicationSettings.Instance.UserAccounts.Add(new LocalUserAccount());

            // Act
            ApplicationSettings.Instance.Save();
            ApplicationSettings.Load();

            // Assert
            Assert.AreEqual(ApplicationSettings.Instance.UserAccounts.Count, 2);
        }

        [TestMethod]
        public void SaveTest()
        {
            // Arrange
            IUserAccountFactory userAccountFactory = new UserAccountFactory();

            ApplicationSettings.Instance.UserAccounts.Add(userAccountFactory.CreateCloudUserAccount("", "", ""));
            ApplicationSettings.Instance.UserAccounts.Add(userAccountFactory.CreateLocalUserAccount("", "", ""));

            // Act
            ApplicationSettings.Instance.Save();

            // Assert
            Assert.IsTrue(File.Exists(ApplicationData.Instance.ApplicationSettingsPath));
            Assert.IsTrue(new FileInfo(ApplicationData.Instance.ApplicationSettingsPath).Length > 0);
        }
    }
}
