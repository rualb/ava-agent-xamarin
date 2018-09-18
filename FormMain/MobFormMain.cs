using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using AvaExt.AndroidEnv.ApplicationBase;
using AvaExt.AndroidEnv.ControlsBase;
using AvaExt.Common;
using AvaExt.Settings;
using AvaAgent.Common;
using AvaExt.FileSystem;
using System.IO;
using AvaExt.PagedSource;
using AvaExt.Manual.Table;
using AvaExt.AndroidEnv;
using AvaAgent.FormMain;
using AvaGE.Common;
using AvaExt.Translating.Tools;
using AvaExt.SQL;
using System.Collections.Generic;
using AvaGE.MobControl;


namespace AvaAgent
{
    [Activity(Label = ToolMobile.Name, MainLauncher = true, Icon = Form.FORM_ICON,
        ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait,
        WindowSoftInputMode = Android.Views.SoftInput.StateAlwaysHidden,
        Name = "com.avatez.avaagent.v2.MobFormMain")]
    public class MobFormMain : MobFormBase
    {

        //public class MobFormMainService : Service
        //{


        //}




        MobButton cBtnMenu { get { return FindViewById<MobButton>(Resource.Id.cBtnMenu); } }
     //   MobTreeView cTree { get { return FindViewById<MobTreeView>(Resource.Id.cTree); } }
        LinearLayout cPanelMenu { get { return FindViewById<LinearLayout>(Resource.Id.cPanelMenu); } }

        public MobFormMain()
            : base(null, Resource.Layout.MobFormMain)
        {



            Closed += MobFormMain_Closed;
            Creating += MobFormMain_Creating;
            Created += MobFormMain_Created;



            //manualInitCall = CurrentVersion.ENV.needNsInit();






        }


        protected override void OnResume()
        {
            base.OnResume();


            if (inited)
            {
                var firmName_ = environment.getSysSettings().getString(SettingsSysMob.MOB_SYS_FIRMNAME) ?? "";
                var agentId_ = environment.getSysSettings().getString(SettingsSysMob.MOB_SYS_AGENT_ID) ?? "";

                firmName_ = ToolString.left(firmName_, 15);

                var newLabel_ = string.Format(
                    "{0} - {1} ({2})",
                    ToolMobile.Name,
                    string.IsNullOrEmpty(firmName_) ? "*" : firmName_,
                    string.IsNullOrEmpty(agentId_) ? "000" : agentId_
                    );

                var oldLabel_ = this.Title;

                if (oldLabel_ != newLabel_)
                    this.Title = newLabel_;


            }


            ///////////////////////////////////////////////////////////////////////////////////////////

            //var _nfcAdapter = Android.Nfc.NfcAdapter.DefaultAdapter;

            //// Create an intent filter for when an NFC tag is discovered.  When
            //// the NFC tag is discovered, Android will u
            //var tagDetected = new IntentFilter(Android.Nfc.NfcAdapter.ActionTagDiscovered);
            //var filters = new[] { tagDetected };

            //// When an NFC tag is detected, Android will use the PendingIntent to come back to this activity.
            //// The OnNewIntent method will invoked by Android.
            //var intent = new Intent(this, GetType()).AddFlags(ActivityFlags.SingleTop);
            //var pendingIntent = PendingIntent.GetActivity(this, 0, intent, 0);


            //_nfcAdapter.EnableForegroundDispatch(this, pendingIntent, filters, null);


            if (errMessageOnResume != null)
            {

                var err = errMessageOnResume;
                errMessageOnResume = null;

                ToolMsg.show(this, err, null);

            }
        }
        ///////////////////////////////////////////////////////////////////////////////////////////

        #region Start

        void MobFormMain_Creating(object sender, EventArgs e)
        {

            addActivityExt(this);//dontwait resume


            //ToolMobile.setContext(this);


            //  MobEnvironment.startEnv();
            //


            if (inited)
            {
                if (ToolMobile.getEnvironment() == null)
                    ToolMsg.show(this, "Cant start", delegate() { Close(); });

            }


              

        }

        void MobFormMain_Created(object sender, EventArgs e)
        {

            //if (CurrentVersion.ENV.needNsInit())
            //{

            //    CurrentVersion.ENV.initNs((o, x) =>
            //    {

            //        this.Recreate();

            //    });


            //}




            

        }



        //protected override void OnNewIntent(Intent intent)
        //{
        //    base.OnNewIntent(intent);

        //    if (intent.Action == Android.Nfc.NfcAdapter.ActionTagDiscovered)
        //    {
        //        var tag = intent.GetParcelableExtra(Android.Nfc.NfcAdapter.ExtraTag) as Android.Nfc.Tag;

        //        byte[] id_ = tag.GetId();

        //        string idStr_ = ToolString.toHex(id_);

        //        Toast.MakeText(this, idStr_, ToastLength.Long).Show();
        //    }

        //}


        void MobFormMain_Closed(object sender, EventArgs e)
        {
            ToolMobile.setEnvironment(null);
            //System.Environment.Exit(0);
        }



        enum ContexMenuCmd : int
        {

            changeAgent = 1,
            export = 2,
            import = 3,
            test = 4

        }
        public override void OnCreateContextMenu(IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
        {
            base.OnCreateContextMenu(menu, v, menuInfo);




            {

                string val_ = CurrentVersion.ENV.getEnvString(CurrentVersion.ENV.NSLIST, string.Empty);
                if (!string.IsNullOrEmpty(val_))
                {
                    menu.Add(0, (int)ContexMenuCmd.changeAgent, 0, translate(WordCollection.T_AGENT));

                }

            }

            {

                menu.Add(0, (int)ContexMenuCmd.export, 0, translate(WordCollection.T_EXPORT));

            }

            {
                menu.Add(0, (int)ContexMenuCmd.import, 0, translate(WordCollection.T_IMPORT));
            }


            //{
            //    menu.Add(0, (int)ContexMenuCmd.test, 0, "Test");
            //}


        }




        public override bool OnContextItemSelected(IMenuItem item)
        {

            doCmd((ContexMenuCmd)item.ItemId);

            return base.OnContextItemSelected(item);
        }

        void doCmd(ContexMenuCmd pCmd)
        {
            try
            {
                switch (pCmd)
                {
                    case ContexMenuCmd.changeAgent:

                        CurrentVersion.ENV.initNs((o, e) =>
                        {
                            ToolMobile.restartEnvironment();
                            this.Recreate();
                        });

                        break;
                    case ContexMenuCmd.export:
                        environment.toActivity("tool.data.export", null).done();
                        break;
                    case ContexMenuCmd.import:
                        environment.toActivity("tool.data.import", null).done();
                        break;
                    case ContexMenuCmd.test:
                        {

                            StartActivity(typeof(ActivityTest));

                        }
                        break;
                }


            }
            catch (Exception exc)
            {

                ToolMobile.setException(exc);
            }
        }


        public override bool OnKeyUp(Keycode keyCode, KeyEvent e)
        {

            if (keyCode == Keycode.Menu)
            {
               // this.OpenContextMenu(cTree);
                this.OpenContextMenu(cPanelMenu);
                
                return true;

            }

            return base.OnKeyUp(keyCode, e);
        }



        protected override void initAfterSettings()
        {
            base.initAfterSettings();


            cBtnMenu.Click += cBtnMenu_Click;

           // RegisterForContextMenu(cTree);
            RegisterForContextMenu(cPanelMenu);

            

            {


                this.settings.enumarate("cTree");

                var listMenuItems = new List<string>();
                if (this.settings.isEnumerValid())
                {
                    var structMenu_ = this.settings.forkEnumer();
                    structMenu_.enumarate();


                    while (structMenu_.moveNext())
                    {
                        var id = structMenu_.getNameEnumer();

                        listMenuItems.Add(id);
                    }

                }
                cPanelMenu.RemoveAllViews();

                foreach (var id in listMenuItems)
                {
                    

                    if (this.settings.enumarate(id))
                    {

                        var itemCmd = this.settings.getStringAttrEnumer("Cmd");
                        var itemText = this.settings.getStringAttrEnumer("Text");
                        itemText = environment.translate(itemText);

                       var v = this.LayoutInflater.Inflate(Resource.Layout.MobMenuItem, cPanelMenu, false);
                     MobButton mobBtn=  v.FindViewById<MobButton>(Resource.Id.cBtnDo);



                     var btn = mobBtn; // new MobButton(this);
                        btn.Text = (itemText);

                        if (itemCmd != null && itemCmd != "")
                            btn.activity = environment.toActivity(itemCmd, null);

                        cPanelMenu.AddView(btn);


                    }

                }




            }

        }


        void cBtnMenu_Click(object sender, EventArgs e)
        {


            this.OpenContextMenu(cPanelMenu);

            //this.OpenContextMenu(cTree);
        }

        #endregion

        ///////////////////////////////////////////////////////////////////////////////////////////



        protected override string globalStoreName()
        {
            return "form.main";
        }


        protected override bool userCancelAllow()
        {


            ToolMsg.confirm(this, MessageCollection.T_MSG_COMMIT_EXIT, () =>
            {

                this.userCancelDone();

            }, null);


            return false;
        }


        public override void Close()
        {
            try
            {
                base.Close();
            }
            finally
            {

                System.Environment.Exit(0);
            }
        }

    }


}

