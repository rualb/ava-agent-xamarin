using System;
using System.Collections.Generic;
using System.Text;
using MobExt.AndroidEnv.ControlsBase;
using System.Drawing;
using System.Runtime.InteropServices;
using System.ComponentModel;

using MobExt.ControlOperation;
using MobExt.Common;
using MobExt.Settings;
using Android.Widget;
using Android.Views;
using Android.Content;

namespace MobGE.MobControl
{

    public class MobContextMenu : PopupMenu, IControlGlobalInit
    {

        public MobContextMenu(Context context, View anchor)
            : base(context, anchor)
        {
            Name = string.Empty;
        }
        public string Name
        {
            get;
            set;
        }

        public virtual void globalRead(IEnvironment pEnv, ISettings pSettings)
        {
            //

            //
            _isGlobalInited = true;
            InitForGlobal.read(this, getGlobalObjactName(),  pEnv, pSettings);
            //
           

        }

        public virtual void globalWrite(IEnvironment pEnv, ISettings pSettings)
        {
            InitForGlobal.write(this, getGlobalObjactName(),  pEnv, pSettings);
        }
        public virtual string getGlobalObjactName()
        {
            return string.Empty;
        }

   

        bool _isGlobalInited = false;
        public bool isGlobalInited()
        {
            return _isGlobalInited;
        }

     
    }
}
