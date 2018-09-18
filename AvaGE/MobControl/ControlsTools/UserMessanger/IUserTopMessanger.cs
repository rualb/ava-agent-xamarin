using System;
using System.Collections.Generic;
using System.Text;

namespace AvaGE.MobControl.ControlsTools.UserMessanger
{
    public interface IUserTopMessanger:IDisposable
    {
        int getID();
        void add(int id,string text);
        void set(int id, string text);
        void clear(int id );
        void clear();
        void write();
        IUserMessanger createChild();
    }
}
