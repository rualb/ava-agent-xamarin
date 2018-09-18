using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;

namespace AvaExt.Manual.Index
{
    public class IndexUNITSETL 
    {
        public const string TABLE = TableUNITSETL.TABLE;
        public static string[] index_1 = new string[] { TableUNITSETL.LOGICALREF };
        public static string[] index_2 = new string[] { TableUNITSETL.CODE, TableUNITSETL.LOGICALREF };
        public static string[] index_3 = new string[] { TableUNITSETL.NAME, TableUNITSETL.LOGICALREF };
        public static string[] index_4 = new string[] { TableUNITSETL.UNITSETREF, TableUNITSETL.LINENR, TableUNITSETL.LOGICALREF };
    }
}
