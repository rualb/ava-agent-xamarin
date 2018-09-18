using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.ECollections;
using AvaExt.TableOperation.CellMathActions;
using AvaExt.TableOperation.RowValidator;
using AvaExt.TableOperation.CellAutomation.TableEvents;

namespace AvaExt.TableOperation.CellAutomation
{

    public class CellAutomationReseter : ITableColumnChange
    {
        string[] cols;
        public CellAutomationReseter(string[] pCols)
        {
            cols = pCols;
        }
 
 


        public void columnChange(DataColumnChangeEventArgs e)
        {
            ToolRow.initTableNewRow(e.Row, cols);
        }

        public void initForColumnChanged(DataRow pRow)
        {

        }


    }

}

