using Microsoft.Practices.Prism.PubSubEvents;
using System.Windows.Media;

namespace SmartPresenter.UI.Controls.Events
{
    public class SelectedTextColorChangedEvent : PubSubEvent<Brush>
    {
    }
}
