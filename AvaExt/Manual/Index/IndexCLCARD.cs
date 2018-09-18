using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;

namespace AvaExt.Manual.Index
{
    public class IndexCLCARD 
    {
        public const string TABLE = TableCLCARD.TABLE;
        public static string[] index_1 = new string[] { TableCLCARD.LOGICALREF };
        public static string[] index_2 = new string[] { TableCLCARD.CODE };
        public static string[] index_3 = new string[] { TableCLCARD.DEFINITION_, TableITEMS.LOGICALREF };

    }
}
