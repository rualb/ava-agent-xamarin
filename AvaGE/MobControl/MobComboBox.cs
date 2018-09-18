using System;
using System.Collections.Generic;
using System.Text;
using MobExt.AndroidEnv.ControlsBase;
using System.Drawing;

using System.Data;

using MobExt.TableOperation;
using MobExt.Settings;
using MobExt.Manual.Table;
using MobExt.ControlOperation;
using MobExt.Common;
using System.ComponentModel;
using Android.Widget;
using Android.Content;
using Android.Util;

namespace MobGE.MobControl
{
    public class MobComboBox : Spinner, IControlBind, IControlGlobalInit
    {

        public MobComboBox(Context context)
            : base(context)
        {


        }
        public MobComboBox(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            Name = string.Empty;
        }
        public string Name
        {
            get;
            set;
        }


        int _MaxLength = short.MaxValue;
        public virtual int MaxLength
        {
            get
            {
                return _MaxLength;
            }
            set
            {
                _MaxLength = Math.Max(0, value);
            }
        }

        public virtual bool ReadOnly
        {
            get { return false; }
            set { }
        }
        //protected override void OnTextChanged(EventArgs e)
        //{

        //    base.OnTextChanged(e);
        //    //
        //    if (this.Text.Length > this.MaxLength)
        //        this.Text = this.Text.Substring(0, this.MaxLength);
        //}
        //protected override void OnKeyPress(KeyPressEventArgs e)
        //{
        //    base.OnKeyPress(e);
        //    if (!char.IsControl(e.KeyChar))
        //        if (this.Text.Length >= this.MaxLength)
        //            e.Handled = true;
        //}
        // Color defaultBackColor;
        //public MobComboBox()
        //    : base()
        //{
        //    MaxLength = short.MaxValue;
        //    //defaultBackColor = BackColor;
        //    this.DataBindings.CollectionChanged += new CollectionChangeEventHandler(DataBindings_CollectionChanged);
        //    this.Validated += new EventHandler(validated);
        //}

        //void DataBindings_CollectionChanged(object sender, CollectionChangeEventArgs e)
        //{
        //    if (e.Action == CollectionChangeAction.Add)
        //        if (typeof(Binding).IsAssignableFrom(e.Element.GetType()))
        //        {
        //            Binding b = (Binding)e.Element;
        //            if (b.PropertyName == DSProperty)
        //                b.DataSourceUpdateMode = DataSourceUpdateMode.Never;
        //        }
        //}

        void validated(object sender, EventArgs e)
        {
            writeBindig();
        }

        //public void writeBindig()
        //{
        //    foreach (Binding b in DataBindings)
        //        if (typeof(DataTable).IsAssignableFrom(b.DataSource.GetType()) && b.IsBinding)
        //            if (b.BindingManagerBase.Current != null &&
        //                b.PropertyName == DSProperty)
        //            {
        //                object dsVal = ((DataRowView)b.BindingManagerBase.Current)[b.BindingMemberInfo.BindingField];
        //                object thisVal = this.GetType().GetProperty(DSProperty).GetValue(this, null);
        //                if (!ToolCell.isNull(thisVal))
        //                    if (!ToolType.isEqual(dsVal, thisVal))
        //                    {
        //                        b.WriteValue();
        //                        b.ReadValue();
        //                    }
        //                b.BindingManagerBase.EndCurrentEdit();
        //            }

        //}


        public virtual void globalRead(IEnvironment pEnv, ISettings pSettings)
        {
            //

            //
            _isGlobalInited = true;
            InitForGlobal.read(this, getGlobalObjactName(), pEnv, pSettings);

            //const string attrDisplaySet = "DisplaySet";
            //string displaySet = pSettings.getStringAttr(this.Name, attrDisplaySet);
            //if (displaySet != null && displaySet != string.Empty)
            //{
            //    string displaySetData = pSettings.getString(displaySet);
            //    if (displaySetData != null && displaySetData != string.Empty)
            //    {
            //        DataTable tab = ToolString.explodeForTable(displaySetData, new string[] { TableDUMMY.LOGICALREF, TableDUMMY.VALUE });
            //        pEnv.translate(tab);
            //        DisplayMember = TableDUMMY.VALUE;
            //        ValueMember = TableDUMMY.LOGICALREF;
            //        DataSource = tab;
            //    }
            //}

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


        string _DSProperty = "SelectedValue";
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
            string col = DSColumn;
            string tab = (DSSubTable != string.Empty ? DSSubTable : DSTable);
            if (col != string.Empty && tab != string.Empty)
                this.MaxLength = env.getColumnLen(tab, col);
        }
    }
}
