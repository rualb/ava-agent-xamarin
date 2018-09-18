using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using AvaExt.AndroidEnv.ControlsTools;
using Android.Content;
using Android.App;

namespace AvaExt.Common
{
    public class ToolMsg
    {
        
        public static void show(Context pContext, string msg, Action pAction)
        {
            ToolMsgAndroid.info(pContext, msg, ToolMobile.Name, pAction);
        }
        //public static void show(string msg, string[] arrData)
        //{
        //    show(msg, arrData, null);
        //}

        //public static void show(string msg, string[] arrData, Action pAction)
        //{
        //    show(msg + (arrData != null && arrData.Length > 0 ? ToolString.joinList(arrData) : string.Empty), (Action)null);
        //}
        public static void confirm(Context pContext, string msg, Action pHandlerPos, Action pHandlerNeg)
        {
            ToolMsgAndroid.confirm(pContext, msg, ToolMobile.Name, pHandlerPos, pHandlerNeg);
            //return (MessageBox.Show(env.translate(msg), ToolMobile.getName(), MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.OK);
        }
        public static void askList(Context pContext, string[] pList, EventHandler<DialogClickEventArgs> pHandler)
        {
            ToolMsgAndroid.askList(pContext, null, ToolMobile.Name, pList, pHandler);
        }

        public static void askText(Context pContext, string pText, string pTitle, Action<string> pHandler)
        {
            ToolMsgAndroid.askText(pContext, pText??"", pTitle ?? ToolMobile.Name,  pHandler);
        }
        //public static void warn(IEnvironment env, string msg)
        //{
        //    show(null,msg,null);
        //}

        public static void progressStart(Context pContext, string pMsg,Action pRun)
        {
            try
            {
                progressStart(pContext, pMsg);
                pRun.Invoke();
            }
            finally
            {
                progressStop();
            }
        }
        public static void progressStart(Context pContext, string pMsg)
        {
            ToolMsgAndroid.progressStart(pContext, pMsg, ToolMobile.Name);
        }

        public static void progressStop()
        {
            ToolMsgAndroid.progressStop();
        }
    }
}
