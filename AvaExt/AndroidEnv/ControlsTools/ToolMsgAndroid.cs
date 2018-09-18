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
using AvaExt.AndroidEnv.ApplicationBase;
using AvaExt.Common;
using AvaExt.Translating.Tools;

namespace AvaExt.AndroidEnv.ControlsTools
{
    public class ToolMsgAndroid
    {
        static ProgressDialog progressDialog;


        static string translate(string msg)
        {
            try
            {
                if (ToolMobile.isEnvInited()) return ToolMobile.getEnvironment().translate(msg);
            }
            catch { }

            return msg;
        }


        static Context _getContext(Context pContext)
        {
            if (pContext == null)
                pContext = ToolMobile.getContextLast();


            //if (pContext == null)
            //    pContext = ToolMobile.getContext();

            return pContext;
        }

        static AlertDialog.Builder create(Context pContext, string pMsg, string pTitle, string[] pItems, EventHandler<DialogClickEventArgs> pHandler)
        {
            pContext = _getContext(pContext);





            AlertDialog.Builder alert = new AlertDialog.Builder(pContext);
            // alert.SetView(LayoutInflater.Inflate(Resource.Layout.msgbox, null));
            alert.SetCancelable(false);
            if (pMsg != null)
                alert.SetMessage(translate(pMsg));


            if (pTitle != null)
                alert.SetTitle(translate(pTitle));
            if (pItems != null && pItems.Length > 0 && pHandler != null)
                alert.SetItems(pItems, pHandler);

            return alert;
        }

        public static void info(Context pContext, string pMsg, string pTitle, Action pHandler)
        {

            AlertDialog.Builder alert = create(pContext, pMsg, pTitle, null, null);
            alert.SetPositiveButton(translate(WordCollection.T_OK), (x, y) =>
            {
                if (pHandler != null) pHandler.Invoke();
            });
            AlertDialog dialog = alert.Create();
            dialog.Show();
        }

        public static void confirm(Context pContext, string pMsg, string pTitle, Action pHandlerPos, Action pHandlerNeg)
        {

            AlertDialog.Builder alert = create(pContext, pMsg, pTitle, null, null);
            alert.SetPositiveButton(translate(WordCollection.T_OK), (x, y) =>
            {
                if (pHandlerPos != null) pHandlerPos.Invoke();
            });
            alert.SetNegativeButton(translate(WordCollection.T_CANCEL), (x, y) =>
            {
                if (pHandlerNeg != null) pHandlerNeg.Invoke();
            });

            AlertDialog dialog = alert.Create();
            dialog.Show();

        }

        public static void askText(Context pContext, string pText, string pTitle, Action<string> pHandler)
        {

            pContext = _getContext(pContext);

            AlertDialog.Builder alert = new AlertDialog.Builder(pContext);
            alert.SetCancelable(false);

            var input = new EditText(pContext);
            input.Text = pText ?? "";


            if (input != null)
                alert.SetView(input);

            if (pTitle != null)
                alert.SetTitle(translate(pTitle));

            alert.SetPositiveButton(translate(WordCollection.T_OK), (x, y) =>
            {
                if (pHandler != null)
                    pHandler.Invoke(input.Text);
            });

            alert.SetNegativeButton(translate(WordCollection.T_CANCEL), (x, y) =>
            {

            });

            AlertDialog dialog = alert.Create();
            dialog.Show();

        }

        public static void askList(Context pContext, string pMsg, string pTitle, string[] pList, EventHandler<DialogClickEventArgs> pHandler)
        {

            AlertDialog.Builder alert = create(pContext, pMsg, pTitle, pList, pHandler);

            alert.SetNegativeButton(translate(WordCollection.T_CANCEL), (x, y) =>
            {

            });

            AlertDialog dialog = alert.Create();
            dialog.Show();

        }


        public static void progressStart(Context pContext, string pMsg, string pTitle)
        {
            progressStop();

            progressDialog = new ProgressDialog(_getContext(pContext));
            progressDialog.SetCancelable(false);
            if (pMsg != null)
                progressDialog.SetMessage(translate(pMsg));
            if (pTitle != null)
                progressDialog.SetTitle(translate(pTitle));

            progressDialog.Show();

        }

        public static void progressStop()
        {
            if (progressDialog != null)
                progressDialog.Dismiss();
            progressDialog = null;
        }
    }
}