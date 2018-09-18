using System;
using System.Collections.Generic;
using System.Text;

namespace AvaExt.Settings
{
    public interface ISettingsStore : IDisposable
    {
        ISettings getByName(string name);
        // ISettings getByName(string name,bool create);

        //ISettings setSettings(string name, ISettings settings);
        void setFlagSourceUpdate(bool pFlag);
        bool getFlagSourceUpdate();
        void save();
    }
}
