using System;
using System.Runtime.Serialization;

namespace SmartPresenter.BO.Social.Facebook.JSON
{
    /// <summary>
    /// Class for Facebook Note info.
    /// </summary>
    [DataContract]
    public class FacebookNoteJSON
    {
        #region Properties

        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "from")]
        public FacebookPersonJSON From { get; set; }
        [DataMember(Name = "subject")]
        public string Subject { get; set; }
        [DataMember(Name = "message")]
        public string Message { get; set; }
        [DataMember(Name = "icon")]
        public string Icon { get; set; }
        [DataMember(Name = "created_time")]
        public string CreatedTimeInternal { get; set; }
        [DataMember(Name = "updated_time")]
        public string UpdatedTimeInternal { get; set; }

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
