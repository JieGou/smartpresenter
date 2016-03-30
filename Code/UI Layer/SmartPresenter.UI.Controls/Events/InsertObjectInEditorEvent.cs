using Microsoft.Practices.Prism.PubSubEvents;
using SmartPresenter.BO.Common.Enums;
using SmartPresenter.Common.Enums;

namespace SmartPresenter.UI.Controls.Events
{
    /// <summary>
    /// A class for InsertObjectInEditorEvent.
    /// </summary>
    public sealed class InsertObjectInEditorEvent : PubSubEvent<ElementType>
    {
    }
}
