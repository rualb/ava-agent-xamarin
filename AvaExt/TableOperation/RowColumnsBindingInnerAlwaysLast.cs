using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.ECollections;
using AvaExt.TableOperation.CellMathActions;
using AvaExt.TableOperation.RowValidator;

namespace AvaExt.TableOperation
{
    public class RowColumnsBindingInnerAlwaysLast:RowColumnsBindingInner 
    {
        string lastCol;
        public RowColumnsBindingInnerAlwaysLast(DataTable table, string[] colArr,string col, double coif, ICellMath pForward, ICellMath pBackward, IRowValidator pValidator)
            : base(table, colArr, coif, pForward, pBackward, pValidator)
        {
            lastCol = col;
        }

        protected override void touchCell(DataRow row, string col)
        {
            base.touchCell(row, col);
            base.touchCell(row, lastCol);
        }
    }
 
}

