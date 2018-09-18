using System;
using System.Collections.Generic;
using System.Text;

namespace AvaExt.Common.Const
{
    public class ConstValues
    {
        public static int compare(double v1, double v2)
        {
            if ((v1 - v2) > minPositive)
                return 1;
            if ((v2 - v1) > minPositive)
                return -1;

            return 0;


        }
        public const double minPositive = 0.0000001; 

        public const double minNumber = -999999999999999;
        public const double maxNumber = 999999999999999;
    }
}
