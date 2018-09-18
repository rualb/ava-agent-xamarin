using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.ECollections;

namespace AvaExt.TableOperation
{
    public class TouchObj
    {
        public IDictionary<string, long> dic = new Dictionary<string, long>();
    }
    public class ImplTableTouchMemory : ITableTouchMemory
    {
        //DataTable accessTable = new DataTable();
        //string colCOL = "colCOL";
        //string colROW = "colROW";
        //string colSEQ = "colSEQ";
        const string colTouchList = "_____SPE_____TLIST_____";
        long counter;
        const long untouched = 0;
        const long touchMax = long.MaxValue;
        public ImplTableTouchMemory()
        {
            counter = untouched;
            //accessTable.PrimaryKey = new DataColumn[]{
            //accessTable.Columns.Add(colCOL, typeof(string)),
            //accessTable.Columns.Add(colROW, typeof(DataRow))};
            //accessTable.Columns.Add(colSEQ, typeof(long));
        }
        TouchObj getList(DataRow row)
        {
            TouchObj list = null;
            int colIndx = row.Table.Columns.IndexOf(colTouchList);
            if (colIndx < 0)
                colIndx = row.Table.Columns.Add(colTouchList, typeof(TouchObj)).Ordinal;

            if (row.IsNull(colIndx))
                row[colIndx] = list = new TouchObj();
            else
                list = (TouchObj)row[colIndx];

            return list;
        }


        public void touchCell(DataRow row, string col)
        {

            TouchObj list = getList(row);
            if (list.dic.ContainsKey(col))
                list.dic[col] = getNextTouchId();
            else
                list.dic.Add(col, getNextTouchId());
            //if (row != null && col != null)
            //{
            //    DataRow rowTouch = accessTable.Rows.Find(new object[] { col, row });
            //    if (rowTouch == null)
            //        accessTable.Rows.Add(new object[] { col, row, getNextTouchId() });
            //    else
            //        rowTouch[colSEQ] = getNextTouchId();
            //}
        }

        public long getCellTouch(DataRow row, string col)
        {
            TouchObj list = getList(row);
            if (list.dic.ContainsKey(col))
                return list.dic[col];
            return untouched;
            //if (row != null && col != null)
            //{
            //    DataRow rowTouch = accessTable.Rows.Find(new object[] { col, row });
            //    if (rowTouch != null)
            //        return (long)rowTouch[colSEQ];
            //}
            //return untouched;
        }

        //public Stack<Dublet<string, DataRow>> sort(Dublet<string, DataRow>[] pairs)
        //{
        //    SortedList<long, Dublet<string, DataRow>> sdic = new SortedList<long, Dublet<string, DataRow>>();
        //    for (int i = 0; i < pairs.Length; ++i)
        //    {
        //        long time = getCellTouch(pairs[i].second, pairs[i].first);
        //        if (time > untouched)
        //            time += pairs.Length;
        //        else
        //            time = i;
        //        sdic.Add(time, pairs[i]);
        //    }
        //    Stack<Dublet<string, DataRow>> res = new Stack<Dublet<string, DataRow>>(sdic.Count);
        //    IEnumerator<long> emun = sdic.Keys.GetEnumerator();
        //    while (emun.MoveNext())
        //        res.Push(sdic[emun.Current]);
        //    return res;
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columns"></param>
        /// <param name="row"></param>
        /// <returns>return empty if untuched</returns>
        //public Stack<string> sort(string[] columns, DataRow row)
        //{
        //    SortedList<long, string> sdic = new SortedList<long, string>();
        //    for (int i = 0; i < columns.Length; ++i)
        //    {
        //        long time = getCellTouch(row, columns[i]);
        //        if (time > untouched)
        //            time += columns.Length;
        //        else
        //            time = columns.Length - i;
        //        sdic.Add(time, columns[i]);
        //    }
        //    Stack<string> res = new Stack<string>(sdic.Count);
        //    IEnumerator<long> emun = sdic.Keys.GetEnumerator();
        //    while (emun.MoveNext())
        //        res.Push(sdic[emun.Current]);
        //    return res;
        //}
        public string getOldest(string[] columns, DataRow row)
        {
            string col = string.Empty;
            long lastTime = touchMax;

            for (int i = 0; i < columns.Length; ++i)
            {
                long time = getCellTouch(row, columns[i]);
                if (time > untouched)
                    time += columns.Length;
                else
                    time = columns.Length - i;

                if (time <= lastTime)
                {
                    lastTime = time;
                    col = columns[i];
                }
            }
            return col;
        }
        long getNextTouchId()
        {
            return ++counter;
        }

    }
}
