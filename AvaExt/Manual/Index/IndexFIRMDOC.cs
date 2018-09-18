using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;

namespace AvaExt.Manual.Index
{
    public class IndexFIRMDOC 
    {
        public const string TABLE = TableFIRMDOC.TABLE;
        public static string[] index_1 = new string[] { TableFIRMDOC.LREF };
        public static string[] index_2 = new string[] { TableFIRMDOC.INFOTYP, TableFIRMDOC.INFOREF, TableFIRMDOC.DOCTYP, TableFIRMDOC.DOCNR, TableFIRMDOC.LREF };
       
    }
}
