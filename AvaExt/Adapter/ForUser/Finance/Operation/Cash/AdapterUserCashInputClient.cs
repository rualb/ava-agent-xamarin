using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Adapter.ForDataSet;
using AvaExt.Manual.Table;
using System.Data;
using System.ComponentModel;
using AvaExt.Common;
using AvaExt.Common.Const;

namespace AvaExt.Adapter.ForUser.Finance.Operation.Cash
{
    public class AdapterUserCashInputClient:AdapterUserCashClient
    {

        public AdapterUserCashInputClient(IEnvironment pEnv, IAdapterDataSet pAdapterDataSet)
            : base(pEnv,pAdapterDataSet)
        {
            docTrCode = (short)ConstCashDocType.clientToCash;
            docSign = (short)ConstOperationSign.debit;

            moduleNo = (short)ConstOperationType.operationCash;
            clientTranSign = (short)ConstOperationSign.credit;
            clientTranCode = (short)ConstClientTran.clientToCash;
            payTranSign = (short)ConstOperationSign.credit;
            payTranCode = (short)1;
        }
         
        protected override void newRowInCollection(DataRow row)
        {
            base.newRowInCollection(row);


            switch (row.Table.TableName)
                {
                    case TableKSLINES.TABLE:
                        break;
           
                }


        }
    }
}
