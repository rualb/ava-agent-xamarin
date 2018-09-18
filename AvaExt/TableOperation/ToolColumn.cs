using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.Common;
using System.Collections;


namespace AvaExt.TableOperation
{
    public class ToolColumn
    {

        public static object getColumnLastValue(DataTable table, string col, object def)
        {
            for (int i = table.Rows.Count - 1; i >= 0; --i)
                if (table.Rows[i].RowState != DataRowState.Deleted)
                    return ToolCell.isNull(table.Rows[i][col], def);
            return def;
        }
        public static void setColumnValue(DataTable table, string col, object val)
        {
            for (int i = 0; i < table.Rows.Count; ++i)
                if (table.Rows[i].RowState != DataRowState.Deleted)
                    ToolCell.set(table.Rows[i], col, val);
        }
        public static void codeFromId(DataTable table, string source, string dest, string format)
        {
            DataColumn colS = table.Columns[source];
            DataColumn colD = table.Columns[dest];
            for (int i = 0; i < table.Rows.Count; ++i)
            {
                DataRow row = table.Rows[i];
                row[colD] = ((int)row[colS]).ToString(format);
            }
        }





        public static void shiftId(DataTable table, String colName, int offset)
        {
            DataColumn col = table.Columns[colName];
            for (int i = 0; i < table.Rows.Count; ++i)
            {
                DataRow row = table.Rows[i];
                if (!row.IsNull(col))
                    row[col] = (int)row[col] + offset;
            }

        }

        public static void shortIdToIntId(DataTable table, string source, string dest)
        {
            DataColumn colS = table.Columns[source];
            DataColumn colD = table.Columns[dest];
            for (int i = 0; i < table.Rows.Count; ++i)
            {
                DataRow row = table.Rows[i];
                row[colD] = (int)((short)row[colS]);
            }
        }

        public static object columnToString(object source, String col)
        {
            string newCol = DateTime.Now.Ticks.ToString();
            DataTable tab = ToolTable.dataTableFromSource(source);
            tab.Columns.Add(newCol, typeof(string));
            for (int i = 0; i < tab.Rows.Count; ++i)
            {
                DataRow row = tab.Rows[i];
                row[newCol] = row[col].ToString();
            }
            tab.Columns.Remove(col);
            tab.Columns.Add(col, typeof(string));
            for (int i = 0; i < tab.Rows.Count; ++i)
            {
                DataRow row = tab.Rows[i];
                row[col] = row[newCol];
            }
            tab.Columns.Remove(newCol);
            return source;
        }

        public static void columnStringToInt(DataTable table, string source, string dest)
        {
            DataColumn colS = table.Columns[source];
            DataColumn colD = table.Columns[dest];
            for (int i = 0; i < table.Rows.Count; ++i)
            {
                DataRow row = table.Rows[i];
                row[colD] = int.Parse(((string)row[colS]));
            }
        }

        public static void copyColumn(DataTable table, string source, string dest)
        {
            DataColumn colS = table.Columns[source];
            DataColumn colD = table.Columns[dest];
            for (int i = 0; i < table.Rows.Count; ++i)
            {
                DataRow row = table.Rows[i];
                row[colD] = row[colS];
            }
        }

        public static void multiplyColumns(DataRow row, string c1, string c2, string colRes)
        {
            if (!(row.IsNull(c1) || row.IsNull(c2)))
                row[colRes] = (Double)row[c1] * (Double)row[c2];
            else
                row[colRes] = DBNull.Value;

        }

        public static double getSum(IEnumerable rows, string col)
        {
            IEnumerator enumer = rows.GetEnumerator();
            enumer.Reset();
            Double sum = 0;
            while (enumer.MoveNext())
                if (!ToolRow.isDeleted((DataRow)enumer.Current))
                    sum += Convert.ToDouble(((DataRow)enumer.Current)[col]);
            return sum;
        }


        public static double getSumDouble(DataRow[] rows, string col)
        {
            double res = 0;
            if (rows != null)
                for (int i = 0; i < rows.Length; ++i)
                    if (!ToolRow.isDeleted(rows[i]))
                        res += (double)ToolCell.isNull(rows[i][col], 0.0);
            return res;
        }

        public static double getSumDouble(DataTable table, string col)
        {
            double res = 0;
            if (table != null)
                for (int i = 0; i < table.Rows.Count; ++i)
                    if (!ToolRow.isDeleted(table.Rows[i]))
                        res += (double)ToolCell.isNull(table.Rows[i][col], 0.0);
            return res;
        }

        public static string translater(IEnvironment env, string colInDB)
        {
            return env.translate("T_" + colInDB.Replace("_", string.Empty));
        }

        const string colSeparator = "_____";

        public static bool isColumnFullName(string col)
        {
            return (col.IndexOf(colSeparator) >= 0);
        }
        public static string getSpeColumnName(string col)
        {
            return "SPE_" + col;
        }
        public static string getColumnFullName(string table, string col)
        {
            return table + colSeparator + col;
        }
        public static string extractColumnName(string colName)
        {
            string[] arr = ToolString.split(colName, colSeparator  );
            return ((arr.Length > 0) ? arr[arr.Length - 1] : string.Empty);
        }
        public static string extractTableName(string colName)
        {

            string[] arr = ToolString.split(colName,colSeparator );
            return ((arr.Length > 1) ? arr[0] : string.Empty);
        }
        public static void trimColumn(DataTable table, string col)
        {
            if ((table != null) && (table.Columns[col].DataType == typeof(string)))
                for (int i = 0; i < table.Rows.Count; ++i)
                    if (!ToolRow.isDeleted(table.Rows[i]))
                        table.Rows[i][col] = ((string)ToolCell.isNull(table.Rows[i][col], string.Empty)).Trim();
        }
        public static void trimAllColumns(DataTable table)
        {

            if (table != null)
                for (int c = 0; c < table.Columns.Count; ++c)
                    if ((table.Columns[c].DataType == typeof(string)) && (!table.Columns[c].ReadOnly))
                        trimColumn(table, table.Columns[c].ColumnName);
        }

        public static void add(DataTable tab, string col, Type type)
        {
            if (!tab.Columns.Contains(col))
                tab.Columns.Add(col, type);
        }


        public static string[] selectFullNameCols(string[] cols)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < cols.Length; ++i)
                if (isColumnFullName(cols[i]))
                    list.Add(cols[i]);
            return list.ToArray();
        }

        public static Type[] getTypes(DataTable pTable, string[] pColumns)
        {
            Type[] arr = new Type[pColumns.Length];
            for (int i = 0; i < pColumns.Length; ++i)
                arr[i] = pTable.Columns[pColumns[i]].DataType;
            return arr;
        }

        public static void changeType(DataTable tab, string col, Type type)
        {
            string newCol = getTmpColName();
            tab.Columns.Add(newCol);
            copyColumn(tab, col, newCol);
            tab.Columns.Remove(col);
            tab.Columns.Add(col, type);
            for (int i = 0; i < tab.Rows.Count; ++i)
            {
                DataRow row = tab.Rows[i];
                row[col] = row[newCol];
            }
            tab.Columns.Remove(newCol);
        }

        public static string getTmpColName()
        {
            return "___COL" + ToolString.shrincDigit(DateTime.Now.Ticks.ToString());
        }
    }
}
