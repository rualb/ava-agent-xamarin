using System;
using System.Collections.Generic;
using System.Text;


using AvaExt.MyException;
using AvaExt.Common;
using System.Globalization;
using AvaExt.UI;
using System.IO;
using System.Data;

namespace AvaExt.MyException
{
    public interface IHandlerException
    {
        void setException(Exception exc, Action pAction);
        void setException(Exception exc);
        void setException(Exception exc, String msg);
        void setException(Exception exc, String msg, object[] vars);
        void setException(Exception exc, String msg, object[] vars, Action pAction);
    }
}
