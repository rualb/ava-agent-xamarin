using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace AvaExt.Common
{
    public class BaseFormat
    {
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
 
    }
}
