using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartPresenter.BO.Common.Interfaces;
using SmartPresenter.BO.Common.UserAccounts;
using SmartPresenter.BO.Common.UserAccounts.Factory;

namespace SmartPresenter.BO.Common.Test.UserAccount
{
    [TestClass]
    public class LocalUserProfileTests
    {
        [TestMethod]
        public void LocalUserProfileTest()
        {
            // Arrange
            IUserAccount userAccount = new LocalUserAccount();
            PrivateObject privateUserAccount = new PrivateObject(userAccount);
            IUserProfileFactory userProfileFactory = new UserProfileFactory();

            // Act
            IUserProfile userProfile = userProfileFactory.CreateLocalUserPrfoile(userAccount.Id, string.Empty);
            IUserProfile userProfileLoaded = userProfileFactory.CreateLocalUserPrfoile(userAccount.Id, string.Empty);
            userProfile.Save();
            userProfileLoaded.Load();

            // Assert
            
        }

        [TestMethod]
        public void SaveTest()
        {
            // Arrange
            IUserAccount userAccount = new LocalUserAccount();
            PrivateObject privateUserAccount = new PrivateObject(userAccount);
            IUserProfileFactory userProfileFactory = new UserProfileFactory();

            // Act
            IUserProfile userProfile = userProfileFactory.CreateLocalUserPrfoile(userAccount.Id, string.Empty);

            // Assert
            Assert.IsNotNull(userProfile);
            Assert.IsNotNull(userProfile.Id);
            Assert.IsFalse(Guid.Empty.Equals(userProfile.Id));
            Assert.IsNotNull(userProfile.DisplayName);
            Assert.IsNotNull(userProfile.GeneralSettings);
            Assert.IsNotNull(userProfile.DisplaySettings);
            Assert.IsNotNull(userProfile.SocialMediaSettings);
            Assert.IsNotNull(userProfile.SyncSettings);
            Assert.IsNotNull(userProfile.UISettings);
            Assert.IsNotNull(userProfile.ProfilePicturePath);
        }
    }
}
