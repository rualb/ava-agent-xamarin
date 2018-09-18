using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using System.IO;
using AvaExt.Formating;

namespace AvaExt.MyLog
{
    public class ImplHandlerLog : IHandlerLog
    {
        string _dir = "log";

        //IEnvironment env;

        public ImplHandlerLog(IEnvironment pEnv)
        {
           // env = pEnv;
            try
            {

                if (!ToolMobile.existsDir(_dir))
                    ToolMobile.createDir(_dir);

                string[] arr = ToolMobile.getFiles(_dir);
                if (arr.Length > 30)
                {
                    List<string> files = new List<string>(arr);
                    files.Sort();
                    for (int i = 0; i < files.Count - 5; ++i)
                        ToolMobile.deleteFile(_dir, files[i]);
                }

            }
            catch //(Exception exc)
            {

            }
        }
        public virtual void set(string text, object[] data)
        {
            set(text);
        }
        public virtual void set(string text)
        {
            try
            {

                if (!ToolMobile.existsDir(_dir))
                    ToolMobile.createDir(_dir);


                var indx = XmlFormating.helper.format(DateTime.Now);

                text = "[EVENT::" + indx + "]\n" + text + "\n";


                string logFile = string.Format("LOG-{0}.txt", ToolString.left(indx, 10));
                ToolMobile.appendFileText(_dir, logFile, text);
            }
            catch (Exception exc)
            {


            }
        }





        public virtual void error(string text)
        {
            set(text);
        }
    }
}
