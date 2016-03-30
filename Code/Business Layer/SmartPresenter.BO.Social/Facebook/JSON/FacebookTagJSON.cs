using System;
using System.Runtime.Serialization;

namespace SmartPresenter.BO.Social.Facebook.JSON
{
    /// <summary>
    /// Class for a Facebook tag. Currently being used for people tag in video.
    /// </summary>
    [DataContract]
    public class FacebookTagJSON : CommonJSON
    {
        #region Properties

        [DataMember(Name = "created_time")]
        public string CreatedTimeInternal { get; set; }

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
