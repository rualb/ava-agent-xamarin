using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.AndroidEnv.ControlsBase;

using AvaExt.Settings;

using AvaExt.ControlOperation;
using AvaExt.Common;
using System.Data;
using Android.Widget;
using Android.Content;
using Android.Util;
using AvaExt.Translating.Tools;
using Android.Runtime;

namespace AvaGE.MobControl
{
    public class MobLabel : TextView, IControlGlobalInit, IControlBind, ITranslateable
    {
        protected MobLabel(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {

        }
        public MobLabel(Context context)
            : base(context)
        {


        }
        public MobLabel(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {

        }


        public MobLabel(Context context, IAttributeSet attrs, int defStyle)
            : base(context, attrs,defStyle)
        {

        }

       
        public string userDesc;

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



        string _DSProperty = "Text";
        public string DSProperty
        {
            get
            {
                return _DSProperty;
            }
            set
            {
                _DSProperty = value;
            }
        }
        string _DSTable = string.Empty;
        public string DSTable
        {
            get
            {
                return _DSTable;
            }
            set
            {
                _DSTable = value;
            }
        }
        string _DSSubTable = string.Empty;
        public string DSSubTable
        {
            get
            {
                return _DSSubTable;
            }
            set
            {
                _DSSubTable = value;
            }
        }
        string _DSColumn = string.Empty;
        public string DSColumn
        {
            get
            {
                return _DSColumn;
            }
            set
            {
                _DSColumn = value;
            }
        }
        bool _isBound = false;
        public bool isBound()
        {
            return _isBound;
        }
        public void bound(IEnvironment env)
        {

        }

        public string getTranslatingText()
        {
            return this.Text;
        }

        public void setTranslatingText(string pText)
        {
            this.Text = pText;
        }
    }
}
