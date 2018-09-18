using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using System.IO;
using System.Threading;
using Android.Content;
using AvaExt.MyLog;
using AvaExt.MyEvents;


namespace AvaGE.MyLog
{
    public class ImplUserHandlerLog : IUserHandlerLog
    {


        public event EventHandler<EventArgsString> NewMessage;
        public event EventHandler Hide;
        public event EventHandler<EventArgsString> Error;

        public Action exceuteInContext { get; set; }

        public static readonly ImplUserHandlerLog instance = new ImplUserHandlerLog();

        //Thread _thread;

        protected IEnvironment _env { get { return ToolMobile.getEnvironment(); } set { } }
        delegate void setMsg(string msg);
        delegate void done();

        private ImplUserHandlerLog()
        {


        }

        public void error(string text)
        {

      
            try
            {
                if (Error != null) Error.Invoke(this, new EventArgsString(_env.translate(text)));

            }
            catch (Exception exc)
            {
                ToolMobile.setRuntimeMsg(exc.ToString());
            }
        }

        public void set(string text)
        {
      

            try
            {
                if (NewMessage != null) NewMessage.Invoke(this, new EventArgsString(_env.translate(text)));

            }
            catch (Exception exc)
            {
                ToolMobile.setRuntimeMsg(exc.ToString());
            }
        }
        public void set(string text, object[] data)
        {
       
            try
            {
                set(_env.translate(text) + ":" + ToolString.joinList(ToolString.toString(data)));
            }
            catch (Exception exc)
            {
                ToolMobile.setRuntimeMsg(exc.ToString());
            }
        }



        public void show()
        {
            

            try
            {

                ToolMobile.startForm(typeof(FormUserHandlerLog));

            }
            catch (Exception exc)
            {
                ToolMobile.setRuntimeMsg(exc.ToString());
            }
        }

        public void hide()
        {
           
            try
            {
                if (Hide != null) Hide.Invoke(this, EventArgs.Empty);
            }
            catch (Exception exc)
            {
                ToolMobile.setRuntimeMsg(exc.ToString());
            }
        }


        public void flush()
        {

        }





    }
}
