using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartPresenter.BO.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartPresenter.BO.Common.UserAccounts;
using SmartPresenter.BO.Common.Interfaces;
using SmartPresenter.BO.Common.Entities;
using System.IO;
using SmartPresenter.BO.Common.UserAccounts.Factory;
namespace SmartPresenter.BO.Common.Tests
{
    [TestClass]
    public class GeneralSettingsTests
    {
        private IUserProfile _userProfile;
        private ApplicationSettings _applicationSettings;
        private IUserAccount _userAccount;
        private UserProfileFactory.UserProfile.UserAccountData _userAccountData;
        IUserProfileFactory userProfileFactory;

        [TestInitialize]
        public void Initialize()
        {
            IUserProfileFactory userProfileFactory = new UserProfileFactory();
            _applicationSettings = ApplicationSettings.Instance;
            _applicationSettings.UserAccounts.Add(new CloudUserAccount());
            _userAccount = _applicationSettings.UserAccounts[0];
            PrivateObject privateLocalUserAccount = new PrivateObject(_userAccount);
            _userAccountData = (UserProfileFactory.UserProfile.UserAccountData)privateLocalUserAccount.GetField("_userAccountData");
            _userAccount.UserProfiles.Add(userProfileFactory.CreateCloudUserPrfoile(_userAccount.Id, string.Empty));
            _userProfile = _userAccount.UserProfiles[0];
        }

        [TestMethod]
        public void GeneralSettingsTest()
        {
            // Arrange

            // Act
            
            // Assert
            Assert.IsNotNull(_userProfile);
            Assert.IsNotNull(_userProfile.GeneralSettings);
            Assert.IsNotNull(_userProfile.GeneralSettings.AudioLibraries);
            Assert.IsNotNull(_userProfile.GeneralSettings.MediaLibraries);
            Assert.IsNotNull(_userProfile.GeneralSettings.PresentationLibraries);            
        }

        [TestMethod]
        public void SaveTest()
        {
            // Arrange
            AudioLibraryFactory audioLibraryFactory = new AudioLibraryFactory();
            MediaLibraryFactory mediaLibraryFactory = new MediaLibraryFactory();
            PresentationLibraryFactory presentationLibraryFactory = new PresentationLibraryFactory();
            _userProfile.GeneralSettings.AudioLibraries.Add((AudioLibrary)audioLibraryFactory.CreateLibrary());
            _userProfile.GeneralSettings.MediaLibraries.Add((MediaLibrary)mediaLibraryFactory.CreateLibrary());
            _userProfile.GeneralSettings.PresentationLibraries.Add((PresentationLibrary)presentationLibraryFactory.CreateLibrary());

            // Act
            _userProfile.Save();
            ApplicationSettings.Load();
            IUserProfile userProfile = _applicationSettings.UserAccounts[0].UserProfiles[0];

            // Assert
            Assert.IsTrue(File.Exists(_userAccountData.GeneralSettingsPath));
            Assert.IsTrue(new FileInfo(_userAccountData.GeneralSettingsPath).Length > 0);
            Assert.AreEqual(userProfile, _userProfile);
        }
    }
}
