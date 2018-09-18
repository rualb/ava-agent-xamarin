using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;

namespace AvaExt.Manual.Index
{
    public class IndexCURRENCYLIST
    {
        public const string TABLE = TableCURRENCYLIST.TABLE;
        public static string[] index_1 = new string[] { TableCURRENCYLIST.LOGICALREF };
        public static string[] index_2 = new string[] { TableCURRENCYLIST.FIRMNR, TableCURRENCYLIST.CURTYPE };
        public static string[] index_3 = new string[] { TableCURRENCYLIST.FIRMNR, TableCURRENCYLIST.CURCODE };
         }
}
