using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;
using System.Data;
using AvaExt.Common;


namespace AvaExt.SQL.Dynamic
{
    public class SqlBuilderClientGenStat : ImplSqlBuilder
    {
        public SqlBuilderClientGenStat(IEnvironment env)
            : base(env, AvaAgent.AvaExt.SQL.Dynamic.Resource.SqlPattern.PatternSqlBuilderClientGenStat, TableGNTOTCL.TABLE)
        {
            addColumnToMeta(TableGNTOTCL.LOGICALREF, typeof(int));
            addColumnToMeta(TableGNTOTCL.TOTTYP, typeof(short));
            addColumnToMeta(TableGNTOTCL.CARDREF, typeof(int));
            addColumnToMeta(TableGNTOTCL.DEBIT, typeof(double));
            addColumnToMeta(TableGNTOTCL.CREDIT, typeof(double));
        }


    }
}
