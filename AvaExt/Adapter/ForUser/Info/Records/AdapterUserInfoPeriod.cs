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

namespace AvaExt.Adapter.ForUser.Info.Records
{
    public class AdapterUserInfoPeriod : AdapterUserRecords
    {



        public AdapterUserInfoPeriod(IEnvironment pEnv, IAdapterDataSet pDsAdapter)
            : base(pEnv, pDsAdapter, TableINFOPERIOD.TABLE)
        {


        }


        protected override void prepareBeforeUpdate(DataSet pDataSet)
        {
            base.prepareBeforeUpdate(pDataSet);
            DataTable tab;
            DataRow row;
            tab = pDataSet.Tables[TableINFOPERIOD.TABLE];
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

 
  
        protected override void newRowInCollection(DataRow row)
        {
            base.newRowInCollection(row);

            switch (row.Table.TableName)
            {
                case TableINFOPERIOD.TABLE:
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
                    case TableINFOPERIOD.TABLE:
                        if (tab.Rows.Count == 0)
                            addRowToTable(tab);
                        break;

                }

            }

        }
 
    }
}
