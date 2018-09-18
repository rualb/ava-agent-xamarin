using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;

namespace AvaExt.Manual.Index
{
    public class IndexFACK
    {
        public const string TABLE = TableFACK.TABLE;
        public static string[] index_1 = new string[] { TableFACK.LOGICALREF };
        public static string[] index_2 = new string[] { TableFACK.FIRMNR, TableFACK.NR };
        public static string[] index_3 = new string[] { TableFACK.FIRMNR, TableFACK.NAME, TableFACK.LOGICALREF };

    }
}
