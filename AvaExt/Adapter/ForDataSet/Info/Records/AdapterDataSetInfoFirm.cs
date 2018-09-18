using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.Adapter.ForDataTable;
using AvaExt.Manual.Table;
using System.Data;
using AvaExt.TableOperation;

namespace AvaExt.Adapter.ForDataSet.Info.Records
{
    public class AdapterDataSetInfoFirm : AdapterDataSetRecords
    {
        public AdapterDataSetInfoFirm(IEnvironment pEnv)
            : base(
            pEnv,
            new IAdapterTable[]
                {   
                    new  AdapterTableInfoFirm ( pEnv, TableINFOFIRM.LOGICALREF )
                }
            )
        {

        }
     
        public override string getCode()
        {
            return _constAdpNamePreix + TableINFOFIRM.TABLE;
        }
        protected override void prepareBeforeUpdate(DataSet pData)
        {
            DataSet dataSet = (DataSet)pData;
            DataTable tab;
            DataRow row;
            tab = dataSet.Tables[TableINFOFIRM.TABLE];
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
