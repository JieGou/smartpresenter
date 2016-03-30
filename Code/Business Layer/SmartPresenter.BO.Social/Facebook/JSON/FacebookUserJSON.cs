using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;

namespace SmartPresenter.BO.Social.Facebook.JSON
{
    /// <summary>
    /// A Facebook User.
    /// </summary>
    [DataContract]
    public class FacebookUserJSON
    {
        #region Properties

        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "bio")]
        public string AboutMe { get; set; }
        [DataMember(Name = "link")]
        public string ProfileLink { get; set; }
        [DataMember(Name = "username")]
        public string UserId { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "first_name")]
        public string FirstName { get; set; }
        [DataMember(Name = "middle_name")]
        public string MiddleName { get; set; }
        [DataMember(Name = "last_name")]
        public string LastName { get; set; }
        [DataMember(Name = "birthday")]
        public string DateOfBirthInternal { get; set; }
        [DataMember(Name = "gender")]
        public string Gender { get; set; }
        [DataMember(Name = "religion")]
        public string Religion { get; set; }
        [DataMember(Name = "political")]
        public string PoliticalView { get; set; }
        [DataMember(Name = "email")]
        public string EMail { get; set; }
        [DataMember(Name = "quotes")]
        public string Quotes { get; set; }
        [DataMember(Name = "relationship_status")]
        public string RelationshipStatus { get; set; }
        [DataMember(Name = "interested_in")]
        public List<string> InterestedIn { get; set; }
        [DataMember(Name = "hometown")]
        public FacebookCityJSON HomeTown { get; set; }
        [DataMember(Name = "location")]
        public FacebookCityJSON CurrentCity { get; set; }
        [DataMember(Name = "timezone")]
        public double TimeZone { get; set; }
        [DataMember(Name = "locale")]
        public string Locale { get; set; }
        [DataMember(Name = "work")]
        public List<FacebookWorkJSON> WorkList { get; set; }
        [DataMember(Name = "sports")]
        public List<FacebookSportJSON> SportList { get; set; }
        [DataMember(Name = "education")]
        public List<FacebookSchoolJSON> Education { get; set; }
        [DataMember(Name = "verified")]
        public bool Verified { get; set; }
        [DataMember(Name = "updated_time")]
        public string UpdatedTimeInternal { get; set; }
        [DataMember(Name = "languages")]
        public List<FacebookLanguageJSON> Languages { get; set; }

        public List<FacebookPersonJSON> Friends { get; set; }
        public List<FacebookFriendListJSON> FriendLists { get; set; }
        public List<FacebookAlbumJSON> Albums { get; set; }
        public List<FacebookUserLikeJSON> Likes { get; set; }
        public List<FacebookUserLikeJSON> Interests { get; set; }
        public List<FacebookMovieJSON> Movies { get; set; }
        public List<FacebookMusicJSON> MusicList { get; set; }
        public List<FacebookActivityJSON> Activities { get; set; }
        public List<FacebookGroupJSON> Groups { get; set; }
        public List<FacebookTelevisionShowJSON> TVShows { get; set; }
        public List<FacebookNoteJSON> Notes { get; set; }
        public List<FacebookPersonJSON> Following { get; set; }
        public List<FacebookPersonJSON> Followers { get; set; }
        public List<FacebookAlbumJSON> TaggedPhotos { get; set; }
        public List<FacebookAlbumJSON> Photos { get; set; }
        public List<FacebookVideoJSON> TaggedVideos { get; set; }
        public List<FacebookVideoJSON> Videos { get; set; }
        public List<FacebookBookJSON> Books { get; set; }
        public List<FacebookFamilyMemberJSON> FamilyMembers { get; set; }
        public List<FacebookPageJSON> Pages { get; set; }
        public List<FacebookLocationJSON> Locations { get; set; }

        public string ProfilePicture { get; set; }

        public DateTime DateOfBirth
        {
            get
            {
                DateTime dateOfBirth;
                string[] formats = { "mm/dd/yyyy", "dd/mm/yyy" };
                DateTime.TryParseExact(DateOfBirthInternal, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateOfBirth);
                return dateOfBirth;
            }
        }

        public DateTime UpdatedTime
        {
            get
            {
                DateTime updatedTime;
                DateTime.TryParse(UpdatedTimeInternal, out updatedTime);
                return updatedTime;
            }
        }

        #endregion

        #region Overridden Methods

        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}
