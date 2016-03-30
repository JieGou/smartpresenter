using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace SmartPresenter.Service.CloudSyncService
{
    [DataContract]
    public class UserAccount
    {
        [DataMember]
        public int Serial { get; set; }
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string EMail { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public string Type { get; set; }
    }
}