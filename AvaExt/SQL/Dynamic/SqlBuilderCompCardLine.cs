using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;
using System.Data;
using AvaExt.Common;


namespace AvaExt.SQL.Dynamic
{
    public class SqlBuilderCompCardLine : ImplSqlBuilder
    {
        public SqlBuilderCompCardLine(IEnvironment env)
            : base(env, AvaAgent.AvaExt.SQL.Dynamic.Resource.SqlPattern.PatternSqlBuilderCompCardLine,TableCMPGNLINE.TABLE)
        {
            addColumnToMeta(TableCMPGNLINE.LOGICALREF, typeof(int));
            addColumnToMeta(TableCMPGNLINE.CAMPCARDREF, typeof(int));
         
        }


    }
}
