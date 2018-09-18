using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;
using System.Data;
using AvaExt.Common;


namespace AvaExt.SQL.Dynamic
{
    public class SqlBuilderDynClientGroupGenStat : ImplSqlBuilder  
    {
 
        public SqlBuilderDynClientGroupGenStat(IEnvironment env)
            : base(env, AvaAgent.AvaExt.SQL.Dynamic.Resource.SqlPattern.PatternSqlBuilderDynClientGroupGenStat, TableGNTOTCL.TABLE)
        {
            addColumnToMeta(TableCLCARD.TABLE, TableCLCARD.PARENTCLREF, typeof(int));
            addColumnToMeta(TableCLCARD.TABLE, TableCLCARD.CARDTYPE, typeof(short));
            //
            addColumnToMeta(TableGNTOTCL.LOGICALREF, typeof(int));
            addColumnToMeta(TableGNTOTCL.TOTTYP, typeof(short));
            addColumnToMeta(TableGNTOTCL.CARDREF, typeof(int));
            addColumnToMeta(TableGNTOTCL.DEBIT, typeof(double));
            addColumnToMeta(TableGNTOTCL.CREDIT, typeof(double));
            //
 
        }


    }
}
