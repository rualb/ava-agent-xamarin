using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.Adapter.ForDataSet;
using AvaExt.Common;
using AvaExt.Adapter.Tools;
using AvaExt.TableOperation;

namespace AvaExt.Adapter.ForUser
{
    public class AdapterUserHeaderedRecords : AdapterUserRecords
    {
        protected short docTrCode;
        String headerTableName;

        public AdapterUserHeaderedRecords(IEnvironment pEnv, IAdapterDataSet pAdapter, String pHeaderTableName, String pLinesTableName)
            : base(pEnv, pAdapter, pLinesTableName)
        {
            headerTableName = pHeaderTableName;
        }
        DataTable getHeaderTable()
        {
            return getTable(headerTableName);
        }
        public virtual void setHeader(String pColName, Object pVal)
        {
            ToolColumn.setColumnValue(getHeaderTable(), pColName, pVal);
        }
        public virtual object getHeader(String pColName, Object pValDef)
        {
            return ToolColumn.getColumnLastValue(getHeaderTable(), pColName, pValDef);
        }

        public virtual DataRow getHeaderRecord()
        {
            return ToolRow.getFirstRealRow(getHeaderTable());
        }

    }
}
