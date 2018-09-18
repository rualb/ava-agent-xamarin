using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.ECollections;
using AvaExt.TableOperation.CellMathActions;
using AvaExt.TableOperation.RowValidator;

namespace AvaExt.TableOperation
{
    public enum RowMaintainDirection : short
    {
        down = 1,
        up = 2
    }
    public enum RowMaintainPlace : short
    {
        top = 1,
        bottom = 2
    }
    public class RowMaintainAtTopBottom : RowEventBindable
    {
        string[] cols;
        object[] values;
        IRowValidator maintainRow;
        RowMaintainDirection direction;
        RowMaintainPlace place;
        RowMaintainPlace noSectionPlace;
        public RowMaintainAtTopBottom(RowMaintainDirection pDirection, RowMaintainPlace pPlace, RowMaintainPlace pNoSectionPlace, DataTable table, string[] pCols, object[] pValues, IRowValidator pValidatorRow, IRowValidator pValidator)
            : base(table, pValidator)
        {
            direction = pDirection;
            place = pPlace;
            noSectionPlace = pNoSectionPlace;
            cols = pCols;
            values = pValues;
            maintainRow = (pValidatorRow == null) ? new RowValidatorTrue() : pValidatorRow;
            tableSource.RowDeleted += new DataRowChangeEventHandler(table_RowDeleted);
        }

        protected void table_RowDeleted(object sender, DataRowChangeEventArgs e)
        {
            check();
        }



        public void check()
        {
            if (block())
            {
                try
                {
                    DataRow fRow = null;
                    switch (direction)
                    {
                        case RowMaintainDirection.down:
                            fRow = ToolRow.getFirstRealRow(tableSource, validator);
                            break;
                        case RowMaintainDirection.up:
                            fRow = ToolRow.getLastRealRow(tableSource, validator);
                            break;
                    }

                    if (fRow == null || !maintainRow.check(fRow))
                    {
                        DataRow nRow = ToolRow.initTableNewRow(tableSource.NewRow());
                        for (int i = 0; i < cols.Length; ++i)
                            ToolCell.set(nRow, cols[i], values[i]);
                        int indx=0;

                        switch (place)
                        {
                            case RowMaintainPlace.top:
                                indx = ToolRow.getSectionStart(tableSource, validator);
                                break;
                            case RowMaintainPlace.bottom:
                                indx = ToolRow.getSectionEnd(tableSource, validator);
                                if (indx >= 0)
                                    ++indx;
                                break;
                        }
                        if (indx < 0)
                            switch (noSectionPlace)
                            {
                                case RowMaintainPlace.top:
                                    indx = 0;
                                    break;
                                case RowMaintainPlace.bottom:
                                    indx = int.MaxValue;
                                    break;
                            }
                        tableSource.Rows.InsertAt(nRow, indx);


                    }
                }

                finally
                {
                    unblock();
                }
            }
        }
    }

}

