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
using AvaAgent.Common;
using AvaExt.FileSystem;
using AvaExt.PagedSource;
using AvaExt.Settings;
using AvaExt.Manual.Table;
using System.IO;
using AvaExt.AndroidEnv.ControlsBase;
using AvaExt.AndroidEnv.ApplicationBase;


namespace AvaAgent
{
    [Application(Label = ToolMobile.Name, Icon = "@drawable/main")]
    public class AvaApplication : ApplicationExt
    {

        public AvaApplication()
            : base()
        { }
        protected AvaApplication(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        { }

        public override void OnCreate()
        {


            //some andoird version has BUGs, interhan catch required or crash app crashed
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
               // Console.WriteLine(args.ExceptionObject.GetType());
            };



            ToolMobile.start();

            base.OnCreate();



        }

        public override void OnLowMemory()
        {
            base.OnLowMemory();
        }

        public override void OnTerminate()
        {
            base.OnTerminate();
        }


        public override void startEnv()
        {
            MobEnvironment.startEnv();
        }

         
    }
}