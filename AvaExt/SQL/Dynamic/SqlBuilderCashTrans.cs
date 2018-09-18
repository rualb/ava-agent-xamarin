using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;
using System.Data;
using AvaExt.Common;


namespace AvaExt.SQL.Dynamic
{
    public class SqlBuilderCashTrans : ImplSqlBuilder
    {
        public SqlBuilderCashTrans(IEnvironment env)
            : base(env, AvaAgent.AvaExt.SQL.Dynamic.Resource.SqlPattern.PatternSqlBuilderCashTrans, TableKSLINES.TABLE)
        {
            //addColumnToMeta(TableKSLINES.LOGICALREF, typeof(string));
            //addColumnToMeta(TableKSLINES.CLIENTREF, typeof(string));
            //addColumnToMeta(TableKSLINES.CARDREF, typeof(string));
            //addColumnToMeta(TableKSLINES.CANCELLED, typeof(short));
            //addColumnToMeta(TableKSLINES.TRCODE, typeof(short));
            //addColumnToMeta(TableKSLINES.FICHENO, typeof(string));
            //addColumnToMeta(TableKSLINES.SPECODE, typeof(string));
            //addColumnToMeta(TableKSLINES.CYPHCODE, typeof(string));
            //addColumnToMeta(TableKSLINES.PROJECTREF, typeof(int));

        }


    }
}
