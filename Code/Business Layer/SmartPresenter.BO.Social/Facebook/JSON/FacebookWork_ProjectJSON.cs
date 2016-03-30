using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SmartPresenter.BO.Social.Facebook.JSON
{
    /// <summary>
    /// A class to hold Work Project information for Facebook user.
    /// </summary>
    [DataContract]
    public class FacebookWork_ProjectJSON
    {
        #region Properties

        [DataMember(Name = "description")]
        public string Description { get; set; }
        [DataMember(Name = "start_date")]
        public string StartDateInternal { get; set; }
        [DataMember(Name = "end_date")]
        public string EndDateInternal { get; set; }
        [DataMember(Name = "with")]
        public List<FacebookPersonJSON> With { get; set; }

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
            return Description;
        }

        #endregion
    }
}
