using System;
using System.Collections.Generic;
using System.Text;

namespace AvaExt.Common
{
    public class ToolTypeSet
    {
        public static ToolTypeSet helper = new ToolTypeSet();
        public Type tLRef;
        public Type tByte;
        public Type tShort;
        public Type tInt;
        public Type tLong;
        public Type tDouble;
        public Type tString;
        public Type tDateTime;
        public Type tBool;
        public Type tDBNull;
        public Type tDecimal;
        public Type tFloat;
        public Type tType;
        public Type tObject;
        public Type tArray;
        public Type tByteArr;
        public ToolTypeSet()
        {
            tByte = typeof(byte);
            tShort = typeof(short);
            tInt = typeof(int);
            tLong = typeof(long);
            tDouble = typeof(double);
            tString = typeof(string);
            tDateTime = typeof(DateTime);
            tBool = typeof(bool);
            tDBNull = DBNull.Value.GetType();
            tDecimal = typeof(decimal);
            tType = typeof(Type);
            tFloat = typeof(float);
            tObject = typeof(object);
            tArray = typeof(Array);
            tByteArr  = typeof(byte[]);

            tLRef = tString;
        }
    }
}
