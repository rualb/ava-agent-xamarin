using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.AndroidEnv.ControlsBase;
using AvaExt.ControlOperation;
using AvaExt.Common;
using AvaExt.Settings;
using Android.Widget;
using Android.Content;
using Android.Util;
using Android.Views;
using AvaGE.MobControl.Tools;
using AvaAgent;

namespace AvaGE.MobControl
{
    public class MobNumInput : LinearLayout, IControlGlobalInit, IIndestructable
    {


        public MobNumInput(Context context)
            : base(context)
        {
            init();

        }
        public MobNumInput(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            init();


        }

        void init()
        {
            if (this.Context != null)
            {
                LayoutInflater inf = (this.Context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater);
                if (inf != null)
                {
                    //return this
                    View v = inf.Inflate(Resource.Layout.MobNumInput, this, true);
                    if (v != null)
                    {
                        for (int i = 0; i < this.ChildCount; ++i) //one of childs is layout with buttons
                            foreach (object o in ToolControl.destruct(this.GetChildAt(i)))
                            {
                                Button b = o as Button;
                                if (b != null && b.Tag != null)
                                    b.Click += BtnClick;
                            }

                    }
                }
            }

        }

        MobNumEdit _numEdit;
        public void setNumEdit(MobNumEdit pNumEdit)
        {
            _numEdit = pNumEdit;
            _numEdit.processCmd(CalcCmd.MS);
            _numEdit.processCmd(CalcCmd.reset);
        }
        public MobNumEdit getNumEdit()
        {
            return _numEdit;

        }

        void BtnClick(object sender, EventArgs e)
        {
            View v = sender as View;

            if (v == null)
                return;

            string cmd_ = v.Tag != null ? v.Tag.ToString() : null;

            if (string.IsNullOrEmpty(cmd_))
                return;

            if (_numEdit != null)
                _numEdit.processCmd(cmd_);
        
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            _numEdit = null;
        }



        string name;
        public string Name
        {
            get { return name == null ? name = ToolMobile.getFromTagName(this) : name; }

        }

        #region IControlGlobalInit Members

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
            return this.Name;
        }



        bool _isGlobalInited = false;
        public bool isGlobalInited()
        {
            return _isGlobalInited;
        }
        #endregion




    }
}
