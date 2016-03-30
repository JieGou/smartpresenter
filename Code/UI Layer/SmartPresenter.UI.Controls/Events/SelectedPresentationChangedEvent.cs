using Microsoft.Practices.Prism.PubSubEvents;
using SmartPresenter.UI.Controls.ViewModel;

namespace SmartPresenter.UI.Controls.Events
{
    /// <summary>
    /// A class for SelectedPresentationChangedEvent.
    /// </summary>
    public sealed class SelectedPresentationChangedEvent : PubSubEvent<PresentationView>
    {
    }
}
