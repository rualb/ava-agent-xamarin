using System;
using System.Collections.Generic;
using System.Text;
using AvaAgent.AvaExt.SQL.Resources;

using AvaExt.Common;
 
using AvaExt.SQL.DBSupport;

namespace AvaAgent.SQL.DBSupport
{
    public class AvaAgentDBApiSupport  : DBSupportBase
    {
        public AvaAgentDBApiSupport(IEnvironment e)
            : base(e, 34, "DBVers", sqlFromFile("database.sql"))
        {
          
        }
    }
}
