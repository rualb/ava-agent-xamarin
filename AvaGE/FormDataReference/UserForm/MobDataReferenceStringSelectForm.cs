using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using AvaExt.AndroidEnv.ControlsBase;
using AvaExt.Common;
using AvaExt.Manual.Table;
using AvaExt.Common.Const;
using Android.Widget;
using AvaGE.MobControl;
using AvaExt.Formating;
using Android.App;
using AvaAgent;


namespace AvaGE.FormDataReference.UserForm
{
    [Activity(Label = Form.FORM_NAME, Icon = Form.FORM_ICON, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, 
        WindowSoftInputMode = Android.Views.SoftInput.StateAlwaysVisible)]
    public class MobDataReferenceStringSelectForm : MobDataReferenceValueSelectForm
    {
        protected override string globalStoreName()
        {
            return ConstRefCode.string_;
        }

        public MobDataReferenceStringSelectForm()
            : base(Resource.Layout.MobDataReferenceStringSelectForm)
        {

            Created += MobDataReferenceStringSelectForm_Created;
        }

        void MobDataReferenceStringSelectForm_Created(object sender, EventArgs e)
        {
            cString.KeyPress += cString_KeyPress;
        }


        void cString_KeyPress(object sender, Android.Views.View.KeyEventArgs e)
        {
            e.Handled = false;

            if (ToolControl.isDone(e.KeyCode, e.Event.Number))
            {
                e.Handled = true;
                returnData(getData());
            }

        }

        protected override void initAfterSettings()
        {
            base.initAfterSettings();
            reset();
        }

        protected override void initTable()
        {
            _table.Columns.Add(TableDUMMY.VALUE);
        }
        protected override DataRow getData()
        {
            return _table.Rows.Add(new object[] { cString.Text });
        }
        public override void setValue(object value)
        {
            if (value != null)
                value = value.ToString();

            string v = value as string;

            if (v != null)
            {

                try
                {
                    cString.Text = v;
                }
                catch { }
            

            }

        }


        protected void reset()
        {
            cString.Text = string.Empty;
            cString.RequestFocus();

            this.Window.SetSoftInputMode(Android.Views.SoftInput.StateVisible);
            
        }

        MobTextBox cString { get { return FindViewById<MobTextBox>(Resource.Id.cString); } }
 


    }
}

