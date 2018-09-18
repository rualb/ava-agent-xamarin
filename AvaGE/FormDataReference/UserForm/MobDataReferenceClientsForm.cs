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
using AvaGE.MobControl.ControlsTools;
using AvaExt.ControlOperation;
using AvaExt.ObjectSource;
using AvaExt.Manual.Table;
using Android.App;
using AvaExt.Common.Const;
using AvaExt.TableOperation;
using AvaExt.Formating;
using AvaExt.Translating.Tools;
using AvaExt.Adapter;
using AvaGE.FormUserEditor.Const;

namespace AvaGE.FormDataReference.UserForm
{
    /// <summary>
    /// info - client info - cmActionInfo_InfoChildActivityDone
    /// refresh - cmActionRefresh_Click
    /// </summary>
    [Activity(Label = Form.FORM_NAME, Icon = Form.FORM_ICON, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, WindowSoftInputMode = Android.Views.SoftInput.StateAlwaysHidden)]
    public class MobDataReferenceClientsForm : MobDataReferenceGridFormBase
    {
        protected override string globalStoreName()
        {
            return ConstRefCode.client;// "ref.fin.client";
        }


        public MobDataReferenceClientsForm()
            : base(0)
        {


        }


        protected override string[] getQuiqFilterColumns()
        {
            var b_ = base.getQuiqFilterColumns();

            return (b_ == null ? new string[] { TableCLCARD.DEFINITION_ } : b_);
        }
        protected override string getQuiqFilterColumn(string pPattern)
        {
            if (!string.IsNullOrEmpty(pPattern))
            {
                if (pPattern.Length >= 5 && ToolString.isDigit(pPattern))
                {
                    return TableCLCARD.BARCODE;
                }
            }

            return base.getQuiqFilterColumn(pPattern);
        }

        // const string SPE_ORDDAY = "SPE_ORDDAY";
        protected override void convertData(DataTable table)
        {


            //if (table != null && settings != null && table.Columns.Contains(SPE_ORDDAY) && table.Columns.Contains(TableCLCARD.ORDDAY))
            //{


            //    char dayOfWeek = '0';
            //    string inRoute = DateTime.Now.ToString(XmlFormating.getDateFormat().ShortDatePattern);
            //    string outRoute = "*";
            //    switch (DateTime.Now.DayOfWeek)
            //    {
            //        case DayOfWeek.Monday: dayOfWeek = '1'; break;
            //        case DayOfWeek.Tuesday: dayOfWeek = '2'; break;
            //        case DayOfWeek.Wednesday: dayOfWeek = '3'; break;
            //        case DayOfWeek.Thursday: dayOfWeek = '4'; break;
            //        case DayOfWeek.Friday: dayOfWeek = '5'; break;
            //        case DayOfWeek.Saturday: dayOfWeek = '6'; break;
            //        case DayOfWeek.Sunday: dayOfWeek = '7'; break;
            //    }


            //    int colIndxExp = table.Columns.IndexOf(TableCLCARD.ORDDAY);
            //    int colIndxVal = table.Columns.IndexOf(SPE_ORDDAY);

            //    foreach (DataRow row in table.Rows)
            //    {
            //        string rout = ((string)row[colIndxExp]).Trim();
            //        var val_ = ((rout.IndexOf(dayOfWeek) >= 0) ? inRoute : outRoute);
            //        ToolCell.set(row, colIndxVal, val_);
            //    }


            //}

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


        protected override bool useMenuItem(CmdMenuItem pItem, DataRow pDbRow)
        {
            switch (pItem.CmdType)
            {
                case CmdType.doc1:
                    return canCash(pDbRow, true);

            }

            return base.useMenuItem(pItem, pDbRow);


        }


        protected override void doCmdUser(CmdMenuItem pMenuItem, DataRow pRow)
        {
            if (pMenuItem.CmdType == CmdType.doc1)
                doCash(pRow);
            else
                base.doCmdUser(pMenuItem, pRow);
        }


        object doCash(DataRow pRow)
        {
            object res = null;
            try
            {
                if (canCash(pRow, false))
                {
                    res = cash(pRow);
                }
            }
            catch (Exception exc)
            {
                environment.getExceptionHandler().setException(exc);
            }
            return res;
        }

        private bool canCash(DataRow dataRow, bool pDbRow)
        {
            if (!ToolMobile.canPayment())
                return false;

            if (ToolMobile.isReader())
                return false;

            return (dataRow != null);
        }

        protected override MobDataReferenceGridFormBase.CmdMenuItem[] createMenuItems()
        {
            List<CmdMenuItem> list = new List<CmdMenuItem>();
            list.AddRange(base.createMenuItems());

            list.Add(new CmdMenuItem() { CmdType = CmdType.doc1, Text = ToolMobile.getEnvironment().translate(WordCollection.T_CASH) });

            return list.ToArray();
        }


        private object cash(DataRow pRow)
        {
            if (pRow == null)
                return null;

            EditingTools _editor = environment.getAdapter(ConstAdapterNames.adp_fin_cash_client_input);

            if (_editor == null)
                return null;

            _editor.adapter.add();
            DataSet ds = _editor.adapter.getDataSet();
            DataTable tab = ds.Tables[TableKSLINES.TABLE];

            ToolColumn.setColumnValue(tab, TableKSLINES.AMOUNT, Math.Max((double)pRow[TableCLCARD.BALANCE], 0));
            ToolColumn.setColumnValue(tab, TableKSLINES.CLIENTREF, pRow[TableCLCARD.LOGICALREF]);
            //ToolColumn.setColumnValue(tab, TableKSLINES.CANCELLED, pRow[TableINVOICE.CANCELLED]);
            //ToolColumn.setColumnValue(tab, TableKSLINES.DATE_, pRow[TableINVOICE.DATE_]);

            object invLref_ = pRow[TableINVOICE.LOGICALREF];

            _editor.handlerReferenceInformer = (EditingTools pTool, object pLref) =>
            {
                refresh(invLref_);
            };

            _editor.edit();

            return invLref_;
        }

    }
}

