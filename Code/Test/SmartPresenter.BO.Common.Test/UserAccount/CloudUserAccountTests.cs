using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartPresenter.BO.Common.UserAccounts;
using SmartPresenter.BO.Common.Enums;
using SmartPresenter.BO.Common.Interfaces;
using System.IO;
using SmartPresenter.BO.Common.UserAccounts.Factory;

namespace SmartPresenter.BO.Common.Test.UserAccount
{
    [TestClass]
    public class CloudUserAccountTests
    {
        [TestMethod]
        public void CloudUserAccountCreationTest()
        {
            // Arrange
            IUserAccount localUserAccount = new CloudUserAccount();

            // Act

            // Assert
            Assert.IsNotNull(localUserAccount.Id);
            Assert.IsNotNull(localUserAccount.Email);
            Assert.IsNotNull(localUserAccount.Password);
            Assert.IsNotNull(localUserAccount.Type);
            Assert.IsNotNull(localUserAccount.UserProfiles);
            Assert.AreEqual(localUserAccount.UserProfiles.Count, 0);
            Assert.AreEqual(localUserAccount.Type, UserAccountType.Cloud);
        }

        [TestMethod]
        public void SaveTest()
        {
            // Arrange
            IUserAccount cloudUserAccount = new CloudUserAccount();
            PrivateObject privateLocalUserAccount = new PrivateObject(cloudUserAccount);
            IUserProfileFactory userProfileFactory = new UserProfileFactory();

            // Act
            cloudUserAccount.UserProfiles.Add(userProfileFactory.CreateCloudUserPrfoile(cloudUserAccount.Id, string.Empty));
            cloudUserAccount.UserProfiles.Add(userProfileFactory.CreateCloudUserPrfoile(cloudUserAccount.Id, string.Empty));
            cloudUserAccount.Save();

            string path = (string)privateLocalUserAccount.Invoke("GetUserAccountPath", null);

            // Assert
            Assert.IsTrue(File.Exists(path));
            Assert.IsTrue(new FileInfo(path).Length > 0);
        }

        [TestMethod]
        public void LoadTest()
        {
            // Arrange
            IUserAccount cloudUserAccount = new CloudUserAccount();
            PrivateObject privateLocalUserAccount = new PrivateObject(cloudUserAccount);
            IUserProfileFactory userProfileFactory = new UserProfileFactory();

            // Act
            cloudUserAccount.UserProfiles.Add(userProfileFactory.CreateCloudUserPrfoile(cloudUserAccount.Id, string.Empty));
            cloudUserAccount.UserProfiles.Add(userProfileFactory.CreateCloudUserPrfoile(cloudUserAccount.Id, string.Empty));
            cloudUserAccount.Save();

            IUserAccount loadedUserAccount = LocalUserAccount.Load(cloudUserAccount.Id);
            // Assert
            Assert.AreEqual(cloudUserAccount, loadedUserAccount);
        }
    }
}
