
using Facebook;
using SmartPresenter.BO.Common;
using SmartPresenter.BO.Social.Facebook.JSON;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
namespace SmartPresenter.BO.Social.Facebook
{
    public sealed class FacebookManager
    {
        #region Private Data Members

        private static volatile FacebookManager _instance;
        private static Object _lockObject = new Object();
        private FacebookClient _facebookClient;
        private FacebookUserJSON _facebookUser;
        private string _accessToken;
        private string _appId;
        private string _appSecret;

        #endregion

        #region Constructor

        /// <summary>
        /// Prevents a default instance of the <see cref="FacebookManager"/> class from being created.
        /// </summary>
        private FacebookManager()
        {
            Initialize();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static FacebookManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lockObject)
                    {
                        if (_instance == null)
                        {
                            _instance = new FacebookManager();
                        }
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is logged in.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is logged in; otherwise, <c>false</c>.
        /// </value>
        public bool IsLoggedIn { get; set; }

        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        /// <value>
        /// The access token.
        /// </value>
        public string AccessToken
        {
            get
            {
                return _accessToken;
            }
            set
            {
                _accessToken = value;
                //Settings.SocialMediaSettings.FacebookAccessToken = value;
                //Settings.SocialMediaSettings.IsLoggedIn = value == null ? false : true;
            }
        }

        /// <summary>
        /// Gets or sets the facebook user.
        /// </summary>
        /// <value>
        /// The facebook user.
        /// </value>
        public FacebookUserJSON FacebookUser
        {
            get
            {
                return _facebookUser;
            }
            set
            {
                _facebookUser = value;
            }
        }

        #endregion

        #region Methods

        #region Private Methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            _appId = ConfigurationManager.AppSettings["facebook:AppId"].ToString();
            _appSecret = ConfigurationManager.AppSettings["facebook:AppSecret"].ToString();
        }

        /// <summary>
        /// Gets the access token from code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        private string GetAccessTokenFromCode(FacebookOAuthResult oauthResult)
        {
            var client = new FacebookClient();
            dynamic result = client.Get("/oauth/access_token",
              new
              {
                  client_id = _appId,
                  client_secret = _appSecret,
                  redirect_uri = "http://localhost/smartpresenter",
                  //redirect_uri = "https://www.facebook.com/connect/login_success.html",
                  code = oauthResult.Code
              });
            return result.access_token;
        }

        /// <summary>
        /// Imports all data from Facebook.
        /// </summary>
        private void Import()
        {
            Task.Run(() =>
            {
                _facebookUser = GetUserProfile();
                _facebookUser.Friends = GetUserFriends();
                _facebookUser.Albums = GetUserAlbums();
                _facebookUser.TaggedVideos = GetUserTaggedVideos();
                _facebookUser.Videos = GetUserVideos();
                _facebookUser.TaggedPhotos = GetUserTaggedPhotos();
                _facebookUser.Photos = GetUserPhotos();
                _facebookUser.Likes = GetUserLikes();
                //_facebookUser.Interests = GetUserInterests();
                _facebookUser.FriendLists = GetUserFriendLists();
                _facebookUser.Movies = GetUserMovies();
                _facebookUser.MusicList = GetUserMusic();
                _facebookUser.Books = GetUserBooks();
                _facebookUser.FamilyMembers = GetUserFamilyMembers();
                _facebookUser.Pages = GetUserPages();
                //_facebookUser.Activities = GetUserActivities();
                _facebookUser.Groups = GetUserGroups();
                //_facebookUser.Locations = GetUserLocations();
                //_facebookUser.Notes = GetUserNotes();
                //_facebookUser.Following = GetUserFollowings();
                //_facebookUser.Followers = GetUserFollowers();
                _facebookUser.TVShows = GetUserTelivisionShows();
                int i = 0;
            }).ContinueWith((task) =>
            {
                OnUserProfileUpdated();
                //SaveProfile(_facebookUser);
            });
        }

        /// <summary>
        /// Gets User's Facebook Profiles.
        /// </summary>
        /// <returns></returns>
        private FacebookUserJSON GetUserProfile()
        {
            FacebookUserJSON user = null;
            string me = _facebookClient.Get(FacebookKeyStrings.ME).ToString();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(FacebookUserJSON));
            using (MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(me)))
            {
                user = serializer.ReadObject(stream) as FacebookUserJSON;
            }
            return user;
        }

        /// <summary>
        /// Gets User's Facebook friends.
        /// </summary>
        /// <returns></returns>
        private List<FacebookPersonJSON> GetUserFriends()
        {
            FriendContainer friendListContainer = null;
            string friends = _facebookClient.Get(FacebookKeyStrings.MY_FRIENDS).ToString();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(FriendContainer));
            using (MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(friends)))
            {
                friendListContainer = serializer.ReadObject(stream) as FriendContainer;
            }
            return friendListContainer.Friends;
        }

        /// <summary>
        /// Gets User's Facebook FriendLists.
        /// </summary>
        /// <returns></returns>
        private List<FacebookFriendListJSON> GetUserFriendLists()
        {
            FriendListContainer friendListContainer = null;
            string friendLists = _facebookClient.Get(FacebookKeyStrings.MY_FRIEND_LISTS).ToString();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(FriendListContainer));
            using (MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(friendLists)))
            {
                friendListContainer = serializer.ReadObject(stream) as FriendListContainer;
            }
            FriendContainer friendsContainer = null;

            foreach (FacebookFriendListJSON list in friendListContainer.FriendList)
            {
                string friendListMembers = _facebookClient.Get(string.Format(FacebookKeyStrings.MY_FRIEND_LISTS_MEMBERS, list.Id)).ToString();
                serializer = new DataContractJsonSerializer(typeof(FriendContainer));
                using (MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(friendListMembers)))
                {
                    friendsContainer = serializer.ReadObject(stream) as FriendContainer;
                    list.Friends = friendsContainer.Friends;
                }
            }

            return friendListContainer.FriendList;
        }

        /// <summary>
        /// Gets User's Facebook Albums.
        /// </summary>
        /// <returns></returns>
        private List<FacebookAlbumJSON> GetUserAlbums()
        {
            AlbumContainer albumListContainer = null;
            string albums = _facebookClient.Get(FacebookKeyStrings.MY_ALBUMS).ToString();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(AlbumContainer));
            using (MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(albums)))
            {
                albumListContainer = serializer.ReadObject(stream) as AlbumContainer;
            }

            PhotoContainer photoListContainer = null;
            foreach (FacebookAlbumJSON album in albumListContainer.Albums)
            {
                string photos = _facebookClient.Get(string.Format(FacebookKeyStrings.MY_ALBUM_PHOTOS, album.Id)).ToString();
                serializer = new DataContractJsonSerializer(typeof(PhotoContainer));
                using (MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(photos)))
                {
                    photoListContainer = serializer.ReadObject(stream) as PhotoContainer;
                    album.Photos = photoListContainer.Photos;
                }
            }

            return albumListContainer.Albums;
        }

        /// <summary>
        /// Gets videos in which user is tagged.
        /// </summary>
        /// <returns></returns>
        private List<FacebookVideoJSON> GetUserTaggedVideos()
        {
            VideosContainer videosContainer = null;
            string videos = _facebookClient.Get(FacebookKeyStrings.MY_TAGGED_VIDEOS).ToString();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(VideosContainer));
            using (MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(videos)))
            {
                videosContainer = serializer.ReadObject(stream) as VideosContainer;
            }
            return videosContainer.Videos;
        }

        /// <summary>
        /// Get videos uploaded by user.
        /// </summary>
        /// <returns></returns>
        private List<FacebookVideoJSON> GetUserVideos()
        {
            VideosContainer videosContainer = null;
            string videos = _facebookClient.Get(FacebookKeyStrings.MY_VIDEOS).ToString();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(VideosContainer));
            using (MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(videos)))
            {
                videosContainer = serializer.ReadObject(stream) as VideosContainer;
            }
            return videosContainer.Videos;
        }

        /// <summary>
        /// Gets the photos in which user is tagged in Facebook.
        /// </summary>
        /// <returns></returns>
        private List<FacebookAlbumJSON> GetUserTaggedPhotos()
        {
            AlbumContainer albumListContainer = null;
            string photos = _facebookClient.Get(FacebookKeyStrings.MY_TAGGED_PHOTOS).ToString();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(AlbumContainer));
            using (MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(photos)))
            {
                albumListContainer = serializer.ReadObject(stream) as AlbumContainer;
            }
            return albumListContainer.Albums;
        }

        /// <summary>
        /// Gets the photos which user has uploaded on Facebook.
        /// </summary>
        /// <returns></returns>
        private List<FacebookAlbumJSON> GetUserPhotos()
        {
            AlbumContainer albumListContainer = null;
            string photos = _facebookClient.Get(FacebookKeyStrings.MY_PHOTOS).ToString();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(AlbumContainer));
            using (MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(photos)))
            {
                albumListContainer = serializer.ReadObject(stream) as AlbumContainer;
            }
            return albumListContainer.Albums;
        }

        /// <summary>
        /// Gets User's Facebook Likes.
        /// </summary>
        /// <returns></returns>
        private List<FacebookUserLikeJSON> GetUserLikes()
        {
            UsersLikesContainer usersLikeContainer = null;
            string likes = _facebookClient.Get(FacebookKeyStrings.MY_LIKES).ToString();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(UsersLikesContainer));
            using (MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(likes)))
            {
                usersLikeContainer = serializer.ReadObject(stream) as UsersLikesContainer;
            }
            return usersLikeContainer.Likes;
        }

        /// <summary>
        /// Gets User's Facebook Interests.
        /// </summary>
        /// <returns></returns>
        private List<FacebookUserLikeJSON> GetUserInterests()
        {
            UsersInterestsContainer usersInterestsContainer = null;
            string interests = _facebookClient.Get(FacebookKeyStrings.MY_INTERESTS).ToString();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(UsersInterestsContainer));
            using (MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(interests)))
            {
                usersInterestsContainer = serializer.ReadObject(stream) as UsersInterestsContainer;
            }
            return usersInterestsContainer.Interests;
        }

        /// <summary>
        /// Gets User's Facebook Movie List.
        /// </summary>
        /// <returns></returns>
        private List<FacebookMovieJSON> GetUserMovies()
        {
            MovieListContainer movieListContainer = null;
            string movies = _facebookClient.Get(FacebookKeyStrings.MY_MOVIES).ToString();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(MovieListContainer));
            using (MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(movies)))
            {
                movieListContainer = serializer.ReadObject(stream) as MovieListContainer;
            }
            return movieListContainer.Movies;
        }

        /// <summary>
        /// Gets User's Facebook Music List.
        /// </summary>
        /// <returns></returns>
        private List<FacebookMusicJSON> GetUserMusic()
        {
            MusicListContainer musicListContainer = null;
            string music = _facebookClient.Get(FacebookKeyStrings.MY_MUSIC).ToString();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(MusicListContainer));
            using (MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(music)))
            {
                musicListContainer = serializer.ReadObject(stream) as MusicListContainer;
            }
            return musicListContainer.MusicList;
        }

        /// <summary>
        /// Gets User's Facebook Book List.
        /// </summary>
        /// <returns></returns>
        private List<FacebookBookJSON> GetUserBooks()
        {
            BookListContainer bookListContainer = null;
            string books = _facebookClient.Get(FacebookKeyStrings.MY_BOOKS).ToString();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(BookListContainer));
            using (MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(books)))
            {
                bookListContainer = serializer.ReadObject(stream) as BookListContainer;
            }
            return bookListContainer.Books;
        }

        /// <summary>
        /// Gets User's Facebook Family Members.
        /// </summary>
        /// <returns></returns>
        private List<FacebookFamilyMemberJSON> GetUserFamilyMembers()
        {
            MemberListContainer familyMemberListContainer = null;
            string familyMemberLists = _facebookClient.Get(FacebookKeyStrings.MY_FAMILY).ToString();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(MemberListContainer));
            using (MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(familyMemberLists)))
            {
                familyMemberListContainer = serializer.ReadObject(stream) as MemberListContainer;
            }
            return familyMemberListContainer.Members;
        }

        /// <summary>
        /// Gets page list tht user administer.
        /// </summary>
        /// <returns></returns>
        private List<FacebookPageJSON> GetUserPages()
        {
            PagesContainer pagesContainer = null;
            string pages = _facebookClient.Get(FacebookKeyStrings.MY_PAGES).ToString();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(PagesContainer));
            using (MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(pages)))
            {
                pagesContainer = serializer.ReadObject(stream) as PagesContainer;
            }
            return pagesContainer.Pages;
        }

        /// <summary>
        /// Get user's activity list.
        /// </summary>
        /// <returns></returns>
        private List<FacebookActivityJSON> GetUserActivities()
        {
            ActivityListContainer activityListContainer = null;
            string activities = _facebookClient.Get(FacebookKeyStrings.MY_ACTIVITIES).ToString();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(ActivityListContainer));
            using (MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(activities)))
            {
                activityListContainer = serializer.ReadObject(stream) as ActivityListContainer;
            }
            return activityListContainer.Activities;
        }

        /// <summary>
        /// Get user groups from Facebook.
        /// </summary>
        /// <returns></returns>
        private List<FacebookGroupJSON> GetUserGroups()
        {
            GroupsContainer groupsContainer = null;
            string groups = _facebookClient.Get(FacebookKeyStrings.MY_GROUPS).ToString();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(GroupsContainer));
            using (MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(groups)))
            {
                groupsContainer = serializer.ReadObject(stream) as GroupsContainer;
            }
            return groupsContainer.Groups;
        }

        /// <summary>
        /// Get users locations where user have been.
        /// </summary>
        /// <returns></returns>
        private List<FacebookLocationJSON> GetUserLocations()
        {
            LocationsContainer locationsContainer = null;
            string locations = _facebookClient.Get(FacebookKeyStrings.MY_LOCATIONS).ToString();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(LocationsContainer));
            using (MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(locations)))
            {
                locationsContainer = serializer.ReadObject(stream) as LocationsContainer;
            }
            return locationsContainer.Locations;
        }

        /// <summary>
        /// Get user's notes from Facebook.
        /// </summary>
        /// <returns></returns>
        private List<FacebookNoteJSON> GetUserNotes()
        {
            NotesContainer notessContainer = null;
            string notes = _facebookClient.Get(FacebookKeyStrings.MY_NOTES).ToString();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(NotesContainer));
            using (MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(notes)))
            {
                notessContainer = serializer.ReadObject(stream) as NotesContainer;
            }
            return notessContainer.Notes;
        }

        /// <summary>
        /// Gets the list of people you follow on Facebook.
        /// </summary>
        /// <returns></returns>
        private List<FacebookPersonJSON> GetUserFollowings()
        {
            FriendContainer followingContainer = null;
            string following = _facebookClient.Get(FacebookKeyStrings.MY_FOLLOWING).ToString();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(FriendContainer));
            using (MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(following)))
            {
                followingContainer = serializer.ReadObject(stream) as FriendContainer;
            }
            return followingContainer.Friends;
        }

        /// <summary>
        /// Gets a list of person following user.
        /// </summary>
        /// <returns></returns>
        private List<FacebookPersonJSON> GetUserFollowers()
        {
            FriendContainer followerContainer = null;
            string followers = _facebookClient.Get(FacebookKeyStrings.MY_FOLLOWERS).ToString();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(FriendContainer));
            using (MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(followers)))
            {
                followerContainer = serializer.ReadObject(stream) as FriendContainer;
            }
            return followerContainer.Friends;
        }

        /// <summary>
        /// Gets users favourite TV shows.
        /// </summary>
        /// <returns></returns>
        private List<FacebookTelevisionShowJSON> GetUserTelivisionShows()
        {
            TelevisionShowsContainer tvShowContainer = null;
            string tvShows = _facebookClient.Get(FacebookKeyStrings.MY_TELEVISION_SHOWS).ToString();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(TelevisionShowsContainer));
            using (MemoryStream stream = new MemoryStream(Encoding.ASCII.GetBytes(tvShows)))
            {
                tvShowContainer = serializer.ReadObject(stream) as TelevisionShowsContainer;
            }
            return tvShowContainer.TelevisionShows;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Generates the login URL.
        /// </summary>
        /// <returns></returns>
        public Uri GenerateLoginUrl()
        {
            dynamic parameters = new ExpandoObject();

            parameters.client_id = _appId;
            //parameters.redirect_uri = "https://www.facebook.com/connect/login_success.html";
            parameters.redirect_uri = "http://localhost/smartpresenter";
            //parameters.display = "popup";
            parameters.response_type = "code";

            if (!string.IsNullOrWhiteSpace(FacebookKeyStrings.ExtendedPermissions))
            {
                parameters.scope = FacebookKeyStrings.ExtendedPermissions;
            }

            FacebookClient facebookClient = new FacebookClient();
            return facebookClient.GetLoginUrl(parameters);
        }

        /// <summary>
        /// Determines whether [is login successfull] [the specified URI].
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        public bool IsLoginSuccessfull(Uri uri)
        {
            FacebookClient client = new FacebookClient();
            FacebookOAuthResult oauthResult;
            if (!client.TryParseOAuthCallbackUrl(uri, out oauthResult))
            {
                return false;
            }
            AccessToken = GetAccessTokenFromCode(oauthResult);
            if (string.IsNullOrEmpty(_accessToken) == false)
            {
                _facebookClient = new FacebookClient();
                _facebookClient.AccessToken = _accessToken;

                Import();
            }
            return oauthResult.IsSuccess;
        }

        #endregion

        #endregion

        #region Events

        public delegate void UserProfileUpdated(FacebookUserJSON userProfile);

        public event UserProfileUpdated UserProfileUpdatedEvent;

        private void OnUserProfileUpdated()
        {
            if (UserProfileUpdatedEvent != null)
            {
                UserProfileUpdatedEvent(_facebookUser);
            }
        }

        #endregion
    }
}
