using SmartPresenter.BO.Common.Enums;
using SmartPresenter.Common.Interfaces;
using SmartPresenter.Data.Common.Interfaces;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace SmartPresenter.BO.Common.Interfaces
{
    /// <summary>
    /// A basic iterface for all Libraries.
    /// </summary>
    public interface ILibrary<T1, T2> : IEntity, ISave, IXmlSerializable
        where T1 : class, IEntity
        where T2 : IPlaylist<T1>
    {
        #region Properties

        /// <summary>
        /// Name of the Library.
        /// </summary>        
        string Name { get; set; }

        /// <summary>
        /// Gets the location.
        /// </summary>
        /// <value>
        /// The location.
        /// </value>
        string Location { get; }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        LibraryType Type { get; }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        ObservableCollection<T1> Items { get; }

        /// <summary>
        /// Gets or sets the playlists.
        /// </summary>
        /// <value>
        /// The playlists.
        /// </value>
        ObservableCollection<T2> Playlists { get; }

        #endregion

        #region Methods



        #endregion
    }
}
