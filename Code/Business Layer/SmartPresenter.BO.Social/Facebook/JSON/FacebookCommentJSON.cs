using System;
using System.Runtime.Serialization;

namespace SmartPresenter.BO.Social.Facebook.JSON
{
    /// <summary>
    /// A Facebook Comment on any post/picture/video/status etc.
    /// </summary>
    [DataContract]
    public class FacebookCommentJSON
    {
        #region Properties

        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "can_remove")]
        public bool CanRemove { get; set; }
        [DataMember(Name = "created_time")]
        public string CreatedTimeInternal { get; set; }
        [DataMember(Name = "from")]
        public FacebookPersonJSON From { get; set; }
        [DataMember(Name = "like_count")]
        public int LikeCount { get; set; }
        [DataMember(Name = "message")]
        public string Message { get; set; }
        [DataMember(Name = "user_likes")]
        public bool UserLikes { get; set; }

        public DateTime CreatedTime
        {
            get
            {
                DateTime createdTime;
                DateTime.TryParse(CreatedTimeInternal, out createdTime);
                return createdTime;
            }
        }

        #endregion
    }
}
