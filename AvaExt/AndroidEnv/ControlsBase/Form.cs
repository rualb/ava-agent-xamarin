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
using System.ComponentModel;
using System.Data;
using System.Reflection;
using AvaExt.TableOperation;
using AvaExt.ObjectSource;
using Ava_Ext.Common;


namespace AvaExt.AndroidEnv.ControlsBase
{

    public class Form : ActivityExt
    {

      

        public BindingContextSet BindingContext = new BindingContextSet();

        public void FinishDataEditing()
        {
            if (this.Window != null && this.Window.CurrentFocus != null)
            {
                View v = this.Window.CurrentFocus;
                var b = BindingContext.getBindingItem(v);
                if (b != null)
                  
                b.write(); // b.writeIfChanged();
            }
        }


        public const string FORM_NAME = "";
        public const string FORM_ICON = "@drawable/main";

        public enum DialogResult
        {
            Node = 0,
            Ok = 1,
            Cancel = 2

        }


        public Form(IEnvironment pEnv, int pDesignId)
            : base(pEnv, pDesignId)
        {
        //  ToolMobile.log("form constructor [" + GetType().FullName + "]" + (environment == null ? " but Env is null" : ""));

            Closed += Form_Closed;
        }

       
        void Form_Closed(object sender, EventArgs e)
        {

            try
            {
                if (BindingContext != null)
                    BindingContext.Dispose();
            }
            catch { }

            BindingContext = null;


            Closed -= Form_Closed;


            ToolDispose.disposeControl(this);


             
        }


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

      
        public override void Finish()
        {
            base.Finish();
        }

        public void Show()
        {

        }
        public void Hide()
        {

        }
        public void Refresh()
        {

        }

        public DialogResult ShowDialog()
        {
            return DialogResult.Node;
        }

        public virtual void Close()
        {
            Finish();

          

            removeActivityExt(this);
 
        }

        protected virtual bool userCancelAllow()
        {
            return true;
        }
        protected virtual bool userCommitAllow()
        {
            return true;
        }
        protected virtual void userCancelDone()
        {
            Close();
        }
        protected virtual void userCommitDone(bool pClose)
        {
            if (pClose)
                Close();
        }
        protected virtual void userRequireCancel()
        {
            if (userCancelAllow())
                userCancelDone();
        }
        protected virtual void userRequireCommit()
        {
            if (userCommitAllow())
                userCommitDone(true);
        }
        public override void OnBackPressed()
        {
         
            userRequireCancel();
        }

       


        public class BindingContextSet : IDisposable
        {
            List<BindingItem> list = new List<BindingItem>();


            public void Add(BindingItem pItm)
            {
                if (list == null)
                    return;

                pItm.context = this;
                list.Add(pItm);
            }

            public BindingItem getBindingItem(View pView)
            {
                if (pView != null)
                    foreach (BindingItem b in list)
                        if (object.ReferenceEquals(pView, b.TargetObject))
                            return b;

                return null;
            }


            public int getBindingItemPosition(View pView)
            {
                BindingItem b = getBindingItem(pView);
                if (b != null)
                    return b.Position;

                return -1;
            }

            public void setBindingItemPosition(View pView, int pPos)
            {
                var b = getBindingItem(pView);
                if (b != null)
                    b.Position = pPos;
            }
            public DataRow getBindingItemRecord(View pView)
            {
                BindingItem b = getBindingItem(pView);
                if (b != null)
                    return b.DataRecord;

                return null;
            }
            void setPosition(object pDataSource, int pPos)
            {
                if (pDataSource == null)
                    return;

                foreach (BindingItem b in list)
                    if (object.ReferenceEquals(pDataSource, b.dataSourceDataView))
                        b._setPosition(pPos);
            }

            public void ReadValues()
            {
                foreach (BindingItem itm in list)
                    itm.read();
            }
            public void ReadWrite()
            {
                foreach (BindingItem itm in list)
                    itm.read();
            }
            public class BindingItem : IDisposable
            {
                internal BindingContextSet context;

                View obj;
                string objProperty;
                PropertyInfo propertyInfo;
                string dsProperty;
                DataColumn column;
                int _position = 0;


                object valueOnFocusIn;

                public bool isReadOnly = false;

                public IObjectConverter ConverterFormat;
                public IObjectConverter ConverterParse;

                public BindingItem(View pObj, string pObjProperty, object pDataSource, string pDsProperty)
                {
                    if (pObj == null || pObjProperty == null || pDataSource == null || pDsProperty == null)
                        throw new ArgumentNullException();

                    obj = pObj;

                    obj.FocusChange += focusChange;


                    objProperty = pObjProperty;
                    propertyInfo = pObj.GetType().GetProperty(ObjectProperty);

                    if (propertyInfo == null || !propertyInfo.CanRead || !propertyInfo.CanWrite)
                        throw new ArgumentException("Object hasnt valid property[" + ObjectProperty + "]");

                    DataSource = pDataSource;
                    dsProperty = pDsProperty;

                    column = dataSourceDataView.Table.Columns[DataSourceProperty];

                    if (column == null)
                        throw new ArgumentException("DataSource hasnt valid property[" + DataSourceProperty + "]");

                    dataSourceDataView.Table.ColumnChanged += Table_ColumnChanged;
                    dataSourceDataView.Table.RowChanged += Table_RowChanged;
                    dataSourceDataView.ListChanged += dataSourceDataView_ListChanged;
                    //
                    read();

                    //if(obj.IsFocused)
                    //valueOnFocusIn = readPropertyObject();
                }


                public void Dispose()
                {
                    context = null;

                    if (obj != null)
                        obj.FocusChange -= focusChange;

                    if (dataSourceDataView != null)
                    {
                        dataSourceDataView.Table.ColumnChanged -= Table_ColumnChanged;
                        dataSourceDataView.Table.RowChanged -= Table_RowChanged;
                    }

                    obj = null;
                    obj = null;
                    objProperty = null;
                    propertyInfo = null;
                    dsProperty = null;
                    column = null;

                    try
                    {
                        if (ConverterFormat != null)
                            ConverterFormat.Dispose();
                    }
                    catch { }


                    ConverterFormat = null;

                    try
                    {
                        if (ConverterParse != null)
                            ConverterParse.Dispose();
                    }
                    catch { }

                    ConverterParse = null;

                    dataSourceDataView = null;

                    dataSource = null;


                    context = null;

                    obj = null;
                    objProperty = null;
                    propertyInfo = null;
                    dsProperty = null;
                    column = null;


                }


                private void focusChange(object sender, View.FocusChangeEventArgs e)
                {
                    if (e.HasFocus)
                        valueOnFocusIn = readPropertyObject();

                    if (!e.HasFocus)
                        propertyChangedCheck();


                }



                bool isChangedAfterFocusIn()
                {
                    object curr_ = readPropertyObject();

                    if (valueOnFocusIn == null && curr_ == null)
                        return false;

                    return !ToolType.isEqual(valueOnFocusIn, curr_);

                }

                void dataSourceDataView_ListChanged(object sender, ListChangedEventArgs e)
                {
                    if (DataSource == null)
                        return;

                    if (e.ListChangedType != ListChangedType.Reset)
                        return;

                    read();
                }

                void Table_RowChanged(object sender, DataRowChangeEventArgs e)
                {
                    if (DataSource == null)
                        return;

                    read();
                }

                void Table_ColumnChanged(object sender, DataColumnChangeEventArgs e)
                {
                    if (DataSource == null)
                        return;

                    if (e.Column.ColumnName != column.ColumnName)
                        return;

                    read();
                }

                object readPropertyObject()
                {
                    return propertyInfo.GetValue(obj, null);
                }
                void writePropertyObject(object pVal)
                {
                    propertyInfo.SetValue(obj, pVal);
                    redraw();
                }

                void redraw()
                {
                    if (obj != null)
                    {
                        //var x = obj as TextView;
                        //if (x != null)
                        //    x.Text = x.Text;

                        obj.Invalidate();
                    }
                }
                object readPropertySource()
                {
                    DataRow row_ = DataRecord;
                    if (row_ != null)
                        return row_[column];

                    return null;
                }

                public int Position
                {
                    get
                    {

                        if (dataSourceDataView != null && dataSourceDataView.Table.Rows.Count > 0)
                        {
                            _position = Math.Max(0, _position);
                            _position = Math.Min(dataSourceDataView.Table.Rows.Count - 1, _position);

                            return _position;
                        }

                        return -1;
                    }
                    set
                    {
                        if (context != null)
                            context.setPosition(dataSourceDataView, value);

                    }
                }

                public DataRow DataRecord
                {
                    get
                    {
                        int pos_ = Position;
                        if (pos_ >= 0)
                            return dataSourceDataView.Table.Rows[pos_];

                        return null;
                    }

                }

                internal void _setPosition(int pPosition)
                {
                    if (_position != pPosition)
                    {
                        _position = pPosition;
                        this.read();
                    }
                }
                void writePropertySource(object pVal)
                {
                    DataRow row_ = DataRecord;
                    if (row_ != null)
                    {
                        try
                        {
                            row_.BeginEdit();
                            row_[column] = pVal;

                        }
                        finally
                        {
                            if (row_ != null)
                                row_.EndEdit();
                        }
                    }
                }


                //public void read()
                //{
                //    if (obj == null)
                //        return;


                //    object s = readPropertySource();
                //    object s2 = convertToObjectType(s);
                //    object d = readPropertyObject();

                //    if (!isSame(d, s2))
                //        writePropertyObject(s2);
                //}
                //public void write()
                //{
                //    if (obj == null)
                //        return;

                //    object s = readPropertyObject();
                //    object s2 = convertToSourceType(s);
                //    object d = readPropertySource();

                //    if (!isSame(d, s2))
                //        writePropertySource(s2);

                //    read();
                //}


                public void read()
                {

                    if (obj == null)
                        return;

                    if (!inSyncMode)
                    {
                        try
                        {
                            inSyncMode = true;

                            _read();

                        }
                        finally
                        {
                            inSyncMode = false;
                        }
                    }
                }

                internal void _read()
                {
                    if (obj == null)
                        return;


                    object s = readPropertySource();
                    object s2 = convertToObjectType(s);
                    object d = readPropertyObject();

                    if (!isSame(d, s2))
                        writePropertyObject(s2);
                }
                public void writeIfChanged()
                {
                    _write(false);
                }
                public void write()
                {

                    _write(true);


                }
                void _write(bool pForce)
                {

                    if (isReadOnly)
                        return;

                    if (obj == null)
                        return;

                    if (!inSyncMode)
                    {

                        if (!pForce)
                            if (!isChangedAfterFocusIn())
                                return;
                        try
                        {
                            inSyncMode = true;

                            _write();

                            _read();
                        }
                        finally
                        {
                            inSyncMode = false;
                        }
                    }
                }

                internal void _write()
                {
                    if (obj == null)
                        return;

                    object s = readPropertyObject();
                    object s2 = convertToSourceType(s);
                    object d = readPropertySource();

                    if (!isSame(d, s2))
                        writePropertySource(s2);


                }

                object convertToObjectType(object pVal)
                {
                    if (ConverterFormat != null)
                    {
                        var v = ConverterFormat.convert(pVal);
                        if (v != null)
                            pVal = v;
                    }

                    return convertToType(pVal, propertyInfo.PropertyType);

                }
                object convertToSourceType(object pVal)
                {
                    if (ConverterParse != null)
                    {
                        var v = ConverterParse.convert(pVal);
                        if (v != null)
                            pVal = v;
                    }

                    return convertToType(pVal, column.DataType);

                }
                object convertToType(object pVal, Type pType)
                {
                    if (ToolCell.isNull(pVal))
                        pVal = ToolType.getTypeDefaulValue(pType);

                    return Convert.ChangeType(pVal, pType);

                }

                bool isSame(object x, object y)
                {
                    IComparable x1 = x as IComparable;
                    IComparable y1 = y as IComparable;

                    if (x1 != null && y1 != null && x1.CompareTo(y1) == 0)
                        return true;

                    return false;

                }
                bool inSyncMode = false;
                void propertyChangedCheck()
                {
                    writeIfChanged();
                }

                public View TargetObject { get { return obj; } }
                public string ObjectProperty { get { return objProperty; } }
                public string DataSourceProperty { get { return dsProperty; } }

                object dataSource;
                internal DataView dataSourceDataView;

                public object DataSource
                {
                    get
                    {
                        return dataSource;
                    }
                    set
                    {

                        DataView v = ToolTable.dataViewFromSource(value);

                        if (v == null)
                            throw new ArgumentException("DataSource is not Table or View");

                        dataSource = value;
                        dataSourceDataView = v;
                    }
                }


             
            }


            public void Dispose()
            {
                
                if (list != null)
                {
                    try
                    {
                        foreach (IDisposable d in list)
                            d.Dispose();
                    }
                    catch { }

                    list.Clear();

                    list = null;
                }
            }

            public void Clear()
            {

                Dispose();
                list = new List<BindingItem>();
            }


        }
    }
}