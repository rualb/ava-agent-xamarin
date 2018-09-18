using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Adapter.ForDataTable;
using AvaExt.Common;
using AvaExt.Adapter.Tools;
using System.Data;
using AvaExt.Manual.Table;
using AvaExt.Common.Const;

namespace AvaExt.Adapter.ForDataSet.Finance.Operation.Cash
{
    public class AdapterDataSetCashOutputClient : AdapterDataSetCashClient
    {

        public AdapterDataSetCashOutputClient(IEnvironment pEnv)
            :base(pEnv)
        {
            
            docTrCode = (short)ConstCashDocType.clientFromCash;
            docSign = (short)ConstOperationSign.credit; 

           moduleNo = (short)ConstOperationType.operationCash;
           clientTranSign = (short)ConstOperationSign.debit;
           clientTranCode = (short)ConstClientTran.clientFromCash;
           payTranSign = (short)ConstOperationSign.debit;
           payTranCode = (short)1;
        }
        
        
    }
}
