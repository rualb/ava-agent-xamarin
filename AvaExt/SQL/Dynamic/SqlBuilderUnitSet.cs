using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;
using System.Data;
using AvaExt.Common;


namespace AvaExt.SQL.Dynamic
{
    public class SqlBuilderUnitSet : ImplSqlBuilder
    {
        public SqlBuilderUnitSet(IEnvironment env)
            : base(env, AvaAgent.AvaExt.SQL.Dynamic.Resource.SqlPattern.PatternSqlBuilderUnitSet, TableUNITSETF.TABLE)
        {
            addColumnToMeta(TableUNITSETF.LOGICALREF, typeof(int));
            addColumnToMeta(TableUNITSETF.CARDTYPE, typeof(short));
            addColumnToMeta(TableUNITSETF.CODE, typeof(string));
            addColumnToMeta(TableUNITSETF.NAME, typeof(string));
        }
    }
}
