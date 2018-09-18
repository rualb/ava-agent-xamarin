using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.ECollections;
using AvaExt.TableOperation.CellMathActions;
using AvaExt.PagedSource;
using System.Collections.Specialized;
using System.Collections;
using AvaExt.Common;
using AvaExt.TableOperation.RowValidator;
using AvaExt.TableOperation.CellAutomation.TableEvents;

namespace AvaExt.TableOperation.CellAutomation
{
    public class CellAutomationSpaceDB : ITableColumnChange
    {
        StringDictionary dicBind = new StringDictionary();
        StringDictionary dicData = new StringDictionary(); 
        IPagedSource source;
        string[] columns;
        string[] bindChildCol;
        string[] bindParentCol;
        string[] childCol;
        string[] parentCol;
        string[] allChildCol;
        string[] allParentCol;
        string[] updateChildCol;
        string[] triggerCols;
        IFlagStore flags = new FlagStore();
        DataTable tableSource;
        DataTable tableDest;
        BlockPoint bp = new BlockPoint();
        IRowValidator validator;
        //UpdateTypeFlags flags = 0;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pTable"></param>
        /// <param name="pSource"></param>
        /// <param name="pChildCol">ID Columns</param>
        /// <param name="pParentCol">ID columns</param>
        /// <param name="pAppendCol">Like name,descripton of ID</param>
        /// <param name="pValidator"></param>
        public string[] getTriggerColumns()
        {
            return columns;
        }
        public CellAutomationSpaceDB(DataTable pTableSource, DataTable pTableDest, IPagedSource pSource, string[] pBindChildCol, string[] pBindParentCol, string[] pChildCol, string[] pParentCol, UpdateTypeFlags pFlags, IRowValidator pValidator)
        {
            init(pTableSource, pTableDest, pSource, pBindChildCol, pBindParentCol, pChildCol, pParentCol, pFlags, pValidator, new string[0]);
        }

        void init(DataTable pTableSource, DataTable pTableDest, IPagedSource pSource, string[] pBindChildCol, string[] pBindParentCol, string[] pChildCol, string[] pParentCol, UpdateTypeFlags pFlags, IRowValidator pValidator, string[] pTriggerCols)
        {
            validator = (pValidator == null) ? new RowValidatorTrue() : pValidator;
            tableSource = pTableSource;
            tableDest = pTableDest;
            flags.flagEnable(pFlags);
            source = pSource;
 
            triggerCols = pTriggerCols;
            bindChildCol = pBindChildCol;
            bindParentCol = pBindParentCol;
            childCol = pChildCol;
            parentCol = pParentCol;
            allChildCol = childCol;
            allParentCol = parentCol;
 
            //
            for (int i = 0; i < bindChildCol.Length; ++i)
                addColForMapBind(bindChildCol[i], bindParentCol[i]);
            for (int i = 0; i < childCol.Length; ++i)
                addColForMapData(childCol[i], parentCol[i]);

            //

            if (flags.isFlagEnabled(UpdateTypeFlags.activeOnRelColumn))
                columns = bindChildCol;
            else
            {
                int len = allChildCol.Length;
                if (flags.isFlagEnabled(UpdateTypeFlags.activateIgnorLast1DrivedChilCols))
                    len -= 1;
                else
                    if (flags.isFlagEnabled(UpdateTypeFlags.activateIgnorLast1DrivedChilCols))
                        len -= 2;
                    else
                        if (flags.isFlagEnabled(UpdateTypeFlags.activateIgnorLast1DrivedChilCols))
                            len -= 3;
                columns = new string[len];
                Array.Copy(allChildCol, columns, len);
            }
            columns = ToolArray.merge<string>(columns, triggerCols);
            //
           // if (flags.isFlagEnabled(UpdateTypeFlags.updateIgnoreRelColumn))
                updateChildCol = childCol;
           // else
           //     updateChildCol = allChildCol;
            //!!!!!!!!!!!!!!!!!!!!!!!!!!!

             

 

        }

        void emptyDest()
        {
            for (int i = 0; i < tableDest.Rows.Count; ++i)
            {
                tableDest.Rows[i].Delete();
                --i;
            }

        }
        bool needUpdate(DataRow row, string[] cols)
        {
            if (row == null)
                return false;



            bool flagResetOnAllNullOrDef = flags.isFlagEnabled(UpdateTypeFlags.resetIfAllCurrentRelColsAreDefaultOrNull);
            bool flagStopIfOneNullOrDef = !flags.isFlagEnabled(UpdateTypeFlags.continueIfOneOfCurrentRelColsDefaultOrNull);


            if (ToolCell.isDefaultOrNullAll(row, cols))
            {
                if (flagResetOnAllNullOrDef)
                {
                    emptyDest();
                    return false;
                }
                if (flagStopIfOneNullOrDef)
                    return false;
            }
            if (ToolCell.hasDefaultOrNull(row, cols))
            {
                if (flagStopIfOneNullOrDef)
                    return false;
            }
            return true;
        }


        void addColForMapBind(string c1, string c2)
        {
            dicBind.Add(c1, c2);
        }
        string getColMapBind(string c1)
        {
            return dicBind[c1];
        }
        void addColForMapData(string c1, string c2)
        {
            dicData.Add(c1, c2);
        }
        string getColMapData(string c1)
        {
            return dicData[c1];
        }

        public void refresh()
        {
            if (!flags.isFlagEnabled(UpdateTypeFlags.stopRefresh))
                if (bp.block())
                {
                    try
                    {
                        for (int i = 0; i < tableSource.Rows.Count; ++i)
                            if (validator.check(tableSource.Rows[i]))
                                distribute(tableSource.Rows[i], bindChildCol);
                    }
                    finally
                    {
                        bp.unblock();
                    }
                }

        }
        public void refresh(DataRow row)
        {
            if (!flags.isFlagEnabled(UpdateTypeFlags.stopRefresh))
                refresh(row, bindChildCol);
        }

        void refresh(DataRow row, string[] cols)
        {
            if (bp.block())
            {
                try
                {
                    if (validator.check(row))
                        distribute(row, cols);
                }
                finally
                {
                    bp.unblock();
                }
            }
        }

        void distribute(DataRow row, string[] cols)
        {
            if (needUpdate(row, cols))
            {
 
                //getData
                source.getBuilder().reset();
                for (int i = 0; i < cols.Length; ++i)
                {
                    string col = ToolColumn.extractColumnName(getColMapBind(cols[i]));
                    string tab = ToolColumn.extractTableName(getColMapBind(cols[i]));
                    object val = row[cols[i]];
                    if ((tab != string.Empty) && (col != string.Empty))
                        source.getBuilder().addParameterValueTable(tab, col, val);
                    else
                        if (col != string.Empty)
                            source.getBuilder().addParameterValue(col, val);
                        else
                            if (col == string.Empty)
                                source.getBuilder().addFreeParameterValue(val);
                }
                emptyDest();
                DataTable tabData = source.getAll() ;
                //
                if (tabData != null)
                { //Has data

                    foreach (DataRow rowData in tabData.Rows)
                    {
                        DataRow nRow = tableDest.NewRow();
                        ToolRow.initTableNewRow(nRow);
                        tableDest.Rows.Add(nRow);
                        for (int i = 0; i < updateChildCol.Length; ++i)
                        {
                            ToolCell.set(nRow, getColMapData(updateChildCol[i]), rowData[updateChildCol[i]]);
                        }
                    }
                }
 
            }
        }


        public void columnChange(DataColumnChangeEventArgs e)
        {
            string curColumn = e.Column.ColumnName;

            List<string> list = new List<string>();
            bool isTrigCol = (Array.IndexOf<string>(triggerCols, curColumn) >= 0);

            if (isTrigCol || flags.isFlagEnabled(UpdateTypeFlags.alwaysIncludeRelColumn))
                list.AddRange(bindChildCol);

            if (!isTrigCol)
                if (list.IndexOf(curColumn) < 0)
                    list.Add(curColumn);

            distribute(e.Row, list.ToArray());
        }



        public virtual void initForColumnChanged(DataRow pRow)
        {

        }


    }
    //[Flags]
    //public enum UpdateTypeFlags : int
    //{
    //    activeOnRelColumn = 1,
    //    updateIgnoreRelColumn = 2,
    //    alwaysIncludeRelColumn = 4,
    //    disableEditCancel = 8,
    //    setTypeDefaultToDrivedChild = 16,
    //    setTypeDefaultToRelChild = 32,
    //    continueIfOneOfCurrentRelColsDefaultOrNull = 64,
    //    resetIfAllCurrentRelColsAreDefaultOrNull = 128,
    //    activateIgnorLast1DrivedChilCols = 256,
    //    activateIgnorLast2DrivedChilCols = 512,
    //    activateIgnorLast3DrivedChilCols = 1024,
    //    __spe__updateIfDrived = 2048

    //}
}

