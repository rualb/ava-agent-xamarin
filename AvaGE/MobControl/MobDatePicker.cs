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

namespace AvaGE.MobControl
{
    public class MobDatePicker : DatePicker, IControlGlobalInit, IControlBind
    {
        public MobDatePicker(Context context)
            : base(context)
        {
            CalendarViewShown = false;
            SpinnersShown = true;

        }
        public MobDatePicker(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            CalendarViewShown = false;
            SpinnersShown = true;
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



        public DateTime Value
        {
            get { return new DateTime(Year, Month, DayOfMonth, 0, 0, 0); }
            set
            {
                DateTime = new DateTime(value.Year,value.Month,value.Day,0,0,0);

            }


        }



        string _DSProperty = "Value";
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


    }
}
