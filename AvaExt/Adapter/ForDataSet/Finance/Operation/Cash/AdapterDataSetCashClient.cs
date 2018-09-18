using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Adapter.ForDataTable;
using AvaExt.Common;
using AvaExt.Adapter.Tools;
using System.Data;
using AvaExt.Manual.Table;
using AvaExt.InfoClass;
using AvaExt.Common.Const;
 
using AvaExt.TableOperation;
using AvaExt.Database.Tools;
using AvaExt.PagedSource;
using AvaExt.SQL.Dynamic.Preparing;
using AvaExt.Manual.Tool;

namespace AvaExt.Adapter.ForDataSet.Finance.Operation.Cash
{
    public class AdapterDataSetCashClient : AdapterDataSetCash
    {

        protected short moduleNo;
        protected short clientTranSign;
        protected short clientTranCode;
        protected short payTranSign;
        protected short payTranCode;

        public AdapterDataSetCashClient(IEnvironment pEnv)
            : base(
            pEnv,
            new IAdapterTable[] {   
                    new AdapterTableCash(pEnv,TableKSLINES.LOGICALREF ) 
            }
            )
        {
        }
 

    }
}
