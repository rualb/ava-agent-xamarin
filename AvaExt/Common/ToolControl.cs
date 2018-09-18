using Android.Views;
using AvaExt.AndroidEnv.ControlsBase;
using AvaExt.ControlOperation;
using System;
using System.Collections.Generic;
using System.Text;


namespace AvaExt.Common
{
    public class ToolControl
    {
        public static object[] destruct(object control)
        {
            List<object> list = new List<object>();
            if (control != null)
            {
               // ToolMobile.log("control destruct [" + control.GetType().FullName + "]");

                list.Add(control);

                if ((control as IIndestructable) == null)
                {
                    ISelfDestructable s = control as ISelfDestructable;
                    if (s != null)
                    {
                        foreach (object o in s.selfDestruct())
                            list.AddRange(destruct(o));

                    }
                    else
                    {
                       // ToolMobile.log("control destruct try as group view [" + control.GetType().FullName + "]");

                        foreach (object o in ToolMobile.getChilds(control as View))
                            list.AddRange(destruct(o));
                    }

                }
            }
            return list.ToArray();
        }





        public static bool isDone(Keycode keycode, char char_)
        {
            if (
                  (keycode == Keycode.Enter)  ||
                 // (keycode == Keycode.Tab) ||
                //  (keycode == Keycode.Space) ||
                  (char_ == '\n')// ||
                 // (char_ == '\r') ||
                 // (char_ == '\t')
                  )
                return true;

            return false;
        }
    }
}
