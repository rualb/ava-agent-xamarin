using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.InfoClass;
using AvaExt.Adapter.Tools;
using AvaExt.Common;
using System.Data;
using AvaExt.Manual.Table;
using AvaExt.Common.Const;
using AvaExt.TableOperation;
using AvaExt.Database.Tools;
using System.Collections;
using AvaExt.Database.Const;

namespace AvaExt.Adapter.Tools
{
    public class ToolSlip
    {


        public static string newInviceNo(int seed)
        {
            return 'S' + seed.ToString("000000000000000");
        }
        public static string newSlipNo(int seed)
        {
            return 'S' + seed.ToString("000000000000000");
        }
        public static string newOrdNo(int seed)
        {
            return 'O' + seed.ToString("0000000");
        }
        public static string newCashNo(int seed)
        {
            return 'S' + seed.ToString("0000000");
        }
        public static string newTranNo(int seed)
        {
            return 'T' + seed.ToString("0000000");
        }

        public static bool isDsSlip(DataSet ds)
        {
            if (ds != null)
                if (
                    ds.Tables.Contains(TableSTLINE.TABLE) &&
                    ds.Tables[TableSTLINE.TABLE].Columns.Contains(TableSTLINE.ORFLINEREF)
                    )
                    return true;
            return false;
        }
        public static bool isDsOrder(DataSet ds)
        {
            if (ds != null)
                if (
                    ds.Tables.Contains(TableSTLINE.TABLE) &&
                  (!ds.Tables[TableSTLINE.TABLE].Columns.Contains(TableSTLINE.ORFLINEREF))
                    )
                    return true;
            return false;
        }



    }
}
