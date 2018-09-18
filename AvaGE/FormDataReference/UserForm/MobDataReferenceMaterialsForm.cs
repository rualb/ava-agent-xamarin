using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using AvaExt.AndroidEnv.ControlsBase;
using AvaExt.Common;
using AvaExt.PagedSource;
using AvaExt.SQL.Dynamic.Preparing;

using AvaExt.ControlOperation;
using AvaExt.ObjectSource;

using AvaExt.SQL.Dynamic;
using AvaExt.Manual.Table;
using AvaExt.TableOperation;
using AvaExt.Adapter.Tools;
using AvaExt.Settings;
using Android.App;
using AvaGE.FormDataReference.UserForm;
using Android.Content.PM;
using AvaExt.Common.Const;

namespace AvaGE.FormDataReference.UserForm
{
    [Activity(Label = Form.FORM_NAME, Icon = Form.FORM_ICON, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, WindowSoftInputMode = Android.Views.SoftInput.StateAlwaysHidden)]
    public class MobDataReferenceMaterialsForm : MobDataReferenceGridFormBase
    {
        protected override string globalStoreName()
        {

            return ConstRefCode.material;// "ref.mm.mat";
        }

        public MobDataReferenceMaterialsForm()
            : base(0)
        {


            //check
            //    cmActionInfo.rowSource = new ImplObjectSourceGridCurrentBindedRow(DATAGRIDMAIN);


            Created += MobDataReferenceClientsForm_Created;
        }

        protected override string[] getQuiqFilterColumns()
        {

            var b_ = base.getQuiqFilterColumns();

            return( b_ == null ? new string[] { TableITEMS.NAME } : b_);
        }

        protected override string getQuiqFilterColumn(string pPattern)
        {
            if (!string.IsNullOrEmpty(pPattern))
            {
                if (pPattern.Length >= 5 && ToolString.isDigit(pPattern))
                {
                    return TableITEMS.BARCODE1;
                }
            }

            return base.getQuiqFilterColumn(pPattern);
        }

        protected override bool cleanQuickFilterAfterSearch(string pPattern)
        {

            if (!string.IsNullOrEmpty(pPattern))
            {
                if (pPattern.Length >= 5 && ToolString.isDigit(pPattern))
                {
                    return true;
                }
            }

            return base.cleanQuickFilterAfterSearch(pPattern);
        }

        void MobDataReferenceClientsForm_Created(object sender, EventArgs e)
        {

        }





        private void cmActionRefresh_Click(object sender, EventArgs e)
        {

            doRefresh(getGridCurDataRow());

        }

        private void cmActionInfo_InfoChildActivityDone(object sender, EventArgs e)
        {
            doInfo(getCurentRec());
        }


        protected override void fillSpeColumns(DataRow pRow)
        {
            base.fillSpeColumns(pRow);


            fillRemain(pRow);
            fillPrice(pRow);
        }
        void fillRemain(DataRow pRow)
        {
            string SPE_REMAIN = "SPE_REMAIN";

            DataTable tabList = getTable();
            if (tabList != null && tabList.DefaultView.Count > 0 && tabList.Columns.Contains(SPE_REMAIN))
            {
                bool isOrder = ToolSlip.isDsOrder(environment.getCurDoc());
                bool isSlip = ToolSlip.isDsSlip(environment.getCurDoc());

                int indxMatLOGICALREF = tabList.Columns.IndexOf(TableITEMS.LOGICALREF);
                int indxMatONHAND = tabList.Columns.IndexOf(TableITEMS.ONHAND);
                int indxMatONHANDIO = tabList.Columns.IndexOf(TableITEMS.ONHANDIO);
                int indxMatONMAIN = tabList.Columns.IndexOf(TableITEMS.ONMAIN);
                int indxMatSPE_REMAIN = tabList.Columns.IndexOf(SPE_REMAIN);




                foreach (DataRowView v in tabList.DefaultView)
                {

                    if (pRow == null || object.ReferenceEquals(v.Row, pRow))
                    {
                        double remain = 0;
                        if (isOrder)
                        {
                            remain = (double)v[indxMatONMAIN];
                        }
                        else
                        // if (   isSlip)
                        {
                            object lref_ = v[indxMatLOGICALREF];

                            remain = (double)v[indxMatONHAND];
                            if (indxMatONHANDIO < 0)
                                remain += environment.getMatIOAmount(lref_);
                            else
                            {
                                remain += Convert.ToDouble(ToolCell.isNull(v[indxMatONHANDIO], 0));
                                remain += environment.getMatIOAmountDoc(lref_);
                            }
                        }


                        v.Row[indxMatSPE_REMAIN] = remain;
                    }
                }


            }


        }



        void fillPrice(DataRow pRow)
        {

            string SPE_PRICE = "SPE_PRICE";

            DataTable tabList = getTable();
            if (tabList != null && tabList.DefaultView.Count > 0 && tabList.Columns.Contains(SPE_PRICE))
            {
                int indxMatPRICE = -1;
                string priceCol = getPriceCol();
                if (tabList.Columns.Contains(priceCol))
                    indxMatPRICE = tabList.Columns.IndexOf(priceCol);
                int indxMatLOGICALREF = tabList.Columns.IndexOf(TableITEMS.LOGICALREF);
                int indxMatSPE_PRICE = tabList.Columns.IndexOf(SPE_PRICE);

                foreach (DataRowView v in tabList.DefaultView)
                {
                    if (pRow == null || object.ReferenceEquals(v.Row, pRow))
                    {
                        double price = 0;
                        if (indxMatPRICE >= 0)
                            price = (double)v[indxMatPRICE];
                        v.Row[indxMatSPE_PRICE] = price;
                    }
                }
            }

        }
        private string getPriceCol()
        {
            short indx = 0;
            DataSet ds = environment.getCurDoc();


            if (ds == null)
                return TableDUMMY.PRICE + environment.getSysSettings().getString(SettingsSysMob.MOB_SYS_DEF_PLIST, "1");

            if (ds.Tables.Contains(TableINVOICE.TABLE))
                indx = (short)ToolColumn.getColumnLastValue(ds.Tables[TableINVOICE.TABLE], TableINVOICE.PRCLIST, (short)0);

            if (indx > 0) // 1 2 3 4
                return TableDUMMY.PRICE + ToolString.shrincDigit(indx.ToString());

            return string.Empty;
        }



        protected override bool canAdd()
        {
            return false;
        }

        protected override bool canEdit(DataRow dataRow, bool pDbRow)
        {
            return false;
        }

        protected override bool canView(DataRow dataRow, bool pDbRow)
        {
            return false;
        }

        protected override bool canCopy(DataRow dataRow, bool pDbRow)
        {
            return false;
        }

        protected override bool canDelete(DataRow dataRow, bool pDbRow)
        {
            return false;
        }
        protected override bool isReadOnly(DataRow dataRow)
        {
            return true;
        }

    }
}

