using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;
using System.Data;
using AvaExt.Common;


namespace AvaExt.SQL.Dynamic
{
    public class SqlBuilderCurrencyList : ImplSqlBuilder
    {
        public SqlBuilderCurrencyList(IEnvironment env)
            : base(env, AvaAgent.AvaExt.SQL.Dynamic.Resource.SqlPattern.PatternSqlBuilderCurrencyList, TableCURRENCYLIST.TABLE)
        {
            addColumnToMeta(TableCURRENCYLIST.LOGICALREF, typeof(int));
            addColumnToMeta(TableCURRENCYLIST.FIRMNR, typeof(short));
            addColumnToMeta(TableCURRENCYLIST.CURTYPE, typeof(short));
            addColumnToMeta(TableCURRENCYLIST.CURCODE, typeof(string));
            addColumnToMeta(TableCURRENCYLIST.CURNAME, typeof(string));
            addColumnToMeta(TableCURRENCYLIST.CURINUSE, typeof(short));
 
        }


    }
}
