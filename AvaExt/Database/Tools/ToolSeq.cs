using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.SQL;
using AvaExt.Common;

using Mono.Data.Sqlite;
 

namespace AvaExt.Database.Tools
{
    public class ToolSeq
    {
        //const string sql1 = "UPDATE LG_$FIRM$_$PERIOD$_GENSEQ SET LASTLREF = LASTLREF + 1 WHERE ID = 1";
        //const string sql2 = "SELECT LASTLREF FROM LG_$FIRM$_$PERIOD$_GENSEQ WHERE ID = 1";

        const string sql1 = "UPDATE LG_GENSEQ SET LASTLREF = LASTLREF + 1 WHERE ID = 1";
        const string sql2 = "SELECT LASTLREF FROM LG_GENSEQ WHERE ID = 1";


        const string tabPattern = "$ID_SOURCE$";

        public static object get(IEnvironment env )
        {
            SqlExecute.executeNonQuery(env, sql1);
            return SqlExecute.executeScalar(env,sql2 );
        }
        public static object get(IEnvironment env, string seqName)
        {
          return SqlExecute.executeScalar(env,AvaAgent.AvaExt.Database.Tools.Resources.SqlText.getNewId.Replace(tabPattern, seqName) );
 
        }
        public static void lockByUpdate(IEnvironment env, string seqName, object id)
        {
            SqlExecute.executeNonQuery(env,AvaAgent.AvaExt.Database.Tools.Resources.SqlText.lockByUpdate.Replace(tabPattern, seqName),  new object[]{id});

        }
    }
}
