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
    public class AdapterDataSetMaterial : AdapterDataSetRecords
    {
        public AdapterDataSetMaterial(IEnvironment pEnv)
            : base(
            pEnv,
            new IAdapterTable[]
                {   
                    new  AdapterTableMaterial ( pEnv, TableITEMS.LOGICALREF )
                }
            )
        {

        }
     
        public override string getCode()
        {
            return _constAdpNamePreix + TableITEMS.TABLE;
        }
        protected override void prepareBeforeUpdate(DataSet pData)
        {
            DataSet dataSet = (DataSet)pData;
            DataTable tab;
            DataRow row;
            tab = dataSet.Tables[TableITEMS.TABLE];
            for (int i = 0; i < tab.Rows.Count; ++i)
            {
                row = tab.Rows[i];
                if (row.RowState != DataRowState.Deleted)
                {
                    if (row.RowState == DataRowState.Added)
                    {
                        docId = row[TableITEMS.LOGICALREF];
                        docNo = (string)row[TableITEMS.CODE];

                    }
                    else
                    {
                        docId = row[TableITEMS.LOGICALREF];
                        docNo = (string)row[TableITEMS.CODE];

                    }

                }
            }

        }
    }
}
