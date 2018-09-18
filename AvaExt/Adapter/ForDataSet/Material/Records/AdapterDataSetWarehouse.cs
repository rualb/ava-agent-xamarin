using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.Adapter.ForDataTable;
using AvaExt.Manual.Table;
using System.Data;
using AvaExt.TableOperation;

namespace AvaExt.Adapter.ForDataSet.Material.Records
{
    public class AdapterDataSetWarehouse : AdapterDataSetRecords
    { 
        public AdapterDataSetWarehouse(IEnvironment pEnv)
            : base(
            pEnv,
            new IAdapterTable[]
                {   
                    new  AdapterTableWarehouse ( pEnv, TableWHOUSE.LOGICALREF )
                }
            )
        {

        }
     
        public override string getCode()
        {
            return _constAdpNamePreix + TableWHOUSE.TABLE;
        }
        protected override void prepareBeforeUpdate(DataSet pData)
        {
            DataSet dataSet = (DataSet)pData;
            DataTable tab;
            DataRow row;
            tab = dataSet.Tables[TableWHOUSE.TABLE];
            for (int i = 0; i < tab.Rows.Count; ++i)
            {
                row = tab.Rows[i];
                if (row.RowState != DataRowState.Deleted)
                {
                    if (row.RowState == DataRowState.Added)
                    {
                       

                    }
                    else
                    {
                        

                    }

                }
            }

        }
    }
}
