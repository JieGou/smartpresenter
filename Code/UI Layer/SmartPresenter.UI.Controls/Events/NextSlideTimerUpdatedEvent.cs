using Microsoft.Practices.Prism.PubSubEvents;

namespace SmartPresenter.UI.Controls.Events
{
    /// <summary>
    /// Class for timer information update.
    /// </summary>
    public class NextSlideTimerUpdatedEvent : PubSubEvent<SlideTimer>
    {
    }

    /// <summary>
    /// Slide Timer data.
    /// </summary>
    public class SlideTimer
    {
        #region Properties

        public int NextSlideDelay { get; set; }
        public bool LoopToFirst { get; set; }

        #endregion
    }
}
