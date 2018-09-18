using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.AndroidEnv.ControlsBase;

namespace AvaGE.MobControl.Tools.Formating
{
    public class FormatFromTable : IFormatProvider,ICustomFormatter
    {
 
        string colInGrid;
        DataTable table;
        string colInTab;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pGrid">grid</param>
        /// <param name="pColInGrid">grid column for pain</param>
        /// <param name="pColInSubTable">columns refreshed</param>
        public FormatFromTable( string pColInGrid, DataTable pTable, string pColInTab)
        { 
            colInGrid = pColInGrid;
            table = pTable;
            colInTab = pColInTab;
            //
             
        }

        //void grid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        //{
        //    if (e.Value != null && e.Value.GetType() != DBNull.Value.GetType() && e.ColumnIndex >= 0)
        //        if (grid.Columns[e.ColumnIndex].Name == colInGrid)
        //            if (e.DesiredType == typeof(string))
        //            {
        //                DataRow row = table.Rows.Find(e.Value);
        //                if (row != null)
        //                {
        //                    e.Value = row[colInTab];
        //                    e.FormattingApplied = true;
        //                }
        //            }
        //}

  
        public object GetFormat(Type formatType)
        {
            return this; 
        }

 
        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            return "XYZ";
        }

       
    }
}
