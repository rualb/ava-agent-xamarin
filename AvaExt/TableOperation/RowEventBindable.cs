using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.ECollections;
using AvaExt.TableOperation.CellMathActions;
using AvaExt.Common;
using AvaExt.TableOperation.RowValidator;

namespace AvaExt.TableOperation
{
    public abstract class RowEventBindable : BlockHandler
    {
        protected TableTouchDetector toucher;
        protected IRowValidator validator;
        protected DataTable tableSource;
        public RowEventBindable(DataTable table, IRowValidator pValidator)
        {
            tableSource = table;
            validator = (pValidator == null) ? new RowValidatorTrue() : pValidator;
            toucher = new TableTouchDetector(table);

        }

 
        public virtual void activityForRow(DataColumnChangeEventArgs e)
        {
             
        }

        protected virtual void touchCell(DataRow row, string col)
        {
            toucher.touchCell(row, col);
        }
       
        
    }


}

