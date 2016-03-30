using SmartPresenter.BO.Common.Interfaces;
using SmartPresenter.Common.Interfaces;
using SmartPresenter.Data.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPresenter.BO.Common
{
    public abstract class SettingsBase<T> : IPersistent<T>
    {
        public void Save() { }

        public abstract void Save(string path);

        public T Load() { return default(T); }

        public abstract T Load(string path);
    }
}
