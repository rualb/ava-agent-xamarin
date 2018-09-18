using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.ECollections;
using AvaExt.TableOperation.CellMathActions;
using AvaExt.PagedSource;
using System.Collections.Specialized;
using System.Collections;
using AvaExt.TableOperation.RowValidator;

namespace AvaExt.TableOperation
{
    //public class RowColumnsBindingDBRelationsCopy : RowColumnsBindingBase
    //{
    //    StringDictionary dic = new StringDictionary();
    //    IPagedSource source;
    //    string[] parentCol;
    //    string[] updateColSource;
    //    string[] updateColDest;
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="pTable"></param>
    //    /// <param name="pSource"></param>
    //    /// <param name="pChildCol">binding</param>
    //    /// <param name="pParentCol">binding</param>
    //    /// <param name="pUpdateColSource">source</param>
    //    /// <param name="pUpdateColDest">dest</param>
    //    /// <param name="pValidator"></param>

    //    public RowColumnsBindingDBRelationsCopy(DataTable pTable, IPagedSource pSource, string[] pChildCol, string[] pParentCol, string[] pUpdateColSource, string[] pUpdateColDest, IRowValidator pValidator)
    //        : base(pTable, null, (pValidator == null) ? new RowValidatorTrue() : pValidator)
    //    {
    //        source = pSource;
    //        columns = pChildCol;
    //        parentCol = pParentCol;
    //        updateColSource = pUpdateColSource;
    //        updateColDest = pUpdateColDest;

 
    //        tableSource.ColumnChanged += new DataColumnChangeEventHandler(table_ColumnChanged);
    //    }


    //    void distribute(DataRow pRow, string pCol)
    //    {
    //        for (int i = 0; i < columns.Length; ++i)
    //            if (pRow.IsNull(columns[i]))
    //                return;
    //        source.getBuilder().reset();
    //        for (int i = 0; i < columns.Length; ++i)
    //            source.getBuilder().addParameterValue(parentCol[i], pRow[columns[i]]);
    //        IDictionary dic = ToolRow.convertFirstToDictionary(source.getAll());
    //        if (dic != null)
    //            for (int i = 0; i < updateColSource.Length; ++i)
    //                ToolCell.set(pRow, updateColDest[i], dic[updateColSource[i]]);
    //        else
    //            pRow.CancelEdit();
    //    }

    //    protected   void table_ColumnChanged(object sender, DataColumnChangeEventArgs e)
    //    {
    //        if (e.Row.RowState != DataRowState.Detached)
    //        {
    //            string curColumn = e.Column.ColumnName;
    //            if (isMyCollumn(curColumn) && validator.check(e.Row))
    //            {
    //                if (block())
    //                {
    //                    try
    //                    {
    //                        distribute(e.Row, curColumn);
    //                    }
 
    //                    finally
    //                    {
    //                        unblock();
    //                    }
    //                }
    //            }
    //        }
    //    }

    //}

}

