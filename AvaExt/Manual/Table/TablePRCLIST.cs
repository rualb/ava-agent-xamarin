using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.TableOperation;

namespace AvaExt.Manual.Table
{
    public class TablePRCLIST
    {
        public const String TABLE = "PRCLIST";

        public const String LOGICALREF = "LOGICALREF"; // int 4
        public const String CARDREF = "CARDREF"; // int 4
        public const String CLIENTCODE = "CLIENTCODE"; // varchar 17
        public const String CLSPECODE = "CLSPECODE"; // varchar 11
        public const String PAYPLANREF = "PAYPLANREF"; // int 4
        public const String PRICE = "PRICE"; // float 8
        public const String UOMREF = "UOMREF"; // int 4
        public const String INCVAT = "INCVAT"; // smallint 2
        public const String CURRENCY = "CURRENCY"; // smallint 2
        public const String PRIORITY = "PRIORITY"; // smallint 2
        public const String PTYPE = "PTYPE"; // smallint 2
        public const String MTRLTYPE = "MTRLTYPE"; // smallint 2
        public const String LEADTIME = "LEADTIME"; // int 4
        public const String BEGDATE = "BEGDATE"; // datetime 8
        public const String ENDDATE = "ENDDATE"; // datetime 8
        public const String CONDITION = "CONDITION"; // varchar 81
        public const String SHIPTYP = "SHIPTYP"; // varchar 5
        public const String SPECIALIZED = "SPECIALIZED"; // smallint 2
        public const String SITEID = "SITEID"; // smallint 2
        public const String RECSTATUS = "RECSTATUS"; // smallint 2
        public const String ORGLOGICREF = "ORGLOGICREF"; // int 4
        public const String WFSTATUS = "WFSTATUS"; // int 4
        public const String UNITCONVERT = "UNITCONVERT"; // smallint 2
        public const String EXTACCESSFLAGS = "EXTACCESSFLAGS"; // int 4
        public const String CYPHCODE = "CYPHCODE"; // varchar 11
        public const String ORGLOGOID = "ORGLOGOID"; // varchar 25
        public const String TRADINGGRP = "TRADINGGRP"; // varchar 17
        public const String BEGTIME = "BEGTIME"; // int 4
        public const String ENDTIME = "ENDTIME"; // int 4
        public const String DEFINITION_ = "DEFINITION_"; // varchar 51
        public const String CODE = "CODE"; // varchar 25
        public const String GRPCODE = "GRPCODE"; // varchar 17
        public const String ORDERNR = "ORDERNR"; // smallint 2
        public const String GENIUSPAYTYPE = "GENIUSPAYTYPE"; // varchar 3
        public const String GENIUSSHPNR = "GENIUSSHPNR"; // int 4
        public const String PRCALTERTYP1 = "PRCALTERTYP1"; // smallint 2
        public const String PRCALTERLMT1 = "PRCALTERLMT1"; // float 8
        public const String PRCALTERTYP2 = "PRCALTERTYP2"; // smallint 2
        public const String PRCALTERLMT2 = "PRCALTERLMT2"; // float 8
        public const String PRCALTERTYP3 = "PRCALTERTYP3"; // smallint 2
        public const String PRCALTERLMT3 = "PRCALTERLMT3"; // float 8
        public const String ACTIVE = "ACTIVE"; // smallint 2

        public static readonly string E_DUMMY__UNIT = ToolColumn.getColumnFullName(TableDUMMY.TABLE,TableDUMMY.UNIT);
        public static readonly string E_DUMMY__PRICE = ToolColumn.getColumnFullName(TableDUMMY.TABLE, TableDUMMY.PRICE);
        public static readonly string E_DUMMY__CURRENCY = ToolColumn.getColumnFullName(TableDUMMY.TABLE, TableDUMMY.CURRENCY);

    }
}
