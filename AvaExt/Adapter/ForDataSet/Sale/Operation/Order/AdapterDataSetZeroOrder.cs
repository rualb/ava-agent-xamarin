using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Adapter.ForDataSet.Sale.Operation.Slip;
using AvaExt.Common;
using System.Data;
using AvaExt.Manual.Table;
using AvaExt.Adapter.ForDataTable;

namespace AvaExt.Adapter.ForDataSet.Sale.Operation.Order
{
    public class AdapterDataSetZeroOrder : AdapterDataSetZeroSlip
    {
        public override string getCode()
        {
            return _constAdpNamePreix + TableORFICHE.TABLE;
        }
        public AdapterDataSetZeroOrder(IEnvironment pEnv)
            : base(pEnv)
        {
            dictionary[TableINVOICE.TABLE] = new AdapterTableOrder(pEnv, TableINVOICE.LOGICALREF);
            dictionary[TableSTLINE.TABLE] = new AdapterTableOrderTrans(pEnv, TableSTLINE.STDOCREF);

        }
        protected override void dataTransfered(System.Data.DataSet pDataSet)
        {
            base.dataTransfered(pDataSet);
            pDataSet.Tables[TableORFICHE.TABLE].TableName = TableINVOICE.TABLE;
            pDataSet.Tables[TableORFLINE.TABLE].TableName = TableSTLINE.TABLE;
        }
    }
}
