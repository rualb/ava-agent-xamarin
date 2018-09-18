using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.SQL.Dynamic;
using System.Data;
using AvaExt.Manual.Table;
using AvaExt.TableOperation;
using AvaExt.Database.Tools;

namespace AvaExt.PagedSource
{
    public class PagedSourceDailyExchangesMagic : PagedSourceDailyExchanges
    {
        public const string E_DATE_ = "E_DATE_";
        public PagedSourceDailyExchangesMagic(IEnvironment pEnv)
            : base(pEnv)
        {
            getBuilder().addColumnToMeta(E_DATE_, typeof(DateTime));
        }

        public override DataTable getFirst()
        {
            return get();
        }
        public override DataTable getFirst(bool prepareForWhere)
        {
            return get();
        }
        public override DataTable getNext()
        {
            return get();
        }
        public override DataTable getPreviose()
        {
            return get();
        }
        public override DataTable getCurrent()
        {
            return get();
        }
        public override DataTable getLast()
        {
            return get();
        }
        public override DataTable getAll()
        {
            return get();
        }
        public override DataTable get()
        {
            DataTable exch;
            int date;
            short curr = (short)ToolCell.isNull(getBuilder().getParameterValue(TableDAILYEXCHANGES.CRTYPE), (short)0);

            object dt = getBuilder().getParameterValue(E_DATE_);

            if (dt != null)
            {
                date = ToolGeneral.date2IntDate((DateTime)dt);
                getBuilder().deleteParameter(E_DATE_);
                getBuilder().addParameterValue(TableDAILYEXCHANGES.DATE_, date);
            }
            else
                date = (int)ToolCell.isNull(getBuilder().getParameterValue(TableDAILYEXCHANGES.DATE_), (int)0);

            if ((curr == 0) || (curr == environment.getInfoApplication().periodCurrencyNativeId))
            {
                exch = getTableInstance();
                exch.Rows.Add(ToolRow.initTableNewRow(exch.NewRow()));
                ToolColumn.setColumnValue(exch, TableDAILYEXCHANGES.CRTYPE, curr);
                ToolColumn.setColumnValue(exch, TableDAILYEXCHANGES.DATE_, date);
                ToolColumn.setColumnValue(exch, TableDAILYEXCHANGES.RATES1, 1);
                ToolColumn.setColumnValue(exch, TableDAILYEXCHANGES.RATES2, 1);
                ToolColumn.setColumnValue(exch, TableDAILYEXCHANGES.RATES3, 1);
                ToolColumn.setColumnValue(exch, TableDAILYEXCHANGES.RATES4, 1);
            }
            else
                exch = base.get();
            exch.Columns.Add(ToolColumn.getColumnFullName(TableDUMMY.TABLE, TableDUMMY.EXCHANGE), typeof(double), TableDAILYEXCHANGES.RATES1);
            return exch;
        }

        public override DataTable getTableInstance()
        {
            DataTable table = new DataTable(TableDAILYEXCHANGES.TABLE);
            table.Columns.Add(TableDAILYEXCHANGES.CRTYPE, typeof(short));
            table.Columns.Add(TableDAILYEXCHANGES.DATE_, typeof(int));
            table.Columns.Add(TableDAILYEXCHANGES.RATES1, typeof(double));
            table.Columns.Add(TableDAILYEXCHANGES.RATES2, typeof(double));
            table.Columns.Add(TableDAILYEXCHANGES.RATES3, typeof(double));
            table.Columns.Add(TableDAILYEXCHANGES.RATES4, typeof(double));
            return table;
        }
    }
}
