using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.Adapter.ForDataTable;
using AvaExt.Manual.Table;
using System.Data;
using AvaExt.TableOperation;

namespace AvaExt.Adapter.ForDataSet.Admin.Records
{
    public class AdapterDataSetFirmParams : AdapterDataSetRecords
    {
        public AdapterDataSetFirmParams(IEnvironment pEnv)
            : base(
            pEnv,
            new IAdapterTable[]
                {   
                    new  AdapterTableFirmParams ( pEnv, TableFIRMPARAMS.CODE )
                }
            )
        {

        }
 
        public override string getCode()
        {
            return _constAdpNamePreix + TableFIRMPARAMS.TABLE;
        }
        protected override void prepareBeforeUpdate(DataSet pData)
        {
            DataSet dataSet = (DataSet)pData;
            DataTable tab;
            DataRow row;
            tab = dataSet.Tables[TableFIRMPARAMS.TABLE];
            for (int i = 0; i < tab.Rows.Count; ++i)
            {
                row = tab.Rows[i];
                if (row.RowState != DataRowState.Deleted)
                {
                    if (row.RowState == DataRowState.Added)
                    {
                        docId = row[TableFIRMPARAMS.CODE];
                        docNo = (string)row[TableFIRMPARAMS.CODE];

                    }
                    else
                    {
                        docId = row[TableFIRMPARAMS.CODE];
                        docNo = (string)row[TableFIRMPARAMS.CODE];

                    }

                }
            }

        }
    }
}
