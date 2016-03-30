using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartPresenter.BO.Common.Entities;
using SmartPresenter.BO.Common.Interfaces;
using SmartPresenter.BO.Common;
using SmartPresenter.Common;

namespace SmartPresenter.BO.Common.Test
{
    [TestClass]
    public class PlaylistTests
    {
        [TestMethod]
        public void CreateNewPlaylistTest()
        {   
            // Arrange            
            IPlaylistFactory<IPresentation> playlistFactory = new PresentationPlaylistFactory();

            // Act
            IPlaylist<IPresentation> playlist = playlistFactory.CreatePlaylist();

            // Assert
            Assert.AreEqual(playlist.Name,Constants.New_Playlist_Name);
        }
    }
}
