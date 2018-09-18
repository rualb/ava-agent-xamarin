using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Adapter.ForDataSet;
using AvaExt.Manual.Table;
using System.Data;
using System.ComponentModel;
using AvaExt.Adapter.Tools;
using AvaExt.InfoClass;
using AvaExt.Common;
using AvaExt.Common.Const;
using AvaExt.TableOperation;
using AvaExt.Database.Tools;
using AvaExt.PagedSource;
using AvaExt.TableOperation.CellAutomation;


namespace AvaExt.Adapter.ForUser.Finance.Operation.Cash
{
    public class AdapterUserCashClient : AdapterUserCash
    {
        protected short moduleNo;
        protected short clientTranSign;
        protected short clientTranCode;
        protected short payTranSign;
        protected short payTranCode;

        protected ICellReltions cellBindingHeader;
        public AdapterUserCashClient(IEnvironment pEnv, IAdapterDataSet pAdapterDataSet)
            : base(pEnv, pAdapterDataSet, TableKSLINES.TABLE, TableKSLINES.TABLE)
        {

        }

        public override void setHeaderClient(object pClienRef)
        {
            ToolColumn.setColumnValue(getTable(TableKSLINES.TABLE), TableKSLINES.CLIENTREF, pClienRef);
        }

        protected override void dataResived(DataSet pDataSet)
        {



            base.dataResived(pDataSet);
            dataResivedForKSLINES(pDataSet);


        }
        protected virtual void dataResivedForKSLINES(DataSet pDataSet)
        {

           // IPagedSource pagedSource;
            DataTable tab = pDataSet.Tables[TableKSLINES.TABLE];
            cellBindingHeader = new ImplCellReltions(tab);

            CellAutomationDB dbAutomation;
            dbAutomation = new CellAutomationDB(tab, new PagedSourceClient(environment),
               new string[] { TableKSLINES.CLIENTREF },
               new string[] { TableCLCARD.LOGICALREF },
               new string[] { },
               new string[] { TableCLCARD.CODE, TableCLCARD.DEFINITION_, TableCLCARD.BARCODE },
               UpdateTypeFlags.resetIfAllCurrentRelColsAreDefaultOrNull,
               null);

            cellBindingHeader.addRelation(dbAutomation.getTriggerColumns(), dbAutomation, null);

        }
        protected override void prepareBeforeUpdate(DataSet pDataSet)
        {
            base.prepareBeforeUpdate(pDataSet);
            DataTable tab;
            DataRow row;
            //
            tab = pDataSet.Tables[TableKSLINES.TABLE];
            for (int i = 0; i < tab.Rows.Count; ++i)
            {
                row = tab.Rows[i];
                if (isUsedRow(row))
                {
                    if (row.RowState == DataRowState.Added)
                    {
                    }
                    else
                    {
                    }

                }
            }
            //



        }
        protected override void newRowInCollection(DataRow pNewRow)
        {
            base.newRowInCollection(pNewRow);
            switch (pNewRow.Table.TableName)
            {
                case TableKSLINES.TABLE:

                    break;

            }
        }

        //




    }
}
