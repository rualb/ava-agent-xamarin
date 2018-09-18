using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;
using System.Data;
using AvaExt.Common;


namespace AvaExt.SQL.Dynamic
{
    public class SqlBuilderDiv : ImplSqlBuilder
    {
        public SqlBuilderDiv(IEnvironment env)
            : base(env, AvaAgent.AvaExt.SQL.Dynamic.Resource.SqlPattern.PatternSqlBuilderDiv, TableDIV.TABLE)
        {
            addColumnToMeta(TableDIV.LOGICALREF, typeof(int));
            addColumnToMeta(TableDIV.NR, typeof(short));
            addColumnToMeta(TableDIV.NAME, typeof(string));
            addColumnToMeta(TableDIV.FIRMNR, typeof(short));
        }


    }
}
