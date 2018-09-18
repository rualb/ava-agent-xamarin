using System;
using System.Collections.Generic;
using System.Text;
using MobExt.AndroidEnv.ControlsBase;
using MobExt.Common;
using MobExt.ControlOperation;
using MobExt.Settings;
using Android.Content;
using Android.Util;

namespace MobGE.MobControl
{
    public class MobMenuItem : MobLabel, IControlGlobalInit
    {


        public MobMenuItem(Context context)
            : base(context)
        {
            base.Click += click;

        }

        void click(object sender, EventArgs e)
        {
            this.OnClick(e);
        }
        public MobMenuItem(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            Name = string.Empty;
        }

        public string Name
        {
            get;
            set;
        }

        public IActivity activity = null;
        protected virtual void OnClick(EventArgs e)
        {
            if (activity != null)
                activity.done();


           
        }
        public virtual void globalRead(IEnvironment pEnv, ISettings pSettings)
        {
            //

            //
            _isGlobalInited = true;
            InitForGlobal.read(this, getGlobalObjactName(), pEnv, pSettings);
            //


        }

        public virtual void globalWrite(IEnvironment pEnv, ISettings pSettings)
        {
            InitForGlobal.write(this, getGlobalObjactName(), pEnv, pSettings);
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
