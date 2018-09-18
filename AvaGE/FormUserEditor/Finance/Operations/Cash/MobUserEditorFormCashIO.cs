using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using AvaExt.AndroidEnv.ControlsBase;
using AvaExt.Common;
using AvaExt.Adapter.ForUser;
using AvaExt.Adapter.ForUser.Finance.Operation.Cash;
using AvaExt.ControlOperation;
using AvaExt.Manual.Table;
using AvaExt.DataRefernce;
using AvaExt.Common.Const;
using AvaExt.ObjectSource;
using AvaExt.TableOperation;
using AvaGE.MobControl.ControlsTools;
using AvaExt.Translating.Tools;
using AvaExt.MyException;
using AvaExt.Settings;
using AvaExt.TableOperation.RowsSelector;
using AvaGE.MobControl;
using AvaExt.Adapter;
using Android.Content;
using AvaAgent;

namespace AvaGE.FormUserEditor.Finance.Operations.Cash
{
    public class MobUserEditorFormCashIO : MobUserEditorFormBase
    {

        string[] descList = new string[] { };
  


        ImplObjectSourceAdapterRow _transRow = new ImplObjectSourceAdapterRow(null, TableKSLINES.TABLE);
        public override void setAdapter(EditingTools pTool)
        {
            base.setAdapter(pTool);

            cashAdapter = userAdapter as AdapterUserCash;
            _transRow.setAdapter(userAdapter);
        }


        AdapterUserCash cashAdapter;


        MobTextBox cDate { get { return FindViewById<MobTextBox>(Resource.Id.cDate); } }
        MobTextBox cName { get { return FindViewById<MobTextBox>(Resource.Id.cName); } }
        MobTextBox cCode { get { return FindViewById<MobTextBox>(Resource.Id.cCode); } }
        MobTextBox cBarcode { get { return FindViewById<MobTextBox>(Resource.Id.cBarcode); } }
        MobTextBox cAmount { get { return FindViewById<MobTextBox>(Resource.Id.cAmount); } }
        MobTextBox cDesc { get { return FindViewById<MobTextBox>(Resource.Id.cDesc); } }

        MobButton cBtnDate { get { return FindViewById<MobButton>(Resource.Id.cBtnDate); } }
        MobButton cBtnName { get { return FindViewById<MobButton>(Resource.Id.cBtnName); } }
        MobButton cBtnCode { get { return FindViewById<MobButton>(Resource.Id.cBtnCode); } }

        MobButton cBtnAmount { get { return FindViewById<MobButton>(Resource.Id.cBtnAmount); } }
        MobButton cBtnDesc { get { return FindViewById<MobButton>(Resource.Id.cBtnDesc); } }


        public MobUserEditorFormCashIO(IEnvironment pEnv)
            : base(pEnv, Resource.Layout.MobUserEditorFormCashIO)
        {

        }


        protected override void reinitBindingProperties()
        {
            #region ds
            this.cDate.DSColumn = "DATE_";
            this.cDate.DSTable = "KSLINES";
            //

            this.cName.DSColumn = "DEFINITION_";
            this.cName.DSSubTable = "CLCARD";
            this.cName.DSTable = "KSLINES";


            this.cCode.DSColumn = "CODE";
            this.cCode.DSSubTable = "CLCARD";
            this.cCode.DSTable = "KSLINES";

            this.cBarcode.DSColumn = "BARCODE";
            this.cBarcode.DSSubTable = "CLCARD";
            this.cBarcode.DSTable = "KSLINES";


            this.cAmount.DSColumn = "AMOUNT";
            this.cAmount.DSTable = "KSLINES";

            this.cDesc.DSColumn = "LINEEXP";
            this.cDesc.DSTable = "KSLINES";
            #endregion

        }

        public override void reinitEditingForData()
        {
            base.reinitEditingForData();
 
            DataTable trans = cashAdapter.getDataSet().Tables[TableKSLINES.TABLE];
 
            IDataReference refer;

            refer = environment.getReference(ConstRefCode.date);
            cBtnDate.activity = new BindDataRefenceAsActivity(refer,
            _transRow,
            TableDUMMY.DATETIME,
            cDate,
            new string[] { TableDUMMY.DATETIME },
            new string[] { TableKSLINES.DATE_ });


            refer = environment.getReference(ConstRefCode.client);
            cBtnName.activity = new BindDataRefenceAsActivity(refer,
                _transRow,
                TableCLCARD.DEFINITION_,
                cName,
                new string[] { TableCLCARD.LOGICALREF },
                new string[] { TableKSLINES.CLIENTREF });

            cBtnCode.activity = new BindDataRefenceAsActivity(refer,
                _transRow,
                TableCLCARD.CODE,
                cCode,
                new string[] { TableCLCARD.LOGICALREF },
                new string[] { TableKSLINES.CLIENTREF });




            refer = environment.getReference(ConstRefCode.number);

            cBtnAmount.activity = new BindDataRefenceAsActivity(refer,
                _transRow,
                TableDUMMY.VALUE,
                cAmount,
                new string[] { TableDUMMY.VALUE },
                new string[] { TableKSLINES.AMOUNT });

            //

            cBtnDesc.Enabled = (descList != null && descList.Length > 0);
            if (cBtnDesc.Enabled)
                cBtnDesc.Click += cBtnDesc_Click;


            //cBarcode.Length = environment.getColumnLen(TableCLCARD.TABLE, TableCLCARD.BARCODE);
            cDate.Enabled = false;
            cBtnDate.Enabled = settings.getBool(SettingsAvaAgent.MOB_EDIT_DATE_B);

        }

        void cBtnDesc_Click(object sender, EventArgs e)
        {
            if (descList != null && descList.Length > 0)
                ToolMsg.askList(this, descList, delegate(object s, DialogClickEventArgs a)
                {
                    if (a.Which >= 0 && a.Which < descList.Length)
                    {
                        string val_ = descList[a.Which];

                        ToolColumn.setColumnValue(userAdapter.getDataSet().Tables[TableKSLINES.TABLE], TableKSLINES.LINEEXP, val_);

                    }
                });
        }


 



        protected override void initBeforeSettings()
        {
            base.initBeforeSettings();


            // Descriptions list
            initLists();

        
        }

         


    


        public override void startSave()
        {
            base.startSave();

            // bool itsOk = true;
            DataTable trans = cashAdapter.getDataSet().Tables[TableKSLINES.TABLE];


            //
            checkDoc(trans);
        }
        void checkDoc(DataTable trans)
        {
            bool isCancelled = false;

            foreach (DataRow rowCurent in trans.Rows)
                if (rowCurent.RowState != DataRowState.Deleted)
                    isCancelled = ((short)ToolCell.isNull(rowCurent[TableKSLINES.CANCELLED], (short)ConstBool.yes) == (short)ConstBool.yes);

            foreach (DataRow rowCurent in trans.Rows)
                if (rowCurent.RowState != DataRowState.Deleted)
                {
                    string[] arrReqCols = ToolString.explodeList(environment.getSysSettings().getString("MOB_REQCOLS_" + getId()));
                    foreach (string col in arrReqCols)
                        if (col != string.Empty && rowCurent.Table.Columns.Contains(col))
                        {
                            string val = ToolCell.isNull(rowCurent, col, string.Empty).ToString().Trim();
                            if (val == string.Empty)
                                throw new MyBaseException(MessageCollection.T_MSG_SET_REQFIELDS);
                        }

                    if (!isCancelled)
                    {
                        if (!CurrentVersion.ENV.isZeroDocAllowed())
                        {
                            double amount = (double)rowCurent[TableKSLINES.AMOUNT];
                            if (amount < ConstValues.minPositive)
                            {
                                throw new MyBaseException(MessageCollection.T_MSG_EMPTY_DOC);
                            }
                        }
                    }

                    if ((string)ToolCell.isNull(rowCurent[TableKSLINES.CLIENTREF], string.Empty) == string.Empty)
                    {
                        throw new MyBaseException(MessageCollection.T_MSG_SET_CLIENT);
                    }
                }
        }


        protected override string getPrefix()
        {
            return "CASH";
        }

        protected override string getId()
        {
            DataTable stslip = cashAdapter.getDataSet().Tables[TableKSLINES.TABLE];
            string id = ToolColumn.getColumnLastValue(stslip, TableKSLINES.TRCODE, 0).ToString();
            id = ToolString.shrincDigit(id);
            return getPrefix() + '_' + id;
        }



        protected virtual void initLists()
        {
            descList = getDescs();
        }

        protected virtual string[] getDescs()
        {
            string PARM = "MOB_DESCS_" + getId();
            string list = environment.getSysSettings().getString(PARM);
            string[] items = ToolString.trim(ToolString.explodeList(list));
            return items;
        }
    }
}


