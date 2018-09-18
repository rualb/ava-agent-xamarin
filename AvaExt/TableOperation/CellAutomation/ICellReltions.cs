using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.TableOperation.RowValidator;
using AvaExt.Common;
using AvaExt.TableOperation.CellAutomation.TableEvents;

namespace AvaExt.TableOperation.CellAutomation
{

    public interface ICellReltions
    {
        void addRelation(string[] pCols, ITableColumnChange pActivity, IRowValidator pValidator);
        void addRelation(string[] pCols, ITableColumnChange pActivity, IBlockPoint pBlock, IRowValidator pValidator);
        void addRelation(string[] pCols, ITableColumnChange pActivity, IRowValidator pValidator, ColumnChangeEventType pEvent);
        void addRelation(string[] pCols, ITableColumnChange pActivity, IBlockPoint pBlock, IRowValidator pValidator, ColumnChangeEventType pEvent);

        IBlockPoint getMainBlockPoint();
        void setMainValidator(IRowValidator pValidator);
        void reinit();
    }
}
