using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;

namespace AvaExt.Manual.Index
{
    public class IndexDEP
    {
        public const string TABLE = TableDEP.TABLE;
        public static string[] index_1 = new string[] { TableDEP.LOGICALREF };
        public static string[] index_2 = new string[] { TableDEP.FIRMNR, TableDEP.NR };
        public static string[] index_3 = new string[] { TableDEP.FIRMNR, TableDEP.NAME, TableDEP.LOGICALREF };

    }
}
