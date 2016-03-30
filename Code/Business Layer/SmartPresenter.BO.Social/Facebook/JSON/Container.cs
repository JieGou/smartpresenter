using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SmartPresenter.BO.Social.Facebook.JSON
{
    /// <summary>
    /// A container to hold like informtaion for picture.
    /// </summary>
    [DataContract]
    public class PictureLikesContainer
    {
        #region Properties

        [DataMember(Name = "data")]
        public List<FacebookPersonJSON> Likes { get; set; }

        #endregion
    }

    /// <summary>
    /// A contaner to hold user's like information.(What pages, person user might be liking)
    /// </summary>
    [DataContract]
    public class UsersLikesContainer
    {
        #region Properties

        [DataMember(Name = "data")]
        public List<FacebookUserLikeJSON> Likes { get; set; }

        #endregion
    }

    /// <summary>
    /// A container for user's interests.
    /// </summary>
    [DataContract]
    public class UsersInterestsContainer
    {
        #region Properties

        [DataMember(Name = "data")]
        public List<FacebookUserLikeJSON> Interests { get; set; }

        #endregion
    }

    /// <summary>
    /// A container for comments.
    /// </summary>
    [DataContract]
    public class CommentsContainer
    {
        #region Properties

        [DataMember(Name = "data")]
        public List<FacebookCommentJSON> Comments { get; set; }

        #endregion
    }

    /// <summary>
    /// A container for Facebook friends data.
    /// </summary>
    [DataContract]
    public class FriendContainer
    {
        #region Properties

        [DataMember(Name = "data")]
        public List<FacebookPersonJSON> Friends { get; set; }

        #endregion
    }

    /// <summary>
    /// A container for Facebook user's family member's list data.
    /// </summary>
    [DataContract]
    public class MemberListContainer
    {
        #region Properties

        [DataMember(Name = "data")]
        public List<FacebookFamilyMemberJSON> Members { get; set; }

        #endregion
    }

    /// <summary>
    /// A container for facebook album data.
    /// </summary>
    [DataContract]
    public class AlbumContainer
    {
        #region Properties

        [DataMember(Name = "data")]
        public List<FacebookAlbumJSON> Albums { get; set; }

        #endregion
    }

    /// <summary>
    /// A container for facebook photo data.
    /// </summary>
    [DataContract]
    public class PhotoContainer
    {
        #region Properties

        [DataMember(Name = "data")]
        public List<FacebookPhotoJSON> Photos { get; set; }

        #endregion
    }

    /// <summary>
    /// A container for facebook Friend Lists.
    /// </summary>
    [DataContract]
    public class FriendListContainer
    {
        #region Properties

        [DataMember(Name = "data")]
        public List<FacebookFriendListJSON> FriendList { get; set; }

        #endregion
    }

    /// <summary>
    /// A container for Facebook Movie data.
    /// </summary>
    [DataContract]
    public class MovieListContainer
    {
        #region Properties

        [DataMember(Name = "data")]
        public List<FacebookMovieJSON> Movies { get; set; }

        #endregion
    }

    /// <summary>
    /// A container for Facebook Music data.
    /// </summary>
    [DataContract]
    public class MusicListContainer
    {
        #region Properties

        [DataMember(Name = "data")]
        public List<FacebookMusicJSON> MusicList { get; set; }

        #endregion
    }

    /// <summary>
    /// A container for Facebook Book data.
    /// </summary>
    [DataContract]
    public class BookListContainer
    {
        #region Properties

        [DataMember(Name = "data")]
        public List<FacebookBookJSON> Books { get; set; }

        #endregion
    }

    /// <summary>
    /// A container for user activity.
    /// </summary>
    [DataContract]
    public class ActivityListContainer
    {
        #region Properties

        [DataMember(Name = "data")]
        public List<FacebookActivityJSON> Activities { get; set; }

        #endregion
    }

    /// <summary>
    /// A container for user's Facebook Groups.
    /// </summary>
    [DataContract]
    public class GroupsContainer
    {
        #region Properties

        [DataMember(Name = "data")]
        public List<FacebookGroupJSON> Groups { get; set; }

        #endregion
    }

    /// <summary>
    /// A container for user's Facebook Television Show.
    /// </summary>
    [DataContract]
    public class TelevisionShowsContainer
    {
        #region Properties

        [DataMember(Name = "data")]
        public List<FacebookTelevisionShowJSON> TelevisionShows { get; set; }

        #endregion
    }

    /// <summary>
    /// A container for user's Facebook notes.
    /// </summary>
    [DataContract]
    public class NotesContainer
    {
        #region Properties

        [DataMember(Name = "data")]
        public List<FacebookNoteJSON> Notes { get; set; }

        #endregion
    }

    /// <summary>
    /// A container for user's Facebook videos.
    /// </summary>
    [DataContract]
    public class VideosContainer
    {
        #region Properties

        [DataMember(Name = "data")]
        public List<FacebookVideoJSON> Videos { get; set; }

        #endregion
    }

    /// <summary>
    /// A container for user's Facebook pages.
    /// </summary>
    [DataContract]
    public class PagesContainer
    {
        #region Properties

        [DataMember(Name = "data")]
        public List<FacebookPageJSON> Pages { get; set; }

        #endregion
    }

    /// <summary>
    /// A container for user's Facebook locations.
    /// </summary>
    [DataContract]
    public class LocationsContainer
    {
        #region Properties

        [DataMember(Name = "data")]
        public List<FacebookLocationJSON> Locations { get; set; }

        #endregion
    }
}
