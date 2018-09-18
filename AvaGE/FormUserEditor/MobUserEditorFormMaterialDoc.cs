using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using AvaExt.AndroidEnv.ControlsBase;
using AvaExt.Common;
using AvaExt.Adapter.ForUser;
using AvaExt.ControlOperation;
using AvaExt.Common.Const;
using AvaExt.Manual.Table;
using AvaExt.Translating.Tools;
using AvaGE.FormDataReference;
using AvaGE.MobControl.ControlsTools;
using AvaExt.DataRefernce;
using AvaExt.ObjectSource;
using AvaExt.TableOperation;
using AvaGE.Common;
using AvaExt.TableOperation.CellAutomation;
using AvaExt.PagedSource;
using AvaExt.TableOperation.RowValidator;
using AvaExt.Adapter.Tools;
using AvaExt.Settings;
using AvaExt.MyException;
using AvaExt.Formating;
using AvaGE.MobControl;
using AvaExt.MobControl.Reporting.XmlReport;
using AvaExt.Reporting;
using AvaExt.TableOperation.RowsSelector;
using AvaExt.Adapter;
using Android.Widget;
using Android.Content;
using AvaExt.SQL;
using Android.Views;
using AvaAgent;

namespace AvaGE.FormUserEditor
{
    public class MobUserEditorFormMaterialDoc : MobUserEditorFormBase
    {

        string[] descList = new string[] { };

        string[] sourceWhList = new string[] { };
        string[] desc1List = new string[] { };
        string[] desc2List = new string[] { };
        string[] desc3List = new string[] { };

        object[] sourceWhListVal = new object[] { };
        object[] desc1ListVal = new object[] { };
        object[] desc2ListVal = new object[] { };
        object[] desc3ListVal = new object[] { };

        string desc1Label = string.Empty;
        string desc2Label = string.Empty;
        string desc3Label = string.Empty;

        string[] discountListDesc = new string[] { };
        object[] discountListVal = new object[] { };
        string[] priceListDesc = new string[] { };
        object[] priceListVal = new object[] { };

        public static readonly string __AMOUNT = ToolColumn.getColumnFullName(TableSTLINE.TABLE, TableSTLINE.AMOUNT);
        public static readonly string __UNIT = ToolColumn.getColumnFullName(TableSTLINE.TABLE, TableSTLINE.UNIT);
        public static readonly string __UNITREF = ToolColumn.getColumnFullName(TableSTLINE.TABLE, TableSTLINE.UNITREF);
        public static readonly string __UINFO1 = ToolColumn.getColumnFullName(TableSTLINE.TABLE, TableSTLINE.UINFO1);
        public static readonly string __UINFO2 = ToolColumn.getColumnFullName(TableSTLINE.TABLE, TableSTLINE.UINFO2);

        ImplObjectSourceAdapterRow _stslipRow = new ImplObjectSourceAdapterRow(null, TableINVOICE.TABLE);
        ImplObjectSourceStaticRow _rowForEdit = new ImplObjectSourceStaticRow(null);

        protected static MobInputItemVarsForm.Helper _editorVars { get { return MobInputItemVarsForm.Helper.instace; } }

        protected IRowValidator validatorLineMatOrPromo = new RowValidatorInListInt(TableSTLINE.LINETYPE, new int[] { (int)ConstLineType.material, (int)ConstLineType.promotion });

        protected bool _paramCorrectNegStock = false;
        protected bool _paramControlNegStock = true;
        protected bool _paramStocAddIgnoreZero = true;
        protected bool _paramUseMatBarcode = false;

        IReport _report;

        bool IsUseMatBarcode
        {
            get
            {

                return _paramUseMatBarcode;

            }

            set
            {

                _paramUseMatBarcode = value;

                cBtnDoUseBarcode.SetBackgroundResource(IsUseMatBarcode ? Resource.Drawable.ic_action_cast_light : Resource.Drawable.ic_action_cast);


            }



        }


        protected virtual bool controlParameter(StockDocParameters pPar)
        {
            if (pPar == StockDocParameters.stockLevel && stockAdapter != null)
            {
                if (ToolType.isEqual(stockAdapter.getCancelled(), ConstBool.yes))
                    return false;
            }
            return true;
        }


        protected virtual bool isNegControl()
        {
            if (!_paramControlNegStock)
                return false;

            return controlParameter(StockDocParameters.stockLevel);

        }

        //protected override string globalStoreName()
        //{
        //    return "form.edit.matdoc";
        //}

        AdapterUserStockDocument stockAdapter;
        public override void setAdapter(EditingTools pAdapter)
        {
            base.setAdapter(pAdapter);

            stockAdapter = userAdapter as AdapterUserStockDocument;
            _stslipRow.setAdapter(userAdapter);

        }



        MobDataGrid cGrid { get { return FindViewById<MobDataGrid>(Resource.Id.cGrid); } }
        MobDataGrid cGrid2 { get { return FindViewById<MobDataGrid>(Resource.Id.cGrid2); } }


        MobTextBox cDate { get { return FindViewById<MobTextBox>(Resource.Id.cDate); } }
        MobTextBox cDesc { get { return FindViewById<MobTextBox>(Resource.Id.cDesc); } }


        MobTextBox cSourceWh { get { return FindViewById<MobTextBox>(Resource.Id.cSourceWh); } }
        MobTextBox cTextExt1 { get { return FindViewById<MobTextBox>(Resource.Id.cTextExt1); } }
        MobTextBox cTextExt2 { get { return FindViewById<MobTextBox>(Resource.Id.cTextExt2); } }
        MobTextBox cTextExt3 { get { return FindViewById<MobTextBox>(Resource.Id.cTextExt3); } }

        MobLabel cLabelSourceWh { get { return FindViewById<MobLabel>(Resource.Id.cLabelSourceWh); } }
        MobLabel cLabelExt1 { get { return FindViewById<MobLabel>(Resource.Id.cLabelExt1); } }
        MobLabel cLabelExt2 { get { return FindViewById<MobLabel>(Resource.Id.cLabelExt2); } }
        MobLabel cLabelExt3 { get { return FindViewById<MobLabel>(Resource.Id.cLabelExt3); } }

        MobTextBox cName { get { return FindViewById<MobTextBox>(Resource.Id.cName); } }
        MobTextBox cCode { get { return FindViewById<MobTextBox>(Resource.Id.cCode); } }
        MobTextBox cBarcode { get { return FindViewById<MobTextBox>(Resource.Id.cBarcode); } }

        //MobTextBox cWarehouse { get { return FindViewById<MobTextBox>(Resource.Id.cWh); } }

        MobNumEdit cDiscount { get { return FindViewById<MobNumEdit>(Resource.Id.cDiscount); } }
        MobTextBox cPriceList { get { return FindViewById<MobTextBox>(Resource.Id.cPriceList); } }



        MobButton cBtnDate { get { return FindViewById<MobButton>(Resource.Id.cBtnDate); } }
        MobButton cBtnName { get { return FindViewById<MobButton>(Resource.Id.cBtnName); } }
        MobButton cBtnCode { get { return FindViewById<MobButton>(Resource.Id.cBtnCode); } }

        MobButton cBtnDiscount { get { return FindViewById<MobButton>(Resource.Id.cBtnDiscount); } }
        MobButton cBtnPriceList { get { return FindViewById<MobButton>(Resource.Id.cBtnPriceList); } }

        MobButton cBtnDesc { get { return FindViewById<MobButton>(Resource.Id.cBtnDesc); } }
        //MobButton cBtnWh { get { return FindViewById<MobButton>(Resource.Id.cBtnWh); } }


        MobButton cBtnSourceWh { get { return FindViewById<MobButton>(Resource.Id.cBtnSourceWh); } }
        MobButton cBtnTextExt1 { get { return FindViewById<MobButton>(Resource.Id.cBtnTextExt1); } }
        MobButton cBtnTextExt2 { get { return FindViewById<MobButton>(Resource.Id.cBtnTextExt2); } }
        MobButton cBtnTextExt3 { get { return FindViewById<MobButton>(Resource.Id.cBtnTextExt3); } }

        MobButton cBtnDoDelete { get { return FindViewById<MobButton>(Resource.Id.cBtnDoDelete); } }
        MobButton cBtnDoUseBarcode { get { return FindViewById<MobButton>(Resource.Id.cBtnDoUseBarcode); } }
        MobButton cBtnDoAddPromoGlob { get { return FindViewById<MobButton>(Resource.Id.cBtnDoAddPromoGlob); } }
        MobButton cBtnDoAddPromo { get { return FindViewById<MobButton>(Resource.Id.cBtnDoAddPromo); } }
        MobButton cBtnDoAdd { get { return FindViewById<MobButton>(Resource.Id.cBtnDoAdd); } }

        protected MobPanel cPahelDocInfo { get { return FindViewById<MobPanel>(Resource.Id.cPanelDocInfo); } }
        protected MobPanel cPanelFin { get { return FindViewById<MobPanel>(Resource.Id.cPanelFin); } }
        // protected MobPanel cPanelWh { get { return FindViewById<MobPanel>(Resource.Id.cPanelWh); } }

        protected MobPanel cPanelSourceWh { get { return FindViewById<MobPanel>(Resource.Id.cPanelSourceWh); } }
        MobPanel cPanelPageMain { get { return FindViewById<MobPanel>(Resource.Id.cPanelPageMain); } }
        MobPanel cPanelPageData { get { return FindViewById<MobPanel>(Resource.Id.cPanelPageData); } }
        MobPanel cPanelPageExt { get { return FindViewById<MobPanel>(Resource.Id.cPanelPageExt); } }

        MobTabControl cPages { get { return FindViewById<MobTabControl>(Resource.Id.cPages); } }

        public MobUserEditorFormMaterialDoc(IEnvironment pEnv, int pLayout)
            : base(pEnv, pLayout <= 0 ?
          (CurrentVersion.ENV.getEnvBool("STOCKSIMPLEFORM", false) ? Resource.Layout.MobUserEditorFormMaterialDocShort : Resource.Layout.MobUserEditorFormMaterialDoc)
            : pLayout)
        {


            //

        }


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);


            _stslipRow = null;
            _rowForEdit = null;
            stockAdapter = null;
        }
        protected override void reinitBindingProperties()
        {
            #region ds


            if (this.cDate != null)
            {
                this.cDate.DSColumn = "DATE_";
                this.cDate.DSTable = "INVOICE";
            }

            if (this.cDesc != null)
            {
                this.cDesc.DSColumn = "GENEXP1";
                this.cDesc.DSTable = "INVOICE";
            }

            if (this.cSourceWh != null)
            {
                this.cSourceWh.DSColumn = "SOURCEINDEX";
                this.cSourceWh.DSTable = "INVOICE";
            }

            if (this.cTextExt1 != null)
            {
                this.cTextExt1.DSColumn = "GENEXP2";
                this.cTextExt1.DSTable = "INVOICE";
            }
            if (this.cTextExt2 != null)
            {
                this.cTextExt2.DSColumn = "GENEXP3";
                this.cTextExt2.DSTable = "INVOICE";
            }
            if (this.cTextExt3 != null)
            {
                this.cTextExt3.DSColumn = "GENEXP4";
                this.cTextExt3.DSTable = "INVOICE";
            }
            if (this.cPriceList != null)
            {
                this.cPriceList.DSColumn = "PRCLIST";
                this.cPriceList.DSTable = "INVOICE";
            }
            if (this.cDiscount != null)
            {
                this.cDiscount.DSColumn = "DISCPER";
                this.cDiscount.DSTable = "INVOICE";
            }

            if (this.cName != null)
            {
                this.cName.DSColumn = "DEFINITION_";
                this.cName.DSSubTable = "CLCARD";
                this.cName.DSTable = "INVOICE";
            }
            if (this.cCode != null)
            {
                this.cCode.DSColumn = "CODE";
                this.cCode.DSSubTable = "CLCARD";
                this.cCode.DSTable = "INVOICE";
            }

            if (this.cBarcode != null)
            {
                this.cBarcode.DSColumn = "BARCODE";
                this.cBarcode.DSSubTable = "CLCARD";
                this.cBarcode.DSTable = "INVOICE";
            }
            //  if (this.cWarehouse != null)
            //{
            //this.cWarehouse.DSColumn = "SOURCEWHNAME";
            //this.cWarehouse.DSSubTable = "DUMMY";
            //this.cWarehouse.DSTable = "INVOICE";
            //}




            #endregion

        }



        bool canChangeWh()
        {
            return environment.getSysSettings().getBool("MOB_SOURCEINDEX_CHANGE_" + getId());

        }


        public override void reinitEditingForData()
        {
            base.reinitEditingForData();

            var _canChangeWh = canChangeWh();

            if (cPanelSourceWh != null)
            {
                cPanelSourceWh.Visible = _canChangeWh;
            }


            if (cPages != null)
            {
                //////////////////////////////////////////////////////////////////////////////
                cPages.Setup();


                TabHost.TabSpec tab1_ = cPages.NewTabSpec(cPanelPageMain.Name);
                tab1_.SetIndicator(translate(WordCollection.T_MAIN));
                tab1_.SetContent(cPanelPageMain.Id);
                cPages.AddTab(tab1_);

                TabHost.TabSpec tab2_ = cPages.NewTabSpec(cPanelPageData.Name);
                tab2_.SetIndicator(translate(WordCollection.T_DATA));
                tab2_.SetContent(cPanelPageData.Id);
                cPages.AddTab(tab2_);

                TabHost.TabSpec tab3_ = cPages.NewTabSpec(cPanelPageExt.Name);
                tab3_.SetIndicator(translate(WordCollection.T_ADDITIONAL));
                tab3_.SetContent(cPanelPageExt.Id);
                cPages.AddTab(tab3_);

                //////////////////////////////////////////////////////////////////////////////
            }

            DataTable stslip = stockAdapter.getDataSet().Tables[TableINVOICE.TABLE];
            DataTable stline = stockAdapter.getDataSet().Tables[TableSTLINE.TABLE];
            IRowSource stslipRow = new ImplObjectSourceAdapterRow(stockAdapter, TableINVOICE.TABLE);
            ////


            //
            ICellReltions cellBindingLines = new ImplCellReltions(stline);
            CellAutomationDB dbAutomation;
            dbAutomation = new CellAutomationDB(stline, new PagedSourcePriceListPlantMagic(environment),
               new string[] { TableSTLINE.STOCKREF, TableSTLINE.PRCLIST },
               new string[] { TableITEMS.LOGICALREF, PagedSourcePriceListPlantMagic.EXTPRICEID },
               new string[] { TableSTLINE.PRICE },
               new string[] { PagedSourcePriceListPlantMagic.EXTPRICECOL },
               UpdateTypeFlags.activeOnRelColumn | UpdateTypeFlags.disableEditCancel | UpdateTypeFlags.alwaysIncludeRelColumn | UpdateTypeFlags.updateIgnoreRelColumn,
               validatorLineMatOrPromo);

            cellBindingLines.addRelation(dbAutomation.getTriggerColumns(), dbAutomation, validatorLineMatOrPromo);

            ///////////////////////////////////////////////////////////////////////////////
            _paramControlNegStock = environment.getAppSettings().getBool(SettingsNamesApp.MOB_CONTROL_NEGATIVE_STOCK_B, _paramControlNegStock);

            _paramCorrectNegStock = environment.getAppSettings().getBool(SettingsNamesApp.MOB_CORRECT_NEGATIVE_STOCK_B, _paramCorrectNegStock);

            _paramStocAddIgnoreZero = settings.getBool(SettingsAvaAgent.MOB_STOCK_ADD_IGNORE_ZERO_B, _paramStocAddIgnoreZero);
            IsUseMatBarcode = settings.getBool(SettingsAvaAgent.MOB_USE_MATBARCODE_B, _paramUseMatBarcode);
            if (cBarcode != null)
            {
                cBarcode.MaxLength = environment.getColumnLen(TableCLCARD.TABLE, TableCLCARD.BARCODE);
                cBarcode.KeyPress += cBarcode_KeyPress;
            }


            cBtnDoAddPromo.Visible = cBtnDoAddPromoGlob.Visible = settings.getBool(SettingsAvaAgent.MOB_USE_PROMOTION_B);
            if (cDate != null)
                cDate.Enabled = cBtnDate.Enabled = settings.getBool(SettingsAvaAgent.MOB_EDIT_DATE_B);

            //


            loadReport();



            ///////////////////////////////////////////////


            IDataReference refer;

            if (cDate != null)
            {

                refer = environment.getReference(ConstRefCode.date);
                cBtnDate.activity = new BindDataRefenceAsActivity(refer,
                _stslipRow,
                TableDUMMY.DATETIME,
                cDate,
                new string[] { TableDUMMY.DATETIME },
                new string[] { TableINVOICE.DATE_ });
            }

            refer = environment.getReference(ConstRefCode.client);
            if (cBtnName != null)
            {
                cBtnName.activity = new BindDataRefenceAsActivity(refer,
                    _stslipRow,
                    TableCLCARD.DEFINITION_,
                    new ImplObjectSourceControlText(cName),
                    new string[] { TableCLCARD.LOGICALREF },
                    new string[] { TableINVOICE.CLIENTREF });
            }

            if (cBtnCode != null)
            {
                cBtnCode.activity = new BindDataRefenceAsActivity(refer,
                    _stslipRow,
                    TableCLCARD.CODE,
                    new ImplObjectSourceControlText(cCode),
                    new string[] { TableCLCARD.LOGICALREF },
                    new string[] { TableINVOICE.CLIENTREF });
            }


            if (cDiscount != null)
            {
                bool editDsc = settings.getBool(SettingsAvaAgent.MOB_EDIT_DISCOUNT_DOC_B, false);


                cDiscount.Enabled = false;

                if (editDsc)
                {
                    refer = environment.getReference(ConstRefCode.numberPercent);

                    cBtnDiscount.activity = new BindDataRefenceAsActivity(refer,
                        _stslipRow,
                        TableDUMMY.VALUE,
                        cDiscount,
                        new string[] { TableDUMMY.VALUE },
                        new string[] { TableINVOICE.DISCPER });
                }
                else
                {
                    //from list

                }

            }


            if (cDesc != null)
            {
                bool editDsc = settings.getBool(SettingsAvaAgent.MOB_EDIT_DESC_B, true);

                cDesc.Enabled = editDsc;

            }

            if (cPriceList != null)
            {

                cPriceList.Enabled = false;
            }



            ////////////////////////////////////////////////////////

            cGrid.DataSource = stline;
            cGrid2.DataSource = stslip;

            stslip.ColumnChanged += new DataColumnChangeEventHandler(stslip_ColumnChanged);
            //
            refreshFin(false);
            //
            if (cDiscount != null)
            {
                cDiscount.Minimum = 0;
                cDiscount.Maximum = 100;//percent
            }
            //

            cBtnDoDelete.Click += cBtnDoDelete_Click;
            cBtnDoUseBarcode.Click += cBtnDoUseBarcode_Click;
            cBtnDoAddPromoGlob.Click += cBtnDoAddPromoGlob_Click;
            cBtnDoAddPromo.Click += cBtnDoAddPromo_Click;
            cBtnDoAdd.Click += cBtnDoAdd_Click;

            cGrid.RowSelected += cGrid_RowSelected;

            /////////////////////////////////////////////////////////////
            if (cBtnDesc != null)
            {
                cBtnDesc.Enabled = (descList != null && descList.Length > 0);
                if (cBtnDesc.Enabled)
                    cBtnDesc.Click += cBtnDesc_Click;
            }



            if (cPanelSourceWh != null)
            {
                if (_canChangeWh)
                {
                    if (cBtnSourceWh.Enabled)
                        cBtnSourceWh.Click += cBtnSourceWh_Click;
                }
            }

            if (cSourceWh != null)
            {
                cSourceWh.Enabled = false;
            }

            if (cLabelExt1 != null)
            {
                if (!string.IsNullOrEmpty(desc1Label))
                    cLabelExt1.Text = desc1Label;
            }

            if (cLabelExt1 != null)
            {
                cBtnTextExt1.Enabled = (desc1List != null && desc1List.Length > 0);
                if (cBtnTextExt1.Enabled)
                    cBtnTextExt1.Click += cBtnTextExt1_Click;
            }
            if (cLabelExt2 != null)
            {
                if (!string.IsNullOrEmpty(desc2Label))
                    cLabelExt2.Text = desc2Label;
            }

            if (cBtnTextExt2 != null)
            {
                cBtnTextExt2.Enabled = (desc2List != null && desc2List.Length > 0);
                if (cBtnTextExt2.Enabled)
                    cBtnTextExt2.Click += cBtnTextExt2_Click;
            }

            if (cLabelExt3 != null)
            {
                if (!string.IsNullOrEmpty(desc3Label))
                    cLabelExt3.Text = desc3Label;
            }

            if (cBtnTextExt3 != null)
            {
                cBtnTextExt3.Enabled = (desc3List != null && desc3List.Length > 0);
                if (cBtnTextExt3.Enabled)
                    cBtnTextExt3.Click += cBtnTextExt3_Click;
            }

            /////////////////////////////////////////////////////////////

            cGrid.CellClick += cGrid_CellClick;
            /////////////////////////////////////////////////////////////
            if (cBtnDiscount != null)
                cBtnDiscount.Click += cBtnDiscount_Click;

            if (cBtnPriceList != null)
                cBtnPriceList.Click += cBtnPriceList_Click;

            /////////////////////////////////////////////////////////////
            //binding
            /////////////////////////////////////////////////////////////
            {
                var b = this.BindingContext.getBindingItem(cPriceList);
                if (b != null)
                {
                    b.isReadOnly = true;
                    b.ConverterFormat = new ObjectConverterByHandler((o) =>
                    {
                        if (ToolType.isEqual(o, 0))
                            return string.Empty;
                        return getDescFromList(priceListDesc, priceListVal, o);
                    });
                    b.read();
                }

            }

            /////////////////////////////////////////////////////////////
        }

        void cBarcode_KeyPress(object sender, Android.Views.View.KeyEventArgs e)
        {
            if (ToolControl.isDone(e.KeyCode, e.Event.Number))
            {
                e.Handled = true;

                var v = this.BindingContext.getBindingItem(cBarcode);
                if (v != null)
                    v.writeIfChanged();

            }

            e.Handled = false;
        }

        void loadReport()
        {
            string node_ = "EDITOR_REPORT";

            string name_ = globalStoreName() + "/" + node_;

            _report = environment.getStateRuntime(name_) as IReport;

            if (_report == null)
            {
                //Init editing report
                string repObj = settings.getString(node_);
                if (repObj == string.Empty)
                    throw new MyExceptionError(MessageCollection.T_MSG_ERROR_INVALID_VAR, new object[] { "EDITOR_REPORT" });
                _report = new ImplXmlReport(repObj, environment);
                environment.setStateRuntime(name_, _report);
            }
        }

        void cGrid_CellClick(object sender, MobDataGrid.EventArgsGrid e)
        {
            activateCell(e.Row, e.Column);
        }





        void cBtnPriceList_Click(object sender, EventArgs e)
        {

            setFromList(priceListDesc, priceListVal, TableINVOICE.PRCLIST);


        }

        void cBtnDiscount_Click(object sender, EventArgs e)
        {

            if (cBtnDiscount.activity == null)//if not editable but list not empty
                setFromList(discountListDesc, discountListVal, TableINVOICE.DISCPER);


        }


        void cBtnDesc_Click(object sender, EventArgs e)
        {

            setFromList(descList, descList, TableINVOICE.GENEXP1);
        }
        void cBtnSourceWh_Click(object sender, EventArgs e)
        {
            setFromList(sourceWhList, sourceWhListVal, TableINVOICE.SOURCEINDEX);
        }
        void cBtnTextExt1_Click(object sender, EventArgs e)
        {
            setFromList(desc1List, desc1ListVal, TableINVOICE.GENEXP2);
        }
        void cBtnTextExt2_Click(object sender, EventArgs e)
        {
            setFromList(desc2List, desc2ListVal, TableINVOICE.GENEXP3);
        }
        void cBtnTextExt3_Click(object sender, EventArgs e)
        {
            setFromList(desc3List, desc3ListVal, TableINVOICE.GENEXP4);
        }

        string getDescFromList(string[] pListDesc, object[] pListVal, object pValue)
        {
            if (pValue != null && pListDesc != null && pListVal != null && pListDesc.Length > 0 && pListVal.Length > 0)
            {
                int x = Math.Min(pListVal.Length, pListDesc.Length);
                for (int i = 0; i < x; ++i)
                {
                    if (ToolType.isEqual(pListVal[i], pValue))
                        return pListDesc[i];
                }

            }

            return null;
        }


        void setFromList(string[] pListDesc, object[] pListVal, string pCol)
        {
            try
            {
                if (pListDesc != null && pListDesc.Length > 0)
                    ToolMsg.askList(this, pListDesc, delegate(object s, DialogClickEventArgs a)
                    {
                        if (a.Which >= 0 && a.Which < pListVal.Length)
                        {
                            object val_ = pListVal[a.Which];

                            ToolCell.set(_stslipRow.get(), pCol, val_);

                        }
                    });

            }
            catch (Exception exc)
            {
                ToolMobile.setException(exc);
            }

        }


        void cGrid_RowSelected(object sender, MobDataGrid.EventArgsGrid e)
        {
            //ok with popup menu not buttons
            //DataRow row = cGrid.getGridCurDataRow();

            //cBtnDoAddPromo.Enabled = (row != null && ToolStockLine.isLineMaterial(row));
            //cBtnDoDelete.Enabled = (row != null);
        }






        private void cBtnDoDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow row = cGrid.ActiveRow;
                if (row != null)
                    ToolMsg.confirm(this, MessageCollection.T_MSG_COMMIT_DELETE, () => { row.Delete(); }, null);
            }
            catch (Exception exc)
            {
                ToolMobile.setException(exc);
            }

        }
        void cBtnDoUseBarcode_Click(object sender, EventArgs e)
        {
            IsUseMatBarcode = !IsUseMatBarcode;
        }

        private void cBtnDoAdd_Click(object sender, EventArgs e)
        {
            addMat();
        }

        private void cBtnDoAddPromoGlob_Click(object sender, EventArgs e)
        {
            addPromo(null);
        }

        private void cBtnDoAddPromo_Click(object sender, EventArgs e)
        {
            DataRow parent = getActiveRowMaterial();
            if (parent != null)
                addPromo(parent);
        }








        protected override void initBeforeSettings()
        {
            base.initBeforeSettings();



            initLists(); //Status boxes

            //


        }



        void stslip_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            switch (e.Column.ColumnName)
            {
                case TableINVOICE.CLIENTREF:
                    refreshFin(true);
                    break;
            }

        }

        protected virtual void refreshFin(bool setDefault)
        {
            DataTable stfiche = stockAdapter.getDataSet().Tables[TableINVOICE.TABLE];
            DataRow rowDocHeader = ToolRow.getFirstRealRow(stfiche);
            double defDisc = 0.0;
            int defPlist = 0;
            string clRef = (string)rowDocHeader[TableINVOICE.CLIENTREF];




            IPagedSource ps = new PagedSourceClient(environment);
            ps.getBuilder().addParameterValue(TableCLCARD.LOGICALREF, clRef);
            DataRow clRec = ToolRow.getFirstRealRow(ps.get());

            //clear if empty
            if (clRef == string.Empty || clRec == null)
            {


                // cNumButDiscPerc.getComoBox().DataSource = null;
                //cBoxPlist.DataSource = null;



                ToolCell.set(rowDocHeader, TableINVOICE.DISCPER, 0);
                ToolCell.set(rowDocHeader, TableINVOICE.PRCLIST, 0);
                return;
            }

            //init
            string listDiscFromDb = XmlFormating.helper.format(ToolCell.isNull(clRec[TableCLCARD.DISCPER], 0));
            string listPlistFromDb = XmlFormating.helper.format(ToolCell.isNull(clRec[TableCLCARD.PRCLIST], 0));


            double[] arrDiscDoc = getDiscounts();
            double[] arrDiscCl = ToolString.strToDoubleArr(listDiscFromDb);


            //discount
            {
                List<double> list = new List<double>();

                foreach (double disc in arrDiscCl)
                    if (!list.Contains(disc))
                        list.Add(disc);

                foreach (double disc in arrDiscDoc)
                    if (!list.Contains(disc))
                        list.Add(disc);

                if (list.Count > 0)
                    defDisc = list[0];

                // cNumButDiscPerc.getComoBox().ValueMember = cNumButDiscPerc.getComoBox().DisplayMember = TableDUMMY.VALUE;
                //DataTable table = new DataTable();
                //table.Columns.Add(TableDUMMY.VALUE, typeof(double));
                //foreach (double disc in list)
                //{
                //    DataRow newRow = table.NewRow();
                //    table.Rows.Add(newRow);
                //    newRow[TableDUMMY.VALUE] = disc;
                //}
                // cNumButDiscPerc.getComoBox().DataSource = table;

                discountListDesc = new string[list.Count];
                discountListVal = new object[list.Count];
                for (int i = 0; i < list.Count; ++i)
                {
                    discountListDesc[i] = list[i].ToString();
                    discountListVal[i] = list[i];
                }

            }

            int[] arrPlistDoc = getPriceLists();
            int[] arrPlistCl = ToolString.strToIntArr(listPlistFromDb);

            {
                List<int> list = new List<int>(arrPlistCl);

                foreach (int plId in arrPlistCl)
                    if (!list.Contains(plId))
                        list.Add(plId);

                foreach (int plId in arrPlistDoc)
                    if (!list.Contains(plId))
                        list.Add(plId);

                if (list.Count > 0)
                    defPlist = list[0];

                //cBoxPlist.DisplayMember = TableDUMMY.NAME;
                //cBoxPlist.ValueMember = TableDUMMY.VALUE;

                string strListPlistDesc = environment.getSysSettings().getString(SettingsSysMob.MOB_USR_DESC_PLIST, "1,T_MAIN");
                IDictionary<string, string> dic = ToolString.explodeForParameters(strListPlistDesc);

                //DataTable table = new DataTable();
                //table.Columns.Add(TableDUMMY.NAME, typeof(string));
                //table.Columns.Add(TableDUMMY.VALUE, typeof(int));

                //foreach (int plId in list)
                //{
                //    string pDesc = plId.ToString();
                //    if (dic.ContainsKey(plId.ToString()))
                //        pDesc = environment.translate(dic[plId.ToString()]);

                //    DataRow newRow = table.NewRow();
                //    table.Rows.Add(newRow);
                //    newRow[TableDUMMY.NAME] = pDesc;
                //    newRow[TableDUMMY.VALUE] = plId;
                //}

                //cBoxPlist.DataSource = table;

                priceListDesc = new string[list.Count];
                priceListVal = new object[list.Count];
                for (int i = 0; i < list.Count; ++i)
                {
                    int plId = list[i];

                    string pDesc = plId.ToString();
                    if (dic.ContainsKey(plId.ToString()))
                        pDesc = environment.translate(dic[plId.ToString()]);

                    priceListDesc[i] = pDesc;
                    priceListVal[i] = plId;
                }

            }

            if (setDefault)
            {
                ToolCell.set(rowDocHeader, TableINVOICE.DISCPER, defDisc);
                ToolCell.set(rowDocHeader, TableINVOICE.PRCLIST, defPlist);
            }


            //foreach (Binding b in cNumButDiscPerc.DataBindings)
            //    b.ReadValue();
            //foreach (Binding b in cBoxPlist.DataBindings)
            //    b.ReadValue();

        }

        void activateCell(DataRow pRow, MobDataGrid.Column pColumn)
        {
            try
            {
                if (pRow == null || pColumn == null)
                    return;



                string colClick_ = pColumn.Code;
                string colEditor_ = _editorVars.converCol(colClick_);

                switch (colEditor_)
                {
                    case TableSTLINE.AMOUNT:
                        _editorVars.edit(this, pRow, colClick_, isNegControl(), _report, null, null);
                        break;
                    case TableSTLINE.PRICE:
                        if (settings.getBool(SettingsAvaAgent.MOB_EDIT_PRICE_B))
                            _editorVars.edit(this, pRow, colClick_, false, _report, null, null);
                        break;
                    case TableSTLINE.TOTAL:
                        if (settings.getBool(SettingsAvaAgent.MOB_EDIT_TOTAL_B))
                            _editorVars.edit(this, pRow, colClick_, false, _report, null, null);
                        break;
                    case TableSTLINE.DISCPER:
                        if (!ToolStockLine.isLinePromotion(pRow))
                            if (settings.getBool(SettingsAvaAgent.MOB_EDIT_DISCOUNT_MAT_B))
                                _editorVars.edit(this, pRow, colClick_, false, _report, null, null);
                        break;
                }
            }
            catch (Exception exc)
            {
                ToolMobile.setException(exc);
            }


        }


        public override void startSave()
        {
            base.startSave();


            DataTable stslip = stockAdapter.getDataSet().Tables[TableINVOICE.TABLE];
            DataTable stline = stockAdapter.getDataSet().Tables[TableSTLINE.TABLE];

            //Header
            checkDoc(stslip, stline);
            //Lines
            deleteEmptyLines(stline);
            checkStockLevel(stslip, stline);
            deleteEmptyLines(stline);
            //Header
            checkDoc(stslip, stline);

        }
        void checkDoc(DataTable stslip, DataTable stline)
        {
            bool isCancelled = false;

            foreach (DataRow rowCurent in stslip.Rows)
                if (rowCurent.RowState != DataRowState.Deleted)
                    isCancelled = ((short)ToolCell.isNull(rowCurent[TableINVOICE.CANCELLED], (short)ConstBool.yes) == (short)ConstBool.yes);

            foreach (DataRow rowCurent in stslip.Rows)
                if (rowCurent.RowState != DataRowState.Deleted)
                {
                    string[] arrReqCols = ToolString.explodeList(environment.getSysSettings().getString("MOB_REQCOLS_" + getId()));
                    foreach (string col in arrReqCols)
                        if (col != string.Empty && rowCurent.Table.Columns.Contains(col))
                        {
 
                            var val = rowCurent[col];
 
                            if (
                                ToolCell.isNull(val) ||
                                ToolType.isEqual(0, val) ||
                                ToolType.isEqual(string.Empty, val)
                                //TODO add date compare
                                )
                                throw new MyBaseException(MessageCollection.T_MSG_SET_REQFIELDS);


                            //string val = ToolCell.isNull(rowCurent, col, string.Empty).ToString().Trim();
                            //if (val == string.Empty)
                            //    throw new MyBaseException(MessageCollection.T_MSG_SET_REQFIELDS);
                        }
                    if (!CurrentVersion.ENV.isZeroDocAllowed())
                    {
                        if (!isCancelled)
                            if (controlParameter(StockDocParameters.total))
                                if ((double)ToolCell.isNull(rowCurent[TableINVOICE.NETTOTAL], 0.0) < ConstValues.minPositive)
                                {
                                    throw new MyBaseException(MessageCollection.T_MSG_EMPTY_DOC);
                                }
                    }
                    if (controlParameter(StockDocParameters.client))
                        if ((string)ToolCell.isNull(rowCurent[TableINVOICE.CLIENTREF], string.Empty) == string.Empty)
                        {
                            throw new MyBaseException(MessageCollection.T_MSG_SET_CLIENT);
                        }
                }
        }
        void checkStockLevel(DataTable stslip, DataTable stline)
        {
            Dictionary<string, double> _levelDic = new Dictionary<string, double>();
            object docLRef = ToolColumn.getColumnLastValue(stslip, TableINVOICE.LOGICALREF, string.Empty);
            DataRow rowLastMat = null;
            int recIndx = 0;
            for (int rowIndx = 0; rowIndx < stline.Rows.Count; ++rowIndx)
            {
                DataRow rowCurent = stline.Rows[rowIndx];
                if (rowCurent.RowState != DataRowState.Deleted)
                {
                    ++recIndx;
                    if (ToolStockLine.isLineMaterial(rowCurent))
                        rowLastMat = rowCurent;
                    //
                    object matLref = rowCurent[TableSTLINE.STOCKREF];
                    double amount = (double)rowCurent[TableSTLINE.AMOUNT];

                    string[] arrDesc = new string[] { 
                        recIndx.ToString(),
                        (string)rowCurent[ToolColumn.getColumnFullName(TableITEMS.TABLE,TableITEMS.CODE)],
                        (string)rowCurent[ToolColumn.getColumnFullName(TableITEMS.TABLE,TableITEMS.NAME)]
                        };
                    string[] arrDescShort = new string[] {  
                        (string)rowCurent[ToolColumn.getColumnFullName(TableITEMS.TABLE,TableITEMS.CODE)],
                        (string)rowCurent[ToolColumn.getColumnFullName(TableITEMS.TABLE,TableITEMS.NAME)]
                        };

                    if (isNegControl())
                    {
                        DataRow matData = ToolStockLine.getItemData(environment, matLref);

                        double onhand = (double)ToolCell.isNull(matData[TableITEMS.ONHAND], 0.0);
                        onhand += environment.getMatIOAmount(matLref);

                        if (onhand < -ConstValues.minPositive)
                        {
                            if (!_paramCorrectNegStock)
                                throw new MyBaseException(MessageCollection.T_MSG_STOCK_NEGATIVE_LEVEL, arrDescShort);
                            else
                            {
                                if (_levelDic.ContainsKey(matLref.ToString()))
                                    _levelDic.Remove(matLref.ToString());
                                _levelDic.Add(matLref.ToString(), onhand);
                            }
                        }
                    }

                }
            }

            if (_paramCorrectNegStock)
            {
                #region Promo
                for (int rowIndx = stline.Rows.Count - 1; rowIndx >= 0; --rowIndx)
                {
                    DataRow rowCurent = stline.Rows[rowIndx];
                    if (rowCurent.RowState != DataRowState.Deleted)
                        if (ToolStockLine.isLinePromotion(rowCurent))
                        {
                            string matLref = (string)rowCurent[TableSTLINE.STOCKREF];
                            if (_levelDic.ContainsKey(matLref))
                            {
                                double amount = (double)rowCurent[TableSTLINE.AMOUNT];
                                double onhand = _levelDic[matLref];

                                double newAmount = Math.Max(0, amount + onhand);
                                double newOnhand = (amount + onhand) - newAmount;

                                ToolCell.set(rowCurent, TableSTLINE.AMOUNT, newAmount);
                                _levelDic[matLref] = newOnhand;
                            }
                        }
                }
                #endregion
                #region Material
                for (int rowIndx = stline.Rows.Count - 1; rowIndx >= 0; --rowIndx)
                {
                    DataRow rowCurent = stline.Rows[rowIndx];
                    if (rowCurent.RowState != DataRowState.Deleted)
                        if (ToolStockLine.isLineMaterial(rowCurent))
                        {
                            string matLref = (string)rowCurent[TableSTLINE.STOCKREF];
                            if (_levelDic.ContainsKey(matLref))
                            {
                                double amount = (double)rowCurent[TableSTLINE.AMOUNT];
                                double onhand = _levelDic[matLref];

                                double newAmount = Math.Max(0, amount + onhand);
                                double newOnhand = (amount + onhand) - newAmount;

                                ToolCell.set(rowCurent, TableSTLINE.AMOUNT, newAmount);
                                _levelDic[matLref] = newOnhand;
                            }
                        }
                }
                #endregion
                foreach (double value in _levelDic.Values)
                    if (value < -ConstValues.minPositive)
                        throw new MyBaseException(MessageCollection.T_MSG_STOCK_NEGATIVE_LEVEL);
            }
        }

        void deleteEmptyLines(DataTable stline)
        {
            for (int rowIndx = 0; rowIndx < stline.Rows.Count; ++rowIndx)
            {
                DataRow rowCurent = stline.Rows[rowIndx];
                if (rowCurent.RowState != DataRowState.Deleted)
                {
                    double amount = (double)rowCurent[TableSTLINE.AMOUNT];
                    if (amount < ConstValues.minPositive)
                    {
                        rowCurent.Delete();
                        --rowIndx;
                    }
                }
            }
        }



        protected override string getPrefix()
        {
            return "INV";
        }

        protected override string getId()
        {
            DataTable stslip = stockAdapter.getDataSet().Tables[TableINVOICE.TABLE];
            string id = ToolColumn.getColumnLastValue(stslip, TableINVOICE.TRCODE, 0).ToString();
            id = ToolString.shrincDigit(id);
            return getPrefix() + '_' + id;
        }
        protected virtual double[] getDiscounts()
        {

            string PARM = "MOB_DISCOUNTS_" + getId();
            string list = environment.getSysSettings().getString(PARM);
            return ToolString.strToDoubleArr(list);
        }
        protected virtual int[] getPriceLists()
        {

            string PARM = "MOB_PRCLISTS_" + getId();
            string list = environment.getSysSettings().getString(PARM);
            return ToolString.strToIntArr(list);
        }

        protected virtual string[] getDescs()
        {
            string PARM = "MOB_DESCS_" + getId();
            string list = environment.getSysSettings().getString(PARM);
            string[] items = ToolString.trim(ToolString.explodeList(list));
            return items;
        }
        protected virtual void initLists()
        {
            //source wh


            using (var ps = new PagedSourceWarehouse(environment))
            {
                ps.setIndex(new string[] { TableWHOUSE.NR });
                ps.setSortingMode(true);
                var wh = ps.getAll();

                var listNr = new List<object>();
                var listName = new List<string>();
                foreach (DataRow r in wh.Rows)
                {
                    var nr = Convert.ToInt16(r[TableWHOUSE.NR]);
                    var name = Convert.ToString(r[TableWHOUSE.NAME]);

                    listNr.Add(nr);
                    listName.Add(name);
                }

                sourceWhList = listName.ToArray();
                sourceWhListVal = listNr.ToArray();
            }

            //Main desc
            descList = getDescs();


            // ...LISTS...  LIST01,T_GROUP,GENEXP2;LIST02,T_TYPE,GENEXP3
            // ...LISTS...  LIST01,T_GROUP,GENEXP2;LIST02,T_TYPE,GENEXP3
            // ..LIST01.... YES,T_YES;NO,T_NO
            string PARM = "MOB_LISTS_" + getId();
            string lists = environment.getSysSettings().getString(PARM);

            //lists = "LIST01,T_GROUP,GENEXP2;LIST02,T_TYPE,GENEXP3";

            if (lists != string.Empty)
            {

                DataTable table = ToolString.explodeForTable(lists, new string[] { TableDUMMY.CODE, TableDUMMY.NAME, TableDUMMY.COLUMN });
                for (int i = 0; i < table.Rows.Count; ++i)
                {

                    DataRow row = table.Rows[i];

                    string items = environment.getSysSettings().getString((string)row[TableDUMMY.CODE]);
                    //items = ",;YES,T_YES;NO,T_NO";
                    DataTable tabList = ToolString.explodeForTable(items, new string[] { TableDUMMY.CODE, TableDUMMY.NAME });
                    environment.translate(tabList);

                    string label_ = (string)row[TableDUMMY.NAME];

                    string[] desc_ = new string[tabList.Rows.Count];
                    string[] val_ = new string[tabList.Rows.Count];

                    for (int i2 = 0; i2 < tabList.Rows.Count; ++i2)
                    {
                        desc_[i2] = tabList.Rows[i2][TableDUMMY.NAME].ToString();
                        val_[i2] = tabList.Rows[i2][TableDUMMY.CODE].ToString();
                    }


                    switch (i)
                    {
                        case 1:
                            desc1Label = label_;
                            desc1List = desc_;
                            desc1ListVal = val_;
                            break;
                        case 2:
                            desc2Label = label_;
                            desc2List = desc_;
                            desc2ListVal = val_;
                            break;
                        case 3:
                            desc3Label = label_;
                            desc3List = desc_;
                            desc3ListVal = val_;
                            break;
                    }

                }




            }
        }
        int getPositionForRealMaterial(DataTable tab)
        {
            for (int i = tab.Rows.Count - 1; i >= 0; --i)
            {
                DataRow row = tab.Rows[i];
                if (row.RowState != DataRowState.Deleted)
                    if (ToolStockLine.isLineStlineLocal(row))
                        return (i + 1);
            }
            return 0; // nothin found make first// tab.Rows.Count;
        }


        DataRow convertMatRow(DataRow matRow)
        {
            if (matRow == null)
                throw new MyExceptionInner(MessageCollection.T_MSG_INVALID_PARAMETER);


            if (!isMatRowExtended(matRow))
                return extendMatRow(matRow);

            return matRow;
        }
        void addMat()
        {
            try
            {
                IDataReference refMat_ = getRefMaterials();

                refMat_.begin(null, null, new ReferenceMode
                {
                    formBatchMode = true,
                    showMode = false,
                    batchModeIndexes = new string[] { string.Empty, WordCollection.T_PROMOTION }
                },

                addMat_RefHandler);
            }
            catch (Exception exc)
            {
                ToolMobile.setException(exc);
            }

        }

        void addMat_RefHandler(object o, EventArgs e)
        {
            IDataReference ref_ = null;
            try
            {
                ref_ = o as IDataReference;

                if (ref_ == null || !ref_.isDataSelected())
                    return;

                DataRow rowMat_ = ref_.getSelected();
                if (rowMat_ == null)
                    return;

                int indx_ = Math.Max(0, ref_.getReferenceMode().lastBatchModeIndex);

                DataTable tTable = stockAdapter.getDataSet().Tables[TableSTLINE.TABLE];

                //Set values

                int newPos = -1;
                ConstBool isGlobal = ConstBool.not;

                if (indx_ <= 0)//mat
                {
                    newPos = getPositionForRealMaterial(tTable);
                    DataRow rowLine = stockAdapter.insertRowIntoTable(tTable, newPos);
                    _addRow(rowLine, rowMat_, isGlobal, ConstLineType.material);
                }
                else
                    if (indx_ == 1) //promo
                    {


                        //
                        newPos = getRowPos(getActiveRowMaterial());
                        if (newPos >= 0)
                        {
                            //check before add rec
                            if (!isPromoMat(rowMat_[TableITEMS.LOGICALREF]))
                            {
                                ToolMsg.show(null, MessageCollection.T_MSG_INVALID_MATERIAL, null);
                                return;
                            }

                            DataRow rowLine = stockAdapter.insertRowIntoTable(tTable, newPos + 1);
                            _addRow(rowLine, rowMat_, isGlobal, ConstLineType.promotion);
                        }
                    }
            }
            catch (Exception exc)
            {
                ToolMobile.setException(exc);

            }
            finally
            {
                try
                {

                    // if (ref_ != null)
                    //    ref_.Dispose(); //ref obj is reused
                }
                catch (Exception exc)
                {
                    ToolMobile.setException(exc);

                }
            }
        }



        int getRowPos(DataRow pRow)
        {
            return pRow != null ? pRow.Table.Rows.IndexOf(pRow) : -1;
        }
        DataRow getActiveRowMaterial()
        {
            DataRow row_ = cGrid.ActiveRow;

            if (row_ != null && ToolStockLine.isLineMaterial(row_))
                return row_;

            return null;
        }

        IDataReference getRefMaterials()
        {
            if (_paramUseMatBarcode)
                return environment.getReference(ConstRefCode.materialBarcode);
            return environment.getReference(ConstRefCode.material);
        }
        void addPromo(DataRow parent)
        {
            try
            {
                if (parent == null || ToolStockLine.isLineMaterial(parent))
                {
                    IDataReference refMat_ = getRefPromoMaterials();

                    refMat_.begin(null, null, new ReferenceMode { formBatchMode = true, showMode = false }, (o, e) =>
                    {
                        try
                        {
                            IDataReference ref_ = o as IDataReference;

                            if (ref_ == null || !ref_.isDataSelected())
                                return;

                            DataRow rowMat_ = ref_.getSelected();
                            if (rowMat_ == null)
                                return;

                            DataTable tTable = stockAdapter.getDataSet().Tables[TableSTLINE.TABLE];
                            //
                            int newPos = (parent != null ? getRowPos(parent) + 1 : tTable.Rows.Count);
                            ConstBool isGlobal = (parent != null ? ConstBool.not : ConstBool.yes);
                            //
                            DataRow rowLine = stockAdapter.insertRowIntoTable(tTable, newPos);

                            _addRow(rowLine, rowMat_, isGlobal, ConstLineType.promotion);

                        }
                        catch (Exception exc)
                        {
                            ToolMobile.setException(exc);

                        }

                    });


                }
            }
            catch (Exception exc)
            {
                ToolMobile.setException(exc);
            }

        }

        bool isPromoMat(object pLRef)
        {
            object v = SqlExecute.executeScalar(environment, "SELECT PROMO FROM LG_$FIRM$_ITEMS WHERE LOGICALREF = @P1", new object[] { pLRef });
            short promo_ = Convert.ToInt16(ToolCell.isNull(v, (short)ConstBool.not));
            return (promo_ == (short)ConstBool.yes);
        }


        protected virtual int amountMode()
        {
            return 2;
        }

        void _addRow(DataRow pDocLine, DataRow pRowMaterial, ConstBool pIsGlobal, ConstLineType pLineType)
        {

            try
            {
                DataRow rowMatFromDb_ = ToolStockLine.getItemData(environment, pRowMaterial[TableITEMS.LOGICALREF]);



                //pRow retern from material reference
                DataRow matRow = convertMatRow(rowMatFromDb_);
                ToolCell.set(pDocLine, TableSTLINE.GLOBTRANS, pIsGlobal);
                ToolCell.set(pDocLine, TableSTLINE.LINETYPE, pLineType);
                ToolCell.set(pDocLine, TableSTLINE.STOCKREF, matRow[TableITEMS.LOGICALREF]);
                ToolCell.set(pDocLine, TableSTLINE.AMOUNT, pRowMaterial.Table.Columns[__AMOUNT] != null ? pRowMaterial[__AMOUNT] : 0.0);
                ToolCell.set(pDocLine, TableSTLINE.UNIT, matRow[__UNIT]);
                ToolCell.set(pDocLine, TableSTLINE.UNITREF, matRow[__UNITREF]);
                ToolCell.set(pDocLine, TableSTLINE.UINFO1, matRow[__UINFO1]);
                ToolCell.set(pDocLine, TableSTLINE.UINFO2, matRow[__UINFO2]);
                //Edit

                switch (amountMode())
                {
                    case 0://live with zero
                        return;
                    case 1://set one
                        ToolCell.set(pDocLine, TableSTLINE.AMOUNT, 1.0);
                        return;
                    case 2://continue edit amount
                        break;

                }

                EventHandler h = (o, e) =>
                {

                    var helper_ = o as MobInputItemVarsForm.Helper;

                    if (_paramStocAddIgnoreZero)
                        if (ConstValues.minPositive > (double)helper_.row[TableSTLINE.AMOUNT])
                            helper_.row.Delete(); //pDocLine


                };

                _editorVars.edit(this, pDocLine, TableSTLINE.AMOUNT, isNegControl(), _report, h, h);

            }
            catch (Exception exc)
            {
                ToolMobile.setExceptionInner(exc);
                throw exc;
            }

        }

        IDataReference getRefPromoMaterials()
        {
            if (_paramUseMatBarcode)
                return environment.getReference(ConstRefCode.promoMaterialBarcode);
            return environment.getReference(ConstRefCode.promoMaterial);
        }


        public static DataRow extendMatRow(DataRow matRow)
        {
            DataRow rowExtended = (matRow.Table.Clone()).NewRow();
            ToolRow.copyRowToRow(matRow, rowExtended);
            rowExtended.Table.Columns.Add(__AMOUNT, typeof(double));
            rowExtended.Table.Columns.Add(__UNIT, typeof(string));
            rowExtended.Table.Columns.Add(__UNITREF, typeof(string));
            rowExtended.Table.Columns.Add(__UINFO1, typeof(double));
            rowExtended.Table.Columns.Add(__UINFO2, typeof(double));
            extendMatRow(rowExtended, 1);
            return rowExtended;
        }
        public static void extendMatRow(DataRow matRow, int unitNr, double amount)
        {
            amount = Math.Abs(amount);

            switch (unitNr)
            {
                case 1:
                    matRow[__AMOUNT] = amount;
                    matRow[__UNIT] = matRow[TableITEMS.UNIT1];
                    matRow[__UNITREF] = matRow[TableITEMS.UNITREF1];
                    matRow[__UINFO1] = matRow[TableITEMS.UNITCF1];
                    matRow[__UINFO2] = matRow[TableITEMS.UNITCF1];
                    break;
                case 2:
                    matRow[__AMOUNT] = amount;
                    matRow[__UNIT] = matRow[TableITEMS.UNIT2];
                    matRow[__UNITREF] = matRow[TableITEMS.UNITREF2];
                    matRow[__UINFO1] = matRow[TableITEMS.UNITCF1];
                    matRow[__UINFO2] = matRow[TableITEMS.UNITCF2];
                    break;
                case 3:
                    matRow[__AMOUNT] = amount;
                    matRow[__UNIT] = matRow[TableITEMS.UNIT3];
                    matRow[__UNITREF] = matRow[TableITEMS.UNITREF3];
                    matRow[__UINFO1] = matRow[TableITEMS.UNITCF1];
                    matRow[__UINFO2] = matRow[TableITEMS.UNITCF3];
                    break;

            }

        }
        public static void extendMatRow(DataRow matRow, int unitNr)
        {

            extendMatRow(matRow, unitNr, 0);
        }
        public static bool isMatRowExtended(DataRow matRow)
        {
            return (matRow.Table.Columns.Contains(__AMOUNT) ||
                    matRow.Table.Columns.Contains(__UNIT) ||
                    matRow.Table.Columns.Contains(__UNITREF) ||
                    matRow.Table.Columns.Contains(__UINFO1) ||
                    matRow.Table.Columns.Contains(__UINFO2));

        }

    }

    public enum StockDocParameters
    {

        client = 1,
        material = 2,
        quantity = 3,
        price = 4,
        total = 5,
        stockLevel = 6


    }
}

