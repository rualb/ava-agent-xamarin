using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;
using System.Data;
using AvaExt.Common;


namespace AvaExt.SQL.Dynamic
{
    public class SqlBuilderInfoFirm : ImplSqlBuilder
    {
        public SqlBuilderInfoFirm(IEnvironment env)
            : base(env,
            string.Format(AvaAgent.AvaExt.SQL.Dynamic.Resource.SqlPattern.PatternSqlBuilder, TableINFOFIRM.TABLE_FULL_NAME, TableINFOFIRM.TABLE),
            TableINFOFIRM.TABLE)
        {
           // addColumnToMeta(TableINFOFIRM.LOGICALREF, typeof(string)); 
      
        }


    }
}
