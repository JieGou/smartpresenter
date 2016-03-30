using SmartPresenter.BO.Common;
using SmartPresenter.UI.Controls.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace SmartPresenter.UI.Controls
{
    /// <summary>
    /// Class for DataTemplateSelector for Playlist types.
    /// </summary>
    public class PlaylistDataTemplateSelector : DataTemplateSelector
    {
        /// <summary>
        /// A plain Playlist.
        /// </summary>
        public DataTemplate PlaylistDataTemplate { get; set; }
        /// <summary>
        /// A Group of Playlist.
        /// </summary>
        public DataTemplate PlaylistGroupDataTemplate { get; set; }
        /// <summary>
        /// A Presentation inside a Playlist.
        /// </summary>
        public DataTemplate PresentationDataTemplate { get; set; }

        /// <summary>
        /// Overridden method From DataTemplateSelector to select correct DataTemplate.
        /// </summary>
        /// <param name="item">item for which template is to be selected</param>
        /// <param name="container"></param>
        /// <returns></returns>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            PlaylistView playlist = item as PlaylistView;
            PresentationView presentation = item as PresentationView;
            
            if (playlist != null)
            {
                if (playlist.Type == PlaylistType.PlaylistGroup)
                    return PlaylistGroupDataTemplate;
                else if (playlist.Type == PlaylistType.Playlist)
                    return PlaylistDataTemplate;
                else
                    return null;
            }
            
            if (presentation != null)
            {
                return PresentationDataTemplate;
            }

            return null;
        }
    }
}
