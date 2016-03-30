using System.Runtime.Serialization;

namespace SmartPresenter.Service.LocalSync
{
    [DataContract]
    public enum SyncFileType : int
    {
        [EnumMember]
        Presentation,
        [EnumMember]
        Image,
        [EnumMember]
        Video,
        [EnumMember]
        Audio,
        [EnumMember]
        Setting,
        [EnumMember]
        Xml,
        [EnumMember]
        Other,
        [EnumMember]
        Playlist,
        [EnumMember]
        Library,
    }
}