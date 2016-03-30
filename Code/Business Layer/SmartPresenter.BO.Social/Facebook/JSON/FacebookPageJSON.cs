using System.Runtime.Serialization;

namespace SmartPresenter.BO.Social.Facebook.JSON
{
    /// <summary>
    /// Class for a facebook Page.
    /// </summary>
    [DataContract]
    public class FacebookPageJSON : CommonJSON
    {
        #region Properties

        [DataMember(Name = "category")]
        public string Category { get; set; }

        #endregion
    }
}
