using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;
using System.Data;
using AvaExt.Common;


namespace AvaExt.SQL.Dynamic
{
    public class SqlBuilderMarkingCodes : ImplSqlBuilder
    {
        public SqlBuilderMarkingCodes(IEnvironment env)
            : base(env, AvaAgent.AvaExt.SQL.Dynamic.Resource.SqlPattern.PatternSqlBuilderMarkingCodes, TableSPECODES.TABLE)
        {
            addColumnToMeta(TableSPECODES.LOGICALREF, typeof(int));
            addColumnToMeta(TableSPECODES.CODETYPE, typeof(short));
            addColumnToMeta(TableSPECODES.SPECODETYPE, typeof(short));
            addColumnToMeta(TableSPECODES.SPECODE, typeof(string));
            addColumnToMeta(TableSPECODES.DEFINITION_, typeof(string));
            addColumnToMeta(TableSPECODES.WINCOLOR, typeof(int));
        }
    }
}
