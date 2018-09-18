using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.AndroidEnv.ControlsBase;
using AvaExt.ControlOperation;
using AvaExt.Common;
using AvaExt.Settings;
using Android.Widget;
using Android.Content;
using Android.Util;
using Android.Views;

namespace AvaGE.MobControl
{
    public class MobPanelRelative : RelativeLayout, IControlGlobalInit, ISelfDestructable
    {


        public MobPanelRelative(Context context)
            : base(context)
        {


        }
        public MobPanelRelative(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
         
        }

        string name;
        public string Name
        {
            get { return name == null ? name = ToolMobile.getFromTagName(this) : name; }

        }

        #region IControlGlobalInit Members

        public virtual void globalRead(IEnvironment pEnv, ISettings pSettings)
        {
            _isGlobalInited = true;
            InitForGlobal.read(this, getGlobalObjactName(), pEnv, pSettings);

        }

        public virtual void globalWrite(IEnvironment pEnv, ISettings pSettings)
        {
            InitForGlobal.write(this, getGlobalObjactName(), pEnv, pSettings);
        }
        public virtual string getGlobalObjactName()
        {
            return this.Name;
        }



        bool _isGlobalInited = false;
        public bool isGlobalInited()
        {
            return _isGlobalInited;
        }
        #endregion



        public object[] selfDestruct()
        {
           return ToolMobile.getChilds(this);

        
        }
    }
}
