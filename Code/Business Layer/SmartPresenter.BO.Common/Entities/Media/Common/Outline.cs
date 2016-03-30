using SmartPresenter.Common.Logger;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Media;

namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// Outline for text and other objects
    /// </summary>
    public struct Outline : ICloneable
    {
        #region Properties

        /// <summary>
        /// Gets or sets the size of Outline.
        /// </summary>
        /// <value>
        /// The size of Outline.
        /// </value>
        public int Size { get; set; }
        /// <summary>
        /// Gets or sets the color of Outline.
        /// </summary>
        /// <value>
        /// The color of Outline.
        /// </value>
        public Color Color { get; set; }

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
            Outline clonedOutline = new Outline();

            try
            {
                using (var stream = new MemoryStream())
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(stream, this);
                    stream.Position = 0;

                    clonedOutline = (Outline)binaryFormatter.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                Logger.LogMsg.Error(ex.Message, ex);
            }

            return clonedOutline;
        }

        #endregion
    }
}
