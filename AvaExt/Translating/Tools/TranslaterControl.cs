using System;
using System.Collections.Generic;
using System.Text;

using System.Globalization;
using AvaExt.Common;
using AvaExt.Settings;
using Android.Views;
using AvaExt.AndroidEnv.ControlsBase;
using AvaExt.ControlOperation;

namespace AvaExt.Translating.Tools
{
    public class TranslaterControl
    {
        public static bool canTranslate(object pTarget)
        {
            if (pTarget == null)
                return false;

            if (typeof(ITranslateable).IsAssignableFrom(pTarget.GetType()))
                return true;


            if (typeof(ISelfDestructable).IsAssignableFrom(pTarget.GetType()))
                return true;

            return false;

        }

        public static void set(object pTarget, string pLang, ISettings  pSetting )
        {
          //  set(pTarget, new TranslaterText(pLang, pSetting ));
            set(pTarget, new TranslaterText(pLang));
        }
        
        static void set(object pTarget, TranslaterText trans)
        {
            //first model ISelfDestructable and ITranslateable

            if (pTarget == null)
                return;

            foreach (object o_ in ToolControl.destruct(pTarget))
            {
                var t_ = o_ as ITranslateable;
                if (t_ != null) t_.setTranslatingText(trans.get(t_.getTranslatingText()));
            }

        }

    }
}
