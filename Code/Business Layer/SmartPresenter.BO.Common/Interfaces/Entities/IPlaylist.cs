using SmartPresenter.Common.Interfaces;
using SmartPresenter.Data.Common.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace SmartPresenter.BO.Common.Interfaces
{
    /// <summary>
    /// A behaviour for every playlist item.
    /// </summary>
    public interface IPlaylist<T> : IEntity, IXmlSerializable, ICloneable, ISavable
        where T : class, IEntity
    {
        #region Properties

        /// <summary>
        /// Name of playlist.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Type of Playlist.
        /// </summary>
        PlaylistType Type { get; }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        ObservableCollection<T> Items { get; }

        #endregion
    }
}
