using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.Common.Const;
using AvaExt.SQL;

using AvaExt.Adapter.Tools;
using System.Collections;
using System.Data;
using Mono.Data.Sqlite;

namespace AvaExt.Database.GL
{
    class CardAccountRef
    {

        public static IDictionary getCardToGLRef(IEnvironment pEnv, int card, ConstCardGlRelationTrcode trcode, ConstCardGlRelationType type)
        {
            SqliteParameter[] par = new SqliteParameter[]{
            SqlPF.get(ConstSqlParametersName.parCon1, SqlDbType.SmallInt, trcode),
            SqlPF.get(ConstSqlParametersName.parCon2, SqlDbType.SmallInt,type),
            SqlPF.get(ConstSqlParametersName.parCon3, SqlDbType.Int, card)};
            return SqlExecute.executeGetLine(pEnv, Resource.SqlText.SqlTextCardGlAccRef, par);
        }
 
    }
}
