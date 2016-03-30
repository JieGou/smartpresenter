using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPresenter.Common.Interfaces
{
    public interface IPersistent : ISave, ILoad
    {
    }

    public interface IPersistent<T> : ISave, ILoad<T>
    {
    }
}
