using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.TableOperation.RowsSelector;
using System.Data;
using AvaExt.AndroidEnv.ControlsBase;


namespace AvaExt.ObjectSource
{
    public class ImplObjectSourceGridCurrentBindedRow : IRowSource
    {
        DataGrid grid;
        public ImplObjectSourceGridCurrentBindedRow(DataGrid pGrid)
        {
            grid = pGrid;



        }
        public DataRow get()
        {
            return grid.ActiveRow;
        }


    }
}
