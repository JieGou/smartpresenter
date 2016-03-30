using SmartPresenter.Common.Logger;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Media;

namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// A construct to hold color with opacity value where opacity can range from 1 to 100.
    /// </summary>
    public struct ColorWithOpacity : ICloneable
    {
        #region Properties

        private int _opacity;

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        public Color Color { get; set; }

        /// <summary>
        /// Gets or sets the opacity.
        /// </summary>
        /// <value>
        /// The opacity.
        /// </value>
        public int Opacity
        {
            get
            {
                return _opacity;
            }
            set
            {
                if (value < 0 || value > 1)
                {
                    throw new ArgumentOutOfRangeException("Opacity", "Opacity should be in range from 0 to 1");
                }
                _opacity = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a new object that is a copy of the current instance.
        /// </summary>
        /// <returns>
        /// A new object that is a copy of this instance.
        /// </returns>
        public object Clone()
        {
            ColorWithOpacity clonedColorWithOpacity = new ColorWithOpacity();

            try
            {
                using (var stream = new MemoryStream())
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(stream, this);
                    stream.Position = 0;

                    clonedColorWithOpacity = (ColorWithOpacity)binaryFormatter.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                Logger.LogMsg.Error(ex.Message, ex);
            }

            return clonedColorWithOpacity;
        }

        #endregion
    }
}
