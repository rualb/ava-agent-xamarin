using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;

namespace AvaExt.Manual.Index
{
    public class IndexDIV
    {
        public const string TABLE = TableDIV.TABLE;
        public static string[] index_1 = new string[] { TableDIV.LOGICALREF };
        public static string[] index_2 = new string[] { TableDIV.FIRMNR,TableDIV.NR };
        public static string[] index_3 = new string[] { TableDIV.FIRMNR, TableDIV.NAME, TableDIV.LOGICALREF };

    }
}
