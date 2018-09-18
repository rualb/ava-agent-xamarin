using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;
using System.Data;
using AvaExt.Common;


namespace AvaExt.SQL.Dynamic
{
    public class SqlBuilderCompCard : ImplSqlBuilder
    {
        public SqlBuilderCompCard(IEnvironment env)
            : base(env, AvaAgent.AvaExt.SQL.Dynamic.Resource.SqlPattern.PatternSqlBuilderCompCard, TableCAMPAIGN.TABLE)
        {
            addColumnToMeta(TableCAMPAIGN.LOGICALREF, typeof(int));
            addColumnToMeta(TableCAMPAIGN.ACTIVE, typeof(short));
            addColumnToMeta(TableCAMPAIGN.CARDTYPE, typeof(short));
            addColumnToMeta(TableCAMPAIGN.CODE, typeof(string));
            addColumnToMeta(TableCAMPAIGN.NAME, typeof(string));
            addColumnToMeta(TableCAMPAIGN.SPECODE, typeof(string));
            addColumnToMeta(TableCAMPAIGN.CYPHCODE, typeof(string));
            addColumnToMeta(TableCAMPAIGN.BEGDATE, typeof(DateTime));
            addColumnToMeta(TableCAMPAIGN.ENDDATE, typeof(DateTime));
        }


    }
}
