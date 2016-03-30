using Microsoft.Practices.Prism.PubSubEvents;
using SmartPresenter.UI.Controls.ViewModel;

namespace SmartPresenter.UI.Controls.Events
{
    /// <summary>
    /// A class for SelectedSlideAskedEvent.
    /// </summary>
    public sealed class SelectedSlideAskedEvent : PubSubEvent<SlideView>
    {
    }
}
