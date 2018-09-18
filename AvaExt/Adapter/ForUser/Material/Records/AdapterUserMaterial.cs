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

namespace AvaExt.Adapter.ForUser.Material.Records
{
    public class AdapterUserMaterial : AdapterUserRecords
    {

     

        public AdapterUserMaterial(IEnvironment pEnv, IAdapterDataSet pDsAdapter)
            : base(pEnv, pDsAdapter, TableITEMS.TABLE)
        {


        }


        protected override void prepareBeforeUpdate(DataSet pDataSet)
        {
            base.prepareBeforeUpdate(pDataSet);
            DataTable tab;
            DataRow row;
            tab = pDataSet.Tables[TableITEMS.TABLE];
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


        protected virtual void tableITEMSColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            object o = e.ProposedValue;
        }
        protected virtual void tableITEMSColumnChanging(object sender, DataColumnChangeEventArgs e)
        {

        }

        protected virtual void tableITEMSRowDeleted(object sender, DataRowChangeEventArgs e)
        {

        }
        protected virtual void tableITEMSRowChanged(object sender, DataRowChangeEventArgs e)
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
                case TableITEMS.TABLE:
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
                    case TableITEMS.TABLE:
                        if (tab.Rows.Count == 0)
                            addRowToTable(tab);
                        break;

                }

            }

        }
 
    }
}
