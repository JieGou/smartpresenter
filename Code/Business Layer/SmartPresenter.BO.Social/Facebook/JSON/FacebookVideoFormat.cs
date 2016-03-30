using System.Runtime.Serialization;

namespace SmartPresenter.BO.Social.Facebook.JSON
{
    /// <summary>
    /// Class for Video Format info.
    /// </summary>
    [DataContract]
    public class FacebookVideoFormat
    {
        #region Properties

        [DataMember(Name = "embed_html")]
        public string EmbedHTML { get; set; }
        [DataMember(Name = "filter")]
        public string Filter { get; set; }
        [DataMember(Name = "picture")]
        public string Picture { get; set; }
        [DataMember(Name = "width")]
        public int Width { get; set; }
        [DataMember(Name = "height")]
        public int Height { get; set; }

        #endregion
    }
}
