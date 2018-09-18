using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;
using System.Data;
using AvaExt.Common;


namespace AvaExt.SQL.Dynamic
{
    public class SqlBuilderClientTrans : ImplSqlBuilder
    {
        public SqlBuilderClientTrans(IEnvironment env)
            : base(env, AvaAgent.AvaExt.SQL.Dynamic.Resource.SqlPattern.PatternSqlBuilderClientTrans, TableCLFLINE.TABLE)
        {
            addColumnToMeta(TableCLFLINE.LOGICALREF, typeof(int));
            addColumnToMeta(TableCLFLINE.CANCELLED, typeof(short));
            addColumnToMeta(TableCLFLINE.DATE_, typeof(DateTime));
            addColumnToMeta(TableCLFLINE.CLIENTREF, typeof(int));
            addColumnToMeta(TableCLFLINE.CLPRJREF, typeof(int));
            addColumnToMeta(TableCLFLINE.MODULENR, typeof(short));
            addColumnToMeta(TableCLFLINE.SOURCEFREF, typeof(int));
            //

        }


    }
}
