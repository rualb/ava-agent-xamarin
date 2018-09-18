using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Manual.Table;

namespace AvaExt.Manual.Index
{
    public class IndexUNITSETF  
    {
        public const string TABLE = TableUNITSETF.TABLE;
        public static string[] index_1 = new string[] { TableUNITSETF.LOGICALREF };
        public static string[] index_2 = new string[] { TableUNITSETF.CODE };
        public static string[] index_3 = new string[] { TableUNITSETF.NAME, TableUNITSETF.LOGICALREF };
        public static string[] index_4 = new string[] { TableUNITSETF.CARDTYPE, TableUNITSETF.CODE };
        public static string[] index_5 = new string[] { TableUNITSETF.CARDTYPE, TableUNITSETF.NAME, TableUNITSETF.LOGICALREF };
    }
}
