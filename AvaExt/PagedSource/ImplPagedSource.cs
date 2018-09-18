using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.SQL.Dynamic;
using System.Data;

using AvaExt.Common;
using System.Collections;
using AvaExt.TableOperation;
using AvaExt.TableOperation.TableConverter;
using Mono.Data.Sqlite;
using AvaExt.SQL;


namespace AvaExt.PagedSource
{
    public class ImplPagedSource : IPagedSource
    {
        ISqlBuilder builder;

        protected IEnvironment environment { get { return ToolMobile.getEnvironment(); } set { } }
        object[] borderData;
        bool isAsc = true;
        string[] indexColumns = new string[0];
        protected DataTable tableInstance = null;

        Dictionary<int, ITableConverter> cnverterList = new Dictionary<int, ITableConverter>();
        int idIndx = 0;

        public ImplPagedSource(IEnvironment pEnv, ISqlBuilder pBuilder)
        {
            builder = pBuilder;
            environment = pEnv;
        }

        public virtual DataTable getFirst()
        {
            return getFirst(false);
        }
        public virtual DataTable getFirst(bool prepareForWhere)
        {
            builder.reset();
            preparingForSort(indexColumns);
            if (prepareForWhere)
                preparingForPagedWhere(indexColumns, borderData);
            DataTable table = new DataTable(builder.getName());
            fill(table);
            setBorder(ToolRow.getLastRealRow(table));
            return convert(table);
        }
        public virtual DataTable getNext()
        {
            builder.reset();
            preparingForSort(indexColumns);
            preparingForPagedWhere(indexColumns, borderData);
            DataTable table = new DataTable(builder.getName());
            fill(table);
            setBorder(ToolRow.getLastRealRow(table));
            return convert(table);
        }
        public virtual DataTable getPreviose()
        {
            reverseSort();
            int count = getBuilder().getCount();
            getBuilder().setCount(count * 2);
            getNext();
            getBuilder().setCount(count);
            reverseSort();
            return getNext();
        }
        public virtual DataTable getCurrent()
        {
            reverseSort();
            getNext();
            reverseSort();
            return getNext();
        }
        public virtual DataTable getLast()
        {
            reverseSort();
            DataTable table = getFirst();
            reverseSort();
            setBorder(ToolRow.getFirstRealRow(table));
            return ToolTable.createReversed(table);
        }
        public virtual DataTable getAll()
        {
            preparingForSort();
            int count = builder.getCount();
            builder.setCountMax();
            DataTable table = new DataTable(builder.getName());
            fill(table);
            builder.setCount(count);
            return convert(table);
        }
        public virtual DataTable get()
        {
            preparingForSort();
            DataTable table = new DataTable(builder.getName());
            fill(table);
            return convert(table);
        }


        void fill(DataTable pTab)
        {
            SqlExecute.fill(builder, pTab);
 
        }

        public ISqlBuilder getBuilder()
        {
            return builder;
        }

        public void preparingForSort(string[] pIndexColumns)
        {
            builder.resetSort();
            if (pIndexColumns != null)
                if (pIndexColumns.Length > 0)
                {

                    SqlTypeRelations sorting = isSortedAsc() ? SqlTypeRelations.sortAsc : SqlTypeRelations.sortDesc;
                    for (int i = 0; i < pIndexColumns.Length; ++i)
                        builder.addSortColumn(pIndexColumns[i], sorting);
                }
        }
        public void preparingForSort()
        {
            preparingForSort(indexColumns);
        }

        public void preparingForPagedWhere(string[] pIndexColumns, object[] pBorderData)
        {
            if ((pIndexColumns.Length > 0) && (pBorderData != null))
            {

                SqlTypeRelations mathE = SqlTypeRelations.equal;
                SqlTypeRelations mathGL = isSortedAsc() ? SqlTypeRelations.greater : SqlTypeRelations.less;
                builder.beginWhereGroup();
                //int indMax = indexColumns.Length - 1;
                //for (int g = 0; g <= (indMax + 1); ++g)
                //{
                //    builder.beginWhereGroup(SqlTypeRelations.boolOr);
                //    for (int i = 0; i <= Math.Min(g, indMax); ++i)
                //        builder.addParameterValue(pIndexColumns[i], pBorderData[pIndexColumns[i]], ((i < g) || (g > indMax)) ? mathE : mathGL);
                //    builder.endWhereGroup();
                //}

                int indMax = pIndexColumns.Length;
                for (int g = 0; g < indMax; ++g)
                {
                    builder.beginWhereGroup(SqlTypeRelations.boolOr);
                    for (int i = 0; i <= g; ++i)
                        builder.addParameterValue(pIndexColumns[i], pBorderData[i], (i < g) ? mathE : mathGL);
                    builder.endWhereGroup();
                }

                builder.beginWhereGroup(SqlTypeRelations.boolOr);
                for (int i = 0; i < pIndexColumns.Length; ++i)
                    builder.addParameterValue(pIndexColumns[i], pBorderData[i], mathE);
                builder.endWhereGroup();

                builder.endWhereGroup();
            }
        }
        public void preparingForPagedWhere()
        {
            preparingForPagedWhere(indexColumns, borderData);
        }

        public bool isSortedAsc()
        {
            return isAsc;
        }

        public void setSortingMode(bool pIsAsc)
        {
            isAsc = pIsAsc;
        }



        public void reverseSort()
        {
            setSortingMode(!isSortedAsc());
        }
        public virtual DataTable getTableInstance()
        {
            if (tableInstance == null)
            {
                tableInstance = getFirst();
                tableInstance.Clear();
            }
            return tableInstance.Clone();
        }
        public void setBorder(DataRow row)
        {
            if (row != null)
            {
                borderData = new object[indexColumns.Length];
                for (int i = 0; i < indexColumns.Length; ++i)
                    borderData[i] = row[indexColumns[i]];
            }
        }
        public string[] getIndex()
        {
            return indexColumns;
        }
        public void setIndex(string[] indx)
        {
            indexColumns = indx;
        }



        public int addTableConverter(ITableConverter pConv)
        {
            int newId = ++idIndx;
            cnverterList.Add(newId, pConv);
            return newId;
        }


        public void deleteTableConverter(int key)
        {
            cnverterList.Remove(key);
        }

        public void deleteTableConverter(int[] keys)
        {
            for (int i = 0; i < keys.Length; ++i)
                cnverterList.Remove(keys[i]);
        }

        DataTable convert(DataTable tab)
        {
            DataTable tabTmp = tab;
            IEnumerator val = cnverterList.Values.GetEnumerator();
            val.Reset();
            while (val.MoveNext())
                tabTmp = ((ITableConverter)val.Current).convert(tabTmp);
            return tabTmp;
        }

        public void Dispose()
        {
            if (builder != null)
                builder.Dispose();

            builder = null;
        }
    }
}
