using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.AndroidEnv.ControlsBase;
using System.Collections;
using AvaGE.MobControl;
using Android.Views;
using AvaExt.AndroidEnv.ApplicationBase;

namespace AvaGE.MobControl.ControlsTools.UserMessanger
{
    public class UserMessangerParentControlText : IUserTopMessanger
    {
        int idSeq = 0;
        Dictionary<int, string> msgs = new Dictionary<int, string>();
        Form control;

        public UserMessangerParentControlText(Form pControl)
        {
            control = pControl;

        }



        public int getID()
        {
            int newId = ++idSeq;
            msgs.Add(newId, string.Empty);
            return newId;
        }

        public void add(int id, string text)
        {
            set(id, text);
        }

        public void set(int id, string text)
        {
            if (msgs.ContainsKey(id))
                msgs[id] = text;

        }



        public void clear(int id)
        {
            set(id, string.Empty);
        }
        public void clear()
        {
            int[] arr = new int[msgs.Keys.Count];
            msgs.Keys.CopyTo(arr, 0);
            for (int i = 0; i < arr.Length; ++i)
                set(arr[i], string.Empty);
        }

        public IUserMessanger createChild()
        {
            return new UserMessangerChild(this, getID());
        }
        public void write()
        {
            
            string text = string.Empty;
            IEnumerator<string> enumer = msgs.Values.GetEnumerator();
            while (enumer.MoveNext())
                text += enumer.Current;
            control.Text = text;
            
        }


        public void Dispose()
        {
            control = null;
        }
    }
}
