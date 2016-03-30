using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SmartPresenter.BO.Social.Facebook.JSON
{
    /// <summary>
    /// Class for a Facebook Photo.
    /// </summary>
    [DataContract]
    public class FacebookPhotoJSON
    {
        #region Properties

        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "created_time")]
        public string CreatedTimeInternal { get; set; }
        [DataMember(Name = "height")]
        public int Height { get; set; }
        [DataMember(Name = "from")]
        public FacebookPersonJSON Owner { get; set; }
        [DataMember(Name = "icon")]
        public string Icon { get; set; }
        [DataMember(Name = "images")]
        public List<FacebookImageJSON> Images { get; set; }
        [DataMember(Name = "link")]
        public string Link { get; set; }
        [DataMember(Name = "picture")]
        public string Picture { get; set; }
        [DataMember(Name = "source")]
        public string Source { get; set; }
        [DataMember(Name = "width")]
        public int Width { get; set; }
        [DataMember(Name = "updated_time")]
        public string UpdatedTimeInternal { get; set; }
        [DataMember(Name = "comments")]
        public CommentsContainer Comments { get; set; }
        [DataMember(Name = "likes")]
        public PictureLikesContainer Likes { get; set; }

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

        #endregion

        #region Overridden Methods

        public override string ToString()
        {
            return Source;
        }

        #endregion
    }

    /// <summary>
    /// Class for an Image object that is contained inside a Photo object.
    /// </summary>
    [DataContract]
    public class FacebookImageJSON
    {
        #region Properties

        [DataMember(Name = "source")]
        public string Source { get; set; }
        [DataMember(Name = "width")]
        public int Width { get; set; }
        [DataMember(Name = "height")]
        public int Height { get; set; }

        #endregion

        #region Overridden Methods

        public override string ToString()
        {
            return Source;
        }

        #endregion
    }
}
