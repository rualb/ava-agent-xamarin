using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.AndroidEnv.ControlsBase;
using System.Globalization;
using System.Text.RegularExpressions;
using AvaExt.Common;
using AvaExt.Common.Const;
using AvaExt.Formating;

namespace AvaGE.MobControl.Tools
{
    public class CalcCmd
    {
        public const string undef = "";

        public const string d1 = "1";
        public const string d2 = "2";
        public const string d3 = "3";
        public const string d4 = "4";
        public const string d5 = "5";
        public const string d6 = "6";
        public const string d7 = "7";
        public const string d8 = "8";
        public const string d9 = "9";
        public const string d0 = "0";

        public const string separator = ".";
        public const string back = "B";
        public const string clear = "C";
        public const string reverse = "R";

        public const string plus = "+";
        public const string minus = "-";
        public const string div = "/";
        public const string mult = "*";

        public const string MC = "MC";
        public const string MS = "MS";
        public const string MR = "MR";
        public const string MP = "MP";

        public const string calc = "=";
        public const string reset = "reset";
    }
    public class HelperNumEdit
    {
        INumEditor editor;

        double _memory = 0;
        double _register1 = 0;
        double _register2 = 0;
        string _calcCmd = CalcCmd.undef;
        //bool _startNew = false;

        static readonly string NegativeSign;
        static readonly string PositiveSign;
        static readonly string DecimalSeparator;
        static readonly string GroupSeparator;

        BlockPoint _block = new BlockPoint();

        static readonly Regex CheckDigtEmptyString;

        string _prevText = string.Empty;

        static HelperNumEdit()
        {
            if (CheckDigtEmptyString == null)
            {


                NumberFormatInfo numInfo = XmlFormating.getNumberFormat();// CultureInfo.CurrentCulture.NumberFormat;
                string escNegativeSign = Regex.Escape(NegativeSign = numInfo.NegativeSign);
                string escPositiveSign = Regex.Escape(PositiveSign = numInfo.PositiveSign);
                string escDecimalSeparator = Regex.Escape(DecimalSeparator = numInfo.NumberDecimalSeparator);
                string escGroupSeparator = Regex.Escape(GroupSeparator = numInfo.NumberGroupSeparator);

                CheckDigtEmptyString = new Regex(
                    "^((" + escNegativeSign + ")|(" + escPositiveSign + ")){0,1}((" + escGroupSeparator + ")|(0))*(" + escDecimalSeparator + "){0,1}((" + escGroupSeparator + ")|(0))*$",
                    RegexOptions.Compiled);

            }


        }

        public HelperNumEdit(INumEditor pEditor)
        {
            editor = pEditor;


        }


        public void textChanged()
        {

            if (_block.block())
            {
                try
                {
                    if (_prevText != editor.Text)
                    {
                        if (isEmptyString(editor.Text))
                        {
                            _prevText = editor.Text;
                            return;
                        }
                        if (isValidString(editor.Text))
                        {

                            double curVal = toValue(editor.Text);
                            double curValFiltered = checkBounds(curVal, editor.Minimum, editor.Maximum);

                            if (Math.Abs(curVal - curValFiltered) < ConstValues.minPositive)
                                _prevText = editor.Text;
                            else
                                _prevText = toText(curValFiltered);


                        }

                        if (editor.Text != _prevText)
                        {
                            try
                            {
                                editor.Text = _prevText;
                            }
                            catch { }
                        }

                    }
                }
                finally
                {

                    _block.unblock();
                }
            }
        }

        public static bool textValidate(string pText,double pMin,double pMax)
        {



            if (isEmptyString(pText))
            {
                return true;
            }
            if (isValidString(pText))
            {

                double curVal = toValue(pText);
                double curValFiltered = checkBounds(curVal, pMin, pMax);

                if (Math.Abs(curVal - curValFiltered) < ConstValues.minPositive)
                    return true;


            }

            return false;       

        }


        public double calcValue(string cmp, double x, double y)
        {
            switch (cmp)
            {
                case CalcCmd.plus:
                    return x + y;
                case CalcCmd.minus:
                    return x - y;
                case CalcCmd.div:
                    return (Math.Abs(y) > ConstValues.minPositive ? x / y : 0);
                case CalcCmd.mult:
                    return x * y;
            }
            return 0;
        }
        public void processCmd(string pCmd)
        {

            // int indx = 0;
            // int len = 0;
            switch (pCmd)
            {
                case CalcCmd.d0:
                case CalcCmd.d1:
                case CalcCmd.d2:
                case CalcCmd.d3:
                case CalcCmd.d4:
                case CalcCmd.d5:
                case CalcCmd.d6:
                case CalcCmd.d7:
                case CalcCmd.d8:
                case CalcCmd.d9:
                    editor.Text += pCmd;

                    break;

                case CalcCmd.plus:
                case CalcCmd.minus:
                case CalcCmd.mult:
                case CalcCmd.div:
                    if (_calcCmd != CalcCmd.undef)
                        processCmd(CalcCmd.calc);

                    _calcCmd = pCmd;
                    _register2 = _register1 = editor.Value;
                    editor.Text = string.Empty;

                    break;
                case CalcCmd.calc:
                    if (_calcCmd != CalcCmd.undef)
                    {
                        _register2 = editor.Value;
                        editor.Value = calcValue(_calcCmd, _register1, _register2);
                        _calcCmd = CalcCmd.undef;
                    }
                    break;

                case CalcCmd.back:

                    if (editor.Text.Length > 0)
                        editor.Text = editor.Text.Remove(editor.Text.Length - 1, 1);
                    break;
                case CalcCmd.clear:

                    editor.Text = string.Empty;
                    break;
                case CalcCmd.reverse:

                    editor.Value *= -1;
                    break;
                case CalcCmd.separator:
                    editor.Text += DecimalSeparator;
                    break;
                case CalcCmd.MC:
                    _memory = 0;
                    break;
                case CalcCmd.MS:
                    _memory = editor.Value;
                    break;
                case CalcCmd.MR:

                    editor.Value = _memory;
                    break;
                case CalcCmd.MP:
                    _memory += editor.Value;
                    break;


                case CalcCmd.reset:
                    _register2 = _register1 = 0;
                    _calcCmd = CalcCmd.undef;
                    break;
            }
        }
        public static bool isEmptyString(string str)
        {
            return CheckDigtEmptyString.IsMatch(str);
        }
        public static bool isValidString(string str)
        {
            try
            {
                XmlFormating.helper.parseDouble(str);
                return true;
            }
            catch
            {


            }
            return false;

        }
        public static double toValue(string str)
        {
            return XmlFormating.helper.parseDouble(str);
        }
        public static string toText(double val)
        {
            return XmlFormating.helper.format(val);

        }
        public static double checkBounds(double val, double min, double max)
        {
            if (val < min)
                return min;
            if (val > max)
                return max;
            return val;
        }




    }
}
