using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.TableOperation.RowsSelector;
using System.Data;
using AvaExt.Common;
using AvaExt.TableOperation;

namespace AvaExt.ObjectSource
{
    public class ImplObjectSourceRowCell : IObjectSource 
    {
        IRowSource rowSource;
        string col;
 
        public ImplObjectSourceRowCell(DataRow pRow, string pCol )
        {
            rowSource = new ImplObjectSourceStaticRow(pRow);
            col = pCol;
     
        }
        public ImplObjectSourceRowCell(IRowSource pRowSource, string pCol)
        {
            rowSource = pRowSource;
            col = pCol;

        }
        public object get()
        {
            return rowSource.get()[col];
        }


    }
}
