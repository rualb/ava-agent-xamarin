using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.ECollections;

namespace AvaExt.TableOperation
{
    public class TableTouchDetector
    {
        DataTable accessTable;
        Dictionary<DataRow, DataRow> accessList;
        long counter;
        const long untouched = 0;
        public TableTouchDetector(DataTable tab)
        {
            counter = untouched;
            accessTable = new DataTable();
            accessList = new Dictionary<DataRow, DataRow>();
            for (int i = 0; i < tab.Columns.Count; ++i)
                accessTable.Columns.Add(tab.Columns[i].ColumnName, typeof(long));
            tab.RowDeleted += new DataRowChangeEventHandler(tab_RowDeleted);
        }

        void tab_RowDeleted(object sender, DataRowChangeEventArgs e)
        {
            accessList.Remove(e.Row);
        }

        public void touchCell(DataRow row, string col)
        {
            if (row != null && col != null)
            {
                if (!accessList.ContainsKey(row))
                    accessList.Add(row, accessTable.NewRow());
                accessList[row][col] = getNextTouchId();
            }
        }

        public long getCellTouch(DataRow row, string col)
        {
            long last = untouched;
            if (row != null && col != null)
                if (accessList.ContainsKey(row))
                    last = (long)ToolCell.isNull(accessList[row][col], (long)untouched);
            return last;
        }

        public Stack<Dublet<string, DataRow>> sort(Dublet<string, DataRow>[] pairs)
        {

            SortedList<long, Dublet<string, DataRow>> sdic = new SortedList<long, Dublet<string, DataRow>>();
            for (int i = 0; i < pairs.Length; ++i)
            {
                long time = getCellTouch(pairs[i].second, pairs[i].first);
                if (time > untouched)
                    time += pairs.Length;
                else
                    time = i;
                sdic.Add(time, pairs[i]);
            }
            Stack<Dublet<string, DataRow>> res = new Stack<Dublet<string, DataRow>>(sdic.Count);
            IEnumerator<long> emun = sdic.Keys.GetEnumerator();
            while (emun.MoveNext())
                res.Push(sdic[emun.Current]);
            return res;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="row"></param>
        /// <returns>return empty if untuched</returns>
        public Stack<string> sort(string[] columns, DataRow row)
        {
            SortedList<long, string> sdic = new SortedList<long, string>();
           
            //SortedDictionary<long, string> sdic = new SortedDictionary<long, string>();
            for (int i = 0; i < columns.Length; ++i)
            {
                long time = getCellTouch(row, columns[i]);
                if (time > untouched)
                    time += columns.Length;
                else
                    time = columns.Length - i;
                sdic.Add(time, columns[i]);
            }
            Stack<string> res = new Stack<string>(sdic.Count);
            IEnumerator<long> emun = sdic.Keys.GetEnumerator();
            while (emun.MoveNext())
                res.Push(sdic[emun.Current]);
            return res;
        }

        long getNextTouchId()
        {
            return ++counter;
        }

    }
}
