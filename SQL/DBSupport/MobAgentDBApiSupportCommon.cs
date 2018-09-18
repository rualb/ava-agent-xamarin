using System;
using System.Collections.Generic;
using System.Text;
using AvaAgent.AvaExt.SQL.Resources;

using AvaExt.Common;
 
using AvaExt.SQL.DBSupport;

namespace AvaAgent.SQL.DBSupport
{
    public class AvaAgentDBApiSupportCommon : DBSupportBase
    {
        public AvaAgentDBApiSupportCommon(IEnvironment e)
            : base(e, 34, "DBApiCommon", sqlFromFile("MADBCommon.sql"))
        {

        }
    }
}
