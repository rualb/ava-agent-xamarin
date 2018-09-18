using System;
using System.Collections.Generic;
using System.Text;
using AvaAgent.AvaExt.SQL.Resources;

using AvaExt.Common;
 
using AvaExt.SQL.DBSupport;

namespace AvaAgent.SQL.DBSupport
{
    public class AvaAgentDBApiSupportFirm : DBSupportBase
    {
        public AvaAgentDBApiSupportFirm(IEnvironment e)
            : base(e, 62, string.Format("DBApiFirm_{0}", e.getInfoApplication().firmId.ToString().PadLeft(3, '0')), sqlFromFile("MADBFirm.sql"))
        {

        }
    }
}
