using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;
using System.Data;
using AvaExt.Common;


namespace AvaExt.SQL.Dynamic
{
    public class SqlBuilderDocPer : ImplSqlBuilder
    {
        public SqlBuilderDocPer(IEnvironment env)
            : base(env,
            string.Format(AvaAgent.AvaExt.SQL.Dynamic.Resource.SqlPattern.PatternSqlBuilder, TablePERDOC.TABLE_LONG, TablePERDOC.TABLE),
            TablePERDOC.TABLE)
        {
            addColumnToMeta(TablePERDOC.LREF, typeof(int));
            addColumnToMeta(TablePERDOC.DOCNR, typeof(int));
            addColumnToMeta(TablePERDOC.DOCTYP, typeof(int));
            addColumnToMeta(TablePERDOC.INFOREF, typeof(int));
            addColumnToMeta(TablePERDOC.INFOTYP, typeof(short));
            addColumnToMeta(TablePERDOC.LDATA, typeof(object));
        }


    }
}
