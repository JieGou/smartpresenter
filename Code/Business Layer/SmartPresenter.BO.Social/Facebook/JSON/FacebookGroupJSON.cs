using System.Runtime.Serialization;

namespace SmartPresenter.BO.Social.Facebook.JSON
{
    /// <summary>
    /// Class for a Facebook Group.
    /// </summary>
    [DataContract]
    public class FacebookGroupJSON : CommonJSON
    {
        #region Properties

        [DataMember(Name = "unread")]
        public int Unread { get; set; }
        [DataMember(Name = "bookmark_order")]
        public int BookmarkOrder { get; set; }

        #endregion
    }
}
