using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.TableOperation;
using System.Data;
using AvaExt.TableOperation.RowValidator;

namespace AvaExt.Adapter.Tools.ImplRowValidator
{
    public class ImplValidRowStockPriced:IRowValidator
    {
 

        public bool check( DataRow row)
        {
            if (
                ToolStockLine.isLineDeposit(row) ||
                ToolStockLine.isLineMaterial(row) ||
                ToolStockLine.isLineMixed(row) ||
                ToolStockLine.isLinePromotion(row) ||
                ToolStockLine.isLineService(row)
                )
                return true;
            return false;
        }
 
    }
}
