using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartPresenter.BO.Common.UserAccounts;
using SmartPresenter.BO.Common.Enums;
using SmartPresenter.BO.Common.Interfaces;
using System.IO;
using SmartPresenter.BO.Common.UserAccounts;
using SmartPresenter.BO.Common.UserAccounts.Factory;

namespace SmartPresenter.BO.Common.Test.UserAccount
{
    [TestClass]
    public class LocalUserAccountTests
    {
        [TestMethod]
        public void LocalUserAccountTest()
        {
            // Arrange
            IUserAccount localUserAccount = new LocalUserAccount();

            // Act

            // Assert
            Assert.IsNotNull(localUserAccount.Id);
            Assert.IsNotNull(localUserAccount.Email);
            Assert.IsNotNull(localUserAccount.Password);
            Assert.IsNotNull(localUserAccount.Type);
            Assert.IsNotNull(localUserAccount.UserProfiles);
            Assert.AreEqual(localUserAccount.UserProfiles.Count, 0);
            Assert.AreEqual(localUserAccount.Type, UserAccountType.Local);
        }

        [TestMethod]
        public void SaveTest()
        {
            // Arrange
            IUserAccount localUserAccount = new LocalUserAccount();
            PrivateObject privateLocalUserAccount = new PrivateObject(localUserAccount);
            IUserProfileFactory userProfileFactory = new UserProfileFactory();
            
            // Act
            localUserAccount.UserProfiles.Add(userProfileFactory.CreateLocalUserPrfoile(localUserAccount.Id, string.Empty));
            localUserAccount.UserProfiles.Add(userProfileFactory.CreateLocalUserPrfoile(localUserAccount.Id, string.Empty));
            localUserAccount.Save();

            string path = (string)privateLocalUserAccount.Invoke("GetUserAccountPath", null);

            // Assert
            Assert.IsTrue(File.Exists(path));
            Assert.IsTrue(new FileInfo(path).Length > 0);
        }

        [TestMethod]
        public void LoadTest()
        {
            // Arrange
            IUserAccount localUserAccount = new LocalUserAccount();
            PrivateObject privateLocalUserAccount = new PrivateObject(localUserAccount);
            IUserProfileFactory userProfileFactory = new UserProfileFactory();

            // Act
            localUserAccount.UserProfiles.Add(userProfileFactory.CreateLocalUserPrfoile(localUserAccount.Id, string.Empty));
            localUserAccount.UserProfiles.Add(userProfileFactory.CreateLocalUserPrfoile(localUserAccount.Id, string.Empty));
            localUserAccount.Save();

            IUserAccount loadedUserAccount = LocalUserAccount.Load(localUserAccount.Id);
            // Assert
            Assert.AreEqual(localUserAccount, loadedUserAccount);
        }
    }
}
