using SmartPresenter.BO.Common.Enums;
using System.Collections.ObjectModel;
using System.ComponentModel;
namespace SmartPresenter.BO.Common.Transitions
{
    public abstract class Transition : INotifyPropertyChanged
    {

        #region Private Data Members

        private TransitionOptions _transitionOption;
        private double _duration;
        #endregion

        #region Contsructor

        public Transition()
        {
            AvailableOptions = new ObservableCollection<TransitionOptions>();
        }

        #endregion

        #region Properties

        public abstract TransitionTypes Type { get; }

        public double Duration
        {
            get
            {
                return _duration;
            }
            set
            {
                _duration = value;
                OnPropertyChanged("Duration");
            }
        }

        public TransitionOptions TransitionOption
        {
            get
            {
                return _transitionOption;
            }
            set
            {
                _transitionOption = value;
                OnPropertyChanged("TransitionOption");
            }
        }

        public ObservableCollection<TransitionOptions> AvailableOptions { get; set; }

        #endregion

        #region Commands

        #region Command Handlers



        #endregion

        #endregion

        #region Methods

        #region Private Methods



        #endregion

        #region Public Methods



        #endregion

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }
}
