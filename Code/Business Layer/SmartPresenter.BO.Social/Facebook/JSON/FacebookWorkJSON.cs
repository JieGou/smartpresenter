using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SmartPresenter.BO.Social.Facebook.JSON
{
    /// <summary>
    /// A class to store Facebook user's work info.
    /// </summary>
    [DataContract]
    public class FacebookWorkJSON
    {
        #region Properties

        [DataMember(Name = "employer")]
        public FacebookEmployerJSON Employer { get; set; }
        [DataMember(Name = "location")]
        public FacebookCityJSON Location { get; set; }
        [DataMember(Name = "position")]
        public FacebookWork_PositionJSON Position { get; set; }
        [DataMember(Name = "start_date")]
        public string StartDateInternal { get; set; }
        [DataMember(Name = "end_date")]
        public string EndDateInternal { get; set; }
        [DataMember(Name = "projects")]
        public List<FacebookWork_ProjectJSON> Projects { get; set; }

        public DateTime StartDate
        {
            get
            {
                DateTime startDate;
                DateTime.TryParse(StartDateInternal, out startDate);
                return startDate;
            }
        }

        public DateTime EndDate
        {
            get
            {
                DateTime endDate;
                DateTime.TryParse(EndDateInternal, out endDate);
                return endDate;
            }
        }

        #endregion

        #region Overridden Methods

        public override string ToString()
        {
            if (Position != null)
            {
                return Position.ToString();
            }
            return string.Empty;
        }

        #endregion

    }
}
