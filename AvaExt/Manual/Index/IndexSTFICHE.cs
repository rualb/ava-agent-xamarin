using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;

namespace AvaExt.Manual.Index
{
    public class IndexINVOICE
    {
        public const string TABLE = TableINVOICE.TABLE;

        public static string[] index_1 = new string[] { TableINVOICE.LOGICALREF };
        public static string[] index_2 = new string[] { TableINVOICE.GRPCODE, TableINVOICE.TRCODE, TableINVOICE.FICHENO };
        public static string[] index_3 = new string[] { TableINVOICE.GRPCODE, TableINVOICE.DATE_, TableINVOICE.FTIME, TableINVOICE.IOCODE, TableINVOICE.TRCODE, TableINVOICE.LOGICALREF };
        public static string[] index_4 = new string[] { TableINVOICE.INVOICEREF, TableINVOICE.FICHECNT, TableINVOICE.LOGICALREF };
        public static string[] index_5 = new string[] { TableINVOICE.TRCODE, TableINVOICE.FICHENO };
        public static string[] index_6 = new string[] { TableINVOICE.DATE_, TableINVOICE.FTIME, TableINVOICE.IOCODE, TableINVOICE.TRCODE, TableINVOICE.LOGICALREF };
        public static string[] index_7 = new string[] { TableINVOICE.ACCFICHEREF, TableINVOICE.LOGICALREF };
        public static string[] index_8 = new string[] { TableINVOICE.SALESMANREF, TableINVOICE.DATE_, TableINVOICE.FTIME, TableINVOICE.LOGICALREF };
        public static string[] index_9 = new string[] { TableINVOICE.PRODORDERREF, TableINVOICE.LOGICALREF };
        public static string[] index_10 = new string[] { TableINVOICE.SOURCEPOLNREF, TableINVOICE.TRCODE, TableINVOICE.LOGICALREF };
        public static string[] index_11 = new string[] { TableINVOICE.DESTPOLNREF, TableINVOICE.TRCODE, TableINVOICE.LOGICALREF };
        public static string[] index_12 = new string[] { TableINVOICE.ORGLOGICREF, TableINVOICE.SITEID, TableINVOICE.LOGICALREF };
        public static string[] index_13 = new string[] { TableINVOICE.PRODORDERREF, TableINVOICE.GRPCODE, TableINVOICE.DATE_, TableINVOICE.FTIME, TableINVOICE.IOCODE, TableINVOICE.TRCODE, TableINVOICE.LOGICALREF };
        public static string[] index_14 = new string[] { TableINVOICE.SOURCEPOLNREF, TableINVOICE.PRODSTAT, TableINVOICE.TRCODE, TableINVOICE.LOGICALREF };
        public static string[] index_15 = new string[] { TableINVOICE.ORGLOGICREF, TableINVOICE.LOGICALREF, TableINVOICE.SITEID };
        public static string[] index_16 = new string[] { TableINVOICE.MAINSTFCREF, TableINVOICE.LOGICALREF };
        public static string[] index_17 = new string[] { TableINVOICE.GRPCODE, TableINVOICE.DOCODE, TableINVOICE.LOGICALREF };

    }
}
