using System;
using System.Collections.Generic;
using System.Text;

namespace AvaGE.MobControl.ControlsTools.UserMessanger
{
    public class UserMessangerChild : IUserMessanger
    {
        IUserTopMessanger parent;
        int id;

        public UserMessangerChild(IUserTopMessanger pParent, int pId)
        {
            parent = pParent;
            id = pId;
        }
        public void add(string text)
        {
            parent.add(id, text);
        }

        public void set(string text)
        {
            parent.set(id, text);
        }

        public void clear()
        {
            parent.clear(id);
        }

        public void write()
        {
            parent.write();
        }
    }
}
