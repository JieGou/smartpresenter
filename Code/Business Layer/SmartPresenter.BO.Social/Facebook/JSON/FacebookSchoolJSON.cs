using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SmartPresenter.BO.Social.Facebook.JSON
{
    /// <summary>
    /// Facebook user's school info class.
    /// </summary>
    [DataContract]
    public class FacebookSchoolJSON
    {
        #region Properties

        [DataMember(Name = "school")]
        public CommonJSON Name { get; set; }

        [DataMember(Name = "year")]
        public FacebookYearJSON Year { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "concentration")]
        public FacebookConcentrationJSON Concentration { get; set; }

        [DataMember(Name = "classes")]
        public List<FacebookClassJSON> Classes { get; set; }

        [DataMember(Name = "degree")]
        public FacebookDegreeJSON Degree { get; set; }

        #endregion

    }
}
