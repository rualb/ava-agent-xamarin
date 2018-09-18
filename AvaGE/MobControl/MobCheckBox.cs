using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.AndroidEnv.ControlsBase;

using AvaExt.ControlOperation;
using System.Data;
using AvaExt.Common;
using AvaExt.Settings;
using Android.Widget;
using Android.Content;
using Android.Util;

namespace AvaGE.MobControl
{
    public class MobCheckBox : CheckBox, IControlBind, IControlGlobalInit
    {



        public MobCheckBox(Context context)
            : base(context)
        {
            // DataBindings.CollectionChanged += new System.ComponentModel.CollectionChangeEventHandler(DataBindings_CollectionChanged);


        }
        public MobCheckBox(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            Name = string.Empty;
        }

        public string Name
        {
            get;
            set;
        }

        //check
        //void DataBindings_CollectionChanged(object sender, System.ComponentModel.CollectionChangeEventArgs e)
        //{
        //    Binding b = (Binding)e.Element;
        //    if (b != null)
        //    {
        //        b.Format += new ConvertEventHandler(b_Format);
        //        b.Parse += new ConvertEventHandler(b_Parse);
        //    }
        //}

        //void b_Parse(object sender, ConvertEventArgs e)
        //{
        //    if (e.Value.GetType() == typeof(Boolean))
        //    {
        //        bool val = (bool)e.Value;
        //        if (val)
        //            e.Value = Convert.ChangeType(_ValueTrue, e.DesiredType, null);
        //        else
        //            e.Value = Convert.ChangeType(_ValueFalse, e.DesiredType, null);
        //    }
        //}

        //void b_Format(object sender, ConvertEventArgs e)
        //{
        //    if (e.Value != null)
        //    {
        //        string val = e.Value.ToString();
        //        if (val == ValueTrue)
        //            e.Value = true;
        //        else
        //            if (val == ValueFalse)
        //                e.Value = false;


        //    }
        //}

        string _DSProperty = "Checked";
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

        string _ValueTrue = "1";
        public string ValueTrue
        {
            get
            {
                return _ValueTrue;
            }
            set
            {
                _ValueTrue = value;
            }
        }
        string _ValueFalse = "0";
        public string ValueFalse
        {
            get
            {
                return _ValueFalse;
            }
            set
            {
                _ValueFalse = value;
            }
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




    }
}
