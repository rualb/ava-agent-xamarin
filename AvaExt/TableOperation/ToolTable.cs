using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using AvaExt.Common;
using System.IO;
using AvaExt.Formating;
using AvaExt.SQL.Dynamic;

namespace AvaExt.TableOperation
{
    public class ToolTable
    {
        public static void addPrimaryKey(DataTable table, string[] keys)
        {
            if (keys.Length > 0)
            {
                DataColumn[] pkCol = new DataColumn[keys.Length];
                for (int i = 0; i < keys.Length; ++i)
                    pkCol[i] = table.Columns[keys[i]];
                table.PrimaryKey = pkCol;
            }

        }
        public static Double getSumOfColumnDouble(DataTable table, String colName)
        {
            Double sum = 0;
            DataColumn col = table.Columns[colName];
            for (int i = 0; i < table.Rows.Count; ++i)
                if (!table.Rows[i].IsNull(col))
                    sum += (Double)table.Rows[i][col];
            return sum;
        }



        public static DataView dataViewFromSource(object source)
        {
            DataView res = null;

            if (res == null)
            {
                DataTable t = source as DataTable;
                if (t != null)
                    res = t.DefaultView;
            }
            if (res == null)
            {
                DataView v = source as DataView;
                if (v != null)
                    res = v;
            }

            return res;
        }

        public static DataTable dataTableFromSource(object source)
        {
            if (source.GetType() == typeof(DataTable))
                return ((DataTable)source);
            else
                return ((DataView)source).Table;
        }

        public static void insertAtEnd(DataTable tableD, ICollection rows)
        {
            IEnumerator<DataRow> enumer = (IEnumerator<DataRow>)rows.GetEnumerator();
            enumer.Reset();
            while (enumer.MoveNext())
                if (!ToolTable.hasRow(tableD, enumer.Current))
                    insertRowAt(tableD, tableD.Rows.Count, enumer.Current);


        }
        internal static void insertRowAt(DataTable table, int i, DataRow row)
        {
            insertRowAt(table, i, row.ItemArray);
        }
        internal static void insertRowAt(DataTable table, int i, object[] arr)
        {
            i = Math.Max(i, 0);
            DataRow rowN = table.NewRow();
            ToolRow.copyArrToRow(arr, rowN);
            table.Rows.InsertAt(rowN, i);
        }
        public static void insertAtBegin(DataTable tableD, ICollection rows)
        {
            IEnumerator<DataRow> enumer = (IEnumerator<DataRow>)rows.GetEnumerator();
            enumer.Reset();
            while (enumer.MoveNext())
                if (!ToolTable.hasRow(tableD, enumer.Current))
                    insertRowAt(tableD, 0, enumer.Current);
        }

        private static bool hasRow(DataTable table, DataRow row)
        {
            DataRow rowS = null;
            if (table != null && row != null && table.PrimaryKey != null && table.PrimaryKey.Length > 0)
            {
                object[] arr = ToolRow.copyRowToArr(table.PrimaryKey, row);
                rowS = table.Rows.Find(arr);
            }
            return (rowS != null);
        }
        public static void removeLast(DataTable table, int count)
        {
            count = Math.Max(0, count);
            if (table != null)
            {
                int rest = Math.Max(0, table.Rows.Count - count);
                while (table.Rows.Count > rest)
                    table.Rows.RemoveAt(table.Rows.Count - 1);
            }
        }
        public static void removeFirst(DataTable table, int count)
        {
            count = Math.Max(0, count);
            if (table != null)
            {
                int rest = Math.Max(0, table.Rows.Count - count);
                while (table.Rows.Count > rest)
                    table.Rows.RemoveAt(0);

            }
        }

        internal static DataTable createReversed(DataTable table)
        {
            if (table != null)
            {
                DataTable nTab = table.Clone();
                for (int i = table.Rows.Count - 1; i >= 0; --i)
                    nTab.Rows.Add(table.Rows[i].ItemArray);
                return nTab;
            }
            return null;
        }



        public static void removeDublicate(DataTable tableData, DataTable tableReal)
        {
            if (tableData != null && tableReal != null && tableReal.PrimaryKey != null)
            {
                for (int i = 0; i < tableData.Rows.Count; ++i)
                    if (hasRow(tableReal, tableData.Rows[i]))
                    {
                        tableData.Rows.RemoveAt(i);
                        --i;
                    }
            }
        }

        public static DataTable getTable(object p)
        {
            if (p != null)
            {
                if (p.GetType() == typeof(DataTable))
                    return (DataTable)p;
                if (p.GetType() == typeof(DataView))
                    return ((DataView)p).Table;
            }
            return null;
        }

        public static void fillNull(DataTable table)
        {
            for (int c = 0; c < table.Columns.Count; ++c)
            {
                object defVal = ToolCell.getCellTypeDefaulValue(table.Columns[c].DataType);
                for (int r = 0; r < table.Rows.Count; ++r)
                    if (table.Rows[r].IsNull(c))
                        table.Rows[r][c] = defVal;
            }

        }

        public static void setPrimaryKey(DataTable table, string[] cols)
        {
            DataColumn[] pkCols = new DataColumn[cols.Length];
            for (int i = 0; i < cols.Length; ++i)
                pkCols[i] = table.Columns[cols[i]];
            table.PrimaryKey = pkCols;
        }


        public static DataTable create(DataRow row)
        {
            DataTable table = null;
            if (row != null)
            {
                table = row.Table.Clone();
                ToolRow.copyRowToTable(row, table);
            }

            return table;
        }

        public static bool isEmpty(DataTable tab)
        {
            return ((tab == null) || (tab.Rows.Count == 0));
        }

        public static DataTable create(string[] cols, string data, char sepChar)
        {
            DataTable table = new DataTable();
            foreach (string col in cols)
                table.Columns.Add(col, typeof(string));
            StringReader reader = new StringReader(data);
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                line = line.Trim();
                if (line != string.Empty)
                {
                    string[] arr = line.Split(sepChar);
                    table.Rows.Add(arr);

                }
            }
            return table;
        }

        public static DataTable[] explodeForGroups(DataTable table, string[] colsArr)
        {
            List<DataTable> list = new List<DataTable>();

            table.DefaultView.Sort = ToolSqlText.getSortString(colsArr, ToolArray.create<SqlTypeRelations>(colsArr.Length, SqlTypeRelations.sortAsc));

            DataColumn[] colsObj = new DataColumn[colsArr.Length];
            for (int i = 0; i < colsArr.Length; ++i)
                colsObj[i] = table.Columns[colsArr[i]];

            object[] curValues = ToolArray.create<object>(colsArr.Length, null);

            DataTable tabTmp = null;
            foreach (DataRowView rowV in table.DefaultView)
            {
                object[] tmpValues = compareData(rowV, colsObj, curValues);
                if (tmpValues != null)
                {
                    curValues = tmpValues;
                    list.Add(tabTmp = table.Clone());
                }

                tabTmp.Rows.Add(rowV.Row.ItemArray);
            }

            table.DefaultView.Sort = string.Empty;

            return list.ToArray();
        }
        public static object[][] getGroups(DataTable table, string[] colsArr)
        {
            List<object[]> list = new List<object[]>();
 
            DataColumn[] colsObj = new DataColumn[colsArr.Length];
            for (int i = 0; i < colsArr.Length; ++i)
                colsObj[i] = table.Columns[colsArr[i]];
 
            foreach (DataRow row in table.Rows)
            {
                object[] curValues = ToolRow.copyRowToArr(colsObj, row);

                foreach (object[] arr in list)
                {
                    if (ToolArray.isEqual(arr, curValues))
                    {
                        curValues = null;
                        break;
                    }
  
                }

                if (curValues != null)
                    list.Add(curValues);
            }



            list.Sort(new ToolArray.Comparer());

            return list.ToArray();

             
        }

      

        static object[] compareData(DataRow row, DataColumn[] cols, object[] values)
        {
            bool sameData = true;
            object[] curValues = ToolRow.copyRowToArr(cols, row);
            for (int i = 0; i < curValues.Length; ++i)
            {
                if (!ToolType.isEqual(curValues[i], values[i]))
                {
                    sameData = false;
                    break;
                }
            }
            return (sameData ? null : curValues);
        }
        static object[] compareData(DataRowView row, DataColumn[] cols, object[] values)
        {
            return compareData(row.Row, cols, values);
        }
    }
}
