using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;

namespace AvaExt.Manual.Index
{
    public class IndexINVOICE 
    {
        public const string TABLE = TableINVOICE.TABLE;
        public static string[] index_3 = new string[] { TableINVOICE.GRPCODE, TableINVOICE.DATE_, TableINVOICE.TIME_, TableINVOICE.TRCODE, TableINVOICE.LOGICALREF };
    }
}
