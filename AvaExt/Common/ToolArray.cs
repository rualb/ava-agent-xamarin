using System;
using System.Collections.Generic;
using System.Text;

namespace AvaExt.Common
{
    public class ToolArray
    {
        public static T[] merge<T>(T[] arr1, T[] arr2)
        {
            T[] tmp = new T[arr1.Length + arr2.Length];
            arr1.CopyTo(tmp, 0);
            arr2.CopyTo(tmp, arr1.Length);
            return tmp;
        }

        public static T[] sub<T>(T[] arr, int startIndx)
        {
            T[] tmp = new T[Math.Max(arr.Length - startIndx, 0)];
            for (int i = startIndx; i < arr.Length; ++i)
                tmp[i - startIndx] = arr[i];
            return tmp;
        }

        public static T[] resize<T>(T[] arr, int newLen)
        {
            T[] tmp = new T[newLen];
            int count = Math.Min(arr.Length, newLen);
            for (int i = 0; i < count; ++i)
                tmp[i] = arr[i];
            return tmp;
        }

        public static T[] create<T>(int len, T fill)
        {
            T[] tmp = new T[len];
            for (int i = 0; i < len; ++i)
                tmp[i] = fill;
            return tmp;
        }

        public static string join(object[] arr)
        {
            StringBuilder b = new StringBuilder();
            foreach (object obj in arr)
                if (obj != null)
                    b.Append(obj.ToString()).Append(',');
            return b.ToString().Trim(',');
        }

        public static string[][] stringArray(object[][] arr)
        {
            if (arr != null)
            {
                string[][] res = new string[arr.Length][];
                for (int i = 0; i < arr.Length; ++i)
                    res[i] = stringArray(arr[i]);
                return res;
            }
            return new string[0][];
        }
        public static string[] stringArray(object[] arr)
        {
            if (arr != null)
            {
                string[] res = new string[arr.Length];
                for (int i = 0; i < arr.Length; ++i)
                    res[i] = (arr[i] != null ? arr[i].ToString() : string.Empty);
                return res;
            }
            return new string[0];
        }


        public static bool isEqual(byte[] x, byte[] y)
        {

            if (x == null || y == null)
                return false;

            if (x.Length != y.Length)
                return false;


            for (int i = 0; i < x.Length; ++i)
                if (x[i] != y[i])
                    return false;

            return true;
        }


        public static bool isEqual(object[] x, object[] y)
        {

            if (x == null || y == null)
                return false;

            if (x.Length != y.Length)
                return false;

            for (int i = 0; i < x.Length; ++i)
            {
                if (!ToolType.isEqual(x[i], y[i]))
                {
                    return false;
                }
            }

            return true;

        }

        public static int compare(object[] x, object[] y)
        {

            if (x == null)
                return 1;

            if (y == null)
                return -1;

            for (int i = 0; i < Math.Min(x.Length, y.Length); ++i)
            {

                var x1 = x[i] as IComparable;

                if (x1 != null)
                {
                    int res_ = x1.CompareTo(y[i]);
                    if (res_ != 0)
                        return res_;
                }
            }

            if (x.Length > y.Length)
                return 1;
            else
                if (x.Length < y.Length)
                    return -1;

            return 0;
        }

        public class Comparer : IComparer<object[]>
        {
            public int Compare(object[] x, object[] y)
            {
                return compare(x, y);
            }
        }

    }
}
