using System.Runtime.Serialization;

namespace SmartPresenter.BO.Social.Facebook.JSON
{
    /// <summary>
    /// Base clss for holding JSON data, most of the objects are in this basic form.
    /// </summary>
    [DataContract]
    public class CommonJSON
    {
        #region Properties

        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }

        #endregion

        #region Overridden Methods

        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}
