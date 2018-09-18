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
    public class AdapterDataSetCashInputClient : AdapterDataSetCashClient
    {

        public AdapterDataSetCashInputClient(IEnvironment pEnv)
            :base(pEnv)
        {
            
            docTrCode = (short)ConstCashDocType.clientToCash;
            docSign = (short)ConstOperationSign.debit; 

           moduleNo = (short)ConstOperationType.operationCash;
           clientTranSign = (short)ConstOperationSign.credit;
           clientTranCode = (short)ConstClientTran.clientToCash;
           payTranSign = (short)ConstOperationSign.credit;
           payTranCode = (short)1;
        }
        
        
    }
}
