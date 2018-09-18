using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.Common;

namespace AvaExt.TableOperation
{
    public class ToolCell
    {
        public static void set(DataRow row, DataColumn pCol, object pVal)
        {

            if ((row != null) && (pVal != null) && (pCol != null) && (row.RowState != DataRowState.Deleted))
                {
                    object curVal = row[pCol];
                    if (!ToolType.isEqual(curVal, pVal))
                    {
                        row[pCol] = pVal;
                    }
                }

        }
        public static void set(DataRow row, string pCol, object pVal)
        {

            if ((row != null) && (pVal != null) && (pCol != null))
                set(row, row.Table.Columns[pCol], pVal);

        }
        public static void set(DataRow row, string[] pCol, object[] pVal)
        {
            if ((pCol != null) && (pVal != null))
                for (int i = 0; i < pCol.Length; ++i)
                    set(row, pCol[i], pVal[i]);
        }

        public static object getCellTypeDefaulValue(Type type)
        {
            return (type == typeof(DateTime)) ? DBNull.Value : ToolType.getTypeDefaulValue(type);
        }
        public static bool isCellTypeDefaulValue(Type type, object val)
        {
            return ToolType.isEqual(val, getCellTypeDefaulValue(type));
        }
        public static void set(DataRow row, int pCol, object pVal)
        {
            if (row != null)
                set(row, row.Table.Columns[pCol].ColumnName, pVal);
        }
        public static object isNull(object val, object defVal)
        {
            return isNull(val) ? defVal : val;
        }
        public static bool isNull(object val)
        {
            return ((val == null) || (val.GetType() == typeof(DBNull)));
        }
        public static object isNull(DataRow row, string col, object defValue)
        {
            if (row != null && col != null)
                return isNull(row[col], defValue);
            return defValue;
        }
        public static bool isNullAll(DataRow row, string[] cols)
        {
            for (int i = 0; i < cols.Length; ++i)
                if (!isNull(row[cols[i]]))
                    return false;
            return true;
        }
        public static bool hasNull(DataRow row, string[] cols)
        {
            for (int i = 0; i < cols.Length; ++i)
                if (isNull(row[cols[i]]))
                    return true;
            return false;
        }
        public static bool hasDefault(DataRow row, string[] cols)
        {
            for (int i = 0; i < cols.Length; ++i)
                if (isCellTypeDefaulValue(row.Table.Columns[cols[i]].DataType, row[cols[i]]))
                    return true;
            return false;
        }
        public static bool isDefaultAll(DataRow row, string[] cols)
        {
            for (int i = 0; i < cols.Length; ++i)
                if (!isCellTypeDefaulValue(row.Table.Columns[cols[i]].DataType, row[cols[i]]))
                    return false;
            return true;
        }
        public static bool isDefaultOrNullAll(DataRow row, string[] cols)
        {
            for (int i = 0; i < cols.Length; ++i)
                if (!(isNull(row[cols[i]]) || isCellTypeDefaulValue(row.Table.Columns[cols[i]].DataType, row[cols[i]])))
                    return false;
            return true;
        }
        public static bool hasDefaultOrNull(DataRow row, string[] cols)
        {
            for (int i = 0; i < cols.Length; ++i)
                if ((isNull(row[cols[i]]) || isCellTypeDefaulValue(row.Table.Columns[cols[i]].DataType, row[cols[i]])))
                    return true;
            return false;
        }
    }
}
