using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SmartPresenter.BO.Social.Facebook.JSON
{
    /// <summary>
    /// A Facebook Album class.
    /// </summary>
    [DataContract]
    public class FacebookAlbumJSON : CommonJSON
    {
        #region Properties

        [DataMember(Name = "from")]
        public FacebookPersonJSON From { get; set; }
        [DataMember(Name = "link")]
        public string Link { get; set; }
        [DataMember(Name = "cover_photo")]
        public long CoverPhotoId { get; set; }
        [DataMember(Name = "privacy")]
        public string Privacy { get; set; }
        [DataMember(Name = "count")]
        public int Count { get; set; }
        [DataMember(Name = "type")]
        public string Type { get; set; }
        [DataMember(Name = "created_time")]
        public string CreatedTimeInternal { get; set; }
        [DataMember(Name = "updated_time")]
        public string UpdatedTimeInternal { get; set; }
        [DataMember(Name = "can_upload")]
        public bool CanUpload { get; set; }
        [DataMember(Name = "likes")]
        public PictureLikesContainer Likes { get; set; }
        [DataMember(Name = "comments")]
        public CommentsContainer Comments { get; set; }

        public DateTime CreatedTime
        {
            get
            {
                DateTime createdTime;
                DateTime.TryParse(CreatedTimeInternal, out createdTime);
                return createdTime;
            }
        }

        public DateTime UpdatedTime
        {
            get
            {
                DateTime updatedTime;
                DateTime.TryParse(UpdatedTimeInternal, out updatedTime);
                return updatedTime;
            }
        }

        public List<FacebookPhotoJSON> Photos { get; set; }

        #endregion
    }
}
