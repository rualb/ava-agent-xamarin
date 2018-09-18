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
    public class AdapterUserCashOutputClient:AdapterUserCashClient
    {

        public AdapterUserCashOutputClient(IEnvironment pEnv, IAdapterDataSet pAdapterDataSet)
            : base(pEnv,pAdapterDataSet)
        {
            docTrCode = (short)ConstCashDocType.clientFromCash;
            docSign = (short)ConstOperationSign.credit;

            moduleNo = (short)ConstOperationType.operationCash;
            clientTranSign = (short)ConstOperationSign.debit;
            clientTranCode = (short)ConstClientTran.clientFromCash;
            payTranSign = (short)ConstOperationSign.debit;
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
