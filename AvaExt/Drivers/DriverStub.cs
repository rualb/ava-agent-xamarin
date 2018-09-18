using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using AvaExt.Common;
using System.Reflection;
using AvaExt.Translating.Tools;
using AvaExt.MyException;

namespace AvaExt.Drivers
{
    public class DriverStub
    {
        object _changer = null;
        MethodInfo mi = null;

        //  static Dictionary<string, Assembly> _plugins = new Dictionary<string, Assembly>();
        IEnvironment environment { get { return ToolMobile.getEnvironment(); } }
        public DriverStub()
        {
            if (_changer == null)
                _changer = new Driver.DriverInterface(); //for performance
        }
        public DriverStub(string pName)
            : this(ToolMobile.getAsmRootPath(), pName)
        {

        }
        public DriverStub(string pDir, string pName)
        {
            // environment = pEnv;
            string fullName = Path.Combine(pDir, pName);// Path.Combine(Path.Combine(ToolMobile.curDir(), "driver"), Path.Combine(pDir, pName));
            initChanger(fullName);
        }

        public static DriverStub createInstanse(IEnvironment pEnv)
        {
            try
            {
                return new DriverStub();
            }
            catch (Exception exc)
            {
                pEnv.getExceptionHandler().setException(exc);
            }

            return null;
        }
        public static DriverStub createInstanse(IEnvironment pEnv, string pDir, string pName)
        {
            try
            {
                return new DriverStub(pDir, pName);
            }
            catch (Exception exc)
            {
                pEnv.getExceptionHandler().setException(exc);
            }

            return null;
        }

        public object call(object[] pData)
        {

            if (mi == null)
                mi = _changer.GetType().GetMethod("call", new Type[] { typeof(object[]) });

            if (mi == null)
                throw new MyExceptionError("Cant find driver method [object call(object[])]");

            object res_ = mi.Invoke(_changer, new object[] { pData });
            return res_;


        }



        void initChanger(string file)
        {


            //Assembly _plugin = null;

            //if (!_plugins.ContainsKey(file))
            //{
            //    if (ToolMobile.existsFile(file))
            //    {
            //        _plugins[file] = Assembly.LoadFrom(file);
            //    }

            //}

            //if (_plugins.ContainsKey(file))
            //    _plugin = _plugins[file];

            //if (_plugin == null)
            //    throw new MyException.MyExceptionError(MessageCollection.T_MSG_ERROR_INVALID_VAR, new object[] { file });

            //_changer = _plugin.CreateInstance("Driver.DriverInterface");
            //if (_changer == null)
            //    throw new MyException.MyExceptionError(MessageCollection.T_MSG_ERROR_INVALID_VAR, new object[] { "Driver.DriverInterface" });

        }



    }
}
