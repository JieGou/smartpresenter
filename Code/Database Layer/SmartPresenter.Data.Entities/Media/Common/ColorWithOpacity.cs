namespace SmartPresenter.Data.Entities
{
    /// <summary>
    /// A construct to hold color with opacity value where opacity can range from 1 to 100.
    /// </summary>
    public struct ColorWithOpacity
    {
        #region Properties

        private int _opacity;

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>
        /// The color.
        /// </value>
        public string Color { get; set; }

        /// <summary>
        /// Gets or sets the opacity.
        /// </summary>
        /// <value>
        /// The opacity.
        /// </value>
        public int Opacity { get; set; }

        #endregion   
    }
}
