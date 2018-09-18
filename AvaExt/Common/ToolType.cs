using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common.Const;
using AvaExt.Formating;

namespace AvaExt.Common
{
    public class ToolType
    {
        //static ToolTypeSet _typeSet = new ToolTypeSet();


        //public static bool isEqual(object val1, object val2)
        //{



        //    if ((val1 != null) && (val2 != null))
        //    {
        //        object obj1 = val1;
        //        object obj2 = val2;

        //        Type type1 = obj1.GetType();
        //        Type type2 = obj2.GetType();

        //        if (type1 == _typeSet.tShort)
        //        {
        //            obj1 = (int)(short)obj1;
        //            type1 = _typeSet.tInt;
        //        }
        //        if (type2 == _typeSet.tShort)
        //        {
        //            obj2 = (int)(short)obj2;
        //            type2 = _typeSet.tInt;
        //        }
        //        if (type1.IsEnum)
        //        {
        //            obj1 = Convert.ToInt32(obj1);
        //            type1 = _typeSet.tInt;
        //        }
        //        if (type2.IsEnum)
        //        {
        //            obj2 = Convert.ToInt32(obj2);
        //            type2 = _typeSet.tInt;
        //        }
        //        if (type1 == _typeSet.tFloat)
        //        {
        //            obj1 = (double)(float)obj1;
        //            type1 = _typeSet.tDouble;
        //        }
        //        if (type2 == _typeSet.tFloat)
        //        {
        //            obj2 = (double)(float)obj2;
        //            type2 = _typeSet.tDouble;
        //        }
        //        // if (type == type2)
        //        {
        //            if ((type1 == _typeSet.tInt) && (type2 == _typeSet.tInt))
        //                return ((int)obj1) == ((int)obj2);
        //            else
        //                if ((type1 == _typeSet.tDouble) && (type2 == _typeSet.tDouble))
        //                    return Math.Abs((double)obj1 - (double)obj2) < ConstValues.minPositive;
        //                else
        //                    if ((type1 == _typeSet.tString) && (type2 == _typeSet.tString))
        //                        return ((string)obj1) == ((string)obj2);
        //                    else
        //                        if ((type1 == _typeSet.tDateTime) && (type2 == _typeSet.tDateTime))
        //                            return ((DateTime)obj1).CompareTo((DateTime)obj2) == 0;
        //                        else
        //                            if ((type1 == _typeSet.tDecimal) && (type2 == _typeSet.tDecimal))
        //                                return Math.Abs((Decimal)obj1 - (Decimal)obj2) < (Decimal)ConstValues.minPositive;
        //                            else
        //                                if ((type1 == _typeSet.tDBNull) && (type2 == _typeSet.tDBNull))
        //                                    return true;
        //        }
        //    }
        //    return false;
        //}

        //public static bool isNumber(object obj)
        //{
        //    if (obj != null)
        //    {
        //        Type type = obj.GetType();
        //        if (
        //            (type == ToolTypeSet.helper.tShort) ||
        //            (type == ToolTypeSet.helper.tInt) ||
        //            (type == ToolTypeSet.helper.tFloat) ||
        //            (type == ToolTypeSet.helper.tDouble) ||
        //            (type == ToolTypeSet.helper.tDecimal)
        //            )
        //            return true;
        //    }
        //    return false;
        //}


        static ToolTypeSet _typeSet = ToolTypeSet.helper; //ch20120811 //new ToolTypeSet();


        public static bool isEqual(object val1, object val3)
        {



            if ((val1 != null) && (val3 != null))
            {
                object obj1 = val1;
                object obj3 = val3;

                Type type1 = obj1.GetType();
                Type type3 = obj3.GetType();

                if ((type1 == _typeSet.tDBNull) && (type3 == _typeSet.tDBNull))
                    return true;

                if ((type1 == _typeSet.tDBNull) || (type3 == _typeSet.tDBNull))
                    return false;

                if ((type1 == _typeSet.tString) && (type3 == _typeSet.tString))
                    return ((string)obj1) == ((string)obj3);

                if (isNumber(type1) && isNumber(type3))
                {
                    obj1 = Convert.ToDouble(obj1);
                    type1 = _typeSet.tDouble;

                    obj3 = Convert.ToDouble(obj3);
                    type3 = _typeSet.tDouble;


                    if ((type1 == _typeSet.tDouble) && (type3 == _typeSet.tDouble))
                        return Math.Abs((double)obj1 - (double)obj3) < ConstValues.minPositive;
                }
                else
                    if (type1.IsEnum || type3.IsEnum)
                    {
                        obj1 = Convert.ToInt32(obj1);
                        type1 = _typeSet.tInt;

                        obj3 = Convert.ToInt32(obj3);
                        type3 = _typeSet.tInt;

                        if ((type1 == _typeSet.tInt) && (type3 == _typeSet.tInt))
                            return ((int)obj1) == ((int)obj3);
                    }

                if ((type1 == _typeSet.tDateTime) && (type3 == _typeSet.tDateTime))
                    return ((DateTime)obj1).CompareTo((DateTime)obj3) == 0;
                else
                    if ((type1 == _typeSet.tDBNull) && (type3 == _typeSet.tDBNull))
                        return true;

                if ((type1 == _typeSet.tBool) && (type3 == _typeSet.tBool))
                    return (((bool)obj1) == ((bool)obj3));

                if ((type1.IsArray && type3.IsArray) && (type1 == type3) && (type1 == ToolTypeSet.helper.tByteArr))
                {
                    return ToolArray.isEqual(obj1 as byte[], obj3 as byte[]);
                }
            }
            return false;
        }

        public static bool isNumber(object obj)
        {


            if (obj != null)
                return isNumber(obj.GetType());
            return false;
        }
        public static bool isNumber(Type type)
        {

            //!!! 


            if (
                (type == _typeSet.tShort) ||
                (type == _typeSet.tInt) ||
                (type == _typeSet.tDouble) ||
                (type == _typeSet.tLong) ||
                (type == _typeSet.tByte) ||
                (type == _typeSet.tDecimal) ||
                (type == _typeSet.tFloat))

                return true;


            if (
              (type == typeof(Java.Lang.Short)) ||
              (type == typeof(Java.Lang.Integer)) ||
              (type == typeof(Java.Lang.Double)) ||
              (type == typeof(Java.Lang.Long)) ||
              (type == typeof(Java.Lang.Byte)) ||
                //  (type == _typeSet.tDecimal) ||
              (type == typeof(Java.Lang.Float)))

                return true;

            return false;

        }

        public static int intoRange(int val, int i1, int i2)
        {
            if (val < i1 || val > i2)
                return i1;
            return val;
        }
        public static bool isString(object obj)
        {
            if (obj != null)
            {
                Type type = obj.GetType();
                if ((type == typeof(string)))
                    return true;
            }
            return false;
        }
        public static bool isDateTime(object obj)
        {
            if (obj != null)
            {
                Type type = obj.GetType();
                if ((type == typeof(DateTime)))
                    return true;
            }
            return false;
        }

        public static object getTypeDefaulValue(Type type)
        {
            object val;
            if (type == typeof(Int32))
                val = 0;
            else
                if (type == typeof(Int16))
                    val = 0;
                else
                    if (type == typeof(double))
                        val = 0;
                    else
                        if (type == typeof(string))
                            val = String.Empty;
                        else
                            if (type == typeof(DateTime))
                                val = DateTime.MinValue;
                            else
                                if (type == typeof(float))
                                    val = 0;
                                else
                                    if (type == typeof(Decimal))
                                        val = 0;
                                    else
                                        val = DBNull.Value;
            return val;
        }


        public static bool isTypeDefaulValue(Type type, object val)
        {
            return isEqual(val, getTypeDefaulValue(type));
        }

        public static Array arrayJoin(Array a1, Array a2)
        {
            object[] arr = new object[a1.Length + a2.Length];
            a1.CopyTo(arr, 0);
            a2.CopyTo(arr, a1.Length);
            return arr;
        }

        public static Type parse(string t)
        {
            switch (t)
            {
                case "string":
                    return _typeSet.tString;
                case "int":
                    return _typeSet.tInt;
                case "short":
                    return _typeSet.tShort;
                case "double":
                case "float":
                    return _typeSet.tDouble;
                case "date":
                    return _typeSet.tDateTime;
                case "array":
                    return _typeSet.tArray;
            }
            return null;
        }
        public static string format(Type t)
        {
            if (t == _typeSet.tString)
                return "string";
            else
                if (t == _typeSet.tInt)
                    return "int";
                else
                    if (t == _typeSet.tShort)
                        return "short";
                    else
                        if (t == _typeSet.tDouble || t == _typeSet.tFloat)
                            return "double";
                        else
                            if (t == _typeSet.tDateTime)
                                return "date";
                            else
                                if (_typeSet.tArray.IsAssignableFrom(t))
                                    return "array";

            return string.Empty;
        }

        public static object[] parse(string[] arrVals, string[] arrType)
        {
            XmlFormating f = new XmlFormating();
            object[] arr = new object[arrType.Length];
            for (int i = 0; i < arrType.Length; ++i)
            {
                Type type = parse(arrType[i]);
                if (arrVals.Length > i)
                    arr[i] = f.parse(arrVals[i], type);
                else
                    arr[i] = ToolType.getTypeDefaulValue(type);
            }
            return arr;
        }




    }
}
