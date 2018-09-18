using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.AndroidEnv.ControlsBase;


using AvaExt.Settings;
using System.Drawing;
using AvaExt.ControlOperation;
using AvaExt.Common;
using Android.Widget;
using Android.Content;
using Android.Util;

namespace AvaGE.MobControl 
{
    public class MobTabControl : TabHost, IControlGlobalInit, ISelfDestructable
    {
        IUserImage images;

        
        public MobTabControl(Context context)
            : base(context)
        {


        }
        public MobTabControl(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
         
        }

        string name;
        public string Name
        {
            get { return name == null ? name = ToolMobile.getFromTagName(this) : name; }

        }

        public virtual void globalRead(IEnvironment pEnv, ISettings pSettings)
        {
            _isGlobalInited = true;
            images = pEnv.getImages();
 
            InitForGlobal.read(this, getGlobalObjactName(),  pEnv, pSettings);


        }

        public virtual void globalWrite(IEnvironment pEnv, ISettings pSettings)
        {
            InitForGlobal.write(this, getGlobalObjactName(),   pEnv, pSettings);
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

        public object[] selfDestruct()
        {
            return ToolMobile.getChilds(this);


        }
    }
}
