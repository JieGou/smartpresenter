using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartPresenter.BO.Common.Entities;
using SmartPresenter.BO.Common.Interfaces;
using SmartPresenter.UI.Controls.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace SmartPresenter.UI.Controls.Test
{
    [TestClass]
    public class PlaylistDataTemplateSelectorTests
    {
        [TestMethod]
        public void SelectTemplateTest()
        {
            // Arrange.
            PlaylistDataTemplateSelector playlistDataTemplateSelector = new PlaylistDataTemplateSelector();

            // Create DataTemplates.
            DataTemplate playlistDataTemplate = new DataTemplate();
            DataTemplate playlistGroupDataTemplate = new DataTemplate();
            DataTemplate presentationDataTemplate = new DataTemplate();

            // Initialize DataTemplate Selector.
            playlistDataTemplateSelector.PlaylistDataTemplate = playlistDataTemplate;
            playlistDataTemplateSelector.PlaylistGroupDataTemplate = playlistGroupDataTemplate;
            playlistDataTemplateSelector.PresentationDataTemplate = presentationDataTemplate;

            // Creates View Objects from Business Objects.
            IPlaylist<IPresentation> IPlaylistItem = new PresentationPlaylist();
            IPlaylist<IPlaylist<IPresentation>> playlistGroupBase = new PresentationPlaylistGroup();

            Presentation presentationBase = new Presentation();

            PlaylistView playlist = new PlaylistView(IPlaylistItem);
            PlaylistView playlistGroup = new PlaylistGroupView(playlistGroupBase);

            PresentationView presentationView = new PresentationView(presentationBase);

            // Creates a container.
            TreeViewItem treeViewItemContainer = new TreeViewItem();

            // Act.
            DataTemplate result1 = playlistDataTemplateSelector.SelectTemplate(playlist, treeViewItemContainer);
            DataTemplate result2 = playlistDataTemplateSelector.SelectTemplate(playlistGroup, treeViewItemContainer);
            DataTemplate result3 = playlistDataTemplateSelector.SelectTemplate(presentationView, treeViewItemContainer);

            DataTemplate result4 = playlistDataTemplateSelector.SelectTemplate(playlist, null);
            DataTemplate result5 = playlistDataTemplateSelector.SelectTemplate(playlistGroup, null);
            DataTemplate result6 = playlistDataTemplateSelector.SelectTemplate(presentationView, null);

            DataTemplate result7 = playlistDataTemplateSelector.SelectTemplate(null, null);

            // Some random invalid data.
            DataTemplate result8 = playlistDataTemplateSelector.SelectTemplate(new DataTrigger(), null);

            // Assert.

            Assert.AreEqual(playlistDataTemplate, result1);
            Assert.AreEqual(playlistGroupDataTemplate, result2);
            Assert.AreEqual(presentationDataTemplate, result3);
            Assert.AreEqual(playlistDataTemplate, result4);
            Assert.AreEqual(playlistGroupDataTemplate, result5);
            Assert.AreEqual(presentationDataTemplate, result6);

            Assert.AreEqual(null, result7);
            Assert.AreEqual(null, result8);
        }
    }
}
