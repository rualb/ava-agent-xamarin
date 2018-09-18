using System;
using System.Collections.Generic;
using System.Text;
using MobExt.AndroidEnv.ControlsBase;

using MobExt.ControlOperation;
using System.Data;
using MobExt.Common;
using Android.Widget;
using Android.Content;
using Android.Util;

namespace MobGE.MobControl
{
    public class MobDateTimePicker : ListView, IControlBind
    {


        public MobDateTimePicker(Context context)
            : base(context)
        {


        }
        public MobDateTimePicker(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            Name = string.Empty;
        }


        public string Name
        {
            get;
            set;
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
