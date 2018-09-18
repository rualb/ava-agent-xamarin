using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.AndroidEnv.ControlsBase;
using System.Runtime.InteropServices;
using System.ComponentModel;

using AvaExt.Settings;
using AvaExt.Translating.Tools;
using AvaExt.ControlOperation;
using AvaExt.Common;
using Android.Widget;
using Android.Content;
using Android.Util;
using Android.Views;
using Android.Runtime;
using Ava_Ext.Common;

namespace AvaGE.MobControl
{
    public class MobButton : Button, IControlGlobalInit, ITranslateable
    {
        public IActivity activity;
        public MobButton(Context context)
            : base(context)
        {

            this.Click += MobButton_Click;
        }

        protected MobButton(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {

        }
        public MobButton(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            this.Click += MobButton_Click;
        }


        void MobButton_Click(object sender, EventArgs e)
        {
            if (activity != null)
                activity.done();
        }

        public bool Visible
        {

            get { return this.Visibility == ViewStates.Visible; }
            set { this.Visibility = value ? ViewStates.Visible : ViewStates.Gone; }

        }
        string name;
        public string Name
        {
            get { return name == null ? name = ToolMobile.getFromTagName(this) : name; }

        }
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


 


        public string getTranslatingText()
        {
            return this.Text;
        }

        public void setTranslatingText(string pText)
        {
            this.Text = pText;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (activity != null)
                activity.Dispose();

            activity = null;

            Click -= MobButton_Click;

        
            ToolDispose.dispose(this.Background);

            this.Background = null;
        }

    }
}
