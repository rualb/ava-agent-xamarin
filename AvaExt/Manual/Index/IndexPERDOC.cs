using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;

namespace AvaExt.Manual.Index
{
    public class IndexPERDOC 
    {
        public const string TABLE = TablePERDOC.TABLE;
        public static string[] index_1 = new string[] { TablePERDOC.LREF };
        public static string[] index_2 = new string[] { TablePERDOC.INFOTYP, TablePERDOC.INFOREF, TablePERDOC.DOCTYP, TablePERDOC.DOCNR, TablePERDOC.LREF };
       
    }
}
