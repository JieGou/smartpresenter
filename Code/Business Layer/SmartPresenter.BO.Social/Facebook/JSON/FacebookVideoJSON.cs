using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SmartPresenter.BO.Social.Facebook.JSON
{
    /// <summary>
    /// Class for Video info.
    /// </summary>
    [DataContract]
    public class FacebookVideoJSON : CommonJSON
    {
        #region Properties

        [DataMember(Name = "from")]
        public FacebookPersonJSON From { get; set; }
        [DataMember(Name = "description")]
        public string Description { get; set; }
        [DataMember(Name = "picture")]
        public string Picture { get; set; }
        [DataMember(Name = "embed_html")]
        public string EmbedHTML { get; set; }
        [DataMember(Name = "icon")]
        public string Icon { get; set; }
        [DataMember(Name = "source")]
        public string Source { get; set; }
        [DataMember(Name = "created_time")]
        public string CreatedTimeInternal { get; set; }
        [DataMember(Name = "updated_time")]
        public string UpdatedTimeInternal { get; set; }
        [DataMember(Name = "format")]
        public List<FacebookVideoFormat> Formats { get; set; }
        [DataMember(Name = "tags")]
        public List<FacebookTagJSON> Tags { get; set; }

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
    }
}
