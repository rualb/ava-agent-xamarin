using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;
using System.Data;
using AvaExt.Common;


namespace AvaExt.SQL.Dynamic
{
    public class SqlBuilderClientPayTrans : ImplSqlBuilder
    {
        public SqlBuilderClientPayTrans(IEnvironment env)
            : base(env, AvaAgent.AvaExt.SQL.Dynamic.Resource.SqlPattern.PatternSqlBuilderClientPayTrans, TablePAYTRANS.TABLE)
        {
            addColumnToMeta(TablePAYTRANS.LOGICALREF, typeof(int));
            addColumnToMeta(TablePAYTRANS.MODULENR, typeof(short));
            addColumnToMeta(TablePAYTRANS.DATE_, typeof(DateTime));
            addColumnToMeta(TablePAYTRANS.FICHEREF, typeof(int));
            addColumnToMeta(TablePAYTRANS.TRCODE, typeof(short));
            addColumnToMeta(TablePAYTRANS.CARDREF, typeof(int));
            addColumnToMeta(TablePAYTRANS.CANCELLED, typeof(short));
            //

        }


    }
}
