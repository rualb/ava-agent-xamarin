using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;
using System.Data;
using AvaExt.Common;


namespace AvaExt.SQL.Dynamic
{
    public class SqlBuilderDocNum : ImplSqlBuilder
    {
        public SqlBuilderDocNum(IEnvironment env)
            : base(env, AvaAgent.AvaExt.SQL.Dynamic.Resource.SqlPattern.PatternSqlBuilderDocNum, TableDOCNUM.TABLE)
        {
            addColumnToMeta(TableDOCNUM.LOGICALREF, typeof(int));
            addColumnToMeta(TableDOCNUM.FIRMID, typeof(int));
            addColumnToMeta(TableDOCNUM.APPMODULE, typeof(int));
            addColumnToMeta(TableDOCNUM.DIVISID, typeof(int));
            addColumnToMeta(TableDOCNUM.DOCIDEN, typeof(int));
            addColumnToMeta(TableDOCNUM.FACTID, typeof(int));
            addColumnToMeta(TableDOCNUM.GROUPID, typeof(int));
            addColumnToMeta(TableDOCNUM.ROLEID, typeof(int));
            addColumnToMeta(TableDOCNUM.WHID, typeof(int));
            addColumnToMeta(TableDOCNUM.USERID, typeof(int));
            addColumnToMeta(TableDOCNUM.EFFEDATE, typeof(DateTime));
            addColumnToMeta(TableDOCNUM.EFFSDATE, typeof(DateTime));
        }


    }
}
