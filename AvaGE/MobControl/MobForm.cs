using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using AvaExt.AndroidEnv.ControlsBase;
using AvaExt.Common;
using AvaExt.Settings;
using AvaExt.ControlOperation;
using Android.Content;
using Android.Util;
using Android.Views;
using Android.OS;
using AvaExt.TableOperation;
using AvaExt.Common.Const;
using AvaExt.Translating.Tools;
using AvaExt.MyException;

namespace AvaGE.MobControl
{
    public class MobForm : Form, IControlGlobalInit, ISelfDestructable, ITranslateable
    {

        protected bool inited = false;
        //  protected bool manualInitCall = false;
        protected ISettings settings;
        bool _translated = false;
        bool _initedFromSettings = false;
        bool _initedBeforeReadSettings = false;
        bool _initedAfterReadSettings = false;
        // 

        public void initForm()
        {
            inited = true;
            if (environment != null)
            {
                ToolMobile.log("start form init");

                if (settings == null)
                    settings = getFormSettings();
                _initBeforeSettings();
                initFromSettings();
                _initAfterSettings();
                translate();

            }

            
        }


        public MobForm(IEnvironment pEnv, int pDesignId)
            : base(pEnv, pDesignId)
        {
            //Text = "T_FORM_NAME";
            // Closed += MobForm_Closed;


        }


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            settings = null;

        }
        //void MobForm_Closed(object sender, EventArgs e)
        //{

        //}
 

        protected virtual ISettings getFormSettings()
        {
            string name = globalStoreName();
            return environment.getSettingsStore().getByName(name);

        }

        void _initBeforeSettings()
        {
            if (!_initedBeforeReadSettings)
            {
                if (settings != null && environment != null)
                {
                    initBeforeSettings();
                    _initedBeforeReadSettings = true;
                }

            }
        }
        void _initAfterSettings()
        {
            if (!_initedAfterReadSettings)
            {
                if (settings != null && environment != null)
                {
                    initAfterSettings();
                    _initedAfterReadSettings = true;
                }

            }
        }
        protected virtual void initFromSettings()
        {
            if (!_initedFromSettings)
            {
                if (settings != null && environment != null)
                {
                  //  ToolMobile.log("start form readSettings 1");
                    InitForGlobal.readSettings(this, environment, settings);
                   // ToolMobile.log("start form readSettings 2");
                    InitForGlobal.readSettings(this, environment, settings);
                    _initedFromSettings = true;
                }

            }
        }
        protected virtual void initBeforeSettings()
        {
          //  ToolMobile.log("start form initBeforeSettings");
        }
        protected virtual void initAfterSettings()
        {
           // ToolMobile.log("start form initAfterSettings");
        }
        protected virtual void translate()
        {
            if (!_translated)
                if (settings != null && environment != null)
                {
                    //ToolMobile.log("start form translate");

                    // environment.translate(this, settings);
                    environment.translate(this);
                    _translated = true;
                }
        }

        protected virtual string translate(string pStr)
        {

            //if (settings != null && environment != null)
            //{
            //    return environment.translate(pStr, settings);

            //}
            if (environment != null)
            {
                return environment.translate(pStr);

            }
            return pStr;
        }


        #region Close



        #endregion


        protected virtual string globalStoreName()
        {
            return "undef";
        }



        public virtual void globalRead(IEnvironment pEnv, ISettings pSettings)
        {
            _isGlobalInited = true;
            InitForGlobal.read(this, getGlobalObjactName(), pEnv, pSettings);

        }

        public virtual void globalWrite(IEnvironment pEnv, ISettings pSettings)
        {
            InitForGlobal.write(this, getGlobalObjactName(), pEnv, pSettings);
        }

        public virtual string getGlobalObjactName()
        {
            return "Form";
        }

        bool _isGlobalInited = false;
        public bool isGlobalInited()
        {
            return _isGlobalInited;
        }




        protected override void OnCreate(Bundle savedInstanceState)
        {
            try
            {

                LayoutInflater.Factory = CustomViewFactory.getInstance();

                base.OnCreate(savedInstanceState);


                initForm();
            }
            catch (Exception exc)
            {
 
                ToolMobile.setExceptionInner(exc);

                var err = ToolException.unwrap(exc);
  
                Close();


               Android.Widget.Toast.MakeText(this.ApplicationContext, err, Android.Widget.ToastLength.Long).Show();

            }
        }


        class CustomViewFactory : Java.Lang.Object, LayoutInflater.IFactory
        {

            private static CustomViewFactory mInstance;

            public static CustomViewFactory getInstance()
            {
                if (mInstance == null)
                {
                    mInstance = new CustomViewFactory();
                }

                return mInstance;
            }

            private CustomViewFactory() { }


            public View OnCreateView(string name, Context context, IAttributeSet attrs)
            {

                // string ns = "http://schemas.android.com/apk/res-auto";
                string ns_ = "http://schemas.android.com/apk/res/android";

                string tag_ = ToolCell.isNull(attrs.GetAttributeValue(ns_, "tag"), string.Empty).ToString();
                // string style_ = ToolCell.isNull(attrs.GetAttributeValue(ns_, "style"), string.Empty).ToString();
                string type_ = ToolObjectName.getArgValue(tag_, ConstCmdLine.type);


                //ToolMobile.log("OnCreateView [" + name + "] with tag [" + tag_ + "]");

                View v = null;

                switch (name)
                {
                    case "GridView":
                        if (type_ == "grid")
                        {
                            v = new MobDataGrid(context, attrs);
                        }
                        break;
                    case "ExpandableListView":
                        if (type_ == "tree")
                        {

                            v = new MobTreeView(context, attrs);
                        }
                        break;
                    case "ListView":
                        if (type_ == "list")
                        {

                            v = new MobListView(context, attrs);
                        }
                        break;
                    case "LinearLayout":
                        if (type_ == "numinput")
                        {
                            v = new MobNumInput(context, attrs);
                        }
                        else
                        {
                            v = new MobPanel(context, attrs);
                        }
                        break;
                    case "RelativeLayout":
                        {
                            v = new MobPanelRelative(context, attrs);
                        }
                        break;
                    case "FrameLayout":
                        {
                            v = new MobFrame(context, attrs);
                        }
                        break;
                    case "TextView":
                        {
                            v = new MobLabel(context, attrs);
                        }
                        break;
                    case "EditText":
                        if (type_ == "num")
                        {
                            v = new MobNumEdit(context, attrs);
                        }
                        else
                            if (type_ == "date")
                            {
                                v = new MobDateEdit(context, attrs);
                            }
                            else
                                v = new MobTextBox(context, attrs);
                        break;
                    case "Button":
                        {
                            v = new MobButton(context, attrs);
                        }
                        break;

                    case "DatePicker":
                        {
                            v = new MobDatePicker(context, attrs);
                        }
                        break;

                    case "TimePicker":
                        {
                            v = new MobTimePicker(context, attrs);
                        }
                        break;

                    case "TabHost":
                        {
                            v = new MobTabControl(context, attrs);
                        }
                        break;
                }

              //  if (v == null)
               //     ToolMobile.log("OnCreateView undefined for [" + name + "] with tag [" + tag_ + "]");


                return v;
            }

            protected override void Dispose(bool disposing)
            {
                base.Dispose(disposing);
            }


        }


        public object[] selfDestruct()
        {
          //  ToolMobile.log("selfDestruct form [" + this.GetType().FullName + "]");
           // ToolMobile.log("selfDestruct form [" + this.Window.GetType().FullName + "]");
          //  ToolMobile.log("selfDestruct form [" + this.Window.DecorView.GetType().FullName + "]");

            return ToolMobile.getChilds(this.Window.DecorView);
        }

        public string getTranslatingText()
        {

            string name = globalStoreName();


            name = "T_" + name.ToUpperInvariant().Replace('.', '_');

            return name;

            return Text;
        }

        public void setTranslatingText(string pText)
        {
            Text = pText;
        }
    }
}