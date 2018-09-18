using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.AndroidEnv.ControlsBase;

using AvaExt.Settings;

using AvaExt.ControlOperation;
using AvaExt.Common;
using System.Data;
using Android.Widget;
using Android.Content;
using Android.Util;
using AvaExt.Translating.Tools;
using AvaExt.ObjectSource;
using System.ComponentModel;
using Android.Text;
using Java.Lang;

namespace AvaGE.MobControl
{
    public class MobTextBox : EditText, IControlGlobalInit, IControlBind, ITranslateable, IControl, IObjectSource
    {
        public MobTextBox(Context context)
            : base(context)
        {
            init();

        }



        public MobTextBox(Context context, IAttributeSet attrs    )
            : base(context, attrs  )
        {
            init();
        }
        public MobTextBox(Context context, IAttributeSet attrs, int styleRes)
            : base(context, attrs, styleRes)
        {
            init();
        }

        void init()
        {

            reinitFilter();

        }

        protected virtual void reinitFilter()
        {
            this.SetFilters(new IInputFilter[] { new ImplInputFilterLen(MaxLength) });

        }


        protected class ImplInputFilterLen : Java.Lang.Object, IInputFilter
        {
            int maxLen = short.MaxValue;

            public ImplInputFilterLen(int pMaxLen)
            {
                maxLen = pMaxLen;

            }

            string subString(ICharSequence source, int start, int end)
            {
                start = System.Math.Max(0, start);
                start = System.Math.Min(source.Length(), start);//the begin index, inclusive.
                end = System.Math.Max(0, end);
                end = System.Math.Min(source.Length(), end);// the end index, exclusive.


                if (start >= 0 && end >= 0 && end >= start)
                    return source.SubSequence(start, end);

                return string.Empty;
            }

            public ICharSequence FilterFormatted(ICharSequence source, int start, int end, ISpanned dest, int dstart, int dend)
            {
                if (maxLen > 0)
                {
                    string s_ = subString(source, start, end);

                    string left_ = subString(dest, 0, dstart);
                    string right_ = subString(dest, dend, dest.Length());

                    string full_ = left_ + s_ + right_;

                    if (full_.Length > maxLen)
                    {
                        int tail_ = full_.Length - maxLen;

                        s_ = ToolString.left(s_, s_.Length - tail_);

                        return new Java.Lang.String(s_);
                    }
                }

                return null;
            }


        }

        string name;
        public string Name
        {
            get { return name == null ? name = ToolMobile.getFromTagName(this) : name; }

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
            return this.Name;
        }



        bool _isGlobalInited = false;
        public bool isGlobalInited()
        {
            return _isGlobalInited;
        }



        string _DSProperty = "Text";
        public string DSProperty
        {
            get
            {
                return _DSProperty;
            }
            set
            {
                _DSProperty = value;
            }
        }
        string _DSTable = string.Empty;
        public string DSTable
        {
            get
            {
                return _DSTable;
            }
            set
            {
                _DSTable = value;
            }
        }
        string _DSSubTable = string.Empty;
        public string DSSubTable
        {
            get
            {
                return _DSSubTable;
            }
            set
            {
                _DSSubTable = value;
            }
        }
        string _DSColumn = string.Empty;
        public string DSColumn
        {
            get
            {
                return _DSColumn;
            }
            set
            {
                _DSColumn = value;
            }
        }
        bool _isBound = false;
        public bool isBound()
        {
            return _isBound;
        }
        public void bound(IEnvironment env)
        {

        }

        public string getTranslatingText()
        {
            return this.Text;
        }

        public void setTranslatingText(string pText)
        {
            this.Text = pText;
        }

        public virtual object get()
        {
            return Text;
        }

        int _MaxLength = 0;
        public virtual int MaxLength
        {
            get
            {
                return _MaxLength;
            }
            set
            {
                _MaxLength = value;
                reinitFilter();
            }
        }



        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

           
        }
    }
}


//public class MobTextBox : EditText, IControlBind, IControlGlobalInit
//{

//    public MobTextBox(Context context)
//        : base(context)
//    {


//    }

//    public MobTextBox(Context context, IAttributeSet attrs)
//        : base(context, attrs)
//    {
//        Name = string.Empty;
//    }

//    public string Name
//    {
//        get;
//        set;
//    }


//    //  Color defaultBackColor;
//    //public MobTextBox()
//    //    : base()
//    //{
//    //    //  defaultBackColor = BackColor;

//    //    this.DataBindings.CollectionChanged += new CollectionChangeEventHandler(DataBindings_CollectionChanged);
//    //    this.Validated += new EventHandler(validated);
//    //    this.KeyPress += new KeyPressEventHandler(textBox_KeyPress);
//    //}
//    //void textBox_KeyPress(object sender, KeyPressEventArgs e)
//    //{
//    //    if (e.KeyChar == '\r')
//    //        validated(this, EventArgs.Empty);
//    //}
//    //void DataBindings_CollectionChanged(object sender, CollectionChangeEventArgs e)
//    //{
//    //    if (e.Action == CollectionChangeAction.Add)
//    //        if (typeof(Binding).IsAssignableFrom(e.Element.GetType()))
//    //        {
//    //            Binding b = (Binding)e.Element;
//    //            if (b.PropertyName == DSProperty)
//    //                b.DataSourceUpdateMode = DataSourceUpdateMode.Never;
//    //        }
//    //}

//    void validated(object sender, EventArgs e)
//    {
//        writeBindig();
//    }

//    //public void writeBindig()
//    //{
//    //    foreach (Binding b in DataBindings)
//    //        if (typeof(DataTable).IsAssignableFrom(b.DataSource.GetType()) && b.IsBinding)
//    //            if (b.BindingManagerBase.Current != null &&
//    //                b.PropertyName == DSProperty)
//    //            {
//    //                object dsVal = ((DataRowView)b.BindingManagerBase.Current)[b.BindingMemberInfo.BindingField];
//    //                object thisVal = this.GetType().GetProperty(DSProperty).GetValue(this, null);
//    //                if (!ToolCell.isNull(thisVal))
//    //                    if (!ToolType.isEqual(dsVal, thisVal))
//    //                    {
//    //                        b.WriteValue();
//    //                        b.ReadValue();
//    //                    }
//    //                b.BindingManagerBase.EndCurrentEdit();
//    //            }

//    //}


//    string _DSProperty = "Text";
//    public string DSProperty
//    {
//        get
//        {
//            return _DSProperty;
//        }
//        set
//        {
//            _DSProperty = value;
//        }
//    }
//    string _DSTable = string.Empty;
//    public string DSTable
//    {
//        get
//        {
//            return _DSTable;
//        }
//        set
//        {
//            _DSTable = value;
//        }
//    }
//    string _DSSubTable = string.Empty;
//    public string DSSubTable
//    {
//        get
//        {
//            return _DSSubTable;
//        }
//        set
//        {
//            _DSSubTable = value;
//        }
//    }
//    string _DSColumn = string.Empty;
//    public string DSColumn
//    {
//        get
//        {
//            return _DSColumn;
//        }
//        set
//        {
//            _DSColumn = value;
//        }
//    }
//    bool _isBound = false;
//    public bool isBound()
//    {
//        return _isBound;
//    }

//    public void bound(IEnvironment env)
//    {
//        string col = DSColumn;
//        string tab = (DSSubTable != string.Empty ? DSSubTable : DSTable);
//        if (col != string.Empty && tab != string.Empty)
//            this.MaxLength = env.getColumnLen(tab, col);
//    }



//    #region IControlGlobalInit Members

//    public virtual void globalRead(IEnvironment pEnv, ISettings pSettings)
//    {
//        _isGlobalInited = true;
//        InitForGlobal.read(this, getGlobalObjactName(), pEnv, pSettings);

//    }

//    public virtual void globalWrite(IEnvironment pEnv, ISettings pSettings)
//    {
//        InitForGlobal.write(this, getGlobalObjactName(), pEnv, pSettings);
//    }
//    public virtual string getGlobalObjactName()
//    {
//        return this.Name;
//    }



//    bool _isGlobalInited = false;
//    public bool isGlobalInited()
//    {
//        return _isGlobalInited;
//    }
//    #endregion
//}