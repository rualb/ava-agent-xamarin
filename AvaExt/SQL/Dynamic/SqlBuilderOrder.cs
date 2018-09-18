using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;
using System.Data;
using AvaExt.Common;


namespace AvaExt.SQL.Dynamic
{
    public class SqlBuilderOrder : ImplSqlBuilder
    {
        public SqlBuilderOrder(IEnvironment env)
            : base(env, AvaAgent.AvaExt.SQL.Dynamic.Resource.SqlPattern.PatternSqlBuilderOrder, TableORFICHE.TABLE)
        {
            //addColumnToMeta(TableORFICHE.FICHENO, typeof(string));
            //addColumnToMeta(TableORFICHE.SPECODE, typeof(string));
            //addColumnToMeta(TableORFICHE.CYPHCODE, typeof(string));
            //addColumnToMeta(TableORFICHE.LOGICALREF, typeof(string));
            //addColumnToMeta(TableORFICHE.TIME_, typeof(int));
            //addColumnToMeta(TableORFICHE.DATE_, typeof(DateTime));
            //addColumnToMeta(TableORFICHE.TRCODE, typeof(short));
            //addColumnToMeta(TableINVOICE.GRPCODE, typeof(short));
            //addColumnToMeta(TableINVOICE.CANCELLED, typeof(short));
        }


    }
}
