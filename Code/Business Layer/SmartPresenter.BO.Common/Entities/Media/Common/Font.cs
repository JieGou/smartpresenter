
using SmartPresenter.Common.Logger;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Media;

namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// A Font object for text objects.
    /// </summary>
    [Serializable]
    public class Font : ICloneable, INotifyPropertyChanged
    {
        #region Private Data Members

        private string _name;
        private int _size;
        private string _style;
        private bool _isStrikeout;
        private bool _isUnderLine;
        [NonSerialized]
        private Color _color;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the Font class.
        /// </summary>
        public Font()
        {
            this.Name = string.Empty;
            this.Size = 0;
            this.Style = string.Empty;
            this.IsStrikeout = false;
            this.IsUnderline = false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        /// <summary>
        /// Gets or sets the style.
        /// </summary>
        /// <value>
        /// The style.
        /// </value>
        public string Style
        {
            get
            {
                return _style;
            }
            set
            {
                _style = value;
                OnPropertyChanged("Style");
            }
        }
        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        /// <value>
        /// The size.
        /// </value>
        public int Size
        {
            get
            {
                return _size;
            }
            set
            {
                if(value < 0)
                {
                    throw new ArgumentOutOfRangeException("Font Size", "Font size can't be negative");
                }
                _size = value;
                OnPropertyChanged("Size");
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is strikeout.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is strikeout; otherwise, <c>false</c>.
        /// </value>
        public bool IsStrikeout
        {
            get
            {
                return _isStrikeout;
            }
            set
            {
                _isStrikeout = value;
                OnPropertyChanged("IsStrikeout");
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is underline.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is underline; otherwise, <c>false</c>.
        /// </value>
        public bool IsUnderline
        {
            get
            {
                return _isUnderLine;
            }
            set
            {
                _isUnderLine = value;
                OnPropertyChanged("IsUnderline");
            }
        }

        /// <summary>
        /// Gets or sets the color of font.
        /// </summary>
        /// <value>
        /// The font color.
        /// </value>
        public Color Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
                OnPropertyChanged("Color");
            }
        }

        #endregion

        #region Methods

        #region Public Methods

        /// <summary>
        /// Method which fires PropertyChanged.
        /// </summary>
        /// <param name="propertyName"></param>
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
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
            Font clonedFont = new Font();

            try
            {
                using (var stream = new MemoryStream())
                {
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    binaryFormatter.Serialize(stream, this);
                    stream.Position = 0;

                    clonedFont = (Font)binaryFormatter.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                Logger.LogMsg.Error(ex.Message, ex);
            }

            return clonedFont;
        }

        #endregion

        #endregion

        #region Events

        /// <summary>
        /// PropertyChanged Event to notify any changes to UI.
        /// </summary>
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
