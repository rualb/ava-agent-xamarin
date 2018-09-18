using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.Adapter.Const;
using AvaExt.Adapter.ForDataSet;
using System.ComponentModel;

using AvaExt.Common;
using AvaExt.MyException;

using AvaExt.TableOperation;
using AvaExt.Manual.Table;
using AvaExt.TableOperation.CellAutomation;
using AvaExt.DataExchange;
using System.Xml;

namespace AvaExt.Adapter.ForUser
{
    public class ImplAdapterUser : IAdapterUser, IDataExchange
    {
        DataSet localDataSet;



        protected AdapterWorkState adapterWorkState;
        protected IEnvironment environment { get { return ToolMobile.getEnvironment(); } set { } }
        protected IAdapterDataSet dataSetAdapter;
        protected ImplDataExchange _dataExchange = null;

        int flagsMask = 0;
        public ImplAdapterUser(IEnvironment pEnv, IAdapterDataSet pDsAdapter)
        {
            environment = pEnv;
            adapterWorkState = AdapterWorkState.stateUndef;
            dataSetAdapter = pDsAdapter;

        }

        public virtual void add()
        {
            adapterWorkState = AdapterWorkState.stateAdd;
            localDataSet = (DataSet)dataSetAdapter.get(null);
            foreach (DataTable tab in localDataSet.Tables)
                tab.RowChanged += new DataRowChangeEventHandler(ImplAdapterUser_RowChanged);
            foreach (DataTable tab in localDataSet.Tables)
                tab.TableNewRow += new DataTableNewRowEventHandler(ImplAdapterUser_TableNewRow);
            addDefaults(localDataSet);
            dataResived(localDataSet);

        }



        public virtual void edit(object pId)
        {
            adapterWorkState = AdapterWorkState.stateEdit;
            localDataSet = (DataSet)dataSetAdapter.get(pId);


            foreach (DataTable tab in localDataSet.Tables)
                tab.RowChanged += new DataRowChangeEventHandler(ImplAdapterUser_RowChanged);
            foreach (DataTable tab in localDataSet.Tables)
                tab.TableNewRow += new DataTableNewRowEventHandler(ImplAdapterUser_TableNewRow);
            addDefaults(localDataSet);
            dataResived(localDataSet);

        }







        public virtual void delete(object pId)
        {
            adapterWorkState = AdapterWorkState.stateDelete;
            localDataSet = (DataSet)dataSetAdapter.get(pId);
            for (int t = 0; t < localDataSet.Tables.Count; ++t)
                for (int r = 0; r < localDataSet.Tables[t].Rows.Count; ++r)
                    localDataSet.Tables[t].Rows[r].Delete();
        }
        public virtual void delete()
        {
            List<DataRow> list = new List<DataRow>();
            adapterWorkState = AdapterWorkState.stateDelete;
            if (localDataSet != null)
            {
                foreach (DataTable t in localDataSet.Tables)
                    foreach (DataRow r in t.Rows)
                        list.Add(r);
            }

            foreach (DataRow r in list)
                if (r.RowState != DataRowState.Deleted && r.RowState != DataRowState.Detached)
                    r.Delete();
        }
        public virtual object update()
        {
            if (CurrentVersion.ENV.isDebugMode())
            {
                ToolMobile.setRuntimeMsg("ImplAdapterUser.update:1");
            }

            prepareBeforeUpdate(localDataSet);
            DataSet ds = localDataSet.Copy();
            deleteFullColumns(ds);

            if (CurrentVersion.ENV.isDebugMode())
            {
                ToolMobile.setRuntimeMsg("ImplAdapterUser.update:3");
            }

            return dataSetAdapter.set(ds);
        }

        private void deleteFullColumns(DataSet ds)
        {
            for (int t = 0; t < ds.Tables.Count; ++t)
            {
                DataTable table = ds.Tables[t];
                for (int c = 0; c < table.Columns.Count; ++c)
                {
                    if (ToolColumn.isColumnFullName(table.Columns[c].ColumnName))
                    {
                        table.Columns.RemoveAt(c);
                        --c;
                    }
                }
            }
        }

        public virtual AdapterWorkState getAdapterWorkState()
        {
            return adapterWorkState;
        }

        public virtual DataSet getDataSet()
        {
            return localDataSet;
        }
        public virtual DataTable getTable(string tableName)
        {
            return localDataSet.Tables[tableName];
        }

        public bool isEmptyData()
        {
            return isEmptyData(localDataSet);
        }
        protected virtual bool isEmptyData(DataSet pDataSet)
        {
            return false;
        }
        public virtual void deleteExcessData()
        {
            deleteExcessData(localDataSet);
        }
        protected virtual void deleteExcessData(DataSet pDataSet)
        {

        }
        protected virtual void prepareBeforeUpdate(DataSet pDataSet)
        {
            for (int t = 0; t < pDataSet.Tables.Count; ++t)
                for (int r = 0; r < pDataSet.Tables[t].Rows.Count; ++r)
                    pDataSet.Tables[t].Rows[r].EndEdit();
        }

        protected virtual void dataResived(DataSet pDataSet)
        {
            foreach (DataTable tab in pDataSet.Tables)
                tab.ColumnChanging += new DataColumnChangeEventHandler(tab_ColumnChanging);
        }

        void tab_ColumnChanging(object sender, DataColumnChangeEventArgs e)
        {

        }

        void ImplAdapterUser_RowChanged(object sender, DataRowChangeEventArgs e)
        {
            if (e.Action == DataRowAction.Add)
                newRowInCollection(e.Row);
        }
        void ImplAdapterUser_TableNewRow(object sender, DataTableNewRowEventArgs e)
        {
            ToolRow.initTableNewRow(e.Row);
        }
        public virtual void clear()
        {
            for (int i = 0; i < localDataSet.Tables.Count; ++i)
                localDataSet.Tables[i].Clear();
            addDefaults(localDataSet);

        }

        protected virtual void newRowInCollection(DataRow pNewRow)
        {

        }
        protected virtual void addDefaults(DataSet pDataSet)
        {


        }


        public IAdapterDataSet getAdapterDataSet()
        {
            return dataSetAdapter;
        }


        public DataRow addRowToTable(DataTable table)
        {
            object[] obj = ToolRow.getVallueArrayForTable(table);
            return table.Rows.Add(obj);
        }
        public DataRow insertRowIntoTable(DataTable table, int pos)
        {
            DataRow row = table.NewRow();
            ToolRow.initTableNewRow(null);
            table.Rows.InsertAt(row, pos);
            return row;
        }

        public void flagEnable(AdapterUserFlags flag)
        {
            flagsMask = (int)flagsMask | (int)flag;
        }

        public void flagDisable(AdapterUserFlags flag)
        {
            flagsMask = (int)flagsMask & (~(int)flag);
        }

        public bool isFlagEnabled(AdapterUserFlags flag)
        {
            return ((int)flagsMask & (int)flag) == (int)flag;
        }

        protected void sortLines(DataTable tab, string col)
        {
            DataTable tabCopy = tab.Copy();
            tab.Clear();
            tabCopy.DefaultView.Sort = string.Format("[{0}] ASC", col);
            tab.Load(tabCopy.DefaultView.ToTable().CreateDataReader());
        }

        public string export()
        {
            initForEI();
            return _dataExchange.export();
        }


        public void export(XmlDocument doc)
        {
            initForEI();
            _dataExchange.export(doc);
        }
        public void import(string pData)
        {
            initForEI();
            _dataExchange.import(pData);
        }
        public void import(XmlDocument doc)
        {
            initForEI();
            _dataExchange.import(doc);
        }


        protected virtual void initForEI()
        {
            if (_dataExchange == null)
                _dataExchange = new ImplDataExchange(environment, this);

        }
        public virtual void resetRefs()
        {
            foreach (DataTable tab in getDataSet().Tables)
                foreach (DataRow row in tab.Rows)
                {
                    ToolCell.set(row, TableDUMMY.ID, ToolCell.getCellTypeDefaulValue(tab.Columns[TableDUMMY.ID].DataType));
                }
        }

        public virtual void setAdded()
        {
            getDataSet().AcceptChanges();
            foreach (DataTable tab in getDataSet().Tables)
                foreach (DataRow row in tab.Rows)
                {
                    row.SetAdded();
                    ToolCell.set(row, TableDUMMY.ID, ToolCell.getCellTypeDefaulValue(tab.Columns[TableDUMMY.ID].DataType));
                }
        }

        public virtual void initCopy()
        {
            adapterWorkState = AdapterWorkState.stateCopy;
        }


    }
}
