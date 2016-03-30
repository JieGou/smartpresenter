using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartPresenter.BO.Common.Entities;
using SmartPresenter.BO.Common.Interfaces;
using SmartPresenter.BO.Common;
using SmartPresenter.Common;

namespace SmartPresenter.BO.Common.Test
{
    [TestClass]
    public class PlaylistGroupTests
    {
        [TestMethod]
        public void CreateNewPlaylistGroupTest()
        {
            // Arrange
            IPlaylistFactory<IPresentation> playlistFactory = new PresentationPlaylistFactory();

            // Act
            IPlaylist<IPlaylist<IPresentation>> playlistGroup = playlistFactory.CreatePlaylistGroup();

            // Assert
            Assert.AreEqual(playlistGroup.Name, Constants.New_Playlist_Group_Name);
        }
    }
}
