using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;

namespace AvaExt.Manual.Index
{
    public class IndexSPECODES 
    {
        public const string TABLE = TableSPECODES.TABLE;
        public static string[] index_1 = new string[] { TableSPECODES.LOGICALREF };
        public static string[] index_2 = new string[] { TableSPECODES.CODETYPE, TableSPECODES.SPECODETYPE, TableSPECODES.SPECODE, TableSPECODES.LOGICALREF  };
    
    }
}
