using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;
using System.Data;
using AvaExt.Common;


namespace AvaExt.SQL.Dynamic
{
    public class SqlBuilderOrderTrans : ImplSqlBuilder
    {
        public SqlBuilderOrderTrans(IEnvironment env)
            : base(env, AvaAgent.AvaExt.SQL.Dynamic.Resource.SqlPattern.PatternSqlBuilderOrderTrans, TableORFLINE.TABLE)
        {
            addColumnToMeta(TableORFLINE.SPECODE, typeof(string));
            addColumnToMeta(TableORFLINE.LOGICALREF, typeof(string));
            addColumnToMeta(TableORFLINE.TIME_, typeof(int));
            addColumnToMeta(TableORFLINE.DATE_, typeof(DateTime));
            addColumnToMeta(TableORFLINE.TRCODE, typeof(short));
            // addColumnToMeta(TableORFLINE.ORDFICHEREF, typeof(string));
            addColumnToMeta(TableSTLINE.STDOCREF, typeof(string));
        }


    }
}
