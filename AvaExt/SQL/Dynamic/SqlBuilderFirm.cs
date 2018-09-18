using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;
using System.Data;
using AvaExt.Common;


namespace AvaExt.SQL.Dynamic
{
    public class SqlBuilderFirm : ImplSqlBuilder
    {
        public SqlBuilderFirm(IEnvironment env)
            : base(env, AvaAgent.AvaExt.SQL.Dynamic.Resource.SqlPattern.PatternSqlBuilderFirm, TableFIRM.TABLE)
        {
            addColumnToMeta(TableFIRM.LOGICALREF, typeof(int));
            addColumnToMeta(TableFIRM.NR, typeof(short));
            addColumnToMeta(TableFIRM.NAME, typeof(string));
 
        }


    }
}
