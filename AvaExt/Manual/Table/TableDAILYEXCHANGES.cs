using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.TableOperation;

namespace AvaExt.Manual.Table
{
    public class TableDAILYEXCHANGES
    {
        public const String TABLE = "DAILYEXCHANGES";
        public const String LREF = "LREF"; // int 4
        public const String DATE_ = "DATE_"; // int 4
        public const String CRTYPE = "CRTYPE"; // smallint 2
        public const String RATES1 = "RATES1"; // float 8
        public const String RATES2 = "RATES2"; // float 8
        public const String RATES3 = "RATES3"; // float 8
        public const String RATES4 = "RATES4"; // float 8
        public const String EDATE = "EDATE"; // datetime 8

        public static readonly String E_DUMMY_DATE_ = ToolColumn.getColumnFullName(TableDUMMY.TABLE, TableDUMMY.DATE_);
        public static readonly String E_DUMMY_RATE = ToolColumn.getColumnFullName(TableDUMMY.TABLE, TableDUMMY.RATE);

    }
}
