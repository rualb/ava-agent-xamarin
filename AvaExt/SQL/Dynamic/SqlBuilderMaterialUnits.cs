using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;
using System.Data;
using AvaExt.Common;


namespace AvaExt.SQL.Dynamic
{
    public class SqlBuilderMaterialUnits : ImplSqlBuilder
    {
        public SqlBuilderMaterialUnits(IEnvironment env)
            : base(env, AvaAgent.AvaExt.SQL.Dynamic.Resource.SqlPattern.PatternSqlBuilderMaterialUnits, TableITMUNITA.TABLE)
        {
            addColumnToMeta(TableITMUNITA.LOGICALREF, typeof(int));
            addColumnToMeta(TableITMUNITA.ITEMREF, typeof(int));
            addColumnToMeta(TableITMUNITA.WEIGHT, typeof(double));
            addColumnToMeta(TableITMUNITA.WEIGHTREF, typeof(int));
            addColumnToMeta(TableITMUNITA.UNITLINEREF, typeof(int));
            addColumnToMeta(TableITMUNITA.BARCODE, typeof(string));
            addColumnToMeta(TableITMUNITA.BARCODE2, typeof(string));
            addColumnToMeta(TableITMUNITA.BARCODE3, typeof(string));
        }


    }
}
