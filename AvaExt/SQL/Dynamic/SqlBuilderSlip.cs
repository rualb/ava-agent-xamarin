using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;
using System.Data;
using AvaExt.Common;


namespace AvaExt.SQL.Dynamic
{
    public class SqlBuilderSlip : ImplSqlBuilder
    {
        public SqlBuilderSlip(IEnvironment env)
            : base(env, AvaAgent.AvaExt.SQL.Dynamic.Resource.SqlPattern.PatternSqlBuilderSlip, TableINVOICE.TABLE)
        {
 
            //addColumnToMeta(TableINVOICE.SPECODE, typeof(string));
            //addColumnToMeta(TableINVOICE.CYPHCODE, typeof(string));
            //addColumnToMeta(TableINVOICE.DOCODE, typeof(string));
            //addColumnToMeta(TableINVOICE.LOGICALREF, typeof(string));
            //addColumnToMeta(TableINVOICE.GRPCODE, typeof(short));
            //addColumnToMeta(TableINVOICE.FTIME, typeof(int));
            //addColumnToMeta(TableINVOICE.DATE_, typeof(DateTime));
            //addColumnToMeta(TableINVOICE.TRCODE, typeof(short));
            //addColumnToMeta(TableINVOICE.INVOICEREF, typeof(string));
            //addColumnToMeta(TableINVOICE.IOCODE, typeof(short));
            //addColumnToMeta(TableINVOICE.CANCELLED, typeof(short));
        }


    }
}
