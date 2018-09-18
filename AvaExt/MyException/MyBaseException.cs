

using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;

namespace AvaExt.MyException
{
     public class MyBaseException : Exception
    {
        public object[] _args;
        public MyBaseException(string msg)
            : base(msg)
        { }
        public MyBaseException(string msg, Exception innerExc)
            : base(msg, innerExc)
        { }
        public MyBaseException(string msg, object[] args)
            : base(msg)
        { _args = args; }
        public MyBaseException(string msg, object[] args, Exception innerExc)
            : base(msg, innerExc)
        { _args = args; }

        public override string Message
        {
            get
            {
                return base.Message + (_args != null ? (" [" + ToolArray.join(_args) + "]") : string.Empty);
            }
        }
    }
}

