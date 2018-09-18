using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AvaExt.Database
{
    public class ImplTableDescriptor : ITableDescriptor
    {
        string tableNameShort;
        string tableNameFull;
        List<TmpWrap> list = new List<TmpWrap>();
        List<TmpWrap> listNotSorted = new List<TmpWrap>();

        public ImplTableDescriptor(string pTableNameShort, string pTableNameFull, string[] pColsNames, int[] pColsSizes, Type[] pColsTypes)
        {
            tableNameShort = pTableNameShort;
            tableNameFull = pTableNameFull;

            for (int i = 0; i < pColsNames.Length; ++i)
            {
                ColumnDescriptor desc = new ColumnDescriptor();
                desc.name = pColsNames[i];
                desc.size = pColsSizes[i];
                desc.type = pColsTypes[i];
                //
                appendColumnDescriptor(desc);
            }
        }

        void appendColumnDescriptor(ColumnDescriptor pDesc)
        {
            TmpWrap t_ = new TmpWrap(pDesc);

            list.Add(t_);
            listNotSorted.Add(t_);
        }
        void prependColumnDescriptor(ColumnDescriptor pDesc)
        {
            list.Insert(0, new TmpWrap(pDesc));
        }
        public string getNameShort()
        {
            return tableNameShort;
        }
        public string getNameFull()
        {
            return tableNameFull;
        }
        string shrinkIndexed(string col)
        {
            while (col.Length > 0 && char.IsDigit(col[col.Length - 1]))
                col = col.Substring(0, col.Length - 1);
            return col;
        }
        bool isColumnIndexed(string col)
        {
            if (col.Length > 0 && char.IsDigit(col[col.Length - 1]))
                return true;
            return false;
        }
        public ColumnDescriptor getColumn(string col)
        {
            for (int i = 0; i < list.Count; ++i)
            {
                TmpWrap desc = list[i];
                if (desc.col.name == col)
                {
                    if (!desc.corrected)
                    {
                        desc.corrected = true;
                        list.RemoveAt(i);
                        list.Insert(0, desc);
                    }
                    return desc.col;
                }
            }
            if (isColumnIndexed(col))
            {
                ColumnDescriptor dsc = getColumn(shrinkIndexed(col));
                if (dsc != null)
                {
                    ColumnDescriptor dscNew = dsc.copy();
                    dscNew.name = col;
                    prependColumnDescriptor(dscNew);
                    return dscNew;
                }

            }
            return null;
        }



        public void Dispose()
        {
            if (list != null) { list.Clear(); list = null; }
        }


        public ColumnDescriptor[] getColumns()
        {
            List<ColumnDescriptor> l_ = new List<ColumnDescriptor>();

            foreach (TmpWrap t_ in listNotSorted)
                l_.Add(t_.col.copy());

            return l_.ToArray();
        }
    }
    class TmpWrap
    {
        public TmpWrap(ColumnDescriptor pDesc)
        {
            col = pDesc;
        }
        public ColumnDescriptor col;
        public bool corrected = false; //sort order for quiq access

    }

}
