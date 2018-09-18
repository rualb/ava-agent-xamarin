using System;
using System.Collections.Generic;
using System.Text;


using AvaExt.MyException;
using AvaExt.Common;
using System.Globalization;
using AvaExt.UI;
using System.IO;
using System.Data;
using AvaExt.MyLog;
using AvaExt.Translating.Tools;
using System.Net.Sockets;
using System.Reflection;

namespace AvaExt.MyException
{
    public class ImplHandlerException : IHandlerException
    {
        IEnvironment env;
        IHandlerLog log;
        bool showUser = true;
        public ImplHandlerException(IEnvironment pEnv, IHandlerLog pLog)
        {
            env = pEnv;
            log = pLog;
        }
        public ImplHandlerException(IEnvironment pEnv, IHandlerLog pLog, bool pShowUser)
        {
            env = pEnv;
            log = pLog;
            showUser = pShowUser;
        }
        public void setException(Exception exc)
        {
            _setException(exc, exc.Message, null, null);
        }
        public void setException(Exception exc, Action pAction)
        {
            _setException(exc, exc.Message, null, pAction);
        }
        public void setException(Exception exc, String msg)
        {
            _setException(exc, msg, null, null);
        }
        public void setException(Exception exc, String msg, object[] vars)
        {
            _setException(exc, msg, vars, null);
        }
        public void setException(Exception exc, String msg, object[] vars, Action pAction)
        {
            _setException(exc, msg, vars, pAction);
        }
        string translate(string msg)
        {
            try
            {
                return env.translate(msg);
            }
            catch { }

            return msg;
        }

        void _setException(Exception exc, String msg, object[] vars, Action pAction)
        {
            try
            {

                {
                    var agg_ = exc as AggregateException;
                    if (agg_ != null && agg_.InnerExceptions != null && agg_.InnerExceptions.Count > 0)
                    {

                        log.set(exc.ToString());

                        foreach (Exception e in agg_.InnerExceptions)
                            setException(e);

                        return;
                    }

                }

                if (msg != null && msg != string.Empty)
                {
                    string text = ToolException.unwrap(exc);


#if DEBUG
                    Console.WriteLine(exc.ToString());
#endif

                    log.set(text);
                    log.set(exc.ToString());



                    if (exc.InnerException != null)
                    {
                        log.set(exc.InnerException.ToString());

                    }
                    // log.flush();
                    if (showUser)
                        ToolMsg.show(null, text, pAction);
                }

            } 
            catch (Exception err)
            {


            }

        }





    }
}
