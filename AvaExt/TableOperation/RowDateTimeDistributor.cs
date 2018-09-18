using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.ECollections;
using AvaExt.TableOperation.CellMathActions;
using AvaExt.Database.Tools;
using AvaExt.TableOperation.RowValidator;
using AvaExt.Common;

namespace AvaExt.TableOperation
{
    public class RowDateTimeDistributor : RowColumnsBindingBase
    {
        DateParts[] parts;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <param name="pCols">first allways FULL DATETIME !</param>
        /// <param name="pParts"></param>
        /// <param name="pValidator"></param>
        public RowDateTimeDistributor(DataTable table, string[] pCols, DateParts[] pParts, IRowValidator pValidator)
            : base(table, pCols, pValidator)
        {
            parts = pParts;
            bool modeCompile = !tableSource.Columns.Contains(columns[0]);
            for (int i = 0; i < columns.Length; ++i)
            {
                if (!tableSource.Columns.Contains(columns[i]))
                    tableSource.Columns.Add(columns[i], partTypeToType(parts[i]));
            }
            for (int i = 0; i < tableSource.Rows.Count; ++i)
            {
                DataRow row = tableSource.Rows[i];
                string col = string.Empty;
                if (modeCompile)
                    col = columns[1];
                else
                    col = columns[0];
                table_ColumnChanged(tableSource, new DataColumnChangeEventArgs(row,tableSource.Columns[ col], row[col]));
            }
            tableSource.ColumnChanged += new DataColumnChangeEventHandler(table_ColumnChanged);
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

                            if (curColumn == columns[0])
                            {
                                distribute(e.Row);
                            }
                            else
                            {
                                compile(e.Row);
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

        private void compile(DataRow dataRow)
        {
            DateTime datetime = (DateTime)ToolCell.isNull(dataRow[columns[0]], DateTime.Now);
            for (int i = 1; i < columns.Length; ++i)
                if(!ToolCell.isNull(dataRow[columns[i]]))
                datetime = setDateTimePatr(datetime, parts[i], dataRow[columns[i]] );
            ToolCell.set(dataRow, columns[0], datetime);
        }

        private void distribute(DataRow dataRow)
        {
            DateTime datetime = (DateTime)ToolCell.isNull(dataRow[columns[0]], DateTime.Now);
            for (int i = 1; i < columns.Length; ++i)
            {
                object part = dateTimePart(datetime, parts[i]);
                ToolCell.set(dataRow, columns[i], part);
            }
        }

        private DateTime setDateTimePatr(DateTime dt, DateParts dateParts, object val)
        {
            DateTime tmpDT;
            int tmpInt;
            switch ((int)dateParts)
            {
                //case (int)DateParts.dateTime:
                //    return dt;
                case (int)DateParts.date:
                    tmpDT = (DateTime)val;
                    return new DateTime(tmpDT.Year, tmpDT.Month, tmpDT.Day, dt.Hour, dt.Minute, dt.Second);
                case (int)DateParts.dateInt:
                    tmpDT = ToolGeneral.intDate2Date((int)val);
                    return new DateTime(tmpDT.Year, tmpDT.Month, tmpDT.Day, dt.Hour, dt.Minute, dt.Second);
                case (int)DateParts.timeInt:
                    tmpDT = ToolGeneral.intTime2Time((int)val);
                    return new DateTime(dt.Year, dt.Month, dt.Day, tmpDT.Hour, tmpDT.Minute, tmpDT.Second);
                case (int)DateParts.year:
                    tmpInt = ToolType.intoRange(Convert.ToInt32(val), 1, 9999);
                    return new DateTime(tmpInt, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
                case (int)DateParts.month:
                    tmpInt = ToolType.intoRange(Convert.ToInt32(val), 1, 12);
                    return new DateTime(dt.Year, tmpInt, dt.Day, dt.Hour, dt.Minute, dt.Second);
                case (int)DateParts.day:
                    tmpInt = ToolType.intoRange(Convert.ToInt32(val), 1, 31);
                    return new DateTime(dt.Year, dt.Month, tmpInt, dt.Hour, dt.Minute, dt.Second);
                case (int)DateParts.hour:
                    tmpInt = ToolType.intoRange(Convert.ToInt32(val), 0, 23);
                    return new DateTime(dt.Year, dt.Month, dt.Day, tmpInt, dt.Minute, dt.Second);
                case (int)DateParts.minute:
                    tmpInt = ToolType.intoRange(Convert.ToInt32(val), 0, 59);
                    return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, tmpInt, dt.Second);
                case (int)DateParts.second:
                    tmpInt = ToolType.intoRange(Convert.ToInt32(val), 0, 59);
                    return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, tmpInt);
            }
            return dt;
        }
        private Type partTypeToType(DateParts dateParts)
        {
            switch ((int)dateParts)
            {
                case (int)DateParts.dateTime:
                    return typeof(DateTime);
                case (int)DateParts.date:
                    return typeof(DateTime);
                case (int)DateParts.dateInt:
                    return typeof(int);
                case (int)DateParts.timeInt:
                    return typeof(int);
                case (int)DateParts.year:
                case (int)DateParts.month:
                case (int)DateParts.day:
                case (int)DateParts.hour:
                case (int)DateParts.minute:
                case (int)DateParts.second:
                    return typeof(short);


            }

            return typeof(object);
        }
        private object dateTimePart(DateTime dt, DateParts dateParts)
        {
            switch ((int)dateParts)
            {
                case (int)DateParts.dateTime:
                    return dt;
                case (int)DateParts.date:
                    return dt.Date;
                case (int)DateParts.dateInt:
                    return ToolGeneral.date2IntDate(dt);
                case (int)DateParts.timeInt:
                    return ToolGeneral.time2IntTime(dt);
                case (int)DateParts.year:
                    return dt.Year;
                case (int)DateParts.month:
                    return dt.Month;
                case (int)DateParts.day:
                    return dt.Day;
                case (int)DateParts.hour:
                    return dt.Hour;
                case (int)DateParts.minute:
                    return dt.Minute;
                case (int)DateParts.second:
                    return dt.Second;
            }
            return typeof(object);
        }
    }


    public enum DateParts
    {
        dateTime = 1,
        date = 2,
        // time = 3,
        dateInt = 4,
        timeInt = 5,
        year = 6,
        month = 7,
        day = 8,
        hour = 9,
        minute = 10,
        second = 11
    }
}

