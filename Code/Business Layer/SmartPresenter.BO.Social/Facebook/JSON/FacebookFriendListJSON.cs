using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SmartPresenter.BO.Social.Facebook.JSON
{
    /// <summary>
    /// Class for Facebook friend list.
    /// </summary>
    [DataContract]
    public class FacebookFriendListJSON : CommonJSON
    {
        #region Properties

        [DataMember(Name = "list_type")]
        public string Type { get; set; }
        [DataMember(Name = "owner")]
        public string Owner { get; set; }

        public List<FacebookPersonJSON> Friends { get; set; }

        #endregion
    }
}
