using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;
using System.Data;
using AvaExt.Common;


namespace AvaExt.SQL.Dynamic
{
    public class SqlBuilderDailyExchanges : ImplSqlBuilder
    {
        public SqlBuilderDailyExchanges(IEnvironment env)
            : base(env, AvaAgent.AvaExt.SQL.Dynamic.Resource.SqlPattern.PatternSqlBuilderDailyExchanges, TableDAILYEXCHANGES.TABLE)
        {
            addColumnToMeta(TableDAILYEXCHANGES.LREF, typeof(int));
            addColumnToMeta(TableDAILYEXCHANGES.CRTYPE, typeof(short));
            addColumnToMeta(TableDAILYEXCHANGES.DATE_, typeof(int));
            addColumnToMeta(TableDAILYEXCHANGES.RATES1, typeof(double));
            addColumnToMeta(TableDAILYEXCHANGES.RATES2, typeof(double));
            addColumnToMeta(TableDAILYEXCHANGES.RATES3, typeof(double));
            addColumnToMeta(TableDAILYEXCHANGES.RATES4, typeof(double));
 
        }


    }
}
