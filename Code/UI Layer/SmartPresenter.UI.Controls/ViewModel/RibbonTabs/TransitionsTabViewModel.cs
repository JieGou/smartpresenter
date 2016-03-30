using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.ServiceLocation;
using SmartPresenter.BO.Common.Enums;
using SmartPresenter.BO.Common.Transitions;
using SmartPresenter.UI.Controls.Events;
using System;
using System.Collections.ObjectModel;

namespace SmartPresenter.UI.Controls.ViewModel
{
    public class TransitionsTabViewModel : BindableBase
    {

        #region Private Data Members

        private double _transitionDuration;
        private Transition _selectedTransition;

        #endregion

        #region Contsructor

        public TransitionsTabViewModel()
        {
            Initialize();
        }

        #endregion

        #region Properties

        public ObservableCollection<Transition> Transitions { get; private set; }

        public double TransitionDuration
        {
            get
            {
                return _selectedTransition.Duration;
            }
            set
            {
                _selectedTransition.Duration = value;
                EventAggregator.GetEvent<TransitionChangedEvent>().Publish(SelectedTransition);
                OnPropertyChanged("TransitionDuration");
                OnPropertyChanged("SelectedTransition.Duration");
            }
        }

        public Transition SelectedTransition
        {
            get
            {
                return _selectedTransition;
            }
            set
            {
                _selectedTransition = value;
                EventAggregator.GetEvent<TransitionChangedEvent>().Publish(value);
                OnPropertyChanged("SelectedTransition");
            }
        }

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

        public DelegateCommand<Transition> TransitionSelectedCommand { get; private set; }

        #region Command Handlers

        private void TransitionSelectedCommand_Executed(Transition transition)
        {
            SelectedTransition = transition;
        }

        #endregion

        #endregion

        #region Methods

        #region Private Methods

        private void Initialize()
        {
            Transitions = new ObservableCollection<Transition>();
            foreach (TransitionTypes transition in Enum.GetValues(typeof(TransitionTypes)))
            {
                Transitions.Add(TransitionFactory.CreateTransition(transition));
            }

            SelectedTransition = TransitionFactory.CreateTransition(TransitionTypes.None);
            TransitionDuration = 5;

            TransitionSelectedCommand = new DelegateCommand<Transition>(TransitionSelectedCommand_Executed);
        }

        #endregion

        #region Public Methods



        #endregion

        #endregion

        #region Events



        #endregion

    }
}
