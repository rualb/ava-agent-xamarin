using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;
using System.Data;
using AvaExt.Common;


namespace AvaExt.SQL.Dynamic
{
    public class SqlBuilderDep : ImplSqlBuilder
    {
        public SqlBuilderDep(IEnvironment env)
            : base(env, AvaAgent.AvaExt.SQL.Dynamic.Resource.SqlPattern.PatternSqlBuilderDep, TableDEP.TABLE)
        {
            addColumnToMeta(TableDEP.LOGICALREF, typeof(int));
            addColumnToMeta(TableDEP.NR, typeof(short));
            addColumnToMeta(TableDEP.NAME, typeof(string));
            addColumnToMeta(TableDEP.FIRMNR, typeof(short));


        }


    }
}
