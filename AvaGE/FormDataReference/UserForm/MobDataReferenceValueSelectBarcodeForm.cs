using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using AvaExt.AndroidEnv.ControlsBase;
using AvaExt.DataRefernce;
using AvaExt.PagedSource;
using AvaExt.Common;
using AvaGE.MobControl.ControlsTools;
using AvaGE.MobControl.ControlsTools.UserMessanger;
using AvaExt.Settings;
using AvaExt.Translating.Tools;
using AvaGE.Common;

using AvaExt.SQL;
using AvaExt.SQL.Dynamic;
using AvaExt.Manual.Table;
using System.IO;
using AvaExt.ControlOperation;
using AvaExt.Common.Const;
using AvaGE.MobControl;
using Android.Views;
using Android.Widget;
using AvaAgent;

namespace AvaGE.FormDataReference.UserForm
{
    public class MobDataReferenceValueSelectBarcodeForm : MobDataReferenceValueSelectForm
    {
        protected void reset()
        {
            cBarcode.Text = string.Empty;
            cBarcode.RequestFocus();

        }

        protected override void initTable()
        {
            _table.Columns.Add(TableDUMMY.VALUE);
        }
        protected override DataRow getData()
        {
            return _table.Rows.Add(new object[] { cBarcode.Text.Trim() });
        }




        protected MobTextBox cBarcode { get { return FindViewById<MobTextBox>(Resource.Id.cBarcode); } }

        public MobDataReferenceValueSelectBarcodeForm()
            : base(Resource.Layout.MobDataReferenceValueSelectBarcodeForm)
        {



            Created += MobDataReferenceValueSelectBarcodeForm_Created;

        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            cBarcode.KeyPress -= cBarcode_KeyPress;

            cBtnScan.Click -= cBtnScan_Click;

            Created -= MobDataReferenceValueSelectBarcodeForm_Created;
        }

        Button cBtnScan
        {
            get
            {
                return FindViewById<Button>(Resource.Id.cBtnScan);
            }
        }

        Button cBtnScanExt
        {
            get
            {
                return FindViewById<Button>(Resource.Id.cBtnScanExt);
            }
        }


        void MobDataReferenceValueSelectBarcodeForm_Created(object sender, EventArgs e)
        {
            cBarcode.KeyPress += cBarcode_KeyPress;

            cBtnScan.Click += cBtnScan_Click;
        }

        void cBtnScan_Click(object sender, EventArgs e)
        {
            //ToolBarcodeScaner.scanSimple((b) => {

            //    cBarcode.Text = b;
            //    returnData(getData());
            
            //});
        }

        void cBarcode_KeyPress(object sender, Android.Views.View.KeyEventArgs e)
        {
            e.Handled = false;

            if (ToolControl.isDone(e.KeyCode, e.Event.Number))
            {
                e.Handled = true;
                returnData(getData());
              
            }
 
        }


        protected virtual void setSource(IPagedSource pSource)
        { }


        protected override void initAfterSettings()
        {
            base.initAfterSettings();
            reset();
        }

        public override void setValue(object value)
        {
            if (value != null && value.GetType() == typeof(string))
                cBarcode.Text = (string)value;

        }

 
    }
}

