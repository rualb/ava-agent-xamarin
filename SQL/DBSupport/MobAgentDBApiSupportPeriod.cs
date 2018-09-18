using System;
using System.Collections.Generic;
using System.Text;
using AvaAgent.AvaExt.SQL.Resources;

using AvaExt.Common;
 
using AvaExt.SQL.DBSupport;

namespace AvaAgent.SQL.DBSupport
{
    public class AvaAgentDBApiSupportPeriod : DBSupportBase
    {
        public AvaAgentDBApiSupportPeriod(IEnvironment e)
            : base(e, 76, string.Format("DBApiPeriod_{0}_{1}", e.getInfoApplication().firmId.ToString().PadLeft(3, '0'), e.getInfoApplication().periodId.ToString().PadLeft(2, '0')), sqlFromFile("MADBPeriod.sql"))
        {
 
        }
    }
}



