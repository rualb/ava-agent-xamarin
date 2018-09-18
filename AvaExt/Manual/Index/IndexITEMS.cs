using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;

namespace AvaExt.Manual.Index
{
    public class IndexITEMS 
    {
        public const string TABLE = TableITEMS.TABLE;
        public static string[] index_1 = new string[] { TableITEMS.LOGICALREF };
        public static string[] index_2 = new string[] { TableITEMS.CODE };
        public static string[] index_3 = new string[] { TableITEMS.NAME,TableITEMS.LOGICALREF };

    }
}
