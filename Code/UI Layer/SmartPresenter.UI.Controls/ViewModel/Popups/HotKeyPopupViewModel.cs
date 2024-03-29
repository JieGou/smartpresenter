﻿using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;
using SmartPresenter.UI.Controls.Events;
using System;

namespace SmartPresenter.UI.Controls.ViewModel
{
    /// <summary>
    /// Class for ViewModel of HotKeyPopup
    /// </summary>
    public class HotKeyPopupViewModel : BindableBase
    {

        #region Private Data Members

        private char? _hotKey;

        #endregion

        #region Contsructor

        /// <summary>
        /// Initializes a new instance of the <see cref="HotKeyPopupViewModel"/> class.
        /// </summary>
        public HotKeyPopupViewModel()
        {
            Initialize();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the hot key.
        /// </summary>
        /// <value>
        /// The hot key.
        /// </value>
        public char? HotKey
        {
            get
            {
                return _hotKey;
            }
            set
            {
                _hotKey = value;
                OnPropertyChanged("HotKey");
            }
        }

        /// <summary>
        /// Gets or sets the finish interaction.
        /// </summary>
        /// <value>
        /// The finish interaction.
        /// </value>
        public Action FinishInteraction { get; set; }

        /// <summary>
        /// Gets the event aggregator.
        /// </summary>
        /// <value>
        /// The event aggregator.
        /// </value>
        private IEventAggregator EventAggregator
        {
            get
            {
                return ServiceLocator.Current.GetInstance<IEventAggregator>();
            }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Gets the ok command.
        /// </summary>
        /// <value>
        /// The ok command.
        /// </value>
        public DelegateCommand OkCommand { get; private set; }

        /// <summary>
        /// Gets the clear command.
        /// </summary>
        /// <value>
        /// The clear command.
        /// </value>
        public DelegateCommand ClearCommand { get; private set; }

        #region Command Handlers

        /// <summary>
        /// Oks the command_ executed.
        /// </summary>
        private void OkCommand_Executed()
        {
            EventAggregator.GetEvent<HotKeyUpdatedEvent>().Publish(HotKey);
            this.FinishInteraction();
        }

        /// <summary>
        /// Clears the command_ executed.
        /// </summary>
        private void ClearCommand_Executed()
        {
            HotKey = null;
            EventAggregator.GetEvent<HotKeyUpdatedEvent>().Publish(HotKey);
            this.FinishInteraction();
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
        }

        /// <summary>
        /// Creates the commands.
        /// </summary>
        private void CreateCommands()
        {
            OkCommand = new DelegateCommand(OkCommand_Executed);
            ClearCommand = new DelegateCommand(ClearCommand_Executed);
        }

        #endregion

        #region Public Methods



        #endregion

        #endregion

        #region Events



        #endregion

    }
}
