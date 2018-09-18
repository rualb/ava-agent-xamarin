using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;
using System.Data;
using AvaExt.Common;


namespace AvaExt.SQL.Dynamic
{
    public class SqlBuilderMaterial : ImplSqlBuilder
    {
        public SqlBuilderMaterial(IEnvironment env)
            : base(env, AvaAgent.AvaExt.SQL.Dynamic.Resource.SqlPattern.PatternSqlBuilderMaterial, TableITEMS.TABLE)
        {
            //addColumnToMeta(TableITEMS.LOGICALREF, typeof(string));
            //addColumnToMeta(TableITEMS.ACTIVE, typeof(short));
            //addColumnToMeta(TableITEMS.CARDTYPE, typeof(short));
            //addColumnToMeta(TableITEMS.CODE, typeof(string));
            //addColumnToMeta(TableITEMS.NAME, typeof(string));
            //addColumnToMeta(TableITEMS.SPECODE, typeof(string));
            //addColumnToMeta(TableITEMS.CYPHCODE, typeof(string));
            //addColumnToMeta(TableITEMS.GTIPCODE, typeof(string));
            //addColumnToMeta(TableITEMS.STGRPCODE, typeof(string));
            //addColumnToMeta(TableITEMS.UNITSETREF, typeof(int));
            //addColumnToMeta(TableITEMS.PROMO, typeof(short));
            //addColumnToMeta(TableITEMS.BARCODE1, typeof(string));
            //addColumnToMeta(TableITEMS.BARCODE2, typeof(string));
            //addColumnToMeta(TableITEMS.BARCODE3, typeof(string));
        }


    }
}
