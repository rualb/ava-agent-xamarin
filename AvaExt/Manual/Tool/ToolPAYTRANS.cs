using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.TableOperation;
using AvaExt.Manual.Table;

namespace AvaExt.Manual.Tool
{
    public class ToolPAYTRANS
    {
        public static void syncWithClTran(DataTable tabPayTrans, DataTable tabClTrans)
        {

            DataRow rowTran = ToolRow.getFirstRealRow(tabClTrans);
            if (rowTran != null)
            {
                DataRow rowPay = ToolRow.getFirstRealRow(tabPayTrans);
                if (rowPay == null)
                    tabPayTrans.Rows.Add(rowPay = ToolRow.initTableNewRow(tabPayTrans.NewRow()));

                ToolCell.set(rowPay, TablePAYTRANS.PAYNO, 1);
                ToolCell.set(rowPay, TablePAYTRANS.PROCDATE, rowTran[TableCLFLINE.DATE_]);
                ToolCell.set(rowPay, TablePAYTRANS.DISCDUEDATE, rowTran[TableCLFLINE.DATE_]);
                ToolCell.set(rowPay, TablePAYTRANS.DATE_, rowTran[TableCLFLINE.DATE_]);
                ToolCell.set(rowPay, TablePAYTRANS.TOTAL, rowTran[TableCLFLINE.AMOUNT]);
                ToolCell.set(rowPay, TablePAYTRANS.REPORTRATE, rowTran[TableCLFLINE.REPORTRATE]);
                ToolCell.set(rowPay, TablePAYTRANS.CARDREF, rowTran[TableCLFLINE.CLIENTREF]);
                ToolCell.set(rowPay, TablePAYTRANS.DISCDUEDATE, rowTran[TableCLFLINE.DATE_]);
                ToolCell.set(rowPay, TablePAYTRANS.TRCURR, rowTran[TableCLFLINE.TRCURR]);
                ToolCell.set(rowPay, TablePAYTRANS.TRNET, rowTran[TableCLFLINE.TRNET]);
            }
        }

    }
}
