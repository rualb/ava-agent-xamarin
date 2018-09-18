using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using AvaExt.Logining;
using AvaExt.Common;
using AvaExt.TableOperation.RowValidator;

namespace AvaExt.TableOperation
{
    public class ToolRow
    {
        public static DataRow copyRowToTable(DataRow row, DataTable dest, bool removeInSource)
        {
            if (row != null && dest != null)
                if ((dest.PrimaryKey.Length == 0) || ((dest.PrimaryKey.Length > 0) && (!dest.Rows.Contains(copyRowToArr(dest.PrimaryKey, row)))))
                {
                    DataRow newRow = dest.Rows.Add(copyRowToArr(row));
                    if (removeInSource)
                        row.Table.Rows.Remove(row);
                    return newRow;
                }
            return null;
        }

        public static DataRow copyRowToTable(DataRow row, DataTable dest)
        {
            return copyRowToTable(row, dest, false);
        }
        public static void copyRowData(DataRow rowS, DataRow rowD)
        {
            for (int i = 0; i < rowS.Table.Columns.Count; ++i)
                rowD[i] = rowS[i];
        }
        public static bool isDeleted(DataRow row)
        {
            return ((row != null) && (row.RowState == DataRowState.Deleted));
        }

        public static object[] getVallueArrayForTable(DataTable table)
        {
            object[] arr = new object[table.Columns.Count];
            for (int i = 0; i < table.Columns.Count; ++i)
                arr[i] = ToolCell.getCellTypeDefaulValue(table.Columns[i].DataType);
            return arr;
        }
        public static DataRow initTableNewRowIfNull(DataRow row)
        {
            DataTable table = row.Table;
            for (int i = 0; i < table.Columns.Count; ++i)
                if (ToolCell.isNull(row[table.Columns[i]]))
                    ToolCell.set(row, i, ToolCell.getCellTypeDefaulValue(table.Columns[i].DataType));
            return row;
        }
        public static DataRow initTableNewRowIfNull(DataRow row, string[] cols)
        {
            DataTable table = row.Table;
            for (int i = 0; i < cols.Length; ++i)
                if (ToolCell.isNull(row[table.Columns[i]]))
                    ToolCell.set(row, cols[i], ToolCell.getCellTypeDefaulValue(table.Columns[cols[i]].DataType));
            return row;
        }
        public static DataRow initTableNewRow(DataRow row)
        {
            if (row != null)
            {
                DataTable table = row.Table;
                for (int i = 0; i < table.Columns.Count; ++i)
                    ToolCell.set(row, i, ToolCell.getCellTypeDefaulValue(table.Columns[i].DataType));
            }
            return row;
        }
        public static DataRow initTableNewRow(DataRow row, string[] cols)
        {
            DataTable table = row.Table;
            for (int i = 0; i < cols.Length; ++i)
                ToolCell.set(row, cols[i], ToolCell.getCellTypeDefaulValue(table.Columns[cols[i]].DataType));
            return row;
        }



        public static DataRow getPrevRow(DataRow row)
        {
            int i = row.Table.Rows.IndexOf(row);
            return (i > 0) ? row.Table.Rows[i - 1] : null;
        }
        public static DataRow createRowCopy(DataRow row)
        {
            if (row != null)
            {
                DataRow rowD = row.Table.NewRow();
                copyRowToRow(row, rowD);
                return rowD;
            }
            return null;
        }
        public static object[] copyRowToArr(DataRow row)
        {
            object[] arr = null;
            if (row != null)
            {
                arr = new object[row.Table.Columns.Count];
                for (int i = 0; i < arr.Length; ++i)
                    arr[i] = row[i];
            }
            return arr;
        }
        public static object[] copyRowToArr(DataColumn[] col, DataRow row)
        {
            object[] arr = new object[col.Length];
            for (int i = 0; i < col.Length; ++i)
                arr[i] = row[col[i]];
            return arr;
        }
        public static object[] copyRowToArr(string[] col, DataRow row)
        {
            object[] arr = new object[col.Length];
            for (int i = 0; i < col.Length; ++i)
                arr[i] = row[col[i]];
            return arr;
        }
        public static void copyArrToRow(object[] arr, DataRow row)
        {
            if ((arr != null) && (row != null))
                for (int i = 0; i < arr.Length; ++i)
                    ToolCell.set(row, i, arr[i]);
        }
        public static void copyRowToRow(DataRow rowS, DataRow rowD)
        {
            if ((rowS != null) && (rowD != null))
                for (int i = 0; i < rowS.Table.Columns.Count; ++i)
                    ToolCell.set(rowD, i, rowS[i]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row1"></param>
        /// <param name="row2"></param>
        //public static void swapRows(DataRow row1, DataRow row2)
        //{
        //    if ((row1 != null) && (row2 != null) && Object.ReferenceEquals(row1.Table, row2.Table) && (!Object.ReferenceEquals(row1, row2)))
        //    {
        //        DataTable table = row1.Table;
        //        int i1 = row1.Table.Rows.IndexOf(row1);
        //        int i2 = row2.Table.Rows.IndexOf(row2);
        //        if ((i1 >= 0) && (i2 >= 0))
        //        {
        //            if (i2 < i1)
        //            {
        //                int iTmp; iTmp = i1; i1 = i2; i2 = iTmp;
        //                DataRow rTmp; rTmp = row1; row1 = row2; row2 = rTmp;
        //            }

        //            table.Rows.RemoveAt(i1);
        //            table.Rows.RemoveAt(Math.Max(0,i2 - 1));
        //            table.Rows.InsertAt(row2,Math.Max(0, i1 - 2));
        //            table.Rows.InsertAt(row1,Math.Max(0, i2 - 1));
        //        }
        //    }

        //}

        public static DataRow getLastRealRow(DataTable table)
        {
            for (int i = table.Rows.Count - 1; i >= 0; --i)
                if (table.Rows[i].RowState != DataRowState.Deleted)
                    return table.Rows[i];
            return null;
        }
        public static DataRow getLastRealRow(DataTable table, IRowValidator validator)
        {
            for (int i = table.Rows.Count - 1; i >= 0; --i)
                if ((table.Rows[i].RowState != DataRowState.Deleted) && validator.check(table.Rows[i]))
                    return table.Rows[i];
            return null;
        }
        public static int getSectionEnd(DataTable table, IRowValidator validator)
        {
            for (int indx = table.Rows.Count - 1; indx >= 0; --indx)
                if ((table.Rows[indx].RowState != DataRowState.Deleted))
                    if (validator.check(table.Rows[indx]))
                        return indx;
            return -1;
        }
        public static DataRow getFirstRealRow(DataTable table)
        {
            for (int i = 0; i < table.Rows.Count; ++i)
                if (table.Rows[i].RowState != DataRowState.Deleted)
                    return table.Rows[i];
            return null;
        }
        public static DataRow getFirstRealRow(DataTable table, IRowValidator validator)
        {
            for (int i = 0; i < table.Rows.Count; ++i)
                if ((table.Rows[i].RowState != DataRowState.Deleted) && validator.check(table.Rows[i]))
                    return table.Rows[i];
            return null;
        }
        public static int getSectionStart(DataTable table, IRowValidator validator)
        {
            for (int indx = 0; indx < table.Rows.Count; ++indx)
                if ((table.Rows[indx].RowState != DataRowState.Deleted))
                    if (validator.check(table.Rows[indx]))
                        return indx;
            return -1;
        }
        public static IDictionary convertRowToDictionary(DataRow row)
        {
            IDictionary dictionary = null;
            if (row != null)
            {
                dictionary = new PropertyCollection();
                for (int c = 0; c < row.Table.Columns.Count; ++c)
                    dictionary[row.Table.Columns[c].ColumnName] = row[c];
            }
            return dictionary;
        }
        public static IDictionary convertFirstToDictionary(DataTable table)
        {
            if (table.Rows.Count > 0)
                return convertRowToDictionary(table.Rows[0]);
            return null;
        }
        public static IDictionary convertLastToDictionary(DataTable table)
        {
            if (table.Rows.Count > 0)
                return convertRowToDictionary(table.Rows[table.Rows.Count - 1]);
            return null;
        }


        public static DataRow search(DataTable pTable, string pCol, object pVal)
        {
            int colIndx = pTable.Columns.IndexOf(pCol);
            foreach (DataRow row in pTable.Rows)
                if (row.RowState != DataRowState.Deleted)
                    if (ToolType.isEqual(row[colIndx], pVal))
                        return row;
            return null;
        }
        public static DataRow search(DataTable pTable, string pCol, object pVal, string pCol2, object pVal2)
        {
            List<DataRow> list_ = new List<DataRow>();
            int colIndx = pTable.Columns.IndexOf(pCol);
            int colIndx2 = pTable.Columns.IndexOf(pCol2);
            foreach (DataRow row in pTable.Rows)
                if (row.RowState != DataRowState.Deleted)
                    if (ToolType.isEqual(row[colIndx], pVal) && ToolType.isEqual(row[colIndx2], pVal2))
                        return row;
            return null;
        }
        public static DataRow[] searchAll(DataTable pTable, string pCol, object pVal)
        {
            List<DataRow> list_ = new List<DataRow>();
            int colIndx = pTable.Columns.IndexOf(pCol);
            foreach (DataRow row in pTable.Rows)
                if (row.RowState != DataRowState.Deleted)
                    if (ToolType.isEqual(row[colIndx], pVal))
                        list_.Add(row);
            return list_.ToArray();
        }

    }
}
