using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using AvaPlugin;
using System.Threading;
using System.Data;

namespace Driver
{
    public class DriverInterface
    {

        HANDLER.PluginTool _doc;
        HANDLER.PluginTool doc { get { if (_doc == null) _doc = new HANDLER.PluginTool(); return _doc; } }

        public object call(object[] pData)
        {
            object arg1 = pData.Length > 0 ? pData[0] : null;
            object arg2 = pData.Length > 1 ? pData[1] : null;
            //object arg3 = pData.Length > 2 ? pData[2] : null;
            //object arg4 = pData.Length > 3 ? pData[3] : null;
            //object arg5 = pData.Length > 4 ? pData[4] : null;

            string cmd_ = arg1 as string;

            switch (cmd_)
            {
                case "_activity":
                    doc._activity = arg2 as WaitCallback;
                    break;
                case "_dataSet":
                    doc._dataSet = arg2 as DataSet;
                    break;
                case "_beginDoc":
                    doc.beginDoc();
                    break;
                case "_saveDoc":
                    doc.changeDoc();
                    break;
                case "_exc":
                    return doc._exception;
                case "_desc":
                    return doc._desc;
                case "_return":
                    return doc._return = arg2;
                case "_print":
                    return null;
                case "_exception":
                    return doc._exception;
            }

            return null;
        }

    }

}