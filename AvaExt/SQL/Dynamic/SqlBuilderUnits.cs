using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;
using System.Data;
using AvaExt.Common;


namespace AvaExt.SQL.Dynamic
{
    public class SqlBuilderUnits : ImplSqlBuilder
    {
        public SqlBuilderUnits(IEnvironment env)
            : base(env, AvaAgent.AvaExt.SQL.Dynamic.Resource.SqlPattern.PatternSqlBuilderUnits, TableUNITSETL.TABLE)
        {
            addColumnToMeta(TableUNITSETL.LOGICALREF, typeof(int));
            addColumnToMeta(TableUNITSETL.UNITSETREF, typeof(int));
            addColumnToMeta(TableUNITSETL.MAINUNIT, typeof(short));
            addColumnToMeta(TableUNITSETL.CODE, typeof(string));
            addColumnToMeta(TableUNITSETL.NAME, typeof(string));
            addColumnToMeta(TableUNITSETL.LINENR, typeof(short));
        }
    }
}
