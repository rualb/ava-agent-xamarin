using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.TableOperation;
using System.Data;
using AvaExt.TableOperation.RowValidator;

namespace AvaExt.Adapter.Tools.ImplRowValidator
{
    public class ImplValidRowStockGlobal : IRowValidator
    {


        public bool check(DataRow row)
        {
            if (
                ToolStockLine.isLineStlineGlobal(row)
                )
                return true;
            return false;
        }

    }
}
