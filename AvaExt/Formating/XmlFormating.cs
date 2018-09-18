using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using AvaExt.Common;

namespace AvaExt.Formating
{
    public class XmlFormating
    {
        public class BoolValue
        {
            public const string boolTrue = "1";
            public const string boolFalse = "0";
            public const string boolTrueText = "true";
            public const string boolFalseText = "false";

            public static bool parse(string pVal)
            {
                if (pVal == boolTrue)
                    return true;
                if (pVal == boolFalse)
                    return false;

                if (pVal.ToLowerInvariant() == boolTrueText)
                    return true;
                if (pVal.ToLowerInvariant() == boolFalseText)
                    return false;

                return bool.Parse(pVal);
            }
            public static string format(bool pVal)
            {
                return pVal ? boolTrue : boolFalse;
            }
        }


        public static XmlFormating helper = new XmlFormating();

        ToolTypeSet _typeSet = new ToolTypeSet();
        NumberFormatInfo numberFormat;
        DateTimeFormatInfo dateFormat;
        const string doubleFStr = "0.#########";


        public XmlFormating()
        {


            numberFormat = getNumberFormat();
            dateFormat = getDateFormat();
        }

        public static NumberFormatInfo getNumberFormat()
        {

            NumberFormatInfo numberFormat = new NumberFormatInfo();
            numberFormat.NumberGroupSeparator = numberFormat.NumberDecimalSeparator = ".";
            return numberFormat;
        }
        public static DateTimeFormatInfo getDateFormat()
        {

            DateTimeFormatInfo dateFormat = new DateTimeFormatInfo();
            dateFormat.TimeSeparator = dateFormat.DateSeparator = "-";
            dateFormat.MonthDayPattern = "MM-dd";
            dateFormat.YearMonthPattern = "yyyy-MM";
            dateFormat.FullDateTimePattern = "yyyy-MM-dd HH-mm-ss";
            dateFormat.LongDatePattern =
            dateFormat.ShortDatePattern = "yyyy-MM-dd";
            dateFormat.LongTimePattern =
            dateFormat.ShortTimePattern = "HH-mm-ss";
            return dateFormat;
        }
        string valueToString(object value)
        {

            Type type = value.GetType();
            if (type == _typeSet.tString)
                return (string)value;
            else
                if (type == _typeSet.tDouble)
                    return ((double)value).ToString(doubleFStr, numberFormat);
                else
                    if (type == _typeSet.tFloat)
                        return ((float)value).ToString(doubleFStr, numberFormat);
                    else
                        if (type == _typeSet.tInt)
                            return ((int)value).ToString(numberFormat);
                        else
                            if (type == _typeSet.tShort)
                                return ((short)value).ToString(numberFormat);
                            else
                                if (type == _typeSet.tDateTime)
                                    return ((DateTime)value).ToString(dateFormat);
                                else
                                    if (type == _typeSet.tBool)
                                        return BoolValue.format(((bool)value));
                                    else
                                        if (!type.IsValueType)
                                            return ToolType.format((Type)value);
            return null;

        }

        public string format(object value)
        {
            return valueToString(value);
        }
        public object parse(string value, Type type, object def)
        {
            object res = parse(value, type);
            if (res != null)
                return res;
            return def;
        }
        public object parse(string value, Type type)
        {
            if (type == _typeSet.tString)
                return value;
            else
                if (type == _typeSet.tDouble)
                    return double.Parse(value, NumberStyles.Any, numberFormat);
                else
                    if (type == _typeSet.tDouble)
                        return float.Parse(value, NumberStyles.Any, numberFormat);
                    else
                        if (type == _typeSet.tShort)
                            return short.Parse(value, NumberStyles.Any, numberFormat);
                        else
                            if (type == _typeSet.tInt)
                                return int.Parse(value, NumberStyles.Any, numberFormat);
                            else
                                if (type == _typeSet.tBool)
                                    return BoolValue.parse(value);
                                else
                                    if (type == _typeSet.tDateTime)
                                        return DateTime.ParseExact(value, dateFormat.FullDateTimePattern, dateFormat);
                                    else
                                        if (type == _typeSet.tType)
                                            return ToolType.parse(value);
                                        else
                                            if (type == _typeSet.tObject)
                                                return (value);
            return null;
        }
        public string parseString(string value)
        {
            return value;
        }
        public double parseDouble(string value)
        {
            return double.Parse(value, NumberStyles.Any, numberFormat);
        }
        public float parseFloat(string value)
        {
            return float.Parse(value, NumberStyles.Any, numberFormat);
        }
        public short parseShort(string value)
        {
            return short.Parse(value, NumberStyles.Any, numberFormat);
        }
        public int parseInt(string value)
        {
            return int.Parse(value, NumberStyles.Any, numberFormat);
        }
        public bool parseBool(string value)
        {
            return BoolValue.parse(value);
        }
        public DateTime parseDateTime(string value)
        {
            return DateTime.ParseExact(value, dateFormat.FullDateTimePattern, dateFormat);
        }
        public Type parseType(string value)
        {
            return ToolType.parse(value);
        }
    }
}
