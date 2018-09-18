using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;
using System.Data;
using AvaExt.Common;


namespace AvaExt.SQL.Dynamic
{
    public class SqlBuilderWarehouse : ImplSqlBuilder
    {
        public SqlBuilderWarehouse(IEnvironment env)
            : base(env, AvaAgent.AvaExt.SQL.Dynamic.Resource.SqlPattern.PatternSqlBuilderWarehouse, TableWHOUSE.TABLE)
        {
            addColumnToMeta(TableWHOUSE.LOGICALREF, typeof(int));
            addColumnToMeta(TableWHOUSE.NR, typeof(string));
            addColumnToMeta(TableWHOUSE.COSTGRP, typeof(short));
            addColumnToMeta(TableWHOUSE.NAME, typeof(string));
            addColumnToMeta(TableWHOUSE.FIRMNR, typeof(short));
            addColumnToMeta(TableWHOUSE.DIVISNR, typeof(short));
            addColumnToMeta(TableWHOUSE.FACTNR, typeof(short));
 
        }


    }
}
