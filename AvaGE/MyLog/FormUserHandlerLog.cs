using Android.App;
using Android.Widget;
using AvaAgent;
using AvaExt.AndroidEnv.ControlsBase;
using AvaExt.Common;
using AvaExt.MyEvents;
using AvaExt.MyLog;
using AvaGE.Common;
using AvaGE.MobControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;


namespace AvaGE.MyLog
{
    /// <summary>
    /// form show log lines
    /// </summary>
    [Activity(Label = Form.FORM_NAME, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, WindowSoftInputMode = Android.Views.SoftInput.StateAlwaysHidden)]
    public class FormUserHandlerLog : MobFormBase
    {
        IUserHandlerLog handlerLog { get { return ToolMobile.getEnvironment().getUserHandlerLog(); } }
        MobLabel cData { get { return FindViewById<MobLabel>(Resource.Id.cData); } }
        MobButton cBtnCancel { get { return FindViewById<MobButton>(Resource.Id.cBtnCancel); } }

        bool error = false;
        bool canClose = true;

        protected override string globalStoreName()
        {
            return "tool.log";
        }

        public FormUserHandlerLog()
            : base(null, Resource.Layout.FormUserHandlerLog)
        {

            Created += FormUserHandlerLog_Created;
            Closed += FormUserHandlerLog_Closed;
        }

        void FormUserHandlerLog_Closed(object sender, EventArgs e)
        {
            unbind();
        }

        void unbind()
        {
            handlerLog.NewMessage -= instance_NewMessage;
            handlerLog.Hide -= instance_Hide;
            handlerLog.Error -= instance_Error;

        }


        void FormUserHandlerLog_Created(object sender, EventArgs e)
        {
            handlerLog.NewMessage += instance_NewMessage;
            handlerLog.Hide += instance_Hide;
            handlerLog.Error += instance_Error;

            canClose = false;
            cBtnCancel.Enabled = false;
            cBtnCancel.Click += cBtnClose_Click;
        }



        void userRequireCancel()
        {
            if (canClose)
                Close();

        }

        void cBtnClose_Click(object sender, EventArgs e)
        {
            if (canClose)
                Close();
        }

        protected override void OnStart()
        {
            base.OnStart();

            try
            {
                if (ImplUserHandlerLog.instance.exceuteInContext != null)
                    ImplUserHandlerLog.instance.exceuteInContext.Invoke();
            }
            finally
            {
                ImplUserHandlerLog.instance.exceuteInContext = null;
            }
        }


        void instance_Error(object sender, EventArgsString e)
        {
            error = true;

            addMsg(e.Data);
        }

        void instance_Hide(object sender, EventArgs e)
        {
            canClose = true;
            this.RunOnUiThread(() =>
            {
                try
                {
                    cBtnCancel.Enabled = true;

                }
                catch (Exception exc)
                {
                    ToolMobile.setRuntimeMsg(exc.ToString());
                }
            });

            if (!error)
                userRequireCancel();
        }

        void instance_NewMessage(object sender, EventArgsString e)
        {
            addMsg(e.Data);
        }


        void setMsg(string msg)
        {
            this.RunOnUiThread(() =>
            {
                try
                {
                    cData.Text = msg;
                    cData.ScrollTo(0, int.MaxValue);


                }
                catch (Exception exc)
                {
                    ToolMobile.setRuntimeMsg(exc.ToString());
                }
            });

        }
        public void addMsg(string msg)
        {
            this.RunOnUiThread(() =>
            {
                try
                {
                    if (cData.Text == string.Empty)
                        setMsg(msg);
                    else
                        cData.Text += "\r\n" + msg;

                    cData.ScrollTo(0, int.MaxValue);


                }
                catch (Exception exc)
                {
                    ToolMobile.setRuntimeMsg(exc.ToString());
                }
            });
        }


    }
}