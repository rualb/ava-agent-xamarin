using AvaExt.Common;
using AvaExt.Translating.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;



namespace AvaExt.MyException
{
    public class ToolException
    {
        public static string unwrap(Exception exc)
        {

            while (exc.InnerException != null)
            {
                exc = exc.InnerException;
            }

            string text = null;
            Type t = exc.GetType();

            if (t == typeof(TimeoutException))
                text = translate(MessageCollection.T_MSG_ERROR_TIMEOUT);
            else
                if (t == typeof(NullReferenceException))
                    text = translate(MessageCollection.T_MSG_ERROR_INNER);
                else
                    if (t == typeof(System.IO.IOException) || t == typeof(Java.IO.IOException))
                        text = translate(MessageCollection.T_MSG_ERROR_IO);
                    else
                        if (t == typeof(DBConcurrencyException))
                            text = translate(MessageCollection.T_MSG_ERROR_CONCURRENCY);
                        else
                            if (t == typeof(System.Net.Sockets.SocketException) || t == typeof(Java.Net.SocketException))
                                text = translate(MessageCollection.T_MSG_ERROR_CONNECTION);
                            else
                                if (typeof(MyBaseException).IsAssignableFrom(t))
                                    text = translate(exc.Message);
                                else
                                    if (t == typeof(TargetInvocationException) && exc.InnerException != null)
                                        text = translate(exc.InnerException.Message);
                                    else
                                        text = translate(exc.Message);

            return text;

        }

        private static string translate(string txt)
        {
            return ToolMobile.getEnvironment().translate(txt);
        }

    }
}