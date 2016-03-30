using System.Runtime.Serialization;

namespace SmartPresenter.BO.Social.Facebook.JSON
{
    /// <summary>
    /// Class for Users Family Members list.
    /// </summary>
    [DataContract]
    public class FacebookFamilyMemberJSON : CommonJSON
    {
        #region Properties

        [DataMember(Name = "relationship")]
        public string Relation { get; set; }

        #endregion
    }
}
