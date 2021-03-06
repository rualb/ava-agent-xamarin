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
    public class AdapterDataSetInfoPeriod : AdapterDataSetRecords
    {
        public AdapterDataSetInfoPeriod(IEnvironment pEnv)
            : base(
            pEnv,
            new IAdapterTable[]
                {   
                    new  AdapterTableInfoPeriod ( pEnv, TableINFOPERIOD.LOGICALREF )
                }
            )
        {

        }
     
        public override string getCode()
        {
            return _constAdpNamePreix + TableINFOPERIOD.TABLE;
        }
        protected override void prepareBeforeUpdate(DataSet pData)
        {
            DataSet dataSet = (DataSet)pData;
            DataTable tab;
            DataRow row;
            tab = dataSet.Tables[TableINFOPERIOD.TABLE];
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
