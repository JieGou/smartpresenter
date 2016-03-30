using Microsoft.Practices.Prism.PubSubEvents;
using SmartPresenter.UI.Controls.ViewModel;

namespace SmartPresenter.UI.Controls.Events
{
    /// <summary>
    /// A class for SelectedSlideChangedEvent.
    /// </summary>
    public sealed class SelectedSlideChangedEvent : PubSubEvent<SlideView>
    {
    }
}
