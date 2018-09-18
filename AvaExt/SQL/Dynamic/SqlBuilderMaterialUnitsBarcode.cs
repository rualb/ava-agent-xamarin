using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;
using System.Data;
using AvaExt.Common;


namespace AvaExt.SQL.Dynamic
{
    public class SqlBuilderMaterialUnitsBarcode : ImplSqlBuilder
    {
        public SqlBuilderMaterialUnitsBarcode(IEnvironment env)
            : base(env, AvaAgent.AvaExt.SQL.Dynamic.Resource.SqlPattern.PatternSqlBuilderMaterialUnitsBarcode, TableUNITBARCODE.TABLE)
        {
            addColumnToMeta(TableUNITBARCODE.LOGICALREF, typeof(int));
            addColumnToMeta(TableUNITBARCODE.ITEMREF, typeof(int));
            addColumnToMeta(TableUNITBARCODE.LINENR, typeof(short));
            addColumnToMeta(TableUNITBARCODE.UNITLINEREF, typeof(int));
            addColumnToMeta(TableUNITBARCODE.BARCODE, typeof(string));

        }


    }
}
