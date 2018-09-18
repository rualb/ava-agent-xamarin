using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.MyException;
using AvaExt.Common;
using System.Data;
using AvaExt.Manual.Table;
using AvaExt.TableOperation;

using AvaExt.Adapter.Tools;
using AvaExt.Common.Const;
using AvaExt.Settings;
using AvaExt.PagedSource;
using AvaExt.Translating.Tools;

namespace AvaExt.Database.Tools
{
    public class ToolGeneral
    {
        public const int invalidLref = 0;
        public static bool isValidLref(object lref)
        {
            if (lref != null)
            {
                if (lref.GetType() == typeof(int))
                    return (int)lref > (int)0;
                throw new MyExceptionError(MessageCollection.T_MSG_ERROR_INVALID_LREF_TYPE);
            }
            return false;
        }
        public static int date2IntDate(DateTime date)
        {
            return (date.Year << 16) + (date.Month << 8) + date.Day;
        }
        public static DateTime intDate2Date(Int32 date)
        {
            int year = ToolType.intoRange((date >> 16), 1, 9999);
            int month = ToolType.intoRange(((date << 16) >> 24), 1, 12);
            int day = ToolType.intoRange(((date << 24) >> 24), 1, 31);
            int hour = ToolType.intoRange(1, 1, 23);
            int minute = ToolType.intoRange(0, 0, 59);
            int second = ToolType.intoRange(0, 0, 59);
            int millisecond = ToolType.intoRange(0, 0, 999);
            return new DateTime(year, month, day, hour, minute, second, millisecond);
        }
        public static int time2IntTime(DateTime date)
        {
            return (date.Hour << 24) + (date.Minute << 16) + (date.Second << 8);
        }
        public static DateTime intTime2Time(Int32 time)
        {
            int year = ToolType.intoRange(1, 1, 9999);
            int month = ToolType.intoRange(1, 1, 12);
            int day = ToolType.intoRange(1, 1, 31);
            int hour = ToolType.intoRange((time >> 24), 1, 23);
            int minute = ToolType.intoRange(((time << 8) >> 24), 0, 59);
            int second = ToolType.intoRange(((time << 16) >> 24), 0, 59);
            int millisecond = ToolType.intoRange(0, 0, 999);
            return new DateTime(year, month, day, hour, minute, second, millisecond);
        }

        public static double getExchange(int curr, DateTime date, IEnvironment env)
        {
            IPagedSource s = new PagedSourceDailyExchangesMagic(env);
            s.getBuilder().addParameterValue(TableDAILYEXCHANGES.CRTYPE, curr);
            s.getBuilder().addParameterValue(TableDAILYEXCHANGES.DATE_, ToolGeneral.date2IntDate(date));
            return (double)ToolColumn.getColumnLastValue(s.getAll(), ToolColumn.getColumnFullName(TableDUMMY.TABLE, TableDUMMY.EXCHANGE), (double)0);
        }

        public static void setRecordCreator(DataRow row, IEnvironment env)
        {
            DateTime date = DateTime.Now;
            ToolCell.set(row, TableDUMMY.CAPIBLOCK_CREATEDBY, env.getInfoApplication().userId);
            ToolCell.set(row, TableDUMMY.CAPIBLOCK_CREADEDDATE, date.Date);
            ToolCell.set(row, TableDUMMY.CAPIBLOCK_CREATEDHOUR, date.Hour);
            ToolCell.set(row, TableDUMMY.CAPIBLOCK_CREATEDMIN, date.Minute);
            ToolCell.set(row, TableDUMMY.CAPIBLOCK_CREATEDSEC, date.Second);
        }
        public static void setRecordEditor(DataRow row, IEnvironment env)
        {
            DateTime date = DateTime.Now;
            ToolCell.set(row, TableDUMMY.CAPIBLOCK_MODIFIEDBY, env.getInfoApplication().userId);
            ToolCell.set(row, TableDUMMY.CAPIBLOCK_MODIFIEDDATE, date.Date);
            ToolCell.set(row, TableDUMMY.CAPIBLOCK_MODIFIEDHOUR, date.Hour);
            ToolCell.set(row, TableDUMMY.CAPIBLOCK_MODIFIEDMIN, date.Minute);
            ToolCell.set(row, TableDUMMY.CAPIBLOCK_MODIFIEDSEC, date.Second);
        }
        public static void setReportingCurrInfo(DataRow row, double rate, String colRate, String colTotal, String colCurTotal)
        {
            if (colRate != null)
                ToolCell.set(row, colRate, rate);
            if ((colTotal != null) && (colCurTotal != null) && (rate > ConstValues.minPositive) && (ToolCell.isNull(row[colTotal], null) != null))
                ToolCell.set(row, colCurTotal, (double)row[colTotal] / rate);
        }
    }
}
