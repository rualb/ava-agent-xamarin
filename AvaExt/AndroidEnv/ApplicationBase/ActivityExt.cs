using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AvaExt.Common;
using Android.Util;
using AvaAgent;


namespace AvaExt.AndroidEnv.ApplicationBase
{

    public class ActivityExt : Activity
    {


        public static string errMessageOnResume = null;


        List<Action> listActionOnResume = new List<Action>();

        protected void addActionOnResume(Action pAction)
        {
            listActionOnResume.Add(pAction);
        }

        protected void runActionOnResume()
        {

            try
            {
                var arr = listActionOnResume.ToArray();

                listActionOnResume.Clear();

                foreach (var x in arr)
                    x.Invoke();
            }
            catch (Exception exc)
            {
                environment.getExceptionHandler().setException(exc);
            }

        }

        static ActivityExt()
        {


            System.Threading.Thread.CurrentThread.CurrentCulture =
                System.Threading.Thread.CurrentThread.CurrentUICulture =
                System.Globalization.CultureInfo.InvariantCulture;
        }

        //////////////////////////////////////////////////////////////////////////////
        static List<ActivityExt> listActivityExt = new List<ActivityExt>();
        protected static void addActivityExt(ActivityExt act)
        {
            lock (listActivityExt)
            {
                listActivityExt.Remove(act);
                listActivityExt.Add(act);
            }

        }
        protected static void removeActivityExt(ActivityExt act)
        {
            lock (listActivityExt)
            {
                listActivityExt.Remove(act);

            }

        }
        public static ActivityExt getActivityExtLast()
        {

            lock (listActivityExt)
            {
                if (listActivityExt.Count > 0)
                {
                    var x = listActivityExt[listActivityExt.Count - 1];
                    if (x == null || x.IsFinishing)
                    {
                        removeActivityExt(x);
                        return getActivityExtLast();
                    }
                    else
                        return x;
                }
                return null;
            }
        }
        protected override void OnResume()
        {
            base.OnResume();

            addActivityExt(this);

            runActionOnResume();
        }

        protected override void OnPause()
        {
            base.OnPause();

            removeActivityExt(this);
        }

        //protected override void OnStop()
        //{
        //    base.OnStop();

        //    removeActivityExt(this);
        //}

        // // ///////////////////////////////////////////////////////////////////////
        protected IEnvironment environment { get { return ToolMobile.getEnvironment(); } }

        int designId;
        //
        public event EventHandler Closed;
        public event EventHandler Creating;
        public event EventHandler Created;
        // 
        //public void setEnvironment(IEnvironment pEnv)
        //{
        //    environment = pEnv;
        //}
        //public IEnvironment getEnvironment()
        //{
        //    return environment;
        //}

        public ActivityExt(IEnvironment pEnv, int pDesignId)
        {
            designId = pDesignId;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            try
            {

               
                if (Creating != null)
                    Creating.Invoke(this, EventArgs.Empty);

                //ToolMobile.log("set form [" + this.GetType().FullName + "] layout");

                base.OnCreate(savedInstanceState);

                //   setEnvironment(ToolMobile.getEnvironment());

                if (designId >= 0)
                    SetContentView(designId == 0 ? Resource.Layout.form : designId);

                if (Created != null)
                    Created.Invoke(this, EventArgs.Empty);

                this.Window.SetSoftInputMode(SoftInput.StateAlwaysHidden);
                //   (WindowManager.LayoutParams.SOFT_INPUT_STATE_ALWAYS_HIDDEN);
            }
            catch (Exception exc)
            {
                ToolMobile.setException(exc);
            }

        }

        protected override void OnDestroy()
        {
           // ToolMobile.log("form [" + this.GetType().FullName + "] destroy");

            base.OnDestroy();

            if (Closed != null)
                Closed.Invoke(this, EventArgs.Empty);


            if (listActionOnResume != null)
                listActionOnResume.Clear();

            listActionOnResume = null;

           
            Closed = null;
            Creating = null;
            Created = null;


        }


        public string Text
        {
            get { return Title; }
            set { Title = value; }
        }




    }
}