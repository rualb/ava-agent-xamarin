using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;
using System.Data;
using AvaExt.Common;


namespace AvaExt.SQL.Dynamic
{
    public class SqlBuilderOrderClientFiltered : ImplSqlBuilder
    {
        public SqlBuilderOrderClientFiltered(IEnvironment env)
            : base(env, AvaAgent.AvaExt.SQL.Dynamic.Resource.SqlPattern.PatternSqlBuilderOrderClientFiltered, TableORFICHE.TABLE)
        {
            //addColumnToMeta(TableORFICHE.FICHENO, typeof(string));
            //addColumnToMeta(TableORFICHE.SPECODE, typeof(string));
            //addColumnToMeta(TableORFICHE.CYPHCODE, typeof(string));
            //addColumnToMeta(TableORFICHE.LOGICALREF, typeof(string));
            //addColumnToMeta(TableORFICHE.TIME_, typeof(int));
            //addColumnToMeta(TableORFICHE.DATE_, typeof(DateTime));
            //addColumnToMeta(TableORFICHE.TRCODE, typeof(short));
            ////
            //addColumnToMeta(TableCLCARD.TABLE, TableCLCARD.LOGICALREF, typeof(int));
            //addColumnToMeta(TableCLCARD.TABLE, TableCLCARD.ACTIVE, typeof(short));
            //addColumnToMeta(TableCLCARD.TABLE, TableCLCARD.CARDTYPE, typeof(short));
            //addColumnToMeta(TableCLCARD.TABLE, TableCLCARD.CODE, typeof(string));
            //addColumnToMeta(TableCLCARD.TABLE, TableCLCARD.DEFINITION_, typeof(string));
            //addColumnToMeta(TableCLCARD.TABLE, TableCLCARD.SPECODE, typeof(string));
            //addColumnToMeta(TableCLCARD.TABLE, TableCLCARD.CYPHCODE, typeof(string));
            //addColumnToMeta(TableCLCARD.TABLE,TableCLCARD.DELIVERYFIRM, typeof(string));
        }


    }
}
