using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;
using System.Data;
using AvaExt.Common;


namespace AvaExt.SQL.Dynamic
{
    public class SqlBuilderUser : ImplSqlBuilder
    {
        public SqlBuilderUser(IEnvironment env)
            : base(env, AvaAgent.AvaExt.SQL.Dynamic.Resource.SqlPattern.PatternSqlBuilderUser, TableUSER.TABLE)
        {
            addColumnToMeta(TableUSER.LOGICALREF, typeof(int));
            addColumnToMeta(TableUSER.NR, typeof(short));
            addColumnToMeta(TableUSER.NAME, typeof(string));
 
 
        }


    }
}
