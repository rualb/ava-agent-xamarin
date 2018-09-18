using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;
using System.Data;
using AvaExt.Common;


namespace AvaExt.SQL.Dynamic
{
    public class SqlBuilderFack : ImplSqlBuilder
    {
        public SqlBuilderFack(IEnvironment env)
            : base(env, AvaAgent.AvaExt.SQL.Dynamic.Resource.SqlPattern.PatternSqlBuilderFack, TableFACK.TABLE)
        {
            addColumnToMeta(TableFACK.LOGICALREF, typeof(int));
            addColumnToMeta(TableFACK.NR, typeof(short));
            addColumnToMeta(TableFACK.NAME, typeof(string));
            addColumnToMeta(TableFACK.FIRMNR, typeof(short));
            addColumnToMeta(TableFACK.DIVISNR, typeof(short));
   
 
        }


    }
}
