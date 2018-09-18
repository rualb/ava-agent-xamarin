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

namespace AvaAgent.FormMain
{
    [Activity(Label = "ActivityTest")]
    public class ActivityTest : Activity
    {


        static int count = 1;


        Button cBtnClose
        {
            get
            {
                return FindViewById<Button>(Resource.Id.cBtnClose);
            }
        }

        Button cBtnTest1
        {
            get
            {
                return FindViewById<Button>(Resource.Id.cBtnTest1);
            }
        }


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);


            SetContentView(Resource.Layout.test);


            ++count;

            cBtnTest1.Text = "Open 1Activity " + (count);
            

            cBtnTest1.Click += cBtnTest1_Click;


            cBtnClose.Click += btn_Click;

            // Create your application here
        }

        void cBtnTest1_Click(object sender, EventArgs e)
        {
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);


            var wrap = new Intent(this, typeof(ActivityTest));


            //wrap.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
            //  wrap.SetFlags(ActivityFlags.NoHistory);

            StartActivity(wrap);
        }


        void btn_Click(object sender, EventArgs e)
        {
            Finish();
        }


        protected override void OnDestroy()
        {


            cBtnTest1.Click -= cBtnTest1_Click;



            base.OnDestroy();
        }
    }
}