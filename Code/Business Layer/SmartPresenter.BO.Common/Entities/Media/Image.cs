using SmartPresenter.BO.Common.Enums;
using SmartPresenter.BO.Common.Interfaces;
using SmartPresenter.Common.Enums;
using SmartPresenter.Common.Extensions;
using System;
using System.IO;
using System.Windows.Media;
using System.Xml;

namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// Class representing an image object.
    /// </summary>
    [Serializable]
    public class Image : Rectangle, IFileSource
    {
        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="Image"/> class.
        /// </summary>
        public Image()
        {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Image"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public Image(string path)
        {
            if (File.Exists(path) == false)
            {
                throw new FileNotFoundException("Specified media file does not exist", path);
            }
            Path = path;
            Initialize();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public override ElementType Type
        {
            get { return ElementType.Image; }
        }

        /// <summary>
        /// Gets or sets the path of Image.
        /// </summary>
        /// <value>
        /// The path of Image.
        /// </value>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the stretch.
        /// </summary>
        /// <value>
        /// The stretch.
        /// </value>
        public Stretch Stretch { get; set; }

        #endregion

        #region Methods

        private void Initialize()
        {
            Stretch = Stretch.Fill;
        }

        /// <summary>
        /// Renders an object.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void Render()
        {
            throw new NotImplementedException();
        }        

        #endregion
    }
}
