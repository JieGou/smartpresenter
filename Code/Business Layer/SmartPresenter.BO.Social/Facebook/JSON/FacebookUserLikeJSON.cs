using System;
using System.Runtime.Serialization;

namespace SmartPresenter.BO.Social.Facebook.JSON
{
    /// <summary>
    /// A class to store Facebook user likes.
    /// </summary>
    [DataContract]
    public class FacebookUserLikeJSON : CommonJSON
    {
        #region Properties

        [DataMember(Name = "category")]
        public string Category { get; set; }
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
