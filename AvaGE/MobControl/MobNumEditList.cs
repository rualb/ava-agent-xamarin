using System;
using System.Collections.Generic;
using System.Text;
using MobExt.AndroidEnv.ControlsBase;
using System.Drawing;
using System.Threading;
using MobExt.Common.Const;

using MobExt.ControlOperation;
using System.Globalization;
using System.Text.RegularExpressions;
using MobExt.Common;
using MobExt.ObjectSource;
using MobGE.MobControl.Tools;

namespace MobGE.MobControl
{

    public class MobNumEditList : MobComboBox, IObjectSource, INumEditor
    {


        HelperNumEdit _helper;

        double _maximum = ConstValues.maxNumber;
        double _minimum = 0;
        double _increment = 0;
        string _text = string.Empty;

        //Regex exp;
        public MobNumEditList()
            : base()
        {
            DSProperty = "Value";
            base.MaxLength = 20;
            this.DropDownStyle = ComboBoxStyle.DropDown;
            _helper = new HelperNumEdit(this);


        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);

            _helper.textChanged();
        }

        public void processCmd(string pCmd)
        {
            _helper.processCmd(pCmd);
        }


        public virtual double Value
        {
            get
            {
                if (_helper.isValidString(Text))
                    return _helper.toValue(Text);
                return Minimum;
            }
            set
            {
                value = _helper.checkBounds(value, Minimum, Maximum);
                if (Math.Abs(value) < ConstValues.minPositive)
                    Text = string.Empty;
                else
                    Text = value.ToString();
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
            set { _maximum = value; }
        }
        public virtual double Minimum
        {
            get { return _minimum; }
            set { _minimum = value; }
        }


        //public override int MaxLength
        //{
        //    get
        //    {
        //        return 0;
        //    }
        //    set
        //    {

        //    }
        //}

        public object get()
        {
            return Value;
        }
    }
}
