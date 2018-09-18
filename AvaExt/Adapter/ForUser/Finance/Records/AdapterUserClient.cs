using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.Adapter.ForDataSet;
using System.Data;
using AvaExt.Manual.Table;
using AvaExt.PagedSource;
using AvaExt.TableOperation;
using AvaExt.Common.Const;
using AvaExt.ObjectSource;
using AvaExt.Database.Const;
using AvaExt.SQL.Dynamic.Preparing;
using AvaExt.TableOperation.CellAutomation;

using AvaExt.TableOperation.TableConverter;

namespace AvaExt.Adapter.ForUser.Finance.Records
{
    public class AdapterUserClient : AdapterUserRecords
    {



        public AdapterUserClient(IEnvironment pEnv, IAdapterDataSet pDsAdapter)
            : base(pEnv, pDsAdapter, TableCLCARD.TABLE)
        {


        }


        protected override void prepareBeforeUpdate(DataSet pDataSet)
        {
            base.prepareBeforeUpdate(pDataSet);
            DataTable tab;
            DataRow row;
            tab = pDataSet.Tables[TableCLCARD.TABLE];
            for (int i = 0; i < tab.Rows.Count; ++i)
            {
                row = tab.Rows[i];
                if (row.RowState == DataRowState.Added)
                {

                }
                else
                {

                }
            }

        }


        protected override void dataResived(DataSet pDataSet)
        {
            base.dataResived(pDataSet);
 
 
        }


        protected virtual void tableCLCARDColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            object o = e.ProposedValue;
        }
        protected virtual void tableCLCARDColumnChanging(object sender, DataColumnChangeEventArgs e)
        {

        }

        protected virtual void tableCLCARDRowDeleted(object sender, DataRowChangeEventArgs e)
        {

        }
        protected virtual void tableCLCARDRowChanged(object sender, DataRowChangeEventArgs e)
        {

        }

        
        
 



        protected override bool isEmptyData(DataSet pDataSet)
        {
            return base.isEmptyData(pDataSet);
        }
        protected override void deleteExcessData(DataSet pDataSet)
        {
            base.deleteExcessData(pDataSet);

        }
        protected override void newRowInCollection(DataRow row)
        {
            base.newRowInCollection(row);

            switch (row.Table.TableName)
            {
                case TableCLCARD.TABLE:
                    break;

            }

        }


        protected override void addDefaults(DataSet pDataSet)
        {
            base.addDefaults(pDataSet);
            for (int i = 0; i < pDataSet.Tables.Count; ++i)
            {
                DataTable tab = pDataSet.Tables[i];
                switch (tab.TableName)
                {
                    case TableCLCARD.TABLE:
                        if (tab.Rows.Count == 0)
                            addRowToTable(tab);
                        break;

                }

            }

        }

     
    }
}
