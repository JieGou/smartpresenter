
namespace SmartPresenter.BO.Social.Facebook
{
    internal static class FacebookKeyStrings
    {
        #region Constants

        public const string ACCESS_TOKEN_URL = "https://graph.facebook.com/oauth/access_token?client_id={0}&client_secret={1}&redirect_uri={2}&code={3}";

        public const string ME = "me";
        public const string MY_FRIENDS = "me/friends";
        public const string MY_FRIEND_LISTS = "me/friendlists";
        public const string MY_FRIEND_LISTS_MEMBERS = "{0}/members";
        public const string MY_ALBUMS = "me/albums";
        public const string MY_ALBUM_PHOTOS = "{0}/photos";
        public const string MY_TAGGED_VIDEOS = "me/videos";
        public const string MY_VIDEOS = "me/videos/uploaded";
        public const string MY_TAGGED_PHOTOS = "me/photos";
        public const string MY_PHOTOS = "me/photos/uploaded";
        public const string MY_LIKES = "me/likes";
        public const string MY_INTERESTS = "me/interests";
        public const string MY_MOVIES = "me/movies";
        public const string MY_MUSIC = "me/music";
        public const string MY_BOOKS = "me/books";
        public const string MY_FAMILY = "me/family";
        public const string MY_PAGES = "me/accounts";
        public const string MY_ACTIVITIES = "me/activities";
        public const string MY_CHECKINS = "me/checkins";
        public const string MY_GROUPS = "me/groups";
        public const string MY_LOCATIONS = "me/locations";
        public const string MY_NOTES = "me/notes";
        public const string MY_QUESTION = "me/questions";
        public const string MY_FOLLOWING = "me/subscribedto";
        public const string MY_FOLLOWERS = "me/subscribers";
        public const string MY_TELEVISION_SHOWS = "me/television";

        public const string ExtendedPermissions =
            "public_profile,"
            + "email,"
            + "user_about_me,"
            + "user_activities,"
            + "user_birthday,"
            + "user_checkins,"
            + "user_education_history,"
            + "user_events,"
            + "user_groups,"
            + "user_hometown,"
            + "user_interests,"
            + "user_likes,"
            + "user_location,"
            + "user_notes,"
            + "user_photos,"
            + "friends_photos,"
            + "user_questions,"
            + "friends_questions,"
            + "user_relationships,"
            + "user_relationship_details,"
            + "user_religion_politics,"
            + "user_status,"
            + "friends_status,"
            + "user_subscriptions,"
            + "user_videos,"
            + "user_website,"
            + "user_work_history,"
            + "read_friendlists,"
            + "read_insights,"
            + "read_mailbox,"
            + "read_requests,"
            + "read_stream,"
            + "xmpp_login,"
            + "user_online_presence,"
            + "friends_online_presence,"
            + "manage_pages";

        #endregion
    }
}
