using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.ECollections;
using AvaExt.TableOperation.CellMathActions;
using AvaExt.Database.Tools;
using AvaExt.TableOperation.RowValidator;

namespace AvaExt.TableOperation
{
    public class RowDateTimeToIntColumnsBinding : RowColumnsBindingBase
    {
        string colDateTime;
        string colIntDate;
 
        delegate DateTime actionFromInt(int val);
        actionFromInt fromInt;
        delegate int actionToInt(DateTime val);
        actionToInt toInt;
        public RowDateTimeToIntColumnsBinding(DataTable table, string pColDateTime, string pColIntDate, bool pConvertDate, IRowValidator pValidator)
            : base(table, new string[] { pColDateTime, pColIntDate }, pValidator)
        {
            colDateTime = pColDateTime;
            colIntDate = pColIntDate;
 
            if (pConvertDate)
            {
                toInt = new actionToInt(ToolGeneral.date2IntDate);
                fromInt = new actionFromInt(ToolGeneral.intDate2Date);
            }
            else
            {
                toInt = new actionToInt(ToolGeneral.time2IntTime);
                fromInt = new actionFromInt(ToolGeneral.intTime2Time);
            }
 
            tableSource.ColumnChanged += new DataColumnChangeEventHandler(table_ColumnChanged);
        }

        protected   void table_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            if (e.Row.RowState != DataRowState.Detached)
            {
                string curColumn = e.Column.ColumnName;
                if (isMyCollumn(curColumn) && validator.check(e.Row))
                {
                    if (block())
                    {
                        try
                        {

                            if (curColumn == colDateTime)
                                ToolCell.set(e.Row, colIntDate, toInt((DateTime)e.ProposedValue));
                            else
                                if (curColumn == colIntDate)
                                    ToolCell.set(e.Row, colDateTime, fromInt((int)e.ProposedValue));

                        }
 
                        finally
                        {
                            unblock();
                        }
                    }
                }
            }
        }

    }

}

