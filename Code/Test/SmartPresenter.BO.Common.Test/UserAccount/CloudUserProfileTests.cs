using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartPresenter.BO.Common.Interfaces;
using SmartPresenter.BO.Common.UserAccounts;
using SmartPresenter.BO.Common.UserAccounts.Factory;

namespace SmartPresenter.BO.Common.Test.UserAccount
{
    [TestClass]
    public class CloudUserProfileTests
    {
        [TestMethod]
        public void CloudUserProfileTest()
        {
            // Arrange
            IUserAccount userAccount = new CloudUserAccount();
            PrivateObject privateUserAccount = new PrivateObject(userAccount);
            IUserProfileFactory userProfileFactory = new UserProfileFactory();

            // Act
            IUserProfile userProfile = userProfileFactory.CreateCloudUserPrfoile(userAccount.Id, string.Empty);

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
