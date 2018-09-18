using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;

namespace AvaExt.Manual.Index
{
    public class IndexWHOUSE
    {
        public const string TABLE = TableWHOUSE.TABLE;
        public static string[] index_1 = new string[] { TableWHOUSE.LOGICALREF };
        public static string[] index_2 = new string[] { TableWHOUSE.FIRMNR, TableWHOUSE.NR };
        public static string[] index_3 = new string[] { TableWHOUSE.FIRMNR, TableWHOUSE.NAME, TableWHOUSE.LOGICALREF };

    }
}
