using AutoMapper;
using SmartPresenter.BO.Common.Interfaces;
using SmartPresenter.Common;
using SmartPresenter.Common.Extensions;
using SmartPresenter.Common.Logger;
using SmartPresenter.Data.Common;
using SmartPresenter.Data.Common.Repositories;
using SmartPresenter.Data.Entities;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Schema;

namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// Class for a Presentation.
    /// </summary>
    public sealed class Presentation : IPresentation
    {
        #region Private Data Members

        /// <summary>
        /// The path of presentation.
        /// </summary>
        private string _path = string.Empty;

        /// <summary>
        /// The location of parent library .
        /// </summary>
        private string _parentLibraryLocation;

        /// <summary>
        /// The slides collection.
        /// </summary>
        private ObservableCollection<ISlide> _slides = new ObservableCollection<ISlide>();

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Presentation"/> class.
        /// </summary>
        public Presentation()
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Presentation"/> class.
        /// </summary>
        /// <param name="presentation">The presentation.</param>
        public Presentation(Presentation presentation)
        {
            Initialize();
            this.Category = presentation.Category;
            this.Name = presentation.Name;
            this.ParentLibraryLocation = presentation.ParentLibraryLocation;
        }

        #endregion

        #region Public Properties

        #region IPresentationItem Member Properties

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has been changed.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance was changes; otherwise, <c>false</c>.
        /// </value>
        public bool IsDirty { get; private set; }

        /// <summary>
        /// Gets or sets the name of presentation item.
        /// </summary>
        /// <value>
        /// The name of presentation item.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets the path of presentation item.
        /// </summary>
        /// <value>
        /// The path of presentation item.
        /// </value>
        public string Path
        {
            get
            {
                return _path;
            }
        }

        /// <summary>
        /// Gets or sets the width of presentation.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the height of presentation.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public int Height { get; set; }

        /// <summary>
        /// Type of Presentation.
        /// </summary>        
        public PresentationType Type
        {
            get
            {
                return PresentationType.Presentation;
            }
        }

        /// <summary>
        /// Gets or sets the slides of this presentation.
        /// </summary>
        /// <value>
        /// The slides of this presentation.
        /// </value>
        public ObservableCollection<ISlide> Slides
        {
            get
            {
                if (_slides == null)
                {
                    _slides = new ObservableCollection<ISlide>();
                }
                return _slides;
            }
        }

        #endregion

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        /// <value>
        /// The category.
        /// </value>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the parent library location.
        /// </summary>
        /// <value>
        /// The parent library location.
        /// </value>
        public string ParentLibraryLocation
        {
            get
            {
                return _parentLibraryLocation;
            }
            set
            {
                _parentLibraryLocation = value;
                _path = string.Concat(System.IO.Path.Combine(_parentLibraryLocation, Name), Constants.Default_Document_Extension);
            }
        }

        #endregion

        #region Methods

        #region Private Methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            Logger.LogEntry();

            this.Id = Guid.NewGuid();
            this.IsDirty = true;
            this.Width = 1600;
            this.Height = 900;

            _slides.CollectionChanged += Slides_CollectionChanged;

            Logger.LogExit();
        }

        /// <summary>
        /// Handles the CollectionChanged event of the Slides control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        private void Slides_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Logger.LogEntry();

            IsDirty = true;
            OnSlidesCollectionChanged(sender, e);
            int count = 0;
            if (e.NewItems != null)
            {
                foreach (ISlide slide in e.NewItems)
                {
                    slide.ParentPresentation = this;
                    slide.SlideNumber = Slides.Count + count++;
                    slide.Width = this.Width;
                    slide.Height = this.Height;
                }
            }

            Logger.LogExit();
        }

        #endregion

        #region Protected Overridden Methods

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            Presentation presentation = obj as Presentation;
            bool result = false;

            if (presentation != null)
            {
                result = Id.Equals(presentation.Id);
            }

            return result;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Loads the specified location.
        /// </summary>
        /// <param name="location">The location.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public static Presentation Load(string path)
        {
            Logger.LogEntry();

            if (File.Exists(path) == false)
            {
                throw new FileNotFoundException("Specified presentation is not found", path);
            }
            Serializer<PresentationDTO> serializer = new Serializer<PresentationDTO>();

            PresentationDTO presentationDTO = (PresentationDTO)serializer.Load(path);

            Mapper.CreateMap<SlideDTO, ISlide>();
            Mapper.CreateMap<Presentation, PresentationDTO>();
            Presentation presentation = new Presentation();
            Mapper.Map<PresentationDTO, IPresentation>(presentationDTO, presentation);

            Logger.LogExit();
            return presentation;
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            Save(this.Path);
        }

        /// <summary>
        /// Saves to.
        /// </summary>
        /// <param name="path">The path.</param>
        public void Save(string path)
        {
            Logger.LogEntry();

            if (IsDirty == true)
            {
                Serializer<PresentationDTO> serializer = new Serializer<PresentationDTO>();
                Mapper.CreateMap<ISlide, SlideDTO>();
                Mapper.CreateMap<Presentation, PresentationDTO>();
                PresentationDTO presentationDTO = new PresentationDTO();
                Mapper.Map<IPresentation, PresentationDTO>(this, presentationDTO);
                if (serializer.Save(presentationDTO, path) == true)
                {
                    this.IsDirty = false;
                }
            }

            Logger.LogExit();
        }

        /// <summary>
        /// Marks this instance as dirty.
        /// </summary>
        public void MarkDirty()
        {
            this.IsDirty = true;
        }

        /// <summary>
        /// Adds the new slide to presentation.
        /// </summary>
        public void AddNewSlide()
        {
            ISlide slide = new Slide() { Label = "Test" };
            //slide.AddElement(new Text() { X = 100, Y = 100, Width = 200, Height = 60, Color = Brushes.White });
            Slides.Add(slide);
        }

        /// <summary>
        /// Adds a new slide to slide.
        /// </summary>
        /// <param name="slide"></param>
        public void AddSlide(ISlide slide)
        {
            this._slides.Add(slide);
        }

        /// <summary>
        /// Removes the element.
        /// </summary>
        /// <param name="slide">The slide.</param>
        public void RemoveSlide(ISlide slide)
        {
            this._slides.Remove(slide);
        }

        /// <summary>
        /// Moves the element.
        /// </summary>
        /// <param name="oldIndex">The old index.</param>
        /// <param name="newIndex">The new index.</param>
        public void MoveSlide(int oldIndex, int newIndex)
        {
            var item = this._slides[oldIndex];
            this._slides.RemoveAt(oldIndex);
            if (this._slides.Count > newIndex)
            {
                this._slides.Insert(newIndex, item);
            }
        }

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public object Clone()
        {
            Logger.LogEntry();

            IPresentation clonedPresentation = new Presentation();

            try
            {
                using (var stream = new MemoryStream())
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(stream, this);
                    stream.Position = 0;

                    clonedPresentation = (IPresentation)binaryFormatter.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                Logger.LogMsg.Error(ex.Message, ex);
            }

            Logger.LogExit();

            return clonedPresentation;
        }

        #endregion

        #region Operators

        public static implicit operator Presentation(PresentationDTO presentationDTO)
        {
            Presentation presentation = new Presentation()
            {
                Category = presentationDTO.Category,
                Height = presentationDTO.Height,
                Id = presentationDTO.Id,
                IsDirty = presentationDTO.IsDirty,
                Name = presentationDTO.Name,
                ParentLibraryLocation = presentationDTO.ParentLibraryLocation,
                Width = presentationDTO.Width,
            };

            //presentationDTO.Slides.ForEach(slide => presentation.AddSlide(slide));

            return presentation;
        }

        #endregion

        #region IPresentationItem Member Methods



        #endregion

        #endregion

        #region Events

        /// <summary>
        /// Occurs when elements collection changes.
        /// </summary>
        public event NotifyCollectionChangedEventHandler SlidesCollectionChanged;

        /// <summary>
        /// Called when elements collection changes.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="NotifyCollectionChangedEventArgs"/> instance containing the event data.</param>
        private void OnSlidesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (SlidesCollectionChanged != null)
            {
                SlidesCollectionChanged(sender, e);
            }
        }

        #endregion

    }
}
