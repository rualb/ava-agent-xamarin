using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using AvaExt.AndroidEnv.ControlsBase;
using AvaExt.Common;
using AvaExt.PagedSource;
using AvaExt.Manual.Table;
using AvaExt.TableOperation;
using AvaExt.Common.Const;
using AvaExt.Settings;
using AvaGE.MobControl.Reporting;
using AvaExt.Reporting;
using AvaExt.ControlOperation;
using AvaExt.ObjectSource;
using AvaExt.MobControl.Reporting.XmlReport;
using System.IO;
using AvaExt.Translating.Tools;
using AvaExt.SQL.Dynamic;
using AvaGE.MobControl;
using AvaExt.Adapter.Tools;
using Android.App;
using Android.Content;
using AvaGE.Common;
using AvaAgent;

namespace AvaGE.FormUserEditor
{
    [Activity(Label = Form.FORM_NAME, Icon = Form.FORM_ICON, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, WindowSoftInputMode = Android.Views.SoftInput.StateAlwaysHidden)]
    public partial class MobInputItemVarsForm : MobFormBase
    {

        public class Helper
        {

            public static readonly Helper instace = new Helper();


            private Helper() { }


            EventHandler handlerOk;
            EventHandler handlerCancel;

            public DataRow row;
            public string column;
            public bool stockLevelControl;

            public IReport report;

            public void edit(Form pContext, DataRow pRow, string pCol, bool pStockLevelControl, IReport pReport, EventHandler pHandlerOk, EventHandler pHandlerCancel)
            {
                row = pRow;
                column = pCol;
                stockLevelControl = pStockLevelControl;
                report = pReport;
                handlerOk = pHandlerOk;
                handlerCancel = handlerOk;
                ToolMobile.startForm(typeof(MobInputItemVarsForm));
            }

            internal void callHandler(bool pOk)
            {
                try
                {
                    if (pOk)
                    {
                        if (handlerOk != null)
                            handlerOk.Invoke(this, EventArgs.Empty);
                    }
                    else
                    {
                        if (handlerCancel != null)
                            handlerCancel.Invoke(this, EventArgs.Empty);
                    }
                }
                catch (Exception exc)
                {
                    ToolMobile.setException(exc);
                }
                finally
                {
                    handlerOk = handlerCancel = null;
                }
            }


            public string converCol(string col)
            {

                if (col.StartsWith(TableSTLINE.AMOUNT))
                    return TableSTLINE.AMOUNT;
                if (col.StartsWith(TableSTLINE.PRICE))
                    return TableSTLINE.PRICE;
                if (col.StartsWith(TableSTLINE.TOTAL))
                    return TableSTLINE.TOTAL;
                if (col.StartsWith(TableSTLINE.DISCPER))
                    return TableSTLINE.DISCPER;
                return string.Empty;
            }

        }

        Helper handler { get { return Helper.instace; } }

        bool _checkLevel = true;
        DataRow _curMatRecord = null;
        UnicodeEncoding _enc = new UnicodeEncoding();

        DataTable _tableSchema = null;
        IBlockPoint _blockPoint = new BlockPoint();
        protected override string globalStoreName()
        {
            return "tool.edit.itemvars";
        }

        MobNumEdit cAmount { get { return FindViewById<MobNumEdit>(Resource.Id.cAmount); } }
        MobTextBox cUnit { get { return FindViewById<MobTextBox>(Resource.Id.cUnit); } }
        MobNumEdit cPrice { get { return FindViewById<MobNumEdit>(Resource.Id.cPrice); } }
        MobNumEdit cDiscount { get { return FindViewById<MobNumEdit>(Resource.Id.cDiscount); } }
        MobNumEdit cTotal { get { return FindViewById<MobNumEdit>(Resource.Id.cTotal); } }

        MobLabel cData { get { return FindViewById<MobLabel>(Resource.Id.cData); } }


        MobButton cBtnUnit { get { return FindViewById<MobButton>(Resource.Id.cBtnUnit); } }



        MobButton cBtnOk { get { return FindViewById<MobButton>(Resource.Id.cBtnOk); } }
        MobButton cBtnCancel { get { return FindViewById<MobButton>(Resource.Id.cBtnCancel); } }

        MobPanel cPanelAmount { get { return FindViewById<MobPanel>(Resource.Id.cPanelAmount); } }
        MobPanel cPanelUnit { get { return FindViewById<MobPanel>(Resource.Id.cPanelUnit); } }
        MobPanel cPanelPrice { get { return FindViewById<MobPanel>(Resource.Id.cPanelPrice); } }
        MobPanel cPanelDiscount { get { return FindViewById<MobPanel>(Resource.Id.cPanelDiscount); } }
        MobPanel cPanelTotal { get { return FindViewById<MobPanel>(Resource.Id.cPanelTotal); } }

        MobNumInput cNumInput { get { return FindViewById<MobNumInput>(Resource.Id.cNumInput); } }

        public MobInputItemVarsForm()
            : base(null, Resource.Layout.MobInputItemVarsForm)
        {







        }


        protected override void initAfterSettings()
        {
            base.initAfterSettings();

            reinitBindingProperties();
            initDataTable();
            bindToData();

            reinitEditingForData();



            cBtnOk.Click += cBtnOk_Click;

            cBtnCancel.Click += cBtnCancel_Click;
        }

        void initDataTable()
        {
            _tableSchema = new DataTable(TableDUMMY.DUMMY);
            _tableSchema.Columns.Add(TableDUMMY.AMOUNT, typeof(double));
            _tableSchema.Columns.Add(TableDUMMY.PRICE, typeof(double));
            _tableSchema.Columns.Add(TableDUMMY.TOTAL, typeof(double));
            _tableSchema.Columns.Add(TableDUMMY.DISCPER, typeof(double));
            _tableSchema.Columns.Add(TableDUMMY.UNIT, typeof(string));
            _tableSchema.Columns.Add(TableDUMMY.UNITREF, typeof(string));
            _tableSchema.Columns.Add(TableDUMMY.UNITCF01, typeof(double));
            _tableSchema.Columns.Add(TableDUMMY.UNITCF02, typeof(double));
            _tableSchema.Columns.Add(TableDUMMY.CONVFACT, typeof(double));
            //
            DataSet ds = new DataSet();
            ds.Tables.Add(_tableSchema);
            //
            _tableSchema.ColumnChanged += new DataColumnChangeEventHandler(_tableSchema_ColumnChanged);
        }
        void cBtnCancel_Click(object sender, EventArgs e)
        {
            userRequireCancel();
        }

        void cBtnOk_Click(object sender, EventArgs e)
        {
            userRequireSave();

        }
        protected virtual void reinitBindingProperties()
        {
            this.cAmount.DSColumn = TableDUMMY.AMOUNT;
            this.cAmount.DSTable = TableDUMMY.DUMMY;

            this.cUnit.DSColumn = TableDUMMY.UNIT;
            this.cUnit.DSTable = TableDUMMY.DUMMY;

            this.cPrice.DSColumn = TableDUMMY.PRICE;
            this.cPrice.DSTable = TableDUMMY.DUMMY;

            this.cDiscount.DSColumn = TableDUMMY.DISCPER;
            this.cDiscount.DSTable = TableDUMMY.DUMMY;

            this.cTotal.DSColumn = TableDUMMY.TOTAL;
            this.cTotal.DSTable = TableDUMMY.DUMMY;
        }
        public virtual void reinitEditingForData()
        {
            _checkLevel = handler.stockLevelControl;

            cPanelUnit.Visible =
            cPanelTotal.Visible =
            cPanelDiscount.Visible =
            cPanelAmount.Visible =
            cPanelPrice.Visible = false;

            _tableSchema.Clear();

            cTotal.Value =
            cDiscount.Value =
            cAmount.Value =
            cPrice.Value = 0.0;



            MobNumEdit numEdit = null;

            switch (handler.converCol(handler.column))
            {
                case TableSTLINE.AMOUNT:
                    cPanelUnit.Visible = true;
                    cPanelAmount.Visible = true;
                    numEdit = cAmount;

                    break;
                case TableSTLINE.PRICE:
                    cPanelUnit.Visible = true;
                    cPanelPrice.Visible = true;
                    numEdit = cPrice;

                    break;
                case TableSTLINE.TOTAL:
                    cPanelTotal.Visible = true;
                    numEdit = cTotal;

                    break;
                case TableSTLINE.DISCPER:
                    cPanelDiscount.Visible = true;
                    numEdit = cDiscount;

                    break;
                default:
                    return;
            }

            cNumInput.setNumEdit(numEdit);
            numEdit.RequestFocus();


            object cancelState = handler.row[TableSTLINE.CANCELLED];
            try
            {
                handler.row[TableSTLINE.CANCELLED] = ConstBool.yes;
                fillSchema(handler.row);
                refreshReport();

            }
            finally
            {
                ToolCell.set(handler.row, TableSTLINE.CANCELLED, cancelState);
            }


            cBtnUnit.Click += cBtnUnit_Click;

        }

        void cBtnUnit_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> list = new List<string>();

                foreach (DataRow r in _tableSchema.Rows)
                    list.Add(r[TableDUMMY.UNIT].ToString());

                ToolMsg.askList(this, list.ToArray(), delegate(object s, DialogClickEventArgs a)
                {
                    this.BindingContext.setBindingItemPosition(cUnit, a.Which);
                });

            }
            catch (Exception exc)
            {
                ToolMobile.setException(exc);
            }
        }
        protected virtual void bindToData()
        {
            InitBinding.bind(this, _tableSchema.DataSet, environment, true);
        }

        protected void userRequireSave()
        {
            if (startSave())
            {
                handler.callHandler(true);

                Close();
            }
        }
        protected bool startSave()
        {
            try
            {
                FinishDataEditing();

                if (_checkLevel)
                {
                    DataRow row = ToolRow.getFirstRealRow(_tableSchema);
                    double amount = (double)ToolCell.isNull(row[TableDUMMY.AMOUNT], 0.0);
                    double onhand = (double)ToolCell.isNull(_curMatRecord[TableDUMMY.ONHAND], 0.0);
                    if ((amount - onhand) > ConstValues.minPositive)
                    {
                        ToolMsg.show(this, MessageCollection.T_MSG_INVALID_QUANTITY, null);
                        return false;
                    }
                }



                DataRow _rowActiveUnit = this.BindingContext.getBindingItemRecord(cUnit);

                //
                if (_rowActiveUnit != null)
                {
                    switch (handler.converCol(handler.column))
                    {
                        case TableDUMMY.AMOUNT:
                            ToolCell.set(handler.row, TableSTLINE.AMOUNT, ToolRow.getFirstRealRow(_tableSchema)[TableDUMMY.AMOUNT]);
                            //
                            if (_rowActiveUnit != null)
                            {
                                ToolCell.set(handler.row, TableSTLINE.UNIT, _rowActiveUnit[TableDUMMY.UNIT]);
                                ToolCell.set(handler.row, TableSTLINE.UNITREF, _rowActiveUnit[TableDUMMY.UNITREF]);
                                ToolCell.set(handler.row, TableSTLINE.UINFO1, _rowActiveUnit[TableDUMMY.UNITCF01]);
                                ToolCell.set(handler.row, TableSTLINE.UINFO2, _rowActiveUnit[TableDUMMY.UNITCF02]);
                            }
                            //
                            return true;
                        case TableDUMMY.PRICE:
                            ToolCell.set(handler.row, TableSTLINE.PRICE, ToolRow.getFirstRealRow(_tableSchema)[TableDUMMY.PRICE]);
                            return true;
                        case TableDUMMY.DISCPER:
                            ToolCell.set(handler.row, TableSTLINE.DISCPER, ToolRow.getFirstRealRow(_tableSchema)[TableDUMMY.DISCPER]);
                            return true;
                        case TableDUMMY.TOTAL:
                            ToolCell.set(handler.row, TableSTLINE.TOTAL, ToolRow.getFirstRealRow(_tableSchema)[TableDUMMY.TOTAL]);
                            return true;
                    }
                }

            }
            catch (Exception exc)
            {
                ToolMobile.setException(exc);
            }
            return false;
        }
        //true for close
        protected override void userRequireCancel()
        {

            handler.callHandler(false);

            base.userRequireCancel();

        }

        public override void OnBackPressed()
        {
            userRequireCancel();

        }




        public override void globalRead(IEnvironment pEnv, ISettings pSettings)
        {
            base.globalRead(pEnv, pSettings);

        }

        void _tableSchema_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            try
            {
                if (e.Row.RowState != DataRowState.Detached)
                    if (!_blockPoint.isBlocked())
                    {
                        double curCf = (double)e.Row[TableDUMMY.CONVFACT];
                        double curAmount = (double)e.Row[TableDUMMY.AMOUNT];
                        double curPrice = (double)e.Row[TableDUMMY.PRICE];
                        double curTot = (double)e.Row[TableDUMMY.TOTAL];
                        double curDiscper = (double)e.Row[TableDUMMY.DISCPER];
                        switch (e.Column.ColumnName)
                        {
                            case TableDUMMY.AMOUNT:
                                refreshVal(curAmount, curCf, TableDUMMY.AMOUNT, false);
                                break;
                            case TableDUMMY.PRICE:
                                refreshVal(curPrice, curCf, TableDUMMY.PRICE, true);
                                break;
                            case TableDUMMY.TOTAL:
                                refreshVal(curTot, TableDUMMY.TOTAL);
                                break;
                            case TableDUMMY.DISCPER:
                                refreshVal(curDiscper, TableDUMMY.DISCPER);
                                break;
                        }
                    }
            }
            catch (Exception exc)
            {
                ToolMobile.setException(exc);
            }
        }

        void refreshVal()
        {
            if (!_blockPoint.isBlocked())
            {
                DataRow rowMain = ToolRow.getFirstRealRow(_tableSchema);
                refreshVal((double)rowMain[TableDUMMY.AMOUNT], (double)rowMain[TableDUMMY.CONVFACT], TableDUMMY.AMOUNT, false);
                refreshVal((double)rowMain[TableDUMMY.PRICE], (double)rowMain[TableDUMMY.CONVFACT], TableDUMMY.PRICE, true);
                refreshVal((double)rowMain[TableDUMMY.TOTAL], TableDUMMY.TOTAL);
                refreshVal((double)rowMain[TableDUMMY.DISCPER], TableDUMMY.DISCPER);
            }
        }
        void refreshVal(double pValue, string pCol)
        {
            if (_blockPoint.block())
            {
                try
                {
                    ToolColumn.setColumnValue(_tableSchema, pCol, pValue);
                }
                finally
                {
                    _blockPoint.unblock();
                }
            }
        }
        void refreshVal(double pValue, double pCf, string pCol, bool pReverse)
        {
            if (_blockPoint.block())
            {
                try
                {
                    if (pCf < ConstValues.minPositive)
                        pCf = 1.0;

                    DataRow rowMain = ToolRow.getFirstRealRow(_tableSchema);
                    double mCf = (double)rowMain[TableDUMMY.CONVFACT];
                    double mAmount = pValue * (pReverse ? (mCf / pCf) : (pCf / mCf));

                    foreach (DataRow curRow in _tableSchema.Rows)
                    {
                        double curCf = (double)curRow[TableDUMMY.CONVFACT];
                        if (curCf < ConstValues.minPositive)
                            curCf = 1.0;
                        ToolCell.set(curRow, pCol, mAmount * (pReverse ? (curCf / mCf) : (mCf / curCf)));
                    }
                }
                finally
                {
                    _blockPoint.unblock();
                }
            }
        }




        private void refreshReport()
        {
            if (environment != null && handler.report != null)
            {
                string res_ = handler.report.getResult() as string;
                if (res_ == null)
                    res_ = string.Empty;
                cData.Text = res_;
            }
            else
                cData.Text = string.Empty;
        }

        private void fillSchema(DataRow rowDocLine)
        {

            _curMatRecord = ToolStockLine.getItemData(environment, rowDocLine[TableSTLINE.STOCKREF]);

            //

            //if (_checkLevel)
            {
                //object docLRef = rowDocHeader[TableINVOICE.LOGICALREF];
                object lref = rowDocLine[TableSTLINE.STOCKREF];
                double onhand = (double)ToolCell.isNull(_curMatRecord[TableITEMS.ONHAND], 0.0);
                onhand += environment.getMatIOAmount(lref);
                //onhand += ToolStockLine.itemAmountInDoc(lref, rowDocLine.Table);
                //onhand += ToolStockLine.itemAmountInDB(environment, lref, docLRef);
                ToolCell.set(_curMatRecord, TableITEMS.ONHAND, onhand);
            }


            //
            if (handler.report != null)
            {
                handler.report.setDataSource(_curMatRecord.Table);
                handler.report.refreshSource();
            }
            //
            _tableSchema.Clear();



            string unit = string.Empty;
            double amount = (double)ToolCell.isNull(rowDocLine[TableSTLINE.AMOUNT], 0.0);
            if (ToolDouble.isZero(amount) && CurrentVersion.ENV.getEnvBool("STOCKAMOUNTONE", false))
                amount = 1.0;

            double price = (double)ToolCell.isNull(rowDocLine[TableSTLINE.PRICE], 0.0);
            double total = (double)ToolCell.isNull(rowDocLine[TableSTLINE.TOTAL], 0.0);
            double disperc = (double)ToolCell.isNull(rowDocLine[TableSTLINE.DISCPER], 0.0);
            double uinfo1 = 0.0;
            double uinfo2 = 0.0;
            DataRow newRow;
            //
            newRow = ToolRow.initTableNewRow(_tableSchema.NewRow());
            newRow[TableDUMMY.UNIT] = unit = (string)ToolCell.isNull(_curMatRecord[TableITEMS.UNIT1], string.Empty);
            newRow[TableDUMMY.UNITREF] = (string)ToolCell.isNull(_curMatRecord[TableITEMS.UNITREF1], string.Empty);
            newRow[TableDUMMY.UNITCF01] = uinfo1 = (double)ToolCell.isNull(_curMatRecord[TableITEMS.UNITCF1], 0.0);
            newRow[TableDUMMY.UNITCF02] = uinfo2 = (double)ToolCell.isNull(_curMatRecord[TableITEMS.UNITCF1], 0.0);
            newRow[TableDUMMY.CONVFACT] = (uinfo1 > ConstValues.minPositive ? uinfo2 / uinfo1 : 1.0);

            newRow[TableDUMMY.AMOUNT] = amount;
            newRow[TableDUMMY.PRICE] = price;
            newRow[TableDUMMY.TOTAL] = total;
            newRow[TableDUMMY.DISCPER] = disperc;

            _tableSchema.Rows.Add(newRow);
            //
            newRow = ToolRow.initTableNewRow(_tableSchema.NewRow());
            newRow[TableDUMMY.UNIT] = unit = (string)ToolCell.isNull(_curMatRecord[TableITEMS.UNIT2], string.Empty);
            newRow[TableDUMMY.UNITREF] = (string)ToolCell.isNull(_curMatRecord[TableITEMS.UNITREF2], string.Empty);
            newRow[TableDUMMY.UNITCF01] = uinfo1 = (double)ToolCell.isNull(_curMatRecord[TableITEMS.UNITCF1], 0.0);
            newRow[TableDUMMY.UNITCF02] = uinfo2 = (double)ToolCell.isNull(_curMatRecord[TableITEMS.UNITCF2], 0.0);
            newRow[TableDUMMY.CONVFACT] = (uinfo1 > ConstValues.minPositive ? uinfo2 / uinfo1 : 1.0);
            if ((unit != string.Empty))
                _tableSchema.Rows.Add(newRow);

            //
            newRow = ToolRow.initTableNewRow(_tableSchema.NewRow());
            newRow[TableDUMMY.UNIT] = unit = (string)ToolCell.isNull(_curMatRecord[TableITEMS.UNIT3], string.Empty);
            newRow[TableDUMMY.UNITREF] = (string)ToolCell.isNull(_curMatRecord[TableITEMS.UNITREF3], string.Empty);
            newRow[TableDUMMY.UNITCF01] = uinfo1 = (double)ToolCell.isNull(_curMatRecord[TableITEMS.UNITCF1], 0.0);
            newRow[TableDUMMY.UNITCF02] = uinfo2 = (double)ToolCell.isNull(_curMatRecord[TableITEMS.UNITCF3], 0.0);
            newRow[TableDUMMY.CONVFACT] = (uinfo1 > ConstValues.minPositive ? uinfo2 / uinfo1 : 1.0);
            if ((unit != string.Empty))
                _tableSchema.Rows.Add(newRow);
            //
            refreshVal();
            //
            //check
            //string userUnitRef = (string)ToolCell.isNull(rowDocLine[TableSTLINE.UNITREF], string.Empty);
            //for (int i = 0; i < _tableSchema.Rows.Count; ++i)
            //{
            //    DataRow row = _tableSchema.Rows[i];
            //    string unitRef = (string)row[TableDUMMY.UNITREF];
            //    if (userUnitRef == unitRef)
            //        cBoxUnits.SelectedIndex = i;
            //}
        }


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);


            _curMatRecord = null;
            _tableSchema = null;
            _blockPoint = null;
        }


        //check
        //private void c_GotFocus(object sender, EventArgs e)
        //{
        //    if (cNumInputMain.getNumEdit() != null)
        //        cNumInputMain.getNumEdit().writeBindig();
        //}






    }
}

//namespace AvaGE.FormUserEditor
//{
//    partial class MobInputItemVarsForm
//    {
//        /// <summary>
//        /// Required designer variable.
//        /// </summary>
//        private System.ComponentModel.IContainer components = null;

//        /// <summary>
//        /// Clean up any resources being used.
//        /// </summary>
//        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && (components != null))
//            {
//                components.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        #region Windows Form Designer generated code

//        /// <summary>
//        /// Required method for Designer support - do not modify
//        /// the contents of this method with the code editor.
//        /// </summary>
//        private void InitializeComponent()
//        {
//            this.cButCancel = new AvaGE.MobControl.MobButton();
//            this.cButSave = new AvaGE.MobControl.MobButton();
//            this.cNumAmount = new AvaGE.MobControl.MobNumEdit();
//            this.cBoxUnits = new AvaGE.MobControl.MobComboBox();
//            this.cLabelAmount = new AvaGE.MobControl.MobLabel();
//            this.cNumPrice = new AvaGE.MobControl.MobNumEdit();
//            this.cLabelPrice = new AvaGE.MobControl.MobLabel();
//            this.cNumInputMain = new AvaGE.MobControl.MobNumInput();
//            this.cPanelBut = new AvaGE.MobControl.MobPanel();
//            this.cData = new AvaGE.MobControl.MobTextBox();
//            this.cPanelAmount = new AvaGE.MobControl.MobPanel();
//            this.cPanelPadAmount = new AvaGE.MobControl.MobPanel();
//            this.cPanelPrice = new AvaGE.MobControl.MobPanel();
//            this.cPanelTotal = new AvaGE.MobControl.MobPanel();
//            this.cLabelTotal = new AvaGE.MobControl.MobLabel();
//            this.cNumTotal = new AvaGE.MobControl.MobNumEdit();
//            this.cPanelDiscount = new AvaGE.MobControl.MobPanel();
//            this.cLabelDiscount = new AvaGE.MobControl.MobLabel();
//            this.cNumDiscount = new AvaGE.MobControl.MobNumEdit();
//            this.cPanelBut.SuspendLayout();
//            this.cPanelAmount.SuspendLayout();
//            this.cPanelPrice.SuspendLayout();
//            this.cPanelTotal.SuspendLayout();
//            this.cPanelDiscount.SuspendLayout();
//            this.SuspendLayout();
//            // 
//            // cButCancel
//            // 
//            this.cButCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
//            this.cButCancel.Location = new System.Drawing.Point(165, 4);
//            this.cButCancel.Name = "cButCancel";
//            this.cButCancel.Size = new System.Drawing.Size(72, 20);
//            this.cButCancel.TabIndex = 2;
//            this.cButCancel.Text = "T_CANCEL";
//            this.cButCancel.Click += new System.EventHandler(this.cButCancel_Click);
//            this.cButCancel.GotFocus += new System.EventHandler(this.c_GotFocus);
//            // 
//            // cButSave
//            // 
//            this.cButSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
//            this.cButSave.Location = new System.Drawing.Point(87, 4);
//            this.cButSave.Name = "cButSave";
//            this.cButSave.Size = new System.Drawing.Size(72, 20);
//            this.cButSave.TabIndex = 1;
//            this.cButSave.Text = "T_SAVE";
//            this.cButSave.Click += new System.EventHandler(this.cButSave_Click);
//            this.cButSave.GotFocus += new System.EventHandler(this.c_GotFocus);
//            // 
//            // cNumAmount
//            // 
//            this.cNumAmount.Dock = System.Windows.Forms.DockStyle.Right;
//            this.cNumAmount.DSColumn = "";
//            this.cNumAmount.DSProperty = "Value";
//            this.cNumAmount.DSSubTable = "";
//            this.cNumAmount.DSTable = "";
//            this.cNumAmount.Increment = 0;
//            this.cNumAmount.Location = new System.Drawing.Point(182, 0);
//            this.cNumAmount.Maximum = 999999999999999;
//            this.cNumAmount.MaxLength = 0;
//            this.cNumAmount.Minimum = 0;
//            this.cNumAmount.Name = "cNumAmount";
//            this.cNumAmount.Size = new System.Drawing.Size(58, 21);
//            this.cNumAmount.TabIndex = 4;
//            this.cNumAmount.Value = 0;
//            // 
//            // cBoxUnits
//            // 
//            this.cBoxUnits.Dock = System.Windows.Forms.DockStyle.Right;
//            this.cBoxUnits.DSColumn = "";
//            this.cBoxUnits.DSProperty = "SelectedValue";
//            this.cBoxUnits.DSSubTable = "";
//            this.cBoxUnits.DSTable = "";
//            this.cBoxUnits.Location = new System.Drawing.Point(105, 0);
//            this.cBoxUnits.MaxLength = 32767;
//            this.cBoxUnits.Name = "cBoxUnits";
//            this.cBoxUnits.ReadOnly = false;
//            this.cBoxUnits.Size = new System.Drawing.Size(72, 22);
//            this.cBoxUnits.TabIndex = 5;
//            this.cBoxUnits.SelectedIndexChanged += new System.EventHandler(this.cBoxUnits_SelectedIndexChanged);
//            this.cBoxUnits.GotFocus += new System.EventHandler(this.c_GotFocus);
//            // 
//            // cLabelAmount
//            // 
//            this.cLabelAmount.Dock = System.Windows.Forms.DockStyle.Left;
//            this.cLabelAmount.DSColumn = "";
//            this.cLabelAmount.DSProperty = "Text";
//            this.cLabelAmount.DSSubTable = "";
//            this.cLabelAmount.DSTable = "";
//            this.cLabelAmount.Location = new System.Drawing.Point(0, 0);
//            this.cLabelAmount.Name = "cLabelAmount";
//            this.cLabelAmount.Size = new System.Drawing.Size(105, 22);
//            this.cLabelAmount.Text = "T_QUANTITY";
//            // 
//            // cNumPrice
//            // 
//            this.cNumPrice.Dock = System.Windows.Forms.DockStyle.Right;
//            this.cNumPrice.DSColumn = "";
//            this.cNumPrice.DSProperty = "Value";
//            this.cNumPrice.DSSubTable = "";
//            this.cNumPrice.DSTable = "";
//            this.cNumPrice.Increment = 0;
//            this.cNumPrice.Location = new System.Drawing.Point(182, 0);
//            this.cNumPrice.Maximum = 999999999999999;
//            this.cNumPrice.MaxLength = 0;
//            this.cNumPrice.Minimum = 0;
//            this.cNumPrice.Name = "cNumPrice";
//            this.cNumPrice.Size = new System.Drawing.Size(58, 21);
//            this.cNumPrice.TabIndex = 4;
//            this.cNumPrice.Value = 0;
//            // 
//            // cLabelPrice
//            // 
//            this.cLabelPrice.Dock = System.Windows.Forms.DockStyle.Left;
//            this.cLabelPrice.DSColumn = "";
//            this.cLabelPrice.DSProperty = "Text";
//            this.cLabelPrice.DSSubTable = "";
//            this.cLabelPrice.DSTable = "";
//            this.cLabelPrice.Location = new System.Drawing.Point(0, 0);
//            this.cLabelPrice.Name = "cLabelPrice";
//            this.cLabelPrice.Size = new System.Drawing.Size(105, 22);
//            this.cLabelPrice.Text = "T_PRICE";
//            // 
//            // cNumInputMain
//            // 
//            this.cNumInputMain.Dock = System.Windows.Forms.DockStyle.Top;
//            this.cNumInputMain.Location = new System.Drawing.Point(0, 88);
//            this.cNumInputMain.Name = "cNumInputMain";
//            this.cNumInputMain.Size = new System.Drawing.Size(240, 108);
//            this.cNumInputMain.TabIndex = 9;
//            // 
//            // cPanelBut
//            // 
//            this.cPanelBut.Controls.Add(this.cButCancel);
//            this.cPanelBut.Controls.Add(this.cButSave);
//            this.cPanelBut.Dock = System.Windows.Forms.DockStyle.Bottom;
//            this.cPanelBut.Location = new System.Drawing.Point(0, 241);
//            this.cPanelBut.Name = "cPanelBut";
//            this.cPanelBut.Size = new System.Drawing.Size(240, 27);
//            // 
//            // cData
//            // 
//            this.cData.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.cData.DSColumn = "";
//            this.cData.DSProperty = "Text";
//            this.cData.DSSubTable = "";
//            this.cData.DSTable = "";
//            this.cData.Font = new System.Drawing.Font("Courier New", 8F, System.Drawing.FontStyle.Regular);
//            this.cData.Location = new System.Drawing.Point(0, 196);
//            this.cData.Multiline = true;
//            this.cData.Name = "cData";
//            this.cData.Size = new System.Drawing.Size(240, 45);
//            this.cData.TabIndex = 8;
//            this.cData.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mobTextBox1_KeyDown);
//            this.cData.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mobTextBox1_KeyPress);
//            // 
//            // cPanelAmount
//            // 
//            this.cPanelAmount.Controls.Add(this.cBoxUnits);
//            this.cPanelAmount.Controls.Add(this.cPanelPadAmount);
//            this.cPanelAmount.Controls.Add(this.cNumAmount);
//            this.cPanelAmount.Controls.Add(this.cLabelAmount);
//            this.cPanelAmount.Dock = System.Windows.Forms.DockStyle.Top;
//            this.cPanelAmount.Location = new System.Drawing.Point(0, 22);
//            this.cPanelAmount.Name = "cPanelAmount";
//            this.cPanelAmount.Size = new System.Drawing.Size(240, 22);
//            // 
//            // cPanelPadAmount
//            // 
//            this.cPanelPadAmount.Dock = System.Windows.Forms.DockStyle.Right;
//            this.cPanelPadAmount.Location = new System.Drawing.Point(177, 0);
//            this.cPanelPadAmount.Name = "cPanelPadAmount";
//            this.cPanelPadAmount.Size = new System.Drawing.Size(5, 22);
//            // 
//            // cPanelPrice
//            // 
//            this.cPanelPrice.Controls.Add(this.cLabelPrice);
//            this.cPanelPrice.Controls.Add(this.cNumPrice);
//            this.cPanelPrice.Dock = System.Windows.Forms.DockStyle.Top;
//            this.cPanelPrice.Location = new System.Drawing.Point(0, 0);
//            this.cPanelPrice.Name = "cPanelPrice";
//            this.cPanelPrice.Size = new System.Drawing.Size(240, 22);
//            // 
//            // cPanelTotal
//            // 
//            this.cPanelTotal.Controls.Add(this.cLabelTotal);
//            this.cPanelTotal.Controls.Add(this.cNumTotal);
//            this.cPanelTotal.Dock = System.Windows.Forms.DockStyle.Top;
//            this.cPanelTotal.Location = new System.Drawing.Point(0, 44);
//            this.cPanelTotal.Name = "cPanelTotal";
//            this.cPanelTotal.Size = new System.Drawing.Size(240, 22);
//            // 
//            // cLabelTotal
//            // 
//            this.cLabelTotal.Dock = System.Windows.Forms.DockStyle.Left;
//            this.cLabelTotal.DSColumn = "";
//            this.cLabelTotal.DSProperty = "Text";
//            this.cLabelTotal.DSSubTable = "";
//            this.cLabelTotal.DSTable = "";
//            this.cLabelTotal.Location = new System.Drawing.Point(0, 0);
//            this.cLabelTotal.Name = "cLabelTotal";
//            this.cLabelTotal.Size = new System.Drawing.Size(105, 22);
//            this.cLabelTotal.Text = "T_TOTAL";
//            // 
//            // cNumTotal
//            // 
//            this.cNumTotal.Dock = System.Windows.Forms.DockStyle.Right;
//            this.cNumTotal.DSColumn = "";
//            this.cNumTotal.DSProperty = "Value";
//            this.cNumTotal.DSSubTable = "";
//            this.cNumTotal.DSTable = "";
//            this.cNumTotal.Increment = 0;
//            this.cNumTotal.Location = new System.Drawing.Point(182, 0);
//            this.cNumTotal.Maximum = 999999999999999;
//            this.cNumTotal.MaxLength = 0;
//            this.cNumTotal.Minimum = 0;
//            this.cNumTotal.Name = "cNumTotal";
//            this.cNumTotal.Size = new System.Drawing.Size(58, 21);
//            this.cNumTotal.TabIndex = 4;
//            this.cNumTotal.Value = 0;
//            // 
//            // cPanelDiscount
//            // 
//            this.cPanelDiscount.Controls.Add(this.cLabelDiscount);
//            this.cPanelDiscount.Controls.Add(this.cNumDiscount);
//            this.cPanelDiscount.Dock = System.Windows.Forms.DockStyle.Top;
//            this.cPanelDiscount.Location = new System.Drawing.Point(0, 66);
//            this.cPanelDiscount.Name = "cPanelDiscount";
//            this.cPanelDiscount.Size = new System.Drawing.Size(240, 22);
//            // 
//            // cLabelDiscount
//            // 
//            this.cLabelDiscount.Dock = System.Windows.Forms.DockStyle.Left;
//            this.cLabelDiscount.DSColumn = "";
//            this.cLabelDiscount.DSProperty = "Text";
//            this.cLabelDiscount.DSSubTable = "";
//            this.cLabelDiscount.DSTable = "";
//            this.cLabelDiscount.Location = new System.Drawing.Point(0, 0);
//            this.cLabelDiscount.Name = "cLabelDiscount";
//            this.cLabelDiscount.Size = new System.Drawing.Size(105, 22);
//            this.cLabelDiscount.Text = "T_DISCOUNT";
//            // 
//            // cNumDiscount
//            // 
//            this.cNumDiscount.Dock = System.Windows.Forms.DockStyle.Right;
//            this.cNumDiscount.DSColumn = "";
//            this.cNumDiscount.DSProperty = "Value";
//            this.cNumDiscount.DSSubTable = "";
//            this.cNumDiscount.DSTable = "";
//            this.cNumDiscount.Increment = 0;
//            this.cNumDiscount.Location = new System.Drawing.Point(182, 0);
//            this.cNumDiscount.Maximum = 100;
//            this.cNumDiscount.MaxLength = 0;
//            this.cNumDiscount.Minimum = 0;
//            this.cNumDiscount.Name = "cNumDiscount";
//            this.cNumDiscount.Size = new System.Drawing.Size(58, 21);
//            this.cNumDiscount.TabIndex = 4;
//            this.cNumDiscount.Value = 0;
//            // 
//            // MobInputItemVarsForm
//            // 
//            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
//            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
//            this.ClientSize = new System.Drawing.Size(240, 268);
//            this.ControlBox = false;
//            this.Controls.Add(this.cData);
//            this.Controls.Add(this.cNumInputMain);
//            this.Controls.Add(this.cPanelBut);
//            this.Controls.Add(this.cPanelDiscount);
//            this.Controls.Add(this.cPanelTotal);
//            this.Controls.Add(this.cPanelAmount);
//            this.Controls.Add(this.cPanelPrice);
//            this.Name = "MobInputItemVarsForm";
//            this.Load += new System.EventHandler(this.MobInputItemVarsForm_Load);
//            this.cPanelBut.ResumeLayout(false);
//            this.cPanelAmount.ResumeLayout(false);
//            this.cPanelPrice.ResumeLayout(false);
//            this.cPanelTotal.ResumeLayout(false);
//            this.cPanelDiscount.ResumeLayout(false);
//            this.ResumeLayout(false);

//        }

//        #endregion

//        private MobControl.MobButton cButCancel;
//        private MobControl.MobButton cButSave;
//        private MobControl.MobNumEdit cNumAmount;
//        private MobControl.MobComboBox cBoxUnits;
//        private MobControl.MobLabel cLabelAmount;
//        private MobControl.MobNumEdit cNumPrice;
//        private MobControl.MobLabel cLabelPrice;
//        private MobControl.MobPanel cPanelBut;
//        private MobControl.MobTextBox cData;
//        private MobControl.MobNumInput cNumInputMain;
//        private MobControl.MobPanel cPanelAmount;
//        private MobControl.MobPanel cPanelPrice;
//        private MobControl.MobPanel cPanelTotal;
//        private MobControl.MobLabel cLabelTotal;
//        private MobControl.MobNumEdit cNumTotal;
//        private MobControl.MobPanel cPanelDiscount;
//        private MobControl.MobLabel cLabelDiscount;
//        private MobControl.MobNumEdit cNumDiscount;
//        private AvaGE.MobControl.MobPanel cPanelPadAmount;
//    }
//}
