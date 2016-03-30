using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using SmartPresenter.BO.Social.Facebook;
using SmartPresenter.BO.Social.Facebook.JSON;
using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace SmartPresenter.UI.Controls.ViewModel
{
    public class FacebookSettingsViewModel : BindableBase
    {
        #region Private Members

        private bool _isLoginSuccessfull;
        private bool _isBrowserVisible;
        private WebBrowser _webBrowser;
        private string _name;
        private string _profilePicture;
        private string _aboutMe;
        private string _hometown;
        private string _currentCity;

        private string _designation;
        private string _organization;
        private string _degree;
        private string _collegeName;
        private string _dateOfBirth;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="FacebookSettingsViewModel"/> class.
        /// </summary>
        public FacebookSettingsViewModel()
        {
            Initialize();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether this instance is browser visible.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is browser visible; otherwise, <c>false</c>.
        /// </value>
        public bool IsBrowserVisible
        {
            get
            {
                return _isBrowserVisible;
            }
            set
            {
                _isBrowserVisible = value;
                OnPropertyChanged("IsBrowserVisible");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is logged in successfull.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is logged in successfull; otherwise, <c>false</c>.
        /// </value>
        public bool IsLoginSuccessfull
        {
            get
            {
                return _isLoginSuccessfull;
            }
            set
            {
                _isLoginSuccessfull = value;
                OnPropertyChanged("IsLoginSuccessfull");
                OnPropertyChanged("IsLoginGridVisible");
            }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is login grid visible.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is login grid visible; otherwise, <c>false</c>.
        /// </value>
        public bool IsLoginGridVisible
        {
            get
            {
                return !IsLoginSuccessfull;
            }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        /// <summary>
        /// Gets or sets the hometown.
        /// </summary>
        /// <value>
        /// The hometown.
        /// </value>
        public string Hometown
        {
            get
            {
                return _hometown;
            }
            set
            {
                _hometown = value;
                OnPropertyChanged("Hometown");
            }
        }

        /// <summary>
        /// Gets or sets the current city.
        /// </summary>
        /// <value>
        /// The current city.
        /// </value>
        public string CurrentCity
        {
            get
            {
                return _currentCity;
            }
            set
            {
                _currentCity = value;
                OnPropertyChanged("CurrentCity");
            }
        }

        /// <summary>
        /// Gets or sets the designation.
        /// </summary>
        /// <value>
        /// The designation.
        /// </value>
        public string Designation
        {
            get
            {
                return _designation;
            }
            set
            {
                _designation = value;
                OnPropertyChanged("Designation");
            }
        }

        /// <summary>
        /// Gets or sets the organization.
        /// </summary>
        /// <value>
        /// The organization.
        /// </value>
        public string Organization
        {
            get
            {
                return _organization;
            }
            set
            {
                _organization = value;
                OnPropertyChanged("Organization");
            }
        }

        /// <summary>
        /// Gets or sets the name of the college.
        /// </summary>
        /// <value>
        /// The name of the college.
        /// </value>
        public string CollegeName
        {
            get
            {
                return _collegeName;
            }
            set
            {
                _collegeName = value;
                OnPropertyChanged("CollegeName");
            }
        }

        /// <summary>
        /// Gets or sets the degree.
        /// </summary>
        /// <value>
        /// The degree.
        /// </value>
        public string Degree
        {
            get
            {
                return _degree;
            }
            set
            {
                _degree = value;
                OnPropertyChanged("Degree");
            }
        }

        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
        /// <value>
        /// The date of birth.
        /// </value>
        public string DateOfBirth
        {
            get
            {
                return _dateOfBirth;
            }
            set
            {
                _dateOfBirth = value;
                OnPropertyChanged("DateOfBirth");
            }
        }

        /// <summary>
        /// Gets or sets the profile picture.
        /// </summary>
        /// <value>
        /// The profile picture.
        /// </value>
        public string ProfilePicture
        {
            get
            {
                return _profilePicture;
            }
            set
            {
                _profilePicture = value;
                OnPropertyChanged("ProfilePicture");
            }
        }

        /// <summary>
        /// Gets or sets the about me.
        /// </summary>
        /// <value>
        /// The about me.
        /// </value>
        public string AboutMe
        {
            get
            {
                return _aboutMe;
            }
            set
            {
                _aboutMe = value;
                OnPropertyChanged("AboutMe");
            }
        }

        /// <summary>
        /// Gets or sets the WebBrowser.
        /// </summary>
        /// <value>
        /// The WebBrowser.
        /// </value>
        public WebBrowser WebBrowser
        {
            get
            {
                return _webBrowser;
            }
            set
            {
                if (_webBrowser != null)
                {
                    _webBrowser.Navigated -= WebBrowser_Navigated;
                }
                _webBrowser = value;
                if (_webBrowser != null)
                {
                    _webBrowser.Navigated += WebBrowser_Navigated;
                }
            }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Gets the login logout command.
        /// </summary>
        /// <value>
        /// The login logout command.
        /// </value>
        public DelegateCommand LoginLogoutCommand { get; private set; }

        #region Command Handlers

        /// <summary>
        /// Logins the logout command_ executed.
        /// </summary>
        /// <param name="isLogin">if set to <c>true</c> [is login].</param>
        private void LoginLogoutCommand_Executed()
        {
            Uri uri = FacebookManager.Instance.GenerateLoginUrl();
            IsBrowserVisible = true;
            WebBrowser.Navigate(uri);
        }

        /// <summary>
        /// Handles the Navigated event of the WebBrowser control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="NavigationEventArgs"/> instance containing the event data.</param>
        private void WebBrowser_Navigated(object sender, NavigationEventArgs e)
        {
            if (FacebookManager.Instance.IsLoginSuccessfull(e.Uri) == true)
            {
                IsBrowserVisible = false;
                IsLoginSuccessfull = true;
            }
        }

        #endregion

        #endregion

        #region Methods

        #region Private Methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            CreateCommands();
            IsBrowserVisible = false;
            FacebookManager.Instance.UserProfileUpdatedEvent += UserProfileUpdatedEvent;
        }

        /// <summary>
        /// Creates the commands.
        /// </summary>
        private void CreateCommands()
        {
            LoginLogoutCommand = new DelegateCommand(LoginLogoutCommand_Executed);
        }

        void UserProfileUpdatedEvent(FacebookUserJSON userProfile)
        {
            Name = userProfile.Name;
            AboutMe = userProfile.AboutMe;
            CurrentCity = userProfile.CurrentCity.Name;
            Hometown = userProfile.HomeTown.Name;
            if (userProfile.Education != null && userProfile.Education.Count > 0)
            {
                CollegeName = userProfile.Education[0].Name.Name;
                //Degree = userProfile.Education[0].Degree.Name;
            }
            if (userProfile.WorkList != null && userProfile.WorkList.Count > 0)
            {
                Designation = userProfile.WorkList[0].Position.Name;
                Organization = userProfile.WorkList[0].Employer.Name;
            }
            DateOfBirth = userProfile.DateOfBirth.ToString("dd MMM, yyyy");
            FacebookAlbumJSON facebookAlbum = userProfile.Albums.FirstOrDefault(album => album.Name.Equals("Profile Pictures"));
            ProfilePicture = facebookAlbum.Photos.FirstOrDefault().Source;
        }

        #endregion

        #region Public Methods



        #endregion

        #endregion
    }
}
