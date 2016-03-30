using System.Runtime.Serialization;

namespace SmartPresenter.BO.Social.Facebook.JSON
{
    /// <summary>
    /// A Facebook class meant for storing user's class in which he/she used to study.
    /// </summary>
    [DataContract]
    public class FacebookClassJSON : CommonJSON
    {
        #region Properties

        [DataMember(Name = "with")]
        public FacebookPersonJSON With { get; set; }

        #endregion
    }
}
