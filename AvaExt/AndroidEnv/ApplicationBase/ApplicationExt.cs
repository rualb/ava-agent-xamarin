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
using System.Threading.Tasks;
using System.IO;


namespace AvaExt.AndroidEnv.ApplicationBase
{
    public abstract class ApplicationExt : Application
    {

        public static Application application;

        public ApplicationExt()
            : base()
        { }
        protected ApplicationExt(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        { }



        public virtual void startEnv()
        {
            application = this;
 
        }

        public virtual void stopEnv()
        {
            ToolMobile.setEnvironment(null);
        }
    }
}