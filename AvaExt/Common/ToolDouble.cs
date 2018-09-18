using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AvaExt.Common.Const;

namespace AvaExt.Common
{
    public class ToolDouble
    {


        public static bool isLess(double x, double y)
        {
            return ((x + ConstValues.minPositive) < y);
        }

        public static bool isLE(double x, double y)
        {
            return (isLess(x, y) || isEqual(x, y));
        }
        public static bool isGreate(double x, double y)
        {
            return !isLE(x, y);
        }
        public static bool isGE(double x, double y)
        {
            return (isGreate(x, y) || isEqual(x, y));
        }
        public static bool isEqual(double x, double y)
        {
            return (Math.Abs(x - y) <= ConstValues.minPositive);
        }
        public static bool isZero(double x)
        {
            return isEqual(x, 0);
        }
        public static bool isBetween(double z, double x, double y)
        {
            return (isGE(z, x) && isLE(z, y));
        }

        public static double div(object pX, object pY)
        {
            double x = Convert.ToDouble(pX);
            double y = Convert.ToDouble(pY);

            if (ToolDouble.isZero(y))
                return 0;

            return x / y;

        }
        public static double mult(object pX, object pY)
        {
            double x = Convert.ToDouble(pX);
            double y = Convert.ToDouble(pY);
            return x * y;
        }
        public static double sum(object pX, object pY)
        {
            double x = Convert.ToDouble(pX);
            double y = Convert.ToDouble(pY);
            return x + y;
        }
        public static double sub(object pX, object pY)
        {
            double x = Convert.ToDouble(pX);
            double y = Convert.ToDouble(pY);
            return x - y;
        }
    }
}