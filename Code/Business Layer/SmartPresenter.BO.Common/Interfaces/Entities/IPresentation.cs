using SmartPresenter.Common.Interfaces;
using SmartPresenter.Data.Common.Interfaces;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Xml.Serialization;

namespace SmartPresenter.BO.Common.Interfaces
{
    /// <summary>
    /// A behaviour for any entity that can be presented in some form like, shapes, images, videos, audio
    /// </summary>
    public interface IPresentation : IEntity, ISave, ISavable
    {
        #region Properties

        /// <summary>
        /// Gets or sets the name of presentation item.
        /// </summary>
        /// <value>
        /// The name of presentation item.
        /// </value>
        string Name { get; set; }
        /// <summary>
        /// Gets the path of presentation item.
        /// </summary>
        /// <value>
        /// The path of presentation item.
        /// </value>
        string Path { get; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        string Category { get; set; }

        /// <summary>
        /// Type of Presentation.
        /// </summary>
        PresentationType Type { get; }

        /// <summary>
        /// Gets or sets the slides of this presentation.
        /// </summary>
        /// <value>
        /// The slides of this presentation.
        /// </value>
        ObservableCollection<ISlide> Slides { get; }

        /// <summary>
        /// Gets or sets the location of parent library.
        /// </summary>
        /// <value>
        /// The location of parent library.
        /// </value>
        string ParentLibraryLocation { get; set; }

        /// <summary>
        /// Gets or sets the width of presentation.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        int Width { get; set; }

        /// <summary>
        /// Gets or sets the height of presentation.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        int Height { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Adds the new slide.
        /// </summary>
        void AddNewSlide();

        /// <summary>
        /// Adds a new shape to slide.
        /// </summary>
        /// <param name="shape"></param>
        void AddSlide(ISlide slide);

        /// <summary>
        /// Removes the element.
        /// </summary>
        /// <param name="slide">The slide.</param>
        void RemoveSlide(ISlide slide);

        /// <summary>
        /// Moves the element.
        /// </summary>
        /// <param name="oldIndex">The old index.</param>
        /// <param name="newIndex">The new index.</param>
        void MoveSlide(int oldIndex, int newIndex);

        #endregion

        #region Events

        /// <summary>
        /// Occurs when [elements collection changed].
        /// </summary>
        event NotifyCollectionChangedEventHandler SlidesCollectionChanged;

        #endregion
    }
}
