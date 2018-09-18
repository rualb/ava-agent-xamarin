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
    [Activity(Label = Form.FORM_NAME, Icon = Form.FORM_ICON, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, WindowSoftInputMode = Android.Views.SoftInput.StateAlwaysHidden)]
    public class MobDataReferenceDateSelectForm : MobDataReferenceValueSelectForm
    {
        protected override string globalStoreName()
        {
            return ConstRefCode.date;
        }

        public MobDataReferenceDateSelectForm()
            : base(Resource.Layout.MobDataReferenceDateSelectForm)
        {


        }

        protected override void initAfterSettings()
        {
            base.initAfterSettings();



        }

        protected override void initTable()
        {
            _table.Columns.Add(TableDUMMY.DATE_);
            _table.Columns.Add(TableDUMMY.DATETIME);
        }
        protected override DataRow getData()
        {
            var date_ = new DateTime(Math.Max(1900,(int)cDate.Year),Math.Max(1, (int)cDate.Month),Math.Max(1, (int)cDate.DayOfMonth), (int)cTime.CurrentHour, (int)cTime.CurrentMinute, 0);
            return _table.Rows.Add(new object[] { date_.Date, date_ });
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
                    cDate.Value = cTime.Value = DateTime.Parse(v);
                }
                catch { }
                try
                {
                    cDate.Value = cTime.Value = XmlFormating.helper.parseDateTime(v);
                }
                catch { }

            }

        }

        MobDatePicker cDate { get { return FindViewById<MobDatePicker>(Resource.Id.cDate); } }
        MobTimePicker cTime { get { return FindViewById<MobTimePicker>(Resource.Id.cTime); } }


    }
}

