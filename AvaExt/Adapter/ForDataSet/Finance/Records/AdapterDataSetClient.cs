using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.Adapter.ForDataTable;
using AvaExt.Manual.Table;
using System.Data;
using AvaExt.TableOperation;

namespace AvaExt.Adapter.ForDataSet.Finance.Records
{
    public class AdapterDataSetClient : AdapterDataSetRecords
    {
        public AdapterDataSetClient(IEnvironment pEnv)
            : base(
            pEnv,
            new IAdapterTable[]
                {   
                    new  AdapterTableClient ( pEnv, TableCLCARD.LOGICALREF )
                }
            )
        {

        }
        
        public override string getCode()
        {
            return _constAdpNamePreix + TableCLCARD.TABLE;
        }
        protected override void prepareBeforeUpdate(DataSet pData)
        {
            DataSet dataSet = (DataSet)pData;
            DataTable tab;
            DataRow row;
            tab = dataSet.Tables[TableCLCARD.TABLE];
            for (int i = 0; i < tab.Rows.Count; ++i)
            {
                row = tab.Rows[i];
                if (row.RowState != DataRowState.Deleted)
                {
                    if (row.RowState == DataRowState.Added)
                    {
                        docId = row[TableCLCARD.LOGICALREF];
                        docNo = (string)row[TableCLCARD.CODE];

                    }
                    else
                    {
                        docId = row[TableCLCARD.LOGICALREF];
                        docNo = (string)row[TableCLCARD.CODE];

                    }

                }
            }

        }
    }
}
