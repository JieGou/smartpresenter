using SmartPresenter.BO.Common.Entities;
using SmartPresenter.BO.Common.Enums;
using SmartPresenter.BO.Common.Transitions;
using SmartPresenter.Common.Interfaces;
using SmartPresenter.Data.Common.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Media;
using System.Xml.Serialization;

namespace SmartPresenter.BO.Common.Interfaces
{
    /// <summary>
    /// An interface for all slide types.
    /// </summary>
    public interface ISlide : IEntity, ISavable, ICloneable
    {
        #region Properties

        /// <summary>
        /// Gets or sets the hot key.
        /// </summary>
        /// <value>The hot key.</value>
        char? HotKey { get; set; }

        /// <summary>
        /// Gets or sets the notes.
        /// </summary>
        /// <value>The notes.</value>
        string Notes { get; set; }

        /// <summary>
        /// Gets or sets the slide label.
        /// </summary>
        string Label { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this slide is enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if this slide is enabled; otherwise, <c>false</c>.
        /// </value>
        bool IsEnabled { get; set; }

        /// <summary>
        /// Gets or sets the presentation ID.
        /// </summary>
        /// <value>The presentation ID.</value>
        IPresentation ParentPresentation { get; set; }

        /// <summary>
        /// Gets the type of the slide.
        /// </summary>
        /// <value>The type of the slide.</value>
        SlideType Type { get; }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        int Width { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        int Height { get; set; }

        /// <summary>
        /// Gets or sets the color of the background of slide.
        /// </summary>
        /// <value>
        /// The color of the background of slide.
        /// </value>
        Brush Background { get; set; }

        /// <summary>
        /// Gets or sets the slide number.
        /// </summary>
        /// <value>The slide number.</value>
        int SlideNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [loop to first].
        /// </summary>
        /// <value><c>true</c> if [loop to first]; otherwise, <c>false</c>.</value>
        bool LoopToFirst { get; set; }

        /// <summary>
        /// Gets or sets the delay before next slide.
        /// </summary>
        /// <value>
        /// The delay before next slide.
        /// </value>
        TimeSpan DelayBeforeNextSlide { get; set; }

        /// <summary>
        /// Gets or sets the elements of slide.
        /// </summary>
        /// <value>
        /// The elements of slide.
        /// </value>
        ObservableCollection<Shape> Elements { get; }

        /// <summary>
        /// Gets or sets the transition data.
        /// </summary>
        /// <value>
        /// The transition data.
        /// </value>
        Transition Transition { get; set; }

        #endregion

        #region Operators

        /// <summary>
        /// Gets the <see cref="Shape"/> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="Shape"/>.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        Shape this[int index] { get; }
        /// <summary>
        /// Gets the <see cref="Shape"/> with the specified identifier.
        /// </summary>
        /// <value>
        /// The <see cref="Shape"/>.
        /// </value>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Shape this[Guid id] { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Renders this slide.
        /// </summary>
        void Render();

        /// <summary>
        /// Adds a new shape to slide.
        /// </summary>
        /// <param name="shape"></param>
        void AddElement(Shape shape);

        /// <summary>
        /// Removes the element.
        /// </summary>
        /// <param name="shape">The shape.</param>
        void RemoveElement(Shape shape);

        /// <summary>
        /// Moves the element.
        /// </summary>
        /// <param name="oldIndex">The old index.</param>
        /// <param name="newIndex">The new index.</param>
        void MoveElement(int oldIndex, int newIndex);

        #endregion

        #region Events

        /// <summary>
        /// Occurs when [elements collection changed].
        /// </summary>
        event NotifyCollectionChangedEventHandler ElementsCollectionChanged;

        #endregion
    }
}
