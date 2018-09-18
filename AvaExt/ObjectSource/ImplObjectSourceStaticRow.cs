using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.TableOperation.RowsSelector;
using System.Data;

namespace AvaExt.ObjectSource
{
    public class ImplObjectSourceStaticRow : IRowSource
    {
        DataRow row;
        public ImplObjectSourceStaticRow(DataRow pRow)
        {
            row = pRow;
        }
        public void set(DataRow pDataRow)
        {
            row = pDataRow;
        }

        public DataRow get()
        {
            return row;
        }


    }
}
