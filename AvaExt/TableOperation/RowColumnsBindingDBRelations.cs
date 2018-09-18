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

namespace AvaExt.TableOperation
{
    public class RowColumnsBindingDBRelations : RowColumnsBindingBase
    {
        StringDictionary dic = new StringDictionary();
        IPagedSource source;
        string[] bindChildCol;
        string[] bindParentCol;
        string[] childCol;
        string[] parentCol;
        string[] allChildCol;
        string[] allParentCol;
        string[] updateChildCol;
        string[] triggerCols;
        IFlagStore flags = new FlagStore();

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

        public RowColumnsBindingDBRelations(DataTable pTable, IPagedSource pSource, string[] pBindChildCol, string[] pBindParentCol, string[] pChildCol, string[] pParentCol, UpdateTypeFlags pFlags, IRowValidator pValidator)
            : base(pTable, null, pValidator)
        {
            init(pTable, pSource, pBindChildCol, pBindParentCol, pChildCol, pParentCol, pFlags, pValidator, new string[0]);

        }
        public RowColumnsBindingDBRelations(DataTable pTable, IPagedSource pSource, string[] pBindChildCol, string[] pBindParentCol, string[] pChildCol, string[] pParentCol, string[] pTriggerCols, UpdateTypeFlags pFlags, IRowValidator pValidator)
            : base(pTable, null, pValidator)
        {
            init(pTable, pSource, pBindChildCol, pBindParentCol, pChildCol, pParentCol, pFlags, pValidator, pTriggerCols);

        }
        void init(DataTable pTable, IPagedSource pSource, string[] pBindChildCol, string[] pBindParentCol, string[] pChildCol, string[] pParentCol, UpdateTypeFlags pFlags, IRowValidator pValidator, string[] pTriggerCols)
        {
            flags.flagEnable(pFlags);
            source = pSource;
            for (int i = pChildCol.Length; i < pParentCol.Length; ++i)
            {
                pChildCol = ToolArray.resize<string>(pChildCol, pChildCol.Length + 1);
   
                pChildCol[pChildCol.Length - 1] = string.Empty;
            }
            for (int i = 0; i < pParentCol.Length; ++i)
            {
                if ((pChildCol[i] == string.Empty) || (pChildCol[i] == null))
                    pChildCol[i] = ToolColumn.getColumnFullName(source.getBuilder().getName(), pParentCol[i]);
            }
            triggerCols = pTriggerCols;
            bindChildCol = pBindChildCol;
            bindParentCol = pBindParentCol;
            childCol = pChildCol;
            parentCol = pParentCol;
            allChildCol = new string[bindChildCol.Length + childCol.Length];
            bindChildCol.CopyTo(allChildCol, 0);
            childCol.CopyTo(allChildCol, bindChildCol.Length);
            allParentCol = new string[bindParentCol.Length + parentCol.Length];
            bindParentCol.CopyTo(allParentCol, 0);
            parentCol.CopyTo(allParentCol, bindParentCol.Length);
            //

            for (int i = 0; i < bindChildCol.Length; ++i)
                addColForMap(bindChildCol[i], bindParentCol[i]);
            for (int i = 0; i < childCol.Length; ++i)
                addColForMap(childCol[i], parentCol[i]);

            //

            if (flags.isFlagEnabled(UpdateTypeFlags.activeOnRelColumn))
                columns = bindChildCol;
            else
            {
                int len = allChildCol.Length;
                if (flags.isFlagEnabled(UpdateTypeFlags.activateIgnorLast1DrivedChilCols))
                    len -= 1;
                else
                    if (flags.isFlagEnabled(UpdateTypeFlags.activateIgnorLast2DrivedChilCols))
                        len -= 2;
                    else
                        if (flags.isFlagEnabled(UpdateTypeFlags.activateIgnorLast3DrivedChilCols))
                            len -= 3;
                columns = new string[len];
                Array.Copy(allChildCol, columns, len);
            }
            columns = ToolArray.merge<string>(columns, triggerCols);
            //
            if (flags.isFlagEnabled(UpdateTypeFlags.updateIgnoreRelColumn))
                updateChildCol = childCol;
            else
                updateChildCol = allChildCol;
            //!!!!!!!!!!!!!!!!!!!!!!!!!!!

            //

            for (int i = 0; i < allChildCol.Length; ++i)
                if (!tableSource.Columns.Contains(allChildCol[i]))
                    tableSource.Columns.Add(allChildCol[i], ToolTypeSet.helper.tObject);//check// source.getBuilder().getColumnType(allParentCol[i]));

            //

            try
            {
                flags.flagEnable(UpdateTypeFlags.__spe__updateIfDrived);
                for (int i = 0; i < tableSource.Rows.Count; ++i)
                    refresh(tableSource.Rows[i]);
            }
            finally
            {
                flags.flagDisable(UpdateTypeFlags.__spe__updateIfDrived);
            }
            //
            tableSource.ColumnChanged += new DataColumnChangeEventHandler(table_ColumnChanged);
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
                    ToolRow.initTableNewRow(row, allChildCol);
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


        void addColForMap(string c1, string c2)
        {
            dic.Add(c1, c2);
        }
        string getColMap(string c1)
        {
            return dic[c1];
        }

        public void refresh()
        {
            if (block())
            {
                try
                {
                    for (int i = 0; i < tableSource.Rows.Count; ++i)
                        distribute(tableSource.Rows[i], bindChildCol);
                }
                finally
                {
                    unblock();
                }
            }

        }
        public void refresh(DataRow row)
        {
            refresh(row, bindChildCol);
        }

        void refresh(DataRow row, string[] cols)
        {
            if (block())
            {
                try
                {
                    distribute(row, cols);
                }
                finally
                {
                    unblock();
                }
            }
        }

        void distribute(DataRow row, string[] cols)
        {
            if (validator.check(row))
            {
                if (needUpdate(row, cols))
                {
                    bool drivedUpdateMode = flags.isFlagEnabled(UpdateTypeFlags.__spe__updateIfDrived);
                    if (drivedUpdateMode)
                    {
                        bool hasFull = false;
                        for (int i = 0; i < allChildCol.Length; ++i)
                            if (ToolColumn.isColumnFullName(allChildCol[i]))
                                hasFull = true;
                        if (!hasFull)
                            return;
                    }
                    //getData
                    source.getBuilder().reset();
                    for (int i = 0; i < cols.Length; ++i)
                    {
                        string col = ToolColumn.extractColumnName(getColMap(cols[i]));
                        string tab = ToolColumn.extractTableName(getColMap(cols[i]));
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
                    IDictionary dicData = ToolRow.convertFirstToDictionary(source.getAll());
                    //
                    if (dicData != null)
                    { //Has data
                        string[] tmpChildCol = (drivedUpdateMode ? ToolColumn.selectFullNameCols(updateChildCol) : updateChildCol);
                        for (int i = 0; i < tmpChildCol.Length; ++i)
                        {
                            ToolCell.set(row, tmpChildCol[i], dicData[getColMap(tmpChildCol[i])]);
                        }
                    }
                    else
                    { //No data
                        if (!flags.isFlagEnabled(UpdateTypeFlags.disableEditCancel))
                            row.CancelEdit();
                        else
                        {
                            if (flags.isFlagEnabled(UpdateTypeFlags.setTypeDefaultToDrivedChild))
                                ToolRow.initTableNewRow(row, (drivedUpdateMode ? ToolColumn.selectFullNameCols(childCol) : childCol));
                            if (flags.isFlagEnabled(UpdateTypeFlags.setTypeDefaultToRelChild))
                                ToolRow.initTableNewRow(row, (drivedUpdateMode ? ToolColumn.selectFullNameCols(bindChildCol) : bindChildCol));
                        }
                    }
                }
            }
        }


        protected void table_ColumnChanged(object sender, DataColumnChangeEventArgs e)
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
                            List<string> list = new List<string>();
                            bool isTrigCol = (Array.IndexOf<string>(triggerCols, curColumn) >= 0);

                            if (isTrigCol || flags.isFlagEnabled(UpdateTypeFlags.alwaysIncludeRelColumn))
                                list.AddRange(bindChildCol);

                            if (!isTrigCol)
                                if (list.IndexOf(curColumn) < 0)
                                    list.Add(curColumn);

                            distribute(e.Row, list.ToArray());
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
    [Flags]
    public enum UpdateTypeFlags : int
    {
        activeOnRelColumn = 1,
        updateIgnoreRelColumn = 2,
        alwaysIncludeRelColumn = 4,
        disableEditCancel = 8,
        setTypeDefaultToDrivedChild = 16,
        setTypeDefaultToRelChild = 32,
        continueIfOneOfCurrentRelColsDefaultOrNull = 64,
        resetIfAllCurrentRelColsAreDefaultOrNull = 128,
        activateIgnorLast1DrivedChilCols = 256,
        activateIgnorLast2DrivedChilCols = 512,
        activateIgnorLast3DrivedChilCols = 1024,
        __spe__updateIfDrived = 2048,
        stopRefresh = 4096

    }
}

