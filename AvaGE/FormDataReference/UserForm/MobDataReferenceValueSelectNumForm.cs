using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using AvaExt.AndroidEnv.ControlsBase;
using AvaExt.DataRefernce;
using AvaExt.PagedSource;
using AvaExt.Common;
using AvaGE.MobControl.ControlsTools;
using AvaGE.MobControl.ControlsTools.UserMessanger;
using AvaExt.Settings;
using AvaExt.Translating.Tools;
using AvaGE.Common;

using AvaExt.SQL;
using AvaExt.SQL.Dynamic;
using AvaExt.Manual.Table;
using System.IO;
using AvaExt.ControlOperation;
using AvaExt.Common.Const;
using AvaGE.MobControl;
using Android.App;
using AvaAgent;

namespace AvaGE.FormDataReference.UserForm
{
    [Activity(Label = Form.FORM_NAME, Icon = Form.FORM_ICON, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, WindowSoftInputMode = Android.Views.SoftInput.StateAlwaysHidden)]
    public partial class MobDataReferenceValueSelectNumForm : MobDataReferenceValueSelectForm
    {

        protected override string globalStoreName()
        {
            return ConstRefCode.number;
        }
        protected override void initTable()
        {
            _table.Columns.Add(TableDUMMY.VALUE);
        }
        protected override DataRow getData()
        {
            return _table.Rows.Add(new object[] { cNumEdit.Value });
        }
        
        public MobDataReferenceValueSelectNumForm()
            : base(Resource.Layout.MobDataReferenceValueSelectNumForm)
        {


  
       
        }

        protected override void initAfterSettings()
        {
            base.initAfterSettings();

            cNumInput.setNumEdit(cNumEdit);
        }
 
        public double Minimum
        {
            get { return cNumEdit.Minimum; }
            set { cNumEdit.Minimum = value; }


        }
        public double Maximum
        {
            get { return cNumEdit.Maximum; }
            set { cNumEdit.Maximum = value; }


        }



        MobNumEdit cNumEdit { get { return FindViewById<MobNumEdit>(Resource.Id.cNumEdit); } }
        MobNumInput cNumInput { get { return FindViewById<MobNumInput>(Resource.Id.cNumInput); } }
  
      

    }
}

