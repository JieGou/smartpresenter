using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartPresenter.BO.Common;
using SmartPresenter.BO.Common.Entities;
using SmartPresenter.BO.Common.Interfaces;
using SmartPresenter.Common;
using SmartPresenter.Common.Test;
using SmartPresenter.UI.Controls.ViewModel;
using System;
using System.Linq;
using System.Reflection;

namespace SmartPresenter.UI.Controls.Test
{
    [TestClass]
    public class LibraryTabViewModelTest
    {
        [TestMethod]
        public void AddLibraryTest()
        {
            // Arrange
            LibraryTabViewModel viewModel = new LibraryTabViewModel();
            PrivateObject privateObject = new PrivateObject(viewModel);

            int count = viewModel.Libraries.Count;
            string testPath1 = @"B:\Projects\SmartPresenter\Test\Data";
            string testPath2 = @"B:\Projects\SmartPresenter\Test\Data1";

            // Act            
            privateObject.Invoke("AddLibrary", testPath1);
            Assert.AreEqual(count + 1, viewModel.Libraries.Count);
            LibraryView newLibraryView1 = viewModel.Libraries[count];

            // Assert            
            Assert.AreEqual(System.IO.Path.GetFileName(testPath1), newLibraryView1.Name);
            Assert.AreEqual(testPath1, newLibraryView1.Location);
            AssertHelper.AssertThrows<ArgumentException>(() =>
                {
                    privateObject.Invoke("AddLibrary", testPath2);
                });

            AssertHelper.AssertThrows<ArgumentException>(() =>
            {
                privateObject.Invoke("AddLibrary", string.Empty);
            });
        }

        [TestMethod]
        public void AddPlaylistCommandTest()
        {
            AddPlaylistCommand_AddingAfterPlaylist();
            AddPlaylistCommand_AddingAfterRootPlaylist();
            AddPlaylistCommand_ToRootPlaylist();
            AddPlaylistCommand_ToPlaylistGroup();
        }

        private void AddPlaylistCommand_AddingAfterPlaylist()
        {
            // Arrange.
            LibraryTabViewModel viewModel = new LibraryTabViewModel();

            PlaylistView playlistView = new PlaylistView(new PresentationPlaylist());
            PlaylistGroupView playlistGroupView = new PlaylistGroupView(new PresentationPlaylistGroup());

            playlistGroupView.Playlists.Add(new PlaylistView(new PresentationPlaylist()));
            playlistView.Parent = playlistGroupView;
            playlistGroupView.Playlists.Add(playlistView);
            playlistGroupView.Playlists.Add(new PlaylistView(new PresentationPlaylist()));

            viewModel.SelectedLibrary = viewModel.SelectedLibrary ?? viewModel.Libraries[0];
            viewModel.SelectedLibrary.Playlists.Add(playlistGroupView);

            int playlistCount = playlistGroupView.Playlists.Count;
            string expectedPlaylistName = Constants.New_Playlist_Name;

            // Act.
            viewModel.AddPlaylistCommand.Execute(playlistView);
            PlaylistGroupView parentPlaylist = (PlaylistGroupView)playlistView.Parent;
            int count = parentPlaylist.Playlists.Count;
            int index = parentPlaylist.Playlists.IndexOf(playlistView);
            PlaylistView addedPlaylistView = parentPlaylist.Playlists[index + 1];

            // Assert.
            Assert.AreEqual(playlistCount + 1, playlistGroupView.Playlists.Count);
            Assert.IsNotNull(playlistView.Parent);
            Assert.AreEqual(playlistView.Parent, addedPlaylistView.Parent);
            Assert.IsTrue(count > index);
            Assert.AreEqual(addedPlaylistView.Name, expectedPlaylistName);
        }

        private void AddPlaylistCommand_AddingAfterRootPlaylist()
        {
            // Arrange.
            LibraryTabViewModel viewModel = new LibraryTabViewModel();

            PlaylistView playlistView = new PlaylistView(new PresentationPlaylist());

            viewModel.SelectedLibrary = viewModel.SelectedLibrary ?? viewModel.Libraries[0];
            viewModel.SelectedLibrary.Playlists.Add(new PlaylistView(new PresentationPlaylist()));
            viewModel.SelectedLibrary.Playlists.Add(playlistView);
            viewModel.SelectedLibrary.Playlists.Add(new PlaylistView(new PresentationPlaylist()));

            int playlistCount = viewModel.SelectedLibrary.Playlists.Count;
            string expectedPlaylistName = Constants.New_Playlist_Name;

            // Act.
            viewModel.AddPlaylistCommand.Execute(playlistView);
            int count = viewModel.SelectedLibrary.Playlists.Count;
            int index = viewModel.SelectedLibrary.Playlists.IndexOf(playlistView);
            PlaylistView addedPlaylistView = viewModel.SelectedLibrary.Playlists[index + 1];

            // Assert.
            Assert.AreEqual(playlistCount + 1, viewModel.SelectedLibrary.Playlists.Count);
            Assert.IsNull(addedPlaylistView.Parent);
            Assert.IsTrue(count > index);
            Assert.AreEqual(addedPlaylistView.Name, expectedPlaylistName);
        }

        private void AddPlaylistCommand_ToRootPlaylist()
        {
            // Arrange.
            LibraryTabViewModel viewModel = new LibraryTabViewModel();

            viewModel.SelectedLibrary = viewModel.SelectedLibrary ?? viewModel.Libraries[0];
            viewModel.SelectedLibrary.Playlists.Add(new PlaylistView(new PresentationPlaylist()));
            viewModel.SelectedLibrary.Playlists.Add(new PlaylistView(new PresentationPlaylist()));
            viewModel.SelectedLibrary.Playlists.Add(new PlaylistView(new PresentationPlaylist()));

            int playlistCount = viewModel.SelectedLibrary.Playlists.Count;
            string expectedPlaylistName = Constants.New_Playlist_Name;

            // Act.
            viewModel.AddPlaylistCommand.Execute(null);
            int count = viewModel.SelectedLibrary.Playlists.Count;
            PlaylistView addedPlaylistView = viewModel.SelectedLibrary.Playlists[count - 1];

            // Assert.
            Assert.AreEqual(playlistCount + 1, viewModel.SelectedLibrary.Playlists.Count);
            Assert.IsNull(addedPlaylistView.Parent);
            Assert.AreEqual(addedPlaylistView.Name, expectedPlaylistName);
        }

        private void AddPlaylistCommand_ToPlaylistGroup()
        {
            // Arrange.
            LibraryTabViewModel viewModel = new LibraryTabViewModel();

            PlaylistGroupView playlistGroupView = new PlaylistGroupView(new PresentationPlaylistGroup());
            playlistGroupView.Playlists.Add(new PlaylistView(new PresentationPlaylist()));
            playlistGroupView.Playlists.Add(new PlaylistView(new PresentationPlaylist()));
            playlistGroupView.Playlists.Add(new PlaylistView(new PresentationPlaylist()));

            viewModel.SelectedLibrary = viewModel.SelectedLibrary ?? viewModel.Libraries[0];
            viewModel.SelectedLibrary.Playlists.Add(playlistGroupView);

            int playlistCount = playlistGroupView.Playlists.Count;
            string expectedPlaylistName = Constants.New_Playlist_Name;

            // Act.
            viewModel.AddPlaylistCommand.Execute(playlistGroupView);
            int count = playlistGroupView.Playlists.Count;
            PlaylistView addedPlaylistView = playlistGroupView.Playlists[count - 1];

            // Assert.
            Assert.AreEqual(playlistCount + 1, playlistGroupView.Playlists.Count);
            Assert.AreEqual(playlistGroupView, addedPlaylistView.Parent);
            Assert.AreEqual(addedPlaylistView.Name, expectedPlaylistName);
        }

        [TestMethod]
        public void AddPlaylistGroupCommandTest()
        {
            AddPlaylistGroupCommand_AfterPlaylistGroup();
            AddPlaylistGroupCommand_AfterPlaylist();
            AddPlaylistGroupCommand_ToRoot();
        }

        private void AddPlaylistGroupCommand_AfterPlaylistGroup()
        {
            // Arrange.            
            PlaylistGroupView playlistGroupView = new PlaylistGroupView(new PresentationPlaylistGroup());

            LibraryTabViewModel viewModel = new LibraryTabViewModel();

            playlistGroupView.Playlists.Add(new PlaylistView(new PresentationPlaylist() { Name = "Playlist 1" }));
            playlistGroupView.Playlists.Add(new PlaylistView(new PresentationPlaylist() { Name = "Playlist 2" }));
            playlistGroupView.Playlists.Add(new PlaylistView(new PresentationPlaylist() { Name = "Playlist 3" }));

            viewModel.SelectedLibrary = viewModel.SelectedLibrary ?? viewModel.Libraries[0];
            viewModel.SelectedLibrary.Playlists.Add(playlistGroupView);

            int playlistCount = viewModel.SelectedLibrary.Playlists.Count;
            string expectedPlaylistGroupName = Constants.New_Playlist_Group_Name;

            // Act.
            viewModel.AddPlaylistGroupCommand.Execute(playlistGroupView);
            int count = viewModel.SelectedLibrary.Playlists.Count;
            int index = viewModel.SelectedLibrary.Playlists.IndexOf(playlistGroupView);
            PlaylistView addedPlaylistView = viewModel.SelectedLibrary.Playlists[index + 1];

            // Assert.
            Assert.AreEqual(playlistCount + 1, viewModel.SelectedLibrary.Playlists.Count);
            Assert.IsNull(playlistGroupView.Parent);
            Assert.IsTrue(count > index);
            Assert.AreEqual(addedPlaylistView.Name, expectedPlaylistGroupName);
        }

        private void AddPlaylistGroupCommand_AfterPlaylist()
        {
            // Arrange.            
            PlaylistView playlistView = new PlaylistView(new PresentationPlaylist());

            LibraryTabViewModel viewModel = new LibraryTabViewModel();

            viewModel.SelectedLibrary = viewModel.SelectedLibrary ?? viewModel.Libraries[0];
            viewModel.SelectedLibrary.Playlists.Add(new PlaylistView(new PresentationPlaylist()));
            viewModel.SelectedLibrary.Playlists.Add(playlistView);
            viewModel.SelectedLibrary.Playlists.Add(new PlaylistView(new PresentationPlaylist()));

            int playlistCount = viewModel.SelectedLibrary.Playlists.Count;
            string expectedPlaylistGroupName = Constants.New_Playlist_Group_Name;

            // Act.
            viewModel.AddPlaylistGroupCommand.Execute(playlistView);
            int count = viewModel.SelectedLibrary.Playlists.Count;
            int index = viewModel.SelectedLibrary.Playlists.IndexOf(playlistView) + 1;
            PlaylistView addedPlaylistView = viewModel.SelectedLibrary.Playlists[index];

            // Assert.
            Assert.AreEqual(playlistCount + 1, viewModel.SelectedLibrary.Playlists.Count);
            Assert.AreEqual(index, viewModel.SelectedLibrary.Playlists.IndexOf(addedPlaylistView));
            Assert.AreEqual(addedPlaylistView.Name, expectedPlaylistGroupName);
        }

        private void AddPlaylistGroupCommand_ToRoot()
        {
            // Arrange.            

            LibraryTabViewModel viewModel = new LibraryTabViewModel();

            viewModel.SelectedLibrary = viewModel.SelectedLibrary ?? viewModel.Libraries[0];
            viewModel.SelectedLibrary.Playlists.Add(new PlaylistView(new PresentationPlaylist()));
            viewModel.SelectedLibrary.Playlists.Add(new PlaylistView(new PresentationPlaylist()));
            viewModel.SelectedLibrary.Playlists.Add(new PlaylistView(new PresentationPlaylist()));

            int playlistCount = viewModel.SelectedLibrary.Playlists.Count;
            string expectedPlaylistGroupName = Constants.New_Playlist_Group_Name;

            // Act.
            viewModel.AddPlaylistGroupCommand.Execute(null);
            int count = viewModel.SelectedLibrary.Playlists.Count;
            PlaylistView addedPlaylistView = viewModel.SelectedLibrary.Playlists[count - 1];

            // Assert.
            Assert.AreEqual(playlistCount + 1, viewModel.SelectedLibrary.Playlists.Count);
            Assert.AreEqual(addedPlaylistView.Name, expectedPlaylistGroupName);
        }

        [TestMethod]
        public void AddPresentationToLibraryCommandTest()
        {
            // Arrange.
            LibraryTabViewModel viewModel = new LibraryTabViewModel();

            LibraryView libraryView1 = new LibraryView(new PresentationLibrary("D:\\Library\\Library1"));
            LibraryView libraryView2 = new LibraryView(new PresentationLibrary("D:\\Library\\Library2"));
            libraryView2.Presentations.Add(new PresentationView(new Presentation()));
            viewModel.Libraries.Add(libraryView1);
            viewModel.Libraries.Add(libraryView2);
            int presentationCount1 = libraryView1.Presentations.Count;
            int presentationCount2 = libraryView2.Presentations.Count;

            string expectedPresentationName = new PrivateObject(new Presentation()).GetField("DEFAULT_PRESENTATION_NAME", BindingFlags.NonPublic | BindingFlags.Static).ToString();

            // Act.
            viewModel.AddPresentationToLibraryCommand.Execute(libraryView1);

            viewModel.AddPresentationToLibraryCommand.Execute(libraryView2);

            // Assert.
            Assert.AreEqual(presentationCount1 + 1, libraryView1.Presentations.Count);
            Assert.AreEqual(expectedPresentationName, libraryView1.Presentations[libraryView1.Presentations.Count - 1].Name);

            Assert.AreEqual(presentationCount2 + 1, libraryView2.Presentations.Count);
            Assert.AreEqual(expectedPresentationName, libraryView2.Presentations[libraryView2.Presentations.Count - 1].Name);

            //viewModel.AddPresentationToLibraryCommand.Execute(null);
        }

        [TestMethod]
        public void AddPresentationToPlaylistCommandTest()
        {
            // Arrange.
            LibraryTabViewModel viewModel = new LibraryTabViewModel();
            IPresentationFactory presentationFactory = new PresentationFactory();

            PresentationPlaylist playlist1 = new PresentationPlaylist();
            playlist1.Items.Add(presentationFactory.CreatePresentation());
            playlist1.Items.Add(new Presentation());
            PlaylistView playlistView1 = new PlaylistView(playlist1);

            PresentationPlaylist playlist2 = new PresentationPlaylist();
            IPresentation presentation = presentationFactory.CreatePresentation();
            playlist2.Items.Add(new Presentation());
            playlist2.Items.Add(presentation);
            PlaylistView playlistView2 = new PlaylistView(playlist2);

            viewModel.SelectedLibrary.Playlists.Add(playlistView1);
            viewModel.SelectedLibrary.Playlists.Add(playlistView2);

            int presentationCount1 = playlistView1.Presentations.Count;
            int presentationCount2 = playlistView2.Presentations.Count;
            string expectedPresentationName = new PrivateObject(new Presentation()).GetField("DEFAULT_PRESENTATION_NAME", BindingFlags.NonPublic | BindingFlags.Static).ToString();

            // Act.
            viewModel.AddPresentationToPlaylistCommand.Execute(playlistView1);
            PresentationView addedPresentationView = playlistView1.Presentations[presentationCount1];

            // Assert.
            Assert.AreEqual(presentationCount1 + 1, playlistView1.Presentations.Count);
            Assert.AreEqual(expectedPresentationName, addedPresentationView.Name);

            // Act.
            viewModel.AddPresentationToPlaylistCommand.Execute(playlistView2.Presentations[presentationCount2 - 1]);
            addedPresentationView = playlistView2.Presentations.FirstOrDefault(pres => pres.Name != null && pres.Name.CompareTo(expectedPresentationName) == 0);

            // Assert.
            Assert.AreEqual(presentationCount2 + 1, playlistView2.Presentations.Count);
            Assert.IsNotNull(addedPresentationView);
            Assert.AreEqual(expectedPresentationName, addedPresentationView.Name);
        }

        [TestMethod]
        public void RemoveLibraryCommandTest()
        {
            // Arrange.
            LibraryTabViewModel viewModel = new LibraryTabViewModel();

            LibraryView libraryView = new LibraryView(new PresentationLibrary("D:\\Library\\Library1"));
            viewModel.Libraries.Add(new LibraryView(new PresentationLibrary("D:\\Library\\Library2")));
            viewModel.Libraries.Add(libraryView);

            int libraryCount = viewModel.Libraries.Count;

            // Act.
            viewModel.RemoveLibraryCommand.Execute(libraryView);

            // Assert.
            Assert.AreEqual(libraryCount - 1, viewModel.Libraries.Count);

            // Act.
            libraryCount = viewModel.Libraries.Count;
            viewModel.RemoveLibraryCommand.Execute(new Object());

            // Assert.
            Assert.AreEqual(libraryCount, viewModel.Libraries.Count);

            // Act.
            libraryCount = viewModel.Libraries.Count;
            viewModel.RemoveLibraryCommand.Execute(null);

            // Assert.
            Assert.AreEqual(libraryCount, viewModel.Libraries.Count);
        }
    }
}
