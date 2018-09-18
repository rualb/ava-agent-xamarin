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
using Android.Text;
using Java.Lang;

namespace AvaGE.MobControl
{

    public class MobNumEdit : MobTextBox, IObjectSource, INumEditor
    {
        public bool roundDecimals = true;



        public MobNumEdit(Context context)
            : base(context)
        {
            init();

        }
        public MobNumEdit(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            init();
        }


        void init()
        {
            DSProperty = "Value";

            _helper = new HelperNumEdit(this);

            //  TextChanged += MobNumEdit_TextChanged;

           // this.InputType = Android.Text.InputTypes.NumberFlagDecimal;

            reinitFilter();
        }

        void MobNumEdit_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            //has problem with selection start on change text in OnTextChanged event
            //use filter


            //int selection_ = this.SelectionStart;//!

            //_helper.textChanged();

            //if (selection_ >= this.Text.Length)//!
            //    selection_ = this.Text.Length;//!

            //if (selection_ != this.SelectionStart)
            //    this.SetSelection(selection_);//! important 
        }

        protected override void reinitFilter()
        {
            this.SetFilters(new IInputFilter[] { new ImplInputFilterNum(Minimum, Maximum, false) });
        }
        protected class ImplInputFilterNum : Java.Lang.Object, IInputFilter
        {
            double min;
            double max;
            bool truncate;

            public ImplInputFilterNum(double pMin, double pMax, bool pTruncate)
            {

                min = pMin;
                max = pMax;
                truncate = pTruncate;

            }
            string subString(ICharSequence source, int start, int end)
            {
                start = System.Math.Max(0, start);
                start = System.Math.Min(source.Length(), start);//the begin index, inclusive.
                end = System.Math.Max(0, end);
                end = System.Math.Min(source.Length(), end);// the end index, exclusive.


                if (start >= 0 && end >= 0 && end >= start)
                    return source.SubSequence(start, end);

                return string.Empty;
            }

            public ICharSequence FilterFormatted(ICharSequence source, int start, int end, ISpanned dest, int dstart, int dend)
            {

                string s_ = subString(source, start, end);

                string left_ = subString(dest, 0, dstart);
                string right_ = subString(dest, dend, dest.Length());

                string full_ = left_ + s_ + right_;

                if (!HelperNumEdit.textValidate(full_, min, max))
                {
                    return new Java.Lang.String(string.Empty);
                }


                return null;
            }


        }



        HelperNumEdit _helper;

        double _maximum = ConstValues.maxNumber;
        double _minimum = 0;
        double _increment = 0;
        string _text = string.Empty;




        public void processCmd(string pCmd)
        {
            _helper.processCmd(pCmd);
        }


        public virtual double Value
        {
            get
            {
                if (HelperNumEdit.isValidString(Text))
                    return HelperNumEdit.toValue(Text);
                return Minimum;
            }
            set
            {
                value = HelperNumEdit.checkBounds(value, Minimum, Maximum);
                if (System.Math.Abs(value) < ConstValues.minPositive)
                    Text = string.Empty;
                else
                    Text = HelperNumEdit.toText(  roundDecimals ? ToolMobile.mathRound( value):value);
            }
        }

        public virtual double Increment
        {
            get { return _increment; }
            set { _increment = value; }
        }
        public virtual double Maximum
        {
            get { return _maximum; }
            set { _maximum = value; reinitFilter(); }
        }
        public virtual double Minimum
        {
            get { return _minimum; }
            set { _minimum = value; reinitFilter(); }
        }


        public override int MaxLength
        {
            get
            {
                return short.MaxValue;
            }
            set
            {

            }
        }

        public override object get()
        {
            return Value;
        }


    }
}
