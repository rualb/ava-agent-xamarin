using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;
using System.Data;
using AvaExt.Common;


namespace AvaExt.SQL.Dynamic
{
    public class SqlBuilderInfoPeriod : ImplSqlBuilder
    {
        public SqlBuilderInfoPeriod(IEnvironment env)
            : base(env,
            string.Format(AvaAgent.AvaExt.SQL.Dynamic.Resource.SqlPattern.PatternSqlBuilder, TableINFOPERIOD.TABLE_FULL_NAME, TableINFOPERIOD.TABLE),
            TableINFOPERIOD.TABLE)
        {
       //     addColumnToMeta(TableINFOPERIOD.LOGICALREF, typeof(string)); 
      
        }


    }
}
