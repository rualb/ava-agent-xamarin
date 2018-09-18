using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;
using System.Data;
using AvaExt.Common;


namespace AvaExt.SQL.Dynamic
{
    public class SqlBuilderSalesManClientRel : ImplSqlBuilder
    {
        public SqlBuilderSalesManClientRel(IEnvironment env)
            : base(env, AvaAgent.AvaExt.SQL.Dynamic.Resource.SqlPattern.PatternSqlBuilderSalesManClientRel, TableSLSCLREL.TABLE)
        {

            addColumnToMeta(TableSLSCLREL.SALESMANREF, typeof(int));
            addColumnToMeta(TableSLSCLREL.CLIENTREF, typeof(int));
            addColumnToMeta(TableSLSCLREL.LOGICALREF, typeof(int));
 
        }


    }
}
