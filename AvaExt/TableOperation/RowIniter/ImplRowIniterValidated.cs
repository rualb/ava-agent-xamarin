using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.TableOperation.RowValidator;

namespace AvaExt.TableOperation.RowIniter
{
    public class ImplRowIniterValidated : IRowIniter
    {
        string[] cols;
        object[] vals;
        IRowValidator validator;
        public ImplRowIniterValidated(string[] pCols, object[] pVals, IRowValidator pValidator)
        {
            cols = pCols;
            vals = pVals;
            validator = pValidator;
        }
        public void set(DataRow pRow)
        {
            if (pRow != null && validator.check(pRow))
                for (int i = 0; i < cols.Length; ++i)
                    ToolCell.set(pRow, cols[i], vals[i]);
        }


    }
}
