using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using SmartPresenter.BO.Common;
using SmartPresenter.BO.Common.Entities;
using SmartPresenter.BO.Common.Interfaces;
using SmartPresenter.Common;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SmartPresenter.UI.Controls.ViewModel
{
    /// <summary>
    /// ViewModel for Library Tab.
    /// </summary>
    [Export(typeof(LibraryTabViewModel))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class LibraryTabViewModel : BindableBase
    {

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryTabViewModel"/> class.
        /// </summary>
        public LibraryTabViewModel()
        {
            CreateCommands();
            Initialize();
            LoadData();
        }

        /// <summary>
        /// Creates a new LibraryTabViewModel.
        /// </summary>
        [ImportingConstructor]
        public LibraryTabViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            CreateCommands();
            Initialize();
            LoadData();
        }

        #endregion

        #region Private Member Variables

        /// <summary>
        /// List of libraries.
        /// </summary>
        private ObservableCollection<LibraryView> _libraries = new ObservableCollection<LibraryView>();

        /// <summary>
        /// The Selected library on UI.
        /// </summary>
        private LibraryView _selectedLibrary;

        /// <summary>
        /// The event aggregator
        /// </summary>
        private readonly IEventAggregator _eventAggregator;


        #endregion

        #region Public Properties

        /// <summary>
        /// List of user presentation libraries
        /// </summary>
        public ObservableCollection<LibraryView> Libraries
        {
            get
            {
                return _libraries;
            }
        }

        /// <summary>
        /// Gets or sets the selected library.
        /// </summary>
        /// <value>
        /// The selected library.
        /// </value>
        public LibraryView SelectedLibrary
        {
            get
            {
                return _selectedLibrary;
            }
            set
            {
                _selectedLibrary = value;
                OnPropertyChanged("SelectedLibrary");
            }
        }

        #endregion

        #region Commands

        public DelegateCommand<Object> DefaultAddLibraryCommand { get; set; }
        public DelegateCommand<Object> AddLibraryCommand { get; set; }
        public DelegateCommand<Object> DefaultAddPlaylistCommand { get; set; }
        public DelegateCommand<Object> AddPlaylistCommand { get; set; }
        public DelegateCommand<Object> AddPlaylistGroupCommand { get; set; }
        public DelegateCommand<Object> AddPresentationToPlaylistCommand { get; set; }
        public DelegateCommand<Object> AddPresentationToLibraryCommand { get; set; }
        public DelegateCommand<Object> RemovePresentationCommand { get; set; }
        public DelegateCommand<Object> RemoveLibraryCommand { get; set; }
        public DelegateCommand<Object> RemovePlaylistCommand { get; set; }
        public DelegateCommand<Object> ChangeLibraryLocationCommand { get; set; }

        public DelegateCommand<Object> SelectCellContentCommand { get; set; }

        #region Command Handlers

        /// <summary>
        /// Defaults the add library command_ executed.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void DefaultAddLibraryCommand_Executed(Object parameter)
        {
            if (parameter == null)
            {
                AddLibraryCommand.Execute(parameter);
            }
            else
            {
                AddPresentationToLibraryCommand.Execute(parameter);
            }
        }

        /// <summary>
        /// Adds the library command_ executed.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void AddLibraryCommand_Executed(Object parameter)
        {
            System.Windows.Forms.FolderBrowserDialog folderDialog = new System.Windows.Forms.FolderBrowserDialog();
            folderDialog.ShowDialog();
            AddLibrary(folderDialog.SelectedPath);
        }

        /// <summary>
        /// Adds the library.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <exception cref="System.ArgumentException">Argument \path\ is empty or invalid</exception>
        private void AddLibrary(string path)
        {
            if (string.IsNullOrEmpty(path) == false && Directory.Exists(path) == true)
            {
                PresentationLibrary newLibrary = new PresentationLibrary(path);
                newLibrary.Name = System.IO.Path.GetFileName(path);
                Libraries.Add(new LibraryView(newLibrary));
            }
            else
            {
                throw new ArgumentException("Argument \"path\" is empty or invalid");
            }
        }

        /// <summary>
        /// Defaults the add playlist command_ executed.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void DefaultAddPlaylistCommand_Executed(Object parameter)
        {
            if (parameter == null)
            {
                AddPlaylistCommand.Execute(parameter);
            }
            else if (parameter is PlaylistView)
            {
                AddPresentationToPlaylistCommand.Execute(parameter);
            }
            else if (parameter is PlaylistGroupView)
            {
                AddPlaylistCommand.Execute(parameter);
            }
            else if (parameter is PresentationView)
            {
                AddPresentationToPlaylistCommand.Execute(parameter);
            }
        }

        /// <summary>
        /// Adds the playlist command_ executed.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void AddPlaylistCommand_Executed(Object parameter)
        {
            IPlaylistFactory<IPresentation> playlistFactory = new PresentationPlaylistFactory();
            // Create new playlist with default name.
            IPlaylist<IPresentation> newPlaylist = playlistFactory.CreatePlaylist();
            PlaylistView newPlaylistView = new PlaylistView(newPlaylist);

            PlaylistGroupView playlistGroupView = parameter as PlaylistGroupView;
            PlaylistView playlistView = parameter as PlaylistView;

            // Add this playlist to proper location.

            if (playlistGroupView != null)
            {
                AddToPlaylistGroup(newPlaylistView, playlistGroupView);
            }
            else if (playlistView == null)
            {
                // If no item was selected while adding a new Playlist, just put it at last.
                SelectedLibrary.Playlists.Add(newPlaylistView);
            }
            else if (playlistView != null)
            {
                AddAfterPlaylist(newPlaylistView, playlistView);
            }
        }

        /// <summary>
        /// Adds the playlist after specified playlist.
        /// </summary>
        /// <param name="newPlaylistView">The new playlist view.</param>
        /// <param name="playlistView">The playlist view.</param>
        private void AddAfterPlaylist(PlaylistView newPlaylistView, PlaylistView playlistView)
        {
            PlaylistGroupView parentPlaylistView = playlistView.Parent as PlaylistGroupView;
            // If a Playlist was selected then put it inside the Group.
            if (parentPlaylistView != null)
            {
                // Find out where to insert newly created playlist.
                int index = parentPlaylistView.Playlists.IndexOf(playlistView) + 1;
                // Set the parent of this playlist.
                newPlaylistView.Parent = parentPlaylistView;
                // Insert it at correct position.
                parentPlaylistView.Playlists.Insert(index, newPlaylistView);
            }
            else if (parentPlaylistView == null)
            {
                // If a playlist from root level was selected, then insert just after it.
                int index = SelectedLibrary.Playlists.IndexOf(playlistView);
                SelectedLibrary.Playlists.Insert(index + 1, newPlaylistView);
            }
        }

        /// <summary>
        /// Adds to playlist group.
        /// </summary>
        /// <param name="newPlaylistView">The new playlist view.</param>
        /// <param name="playlistGroupView">The playlist group view.</param>
        private static void AddToPlaylistGroup(PlaylistView newPlaylistView, PlaylistGroupView playlistGroupView)
        {
            newPlaylistView.Parent = playlistGroupView;
            // If a Playlist Group was selected then put it inside the Group.
            playlistGroupView.Playlists.Add(newPlaylistView);
        }

        /// <summary>
        /// Adds the playlist group command_ executed.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void AddPlaylistGroupCommand_Executed(Object parameter)
        {
            IPlaylistFactory<IPresentation> playlistFactory = new PresentationPlaylistFactory();

            IPlaylist<IPlaylist<IPresentation>> newPlaylistGroup = playlistFactory.CreatePlaylistGroup();
            PlaylistGroupView newPlaylistGroupView = new PlaylistGroupView(newPlaylistGroup);
            newPlaylistGroupView.Name = Constants.New_Playlist_Group_Name;

            PlaylistView playlist = parameter as PlaylistView;

            if (playlist != null)
            {
                PlaylistGroupView parentPlaylist = null;
                //if (playlist.Type == PlaylistType.Playlist)
                //{
                parentPlaylist = playlist.Parent as PlaylistGroupView;
                if (parentPlaylist != null)
                {
                    newPlaylistGroupView.Parent = parentPlaylist;
                    int index = parentPlaylist.Playlists.IndexOf(playlist);
                    parentPlaylist.Playlists.Insert(index + 1, newPlaylistGroupView);
                }
                else
                {
                    SelectedLibrary.Playlists.Insert(SelectedLibrary.Playlists.IndexOf(playlist) + 1, newPlaylistGroupView);
                }
                //}
                //else if (playlist.Type == PlaylistType.PlaylistGroup)
                //{
                //    newPlaylistGroupView.Parent = playlist;
                //    PlaylistGroupView playlistGroupView = (playlist as PlaylistGroupView);
                //    int index = playlistGroupView.Playlists.IndexOf(playlist);
                //    playlistGroupView.Playlists.Insert(index + 1, newPlaylistGroupView);
                //}
            }
            else
            {
                SelectedLibrary.Playlists.Add(newPlaylistGroupView);
            }
        }

        /// <summary>
        /// Adds the presentation to library command_ can executed.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        private bool AddPresentationToLibraryCommand_CanExecuted(Object parameter)
        {
            if (SelectedLibrary == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Adds the presentation to library command_ executed.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void AddPresentationToLibraryCommand_Executed(Object parameter)
        {
            if (parameter != null)
            {
                LibraryView selectedLibrary = (LibraryView)parameter;

                IPresentationFactory presentationFactory = new PresentationFactory();

                Presentation newPresentation = (Presentation)presentationFactory.CreatePresentation();
                int count = selectedLibrary.Presentations.Where(item => item.Name.StartsWith(Constants.Default_Presentation_Name)).Count();
                if (count > 0)
                {
                    newPresentation.Name = string.Concat(newPresentation.Name, " ", count);
                }
                newPresentation.ParentLibraryLocation = selectedLibrary.Location;
                newPresentation.Save();
                PresentationView newPresentationView = new PresentationView(newPresentation);

                selectedLibrary.Presentations.Add(newPresentationView);
            }
        }

        /// <summary>
        /// Adds the presentation to playlist command_ executed.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void AddPresentationToPlaylistCommand_Executed(Object parameter)
        {
            PlaylistView playlist = parameter as PlaylistView;
            PresentationView documentView = parameter as PresentationView;

            IPresentationFactory presentationFactory = new PresentationFactory();
            Presentation newPresentation = (Presentation)presentationFactory.CreatePresentation();
            PresentationView newPresentationView = new PresentationView(newPresentation);

            if (playlist != null)
            {
                if (playlist.Type == PlaylistType.Playlist)
                {
                    newPresentationView.Parent = playlist;
                    playlist.Presentations.Add(newPresentationView);
                }
            }
            else if (documentView != null)
            {
                PlaylistView parentPlaylist = (PlaylistView)(documentView.Parent);

                if (parentPlaylist != null)
                {
                    int index = parentPlaylist.Presentations.IndexOf(documentView);
                    newPresentationView.Parent = parentPlaylist;
                    parentPlaylist.Presentations.Insert(index + 1, newPresentationView);
                }
            }
        }

        /// <summary>
        /// Removes the library command_ can executed.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        private bool RemoveLibraryCommand_CanExecuted(Object parameter)
        {
            if (SelectedLibrary == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Removes the library command_ executed.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void RemoveLibraryCommand_Executed(Object parameter)
        {
            LibraryView libraryView = parameter as LibraryView;
            if (libraryView != null)
            {
                libraryView.Delete();
                Libraries.Remove(libraryView);
            }
        }

        /// <summary>
        /// Changes the library location command_ executed.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void ChangeLibraryLocationCommand_Executed(Object parameter)
        {
            LibraryView oldLibraryView = parameter as LibraryView;
            if (parameter != null)
            {
                // Select new location.
                System.Windows.Forms.FolderBrowserDialog folderDialog = new System.Windows.Forms.FolderBrowserDialog();
                folderDialog.ShowDialog();
                if (string.IsNullOrEmpty(folderDialog.SelectedPath) == false && Directory.Exists(folderDialog.SelectedPath) == true)
                {
                    PresentationLibrary newLibrary = new PresentationLibrary(folderDialog.SelectedPath);
                    newLibrary.Name = oldLibraryView.Name;

                    if (string.IsNullOrEmpty(oldLibraryView.Location) == false && Directory.Exists(oldLibraryView.Location))
                    {
                        String[] documentsInOldLibrary = Directory.GetFiles(oldLibraryView.Location, KnownFileTypes.Instance.DocumentExtension);
                        foreach (string document in documentsInOldLibrary)
                        {
                            string newDocumentLocation = System.IO.Path.Combine(newLibrary.Location, System.IO.Path.GetFileName(document));
                            try
                            {
                                File.Move(document, newDocumentLocation);
                            }
                            catch (IOException)
                            {

                            }
                            var PresentationView = oldLibraryView.Presentations.FirstOrDefault(doc => string.Compare(doc.Name, document, StringComparison.Ordinal) == 0);
                            if (PresentationView != null)
                            {
                                PresentationView.ParentLibraryLocation = newLibrary.Location;
                                //newLibrary.Presentations.Add(Presentation);
                            }
                        }

                        Libraries.Remove(oldLibraryView);
                        Libraries.Add(new LibraryView(newLibrary));
                    }
                }
            }
        }

        /// <summary>
        /// Removes the playlist command_ executed.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void RemovePlaylistCommand_Executed(Object parameter)
        {
            PlaylistView playlist = parameter as PlaylistView;
            PresentationView documentView = parameter as PresentationView;
            if (playlist != null)
            {
                PlaylistGroupView parentPlaylist = playlist.Parent as PlaylistGroupView;
                // If playlist has a parent the remove it.
                if (parentPlaylist != null)
                {
                    if (parentPlaylist.Type == PlaylistType.PlaylistGroup)
                    {
                        (parentPlaylist).Playlists.Remove(playlist);
                    }
                }
                else
                {
                    // Otherwise remove it from the root.
                    SelectedLibrary.Playlists.Remove(playlist);
                }
            }
            else if (documentView != null)
            {
                PlaylistView parentPlaylist = documentView.Parent as PlaylistView;
                if (parentPlaylist != null)
                {
                    parentPlaylist.Presentations.Remove(documentView);
                }
            }
        }

        /// <summary>
        /// Selects the cell content command_ executed.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        private void SelectCellContentCommand_Executed(Object parameter)
        {
            TextBox cellTextBox = parameter as TextBox;
            if (cellTextBox != null)
            {
                cellTextBox.Focus();
                cellTextBox.SelectAll();
            }
        }

        #endregion

        #endregion

        #region Private Methods

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            // In case of Unit Testing it will be null.
            if (Application.Current != null)
            {
                Application.Current.Dispatcher.ShutdownStarted += Dispatcher_ShutdownStarted;
            }
        }

        private void Libraries_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (LibraryView item in e.NewItems)
                {
                    ApplicationSettings.Instance.ActiveUserAccount.ActiveProfile.GeneralSettings.PresentationLibraries.Add(item.Library);
                    SelectedLibrary = item;
                }
            }
        }

        /// <summary>
        /// Handles the ShutdownStarted event of the Dispatcher control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Dispatcher_ShutdownStarted(object sender, EventArgs e)
        {
            UpdateBusinessObject();
        }

        /// <summary>
        /// Creates the commands.
        /// </summary>
        private void CreateCommands()
        {
            DefaultAddLibraryCommand = new DelegateCommand<Object>(DefaultAddLibraryCommand_Executed);
            AddLibraryCommand = new DelegateCommand<Object>(AddLibraryCommand_Executed);

            DefaultAddPlaylistCommand = new DelegateCommand<Object>(DefaultAddPlaylistCommand_Executed);
            AddPlaylistCommand = new DelegateCommand<Object>(AddPlaylistCommand_Executed);

            AddPlaylistGroupCommand = new DelegateCommand<Object>(AddPlaylistGroupCommand_Executed);
            AddPresentationToLibraryCommand = new DelegateCommand<Object>(AddPresentationToLibraryCommand_Executed, AddPresentationToLibraryCommand_CanExecuted);
            AddPresentationToPlaylistCommand = new DelegateCommand<Object>(AddPresentationToPlaylistCommand_Executed);
            RemoveLibraryCommand = new DelegateCommand<Object>(RemoveLibraryCommand_Executed, RemoveLibraryCommand_CanExecuted);
            RemovePlaylistCommand = new DelegateCommand<Object>(RemovePlaylistCommand_Executed);
            ChangeLibraryLocationCommand = new DelegateCommand<Object>(ChangeLibraryLocationCommand_Executed);

            SelectCellContentCommand = new DelegateCommand<Object>(SelectCellContentCommand_Executed);
        }

        /// <summary>
        /// Loads the data.
        /// </summary>
        private void LoadData()
        {
            GeneralSettings generalSettings = ApplicationSettings.Instance.ActiveUserAccount.ActiveProfile.GeneralSettings;
            foreach (PresentationLibrary library in generalSettings.PresentationLibraries)
            {
                Libraries.Add(new LibraryView(library));
                if (library.Id.Equals(generalSettings.SelectedPresentationLibrary.Id))
                {
                    SelectedLibrary = Libraries[Libraries.Count - 1];
                }
            }

            Libraries.CollectionChanged += Libraries_CollectionChanged;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Updates the business object.
        /// </summary>
        public void UpdateBusinessObject()
        {
            if (SelectedLibrary != null && ApplicationSettings.Instance.ActiveUserAccount.ActiveProfile.GeneralSettings.SelectedPresentationLibrary != SelectedLibrary.Library)
            {
                ApplicationSettings.Instance.ActiveUserAccount.ActiveProfile.GeneralSettings.SelectedPresentationLibrary = SelectedLibrary.Library;
            }
            foreach (LibraryView libraryView in Libraries)
            {
                libraryView.UpdateBusinessObject();
                foreach (PresentationView presentation in libraryView.Presentations)
                {
                    presentation.UpdateBusinessObject();
                }
            }

            SaveSettings();
        }

        /// <summary>
        /// Saves the settings.
        /// </summary>
        private static void SaveSettings()
        {
            ApplicationSettings.Instance.ActiveUserAccount.ActiveProfile.Save();
        }

        #endregion
    }
}
