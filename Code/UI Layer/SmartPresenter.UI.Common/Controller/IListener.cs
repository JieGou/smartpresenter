using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPresenter.UI.Common.Controller
{
    /// <summary>
    /// Listener interface to be implemented by every viewmodel who wants to be notified of events.
    /// </summary>
    public interface IListener
    {
        void Listen(Type type, string propertyName, Object value);
    }
}
