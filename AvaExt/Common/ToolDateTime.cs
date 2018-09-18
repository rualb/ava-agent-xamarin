using System;
using System.Collections.Generic;
using System.Text;

namespace AvaExt.Common
{
    public class ToolDateTime
    {
        public static DateTime insertIntoRange(DateTime min, DateTime max, DateTime val)
        {
            if (val < min)
                return min;
            if (val > max)
                return max;
            return val;
        }

        public static bool isBetween(DateTime val, DateTime b, DateTime e)
        {
            return ((b.CompareTo(val) <= 0) && (e.CompareTo(val) >= 0));
        }
    }
}
