using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;
using System.Data;
using AvaExt.Common;


namespace AvaExt.SQL.Dynamic
{
    public class SqlBuilderClientTransClientFiltered : ImplSqlBuilder
    {
        public SqlBuilderClientTransClientFiltered(IEnvironment env)
            : base(env, AvaAgent.AvaExt.SQL.Dynamic.Resource.SqlPattern.PatternSqlBuilderClientTransClientFiltered, TableCLFLINE.TABLE)
        {
            addColumnToMeta(TableCLFLINE.LOGICALREF, typeof(int));
            addColumnToMeta(TableCLFLINE.CANCELLED, typeof(short));
            addColumnToMeta(TableCLFLINE.DATE_, typeof(DateTime));
            addColumnToMeta(TableCLFLINE.CLIENTREF, typeof(int));
            addColumnToMeta(TableCLFLINE.CLPRJREF, typeof(int));
            //
            addColumnToMeta(TableCLCARD.TABLE, TableCLCARD.LOGICALREF, typeof(int));
            addColumnToMeta(TableCLCARD.TABLE, TableCLCARD.ACTIVE, typeof(short));
            addColumnToMeta(TableCLCARD.TABLE, TableCLCARD.CARDTYPE, typeof(short));
            addColumnToMeta(TableCLCARD.TABLE, TableCLCARD.CODE, typeof(string));
            addColumnToMeta(TableCLCARD.TABLE, TableCLCARD.DEFINITION_, typeof(string));
            addColumnToMeta(TableCLCARD.TABLE, TableCLCARD.SPECODE, typeof(string));
            addColumnToMeta(TableCLCARD.TABLE, TableCLCARD.CYPHCODE, typeof(string));
            addColumnToMeta(TableCLCARD.TABLE, TableCLCARD.DELIVERYFIRM, typeof(string));
        }


    }
}
