using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using AvaExt.Common;
using AvaExt.Adapter.ForDataTable;
using AvaExt.Manual.Table;
using AvaExt.Adapter.Tools;
using AvaExt.TableOperation;
using AvaExt.Database.Tools;

namespace AvaExt.Adapter.ForDataSet
{
    public class ImplAdapterDataSet : IAdapterDataSet
    {
        protected Dictionary<String, IAdapterTable> dictionary;
        protected List<String> tableNames;
        protected IEnvironment environment { get { return ToolMobile.getEnvironment(); } set { } }
        public ImplAdapterDataSet(IEnvironment pEnv, IAdapterTable[] pAdapterCol)
        {
            environment = pEnv;
            dictionary = new Dictionary<string, IAdapterTable>();
            tableNames = new List<string>();
            for (int i = 0; i < pAdapterCol.Length; ++i)
            {
                IAdapterTable adp = pAdapterCol[i];
                string name = adp.getName();
                tableNames.Add(name);
                dictionary.Add(name, adp);
            }
            tableNames.Sort();
        }


        public virtual object get(object pId)
        {
            object result = null;
            DataSet dataSet = null;
            try
            {
                if (pId == null)
                    pId = DBNull.Value;
                dataSet = new DataSet(getCode());
                for (int i = 0; i < tableNames.Count; ++i)
                {
                    DataTable tab = (DataTable)dictionary[tableNames[i]].get(new object[] { pId });
                    dataSet.Tables.Add(tab);
                }
                dataTransfered(dataSet);
                result = dataSet; //All ok return DataSet
            }
            catch (Exception exc)
            {
                result = exc;
            }
            return result;
        }
        protected virtual void dataTransfered(DataSet pDataSet)
        {
        }
        public virtual object set(object pData)
        {

            if (CurrentVersion.ENV.isDebugMode())
            {
                ToolMobile.setRuntimeMsg("ImplAdapterDataSet.set:beg");
            }


            object result = null;
            DataSet dataSet = null;
            try
            {
                dataSet = (DataSet)pData;
                changeDataStructure(dataSet);
                setNewIdAll(dataSet);
                prepareBeforeUpdate(dataSet);

                for (int i = 0; i < tableNames.Count; ++i)
                {
                    String curTab = tableNames[i];
                    DataTable table = dataSet.Tables[curTab];
                    correctRecordsStatus(table);
                    checkForTouch(table);

                    if (CurrentVersion.ENV.isDebugMode())
                    {
                        ToolMobile.setRuntimeMsg("dictionary[curTab].set(table)");
                    }


                    dictionary[curTab].set(table);
                }
                result = getReturnResult();
            }
            catch (Exception exc)
            {
                ToolMobile.setRuntimeMsg(exc.ToString());

                environment.rollbackBatch();

                result = exc;
            }


            if (CurrentVersion.ENV.isDebugMode())
            {
                ToolMobile.setRuntimeMsg("ImplAdapterDataSet.set:end");
            }



            return result;
        }

        private void checkForTouch(DataTable table)
        {
            for (int i = 0; i < table.Rows.Count; ++i)
            {
                DataRow row = table.Rows[i];
                switch (row.RowState)
                {
                    case DataRowState.Added:
                        ToolGeneral.setRecordCreator(row, environment);
                        break;
                    case DataRowState.Modified:
                        ToolGeneral.setRecordEditor(row, environment);
                        break;
                }
            }
        }

        private void correctRecordsStatus(DataTable table)
        {
            for (int i = 0; i < table.Rows.Count; ++i)
            {
                DataRow row = table.Rows[i];
                if (row.RowState == DataRowState.Modified)
                {
                    bool changed = false;
                    for (int c = 0; c < table.Columns.Count; ++c)
                    {
                        object cur = row[c];
                        object orj = row[c, DataRowVersion.Original];
                        if (!ToolType.isEqual(cur, orj))
                        {
                            changed = true;
                            break;
                        }
                    }
                    if (!changed)
                    {
                        row.AcceptChanges();
                    }


                }
            }
        }

        protected virtual object getReturnResult()
        {
            return new object();
        }

        protected virtual void prepareBeforeUpdate(DataSet pData)
        {

        }
        protected virtual void changeDataStructure(DataSet pData)
        {

        }
        protected virtual void setNewIdAll(DataSet pData)
        {
            DataSet dataSet = (DataSet)pData;
            for (int i = 0; i < tableNames.Count; ++i)
            {
                String curTabName = tableNames[i];
                DataTable tab = dataSet.Tables[curTabName];
                if (tab != null)
                    setNewIdTable(tab, dictionary[curTabName]);
            }
        }

        protected virtual void setNewIdTable(DataTable pDataTable, IAdapterTable pAdapterTable)
        {
            DataTable dataTabe = (DataTable)pDataTable;
            for (int i = 0; i < dataTabe.Rows.Count; ++i)
            {
                DataRow row = dataTabe.Rows[i];
                if ((row.RowState == DataRowState.Added) && !hasValidId(row, TableDUMMY.ID))
                    row[TableDUMMY.ID] = pAdapterTable.getNewId();
            }
        }
        protected virtual bool hasValidId(DataRow row, string col)
        {
            object defVal = ToolCell.getCellTypeDefaulValue(row.Table.Columns[col].DataType);
            object curId = row[col];
            if (ToolCell.isNull(curId) || ToolType.isEqual(defVal, curId))
                return false;
            return true;
        }
        //protected virtual object beginBatch()
        //{
        //    if (imlisitBatch)
        //        environment.beginBatch();
        //    return null;
        //}

        //protected virtual object commitBatch()
        //{
        //    if (imlisitBatch)
        //        environment.commitBatch();
        //    return null;
        //}


        //public virtual object beginEplisitBatch()
        //{
        //    beginBatch();
        //    imlisitBatch = false;
        //    return null;
        //}

        //public virtual object commitEplisitBatch()
        //{
        //    imlisitBatch = true;
        //    commitBatch();
        //    return null;
        //}



        public virtual string getCode()
        {
            return string.Empty;
        }
    }
}
