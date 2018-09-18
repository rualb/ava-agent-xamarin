using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AvaExt.TableOperation;
using System.Data;
using AvaExt.Manual.Table;
using AvaExt.Formating;

namespace AvaExt.Common
{
    public class ToolLRef
    {
        public readonly static Type lrefType = ToolTypeSet.helper.tString;

        public static readonly object dummyLRef = "2147483647";
        public static readonly object defaultLRef = "0";
        public static readonly object defaultRecNr = "0";
        public static bool isEmptyLRef(object val)
        {
            string i = Convert.ToString(ToolCell.isNull(val, ToolLRef.dummyLRef));
            return (i == "" || i == "0" || i == dummyLRef.ToString());
        }
        public static bool isEqual(object l1, object l2)
        {
            if (!isEmptyLRef(l1) && !isEmptyLRef(l2))
                return ((int)l1 == (int)l2);

            return false;

             
        }

        public static string getTableIdCol(DataTable pTab)
        {
            if (pTab != null)
            {
                DataColumn col_;
                col_ = pTab.Columns[TableDUMMY.LOGICALREF];
                if (col_ != null)
                    return col_.ColumnName;

 
            }

            return string.Empty;
        }
        public static object getRecordId(DataRow pRow)
        {
            if (pRow != null)
            {
                DataColumn col_;
                col_ = pRow.Table.Columns[TableDUMMY.LOGICALREF];
                if (col_ != null)
                    return pRow[col_];

 
            }
            return ToolLRef.dummyLRef;
        }
        
       
        public static string format(object pLRef)
        {
            if (isEmptyLRef(pLRef))
                return string.Empty;
            return XmlFormating.helper.format(pLRef);
        }
        public static object parse(string pLRef)
        {
            pLRef = pLRef.Trim();
            if (pLRef == string.Empty)
                return dummyLRef;
            return XmlFormating.helper.parse(pLRef, lrefType);
        }
    }
}