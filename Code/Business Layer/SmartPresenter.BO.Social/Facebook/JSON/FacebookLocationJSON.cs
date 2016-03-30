using System;
using System.Runtime.Serialization;

namespace SmartPresenter.BO.Social.Facebook.JSON
{
    /// <summary>
    /// Class for Facebook location.
    /// </summary>
    [DataContract]
    public class FacebookLocationJSON
    {
        #region Properties

        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "from")]
        public FacebookPersonJSON From { get; set; }
        [DataMember(Name = "type")]
        public string Type { get; set; }
        [DataMember(Name = "place")]
        public Place Place { get; set; }
        [DataMember(Name = "application")]
        public CommonJSON Application { get; set; }
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

    /// <summary>
    /// Class for a Place.
    /// </summary>
    [DataContract]
    public class Place : CommonJSON
    {
        #region Properties

        [DataMember(Name = "location")]
        public Address Address { get; set; }

        #endregion
    }

    /// <summary>
    /// Class for an Address of a location.
    /// </summary>
    [DataContract]
    public class Address
    {
        #region Properties

        [DataMember(Name = "street")]
        public string Street { get; set; }
        [DataMember(Name = "zip")]
        public string Zip { get; set; }
        [DataMember(Name = "latitude")]
        public double Latitude { get; set; }
        [DataMember(Name = "longitude")]
        public double Longitude { get; set; }

        #endregion
    }
}
