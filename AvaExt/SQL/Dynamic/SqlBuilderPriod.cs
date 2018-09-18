using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;
using System.Data;
using AvaExt.Common;


namespace AvaExt.SQL.Dynamic
{
    public class SqlBuilderPeriod : ImplSqlBuilder
    {
        public SqlBuilderPeriod(IEnvironment env)
            : base(env, AvaAgent.AvaExt.SQL.Dynamic.Resource.SqlPattern.PatternSqlBuilderPeriod, TablePERIOD.TABLE)
        {
            addColumnToMeta(TablePERIOD.LOGICALREF, typeof(int));
            addColumnToMeta(TablePERIOD.NR, typeof(short));
            addColumnToMeta(TablePERIOD.FIRMNR, typeof(short));
            addColumnToMeta(TablePERIOD.ACTIVE, typeof(short));

 
        }


    }
}
