using System;
using System.Collections.Generic;
using System.Text;
 
using System.Data;
using AvaExt.Common;
using AvaExt.TableOperation;

namespace AvaExt.ObjectSource
{
    public class ImplObjectSourceTableCell : IObjectSource 
    {
        DataTable tab; 
        string col;
        object def;
        public ImplObjectSourceTableCell(DataTable pTab, string pCol,object pDef)
        {
            tab = pTab;
            col = pCol;
            def = pDef;
        }
        public object get()
        {
            return ToolColumn.getColumnLastValue(tab, col, def);
        }


    }
}
