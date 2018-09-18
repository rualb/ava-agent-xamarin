using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;
using System.Data;
using AvaExt.Common;


namespace AvaExt.SQL.Dynamic
{
    public class SqlBuilderMaterialGenStat : ImplSqlBuilder
    {
        public SqlBuilderMaterialGenStat(IEnvironment env)
            : base(env, AvaAgent.AvaExt.SQL.Dynamic.Resource.SqlPattern.PatternSqlBuilderMaterialGenStat, TableGNTOTST.TABLE)
        {
            addColumnToMeta(TableGNTOTST.LOGICALREF, typeof(int));
            addColumnToMeta(TableGNTOTST.INVENNO, typeof(short));
            addColumnToMeta(TableGNTOTST.STOCKREF, typeof(int));
            addColumnToMeta(TableGNTOTST.ONHAND, typeof(double));
            addColumnToMeta(TableGNTOTST.RESERVED, typeof(double));
        }


    }
}
