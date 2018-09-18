using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.AndroidEnv.ControlsBase;
using System.Drawing;
using System.Threading;
using AvaExt.Common.Const;

using AvaExt.ControlOperation;
using System.Globalization;
using System.Text.RegularExpressions;
using AvaExt.Common;
using AvaExt.ObjectSource;
using AvaGE.MobControl.Tools;
using AvaExt.Settings;
using Android.Content;
using Android.Util;
using AvaExt.Formating;

namespace AvaGE.MobControl
{

    public class MobDateEdit : MobTextBox, IObjectSource
    {
        public MobDateEdit(Context context)
            : base(context)
        {
            init();

        }
        public MobDateEdit(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            init();
        }

        DateTimeFormatInfo dateFormat;
        void init()
        {
            DSProperty = "Value";


            dateFormat = new DateTimeFormatInfo();
            dateFormat.TimeSeparator = dateFormat.DateSeparator = "-";
            //   dateFormat.MonthDayPattern = "MM-dd";
            //    dateFormat.YearMonthPattern = "yyyy-MM";
            dateFormat.FullDateTimePattern = "yyyy-MM-dd HH:mm";
            dateFormat.LongDatePattern =
            dateFormat.ShortDatePattern = "yyyy-MM-dd";
            dateFormat.LongTimePattern =
            dateFormat.ShortTimePattern = "HH:mm";



            this.TextChanged += MobDateEdit_TextChanged;
            this.InputType = Android.Text.InputTypes.ClassDatetime;
        }

        void MobDateEdit_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {

        }








        public virtual DateTime Value
        {
            get
            {
                try
                {
                    return DateTime.Parse(Text, dateFormat);
                }
                catch
                {

                }

                return new DateTime(1900, 1, 1);
            }
            set
            {
                Text = value.ToString(dateFormat);
            }
        }




        public override object get()
        {
            return Value;
        }
    }
}
