using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;
using System.Data;
using AvaExt.Common;


namespace AvaExt.SQL.Dynamic
{
    public class SqlBuilderSalesMan : ImplSqlBuilder
    {
        public SqlBuilderSalesMan(IEnvironment env)
            : base(env, AvaAgent.AvaExt.SQL.Dynamic.Resource.SqlPattern.PatternSqlBuilderSalesMan, TableSLSMAN.TABLE)
        {
         
            addColumnToMeta(TableSLSMAN.SPECODE, typeof(string));
            addColumnToMeta(TableSLSMAN.CYPHCODE, typeof(string));
            addColumnToMeta(TableSLSMAN.LOGICALREF, typeof(int));
            addColumnToMeta(TableSLSMAN.CODE, typeof(string));
            addColumnToMeta(TableSLSMAN.DEFINITION_, typeof(string));
            addColumnToMeta(TableSLSMAN.FIRMNR, typeof(short));
            addColumnToMeta(TableSLSMAN.USERID, typeof(short));
        }


    }
}
