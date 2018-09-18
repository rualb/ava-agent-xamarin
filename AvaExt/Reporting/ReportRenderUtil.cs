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
using Java.Interop;
using System.Text.RegularExpressions;
using System.IO;
using Android.Webkit;
using Android.Graphics;

namespace AvaExt.Reporting
{
    public class ReportRenderUtil : Java.Lang.Object, Java.IO.ISerializable
    {
        static Regex patternHex = new Regex("\\[0x[A-Z0-9][A-Z0-9]\\]", RegexOptions.Compiled);

        public ReportRenderUtil()
        {
        }
        public ReportRenderUtil(IntPtr handle, JniHandleOwnership transfer)
            : base(handle, transfer)
        {



        }


        //public override object getResult()
        //{
        //    string data = eval(_doc.DocumentElement, getDataSet());
        //    data = _environment.translate(data);
        //    StringBuilder strBuilder = new StringBuilder(data);
        //    string replace = ToolXml.getAttribValue(_doc.DocumentElement, _constAttrReplace, string.Empty);
        //    if (replace != string.Empty)
        //    {
        //        string[][] arrItems = ToolString.explodeGroupList(replace);
        //        foreach (string[] arr in arrItems)
        //            if (arr.Length == 2)
        //                strBuilder.Replace(arr[0], arr[1]);
        //    }
        //    return strBuilder.ToString();
        //    // return _enc.GetBytes(strBuilder.ToString());
        //}

        public RenderingInfo renderingInfo
        {
            get;
            set;
        }

        public string renderingData
        {
            get;
            set;
        }


        static bool isHtml(string pTxt)
        {
            return pTxt != null && pTxt.StartsWith("<html>");

        }


        public void renderTo(object pTarget)
        {
            try
            {
                if (renderingInfo == null || renderingData == null)
                    return;


                {

                    if (pTarget == null || (pTarget as string == "printer"))
                    {
                        outToPrinter();
                        return;
                    }
                }

                {
                    //var x = pTarget as TextView;
                    //if (x != null)
                    //{
                    //    outToView(x);
                    //    return;
                    //}
                    var x = pTarget as ViewGroup;
                    if (x != null)
                    {
                        outToView(x);
                        return;
                    }


                }


                {

                    if ((pTarget as string == "share"))
                    {

                        share();

                        return;
                    }
                }

            }
            catch (Exception exc)
            {

                ToolMobile.setException(exc);

            }
        }
        void share()
        {
            string text_ = renderingData;
            text_ = openSpeChar(text_);
            text_ = hideEscPosParts(text_);

            var cntxt = ToolMobile.getContextLast();

            if (cntxt == null)
                return;


            Intent sharingIntent = new Intent(Intent.ActionSend);
            // sharingIntent.SetType("text/html");
            sharingIntent.SetType("text/*");
            // sharingIntent.setType("application/*|text/*");

            var data = text_;

            sharingIntent.PutExtra(Intent.ExtraText,
                //  Android.Text.Html.FromHtml(text_) //cat tag's
                  text_
                );


            // cData.Text);  // 


            cntxt.StartActivity(Intent.CreateChooser(sharingIntent, ""));


        }

        void outToView(TextView pV)
        {
            string text_ = renderingData;
            text_ = openSpeChar(text_);
            text_ = hideEscPosParts(text_);

            if (pV != null)
            {
                pV.Text = text_;

            }

        }

        void outToView(ViewGroup pV)
        {
            string text_ = renderingData;
            text_ = openSpeChar(text_);
            text_ = hideEscPosParts(text_);

            if (pV != null)
            {
                pV.RemoveAllViews();

                if (isHtml(text_))
                {

                    var web = new WebView(pV.Context);
                    web.LayoutParameters = new LinearLayout.LayoutParams(

                        LinearLayout.LayoutParams.MatchParent,
                        LinearLayout.LayoutParams.MatchParent);


                    pV.AddView(web);


                    var html = text_;

                    web.LoadData(html, "text/html", Encoding.UTF8.EncodingName);
                }
                else
                {


                    var scroll = new ScrollView(pV.Context);
                    scroll.LayoutParameters = new LinearLayout.LayoutParams(

                        LinearLayout.LayoutParams.MatchParent,
                        LinearLayout.LayoutParams.MatchParent);


                    var view = new TextView(pV.Context);

                    view.SetTextSize(Android.Util.ComplexUnitType.Sp, 13);
                    view.SetTypeface(Typeface.Monospace, TypefaceStyle.Normal);
                    view.LayoutParameters = new LinearLayout.LayoutParams(

                       LinearLayout.LayoutParams.MatchParent,
                       LinearLayout.LayoutParams.MatchParent);

                    {//readonly

                        view.KeyListener = (null);
                        view.SetCursorVisible(false);
                        view.Pressed = (false);
                        view.Focusable = (false);
                    }


                    scroll.AddView(view);

                    pV.AddView(scroll);

                    view.Text = text_;
                }

            }

        }


        void outToPrinter()
        {

            string text_ = renderingData;

            if (isHtml(text_))
            {
                // share();
                return;
            }



            text_ = openSpeChar(text_);

            Encoding enc = this.renderingInfo.encoding != string.Empty ? Encoding.GetEncoding(this.renderingInfo.encoding) : Encoding.ASCII;

            if (this.renderingInfo.replace != string.Empty)
            {
                StringBuilder sb = new StringBuilder(text_);
                string[][] arrItems = ToolString.explodeGroupList(this.renderingInfo.replace);
                foreach (string[] arr in arrItems)
                    if (arr.Length == 2)
                        sb.Replace(arr[0], arr[1]);

                text_ = sb.ToString();
            }

            //
            for (int i = 0; i < renderingInfo.count; ++i)
                ToolPrint.print(text_, enc);
        }

        string openSpeChar(string pStr)
        {
            pStr = patternHex.Replace(pStr, new MatchEvaluator((x) =>
            {
                //[0xAA]
                string v_ = x.Value;
                if (v_.Length == 6)
                {
                    byte b_ = byte.Parse(v_ = v_.Substring(3, 2), System.Globalization.NumberStyles.HexNumber);
                    char c_ = Convert.ToChar(b_);
                    v_ = c_.ToString();
                }

                return v_;

            }));

            return pStr;
        }
        string hideEscPosParts(string pStr)
        {
            StringBuilder sb = new StringBuilder();
            StringReader sr = new StringReader(pStr);
            string str = null;
            while ((str = sr.ReadLine()) != null)
            {

                bool isCmd = false;


                if (str.Length > 0 && (str[0] == '\x1B' || str[0] == '\x1C' || str[0] == '\x1D'))
                    isCmd = true;

                if (str.StartsWith("#") && str.EndsWith("#"))
                    isCmd = true;

                if (!isCmd)
                    sb.AppendLine(str);
            }

            return sb.ToString();


        }
        //public void Dispose()
        //{

        //}

        public void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
        {

        }


        [Export("readObject", Throws = new[] {
        typeof (Java.IO.IOException),
        typeof (Java.Lang.ClassNotFoundException)})]
        private void ReadObjectDummy(Java.IO.ObjectInputStream source)
        {
            renderingInfo = source.ReadObject() as RenderingInfo;
            renderingData = source.ReadUTF();
        }

        [Export("writeObject", Throws = new[] {
        typeof (Java.IO.IOException),
        typeof (Java.Lang.ClassNotFoundException)})]
        private void WriteObjectDummy(Java.IO.ObjectOutputStream destination)
        {
            destination.WriteObject(renderingInfo);
            destination.WriteUTF(renderingData);

        }
    }
}