using System;
using System.Collections.Generic;
using System.Text;

namespace AvaExt.Common
{
    public class ToolDate
    {
        public static DateTime min(DateTime d1, DateTime d2)
        {
            return ((DateTime.Compare(d1,d2) > 0) ? d2 : d1);
        }
        public static DateTime max(DateTime d1, DateTime d2)
        {
            return ((DateTime.Compare(d1,d2) > 0) ? d1 : d2);
        }
    }
}
