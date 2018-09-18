using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.Adapter.Const;
using AvaExt.Common;

using AvaExt.Adapter.Tools;
using AvaExt.PagedSource;
using AvaExt.SQL;
using AvaExt.SQL.Dynamic.Preparing;
using AvaExt.TableOperation;
using AvaExt.Database.Tools;
using Mono.Data.Sqlite;
using AvaExt.Settings;
using AvaExt.Database;
using System.Data.Common;
using AvaExt.MyException;
using AvaExt.SQL.Dynamic;
using AvaExt.Manual.Table;

namespace AvaExt.Adapter.ForDataTable
{
    public class ImplAdapterTableExt : IAdapterTable
    {
        protected IEnvironment environment;
        //  protected SqliteDataAdapter adapter;

        SqliteDataAdapter createAdapter()
        {
            SqliteDataAdapter adapter = new SqliteDataAdapter();
            adapter.AcceptChangesDuringUpdate = false;

            return adapter;
        }


        //  protected SqliteCommandBuilder builder;
        protected String newIdTable;


        //
        IPagedSource source;
        string[] columns;
        public ImplAdapterTableExt(IEnvironment pEnv, IPagedSource pSource, string[] pCol, String pSqlNewIdTable, ISqlBuilderPreparer[] preps)
        {
            environment = pEnv;
            source = pSource;
            columns = pCol;
            newIdTable = pSqlNewIdTable;
            //
            if (preps != null)
                for (int i = 0; i < preps.Length; ++i)
                    source.getBuilder().addPereparer(preps[i]);
            //
            //adapter = new SqliteDataAdapter();
            //adapter.AcceptChangesDuringUpdate = false;


            //source.getBuilder().reset();
            //source.getBuilder().setCountMin();
            //for (int i = 0; i < columns.Length; ++i)
            //    source.getBuilder().addParameterValue(columns[i], DBNull.Value);
            //adapter.SelectCommand = source.getBuilder().get();


            //builder = new SqliteCommandBuilder(adapter);

            // builder.ConflictOption = ConflictOption.CompareAllSearchableValues;
            // builder.SetAllValues = true;
            //
            // adapter.RowUpdating += new SqliteRowUpdatingEventHandler(adapter_RowUpdating);
        }


        //void adapter_RowUpdating(object sender, SqliteRowUpdatingEventArgs e)
        //{

        //}

        public virtual object set(object pObj)
        {

            SqliteDataAdapter adp_ = null;

            try
            {

                if (CurrentVersion.ENV.isDebugMode())
                {
                    ToolMobile.setRuntimeMsg("ImplAdapterTableExt:set:beg");
                }


                adp_ = createAdapter();

                DataTable dataTable = (DataTable)pObj;

           

                ITableDescriptor desc_ = environment.getDbDescriptor().getTable(getName());
                if (desc_ == null)
                    throw new MyExceptionError("Cant find table description for [" + getName() + "]");

                initCommands(adp_, dataTable, desc_);

                 


                update(adp_, dataTable);

                //adapter.Update(dataTable);


                if (CurrentVersion.ENV.isDebugMode())
                {
                    ToolMobile.setRuntimeMsg("ImplAdapterTableExt:set:end");
                }


                return null;

            }
            catch (Exception exc)
            {
                ToolMobile.setRuntimeMsg(exc.ToString());

                throw new Exception(exc.Message, exc);
            }
            finally
            {
                if (adp_ != null && adp_.InsertCommand != null) { adp_.InsertCommand.Dispose(); adp_.InsertCommand = null; }
                if (adp_ != null && adp_.UpdateCommand != null) { adp_.UpdateCommand.Dispose(); adp_.UpdateCommand = null; }
                if (adp_ != null && adp_.DeleteCommand != null) { adp_.DeleteCommand.Dispose(); adp_.DeleteCommand = null; }
                if (adp_ != null) adp_.Dispose();
            }
        }

        private void update(SqliteDataAdapter pAdapter, DataTable pDataTable)
        {
            if (CurrentVersion.ENV.isDebugMode())
            {
                ToolMobile.setRuntimeMsg("ImplAdapterTableExt:update:beg");
            }

            string CODE = null;


            foreach (DataRow row in pDataTable.Rows)
                if (row.RowState != DataRowState.Unchanged)
                {
                    DbCommand cmd_ = null;
                    switch (row.RowState)
                    {
                        case DataRowState.Added:
                            cmd_ = pAdapter.InsertCommand;

                            break;
                        case DataRowState.Deleted:
                            cmd_ = pAdapter.DeleteCommand;
                            break;
                        case DataRowState.Modified:
                            cmd_ = pAdapter.UpdateCommand;
                            break;
                    }

                    if (cmd_ == null)
                        throw new MyExceptionError("Sql cmd is not generated");

                    foreach (DbParameter prm in cmd_.Parameters)
                    {
                        object val_ = row[prm.SourceColumn, prm.SourceVersion];
                        prm.Value = val_;
                    }

                    if (CurrentVersion.ENV.isDebugMode())
                    {

                      
                        ToolMobile.setRuntimeMsg(cmd_.CommandText);

                        var list = new List<string>();

                        foreach (DbParameter prm in cmd_.Parameters)
                        {
                            object val_ = row[prm.SourceColumn, prm.SourceVersion];
                            list.Add(val_.ToString());

                        }

                       
                    }

                    int count_ = 0;


                    if (row.RowState != DataRowState.Deleted)
                    {
                        if (row.Table.Columns.Contains("CODE"))
                            CODE = row["CODE"].ToString();

                    }


                    try
                    {
                        count_ = cmd_.ExecuteNonQuery();

                    }
                    catch (Exception exc)
                    {

                        ToolMobile.setRuntimeMsg("SQL Error: CODE: " + (CODE ?? "NULL")+": SQL:" + cmd_.CommandText);

                        throw new Exception("SQL Error: " + (CODE ?? "NULL"), exc);
                       
                    }

                    if (count_ == 0)
                    {
                       
                        ToolMobile.setRuntimeMsg("DBConcurrencyException: "+(CODE??"NULL"));
                        

                        throw new DBConcurrencyException("DBConcurrencyException: " + (CODE ?? "NULL"));

                    }


               

                }
        }
        private void initCommands(DbDataAdapter pAdp, DataTable pData, ITableDescriptor pTabDesc)
        {

            if (CurrentVersion.ENV.isDebugMode())
            {
                ToolMobile.setRuntimeMsg("ImplAdapterTableExt:initCommands:beg");
            }

            DataColumn colV_ = pData.Columns[TableDUMMY.RECVERS];

            foreach (DataRow row in pData.Rows)
            {

                switch (row.RowState)
                {
                    case DataRowState.Added:
                        if (pAdp.InsertCommand == null)
                            initCommandInsert(pAdp, pTabDesc);
                        break;
                    case DataRowState.Deleted:
                        if (pAdp.DeleteCommand == null)
                            initCommandDelete(pAdp, pTabDesc);
                        break;
                    case DataRowState.Modified:
                        if (colV_ != null)
                        {
                            short rv_ = Convert.ToInt16(ToolCell.isNull(row[colV_, DataRowVersion.Original], 0));
                            ++rv_;
                            ToolCell.set(row, colV_, rv_);
                        }
                        if (pAdp.UpdateCommand == null)
                            initCommandUpdate(pAdp, pTabDesc);
                        break;

                }

            }
        }


        void initCommandInsert(DbDataAdapter pAdp, ITableDescriptor pTabDesc)
        {

            if (string.IsNullOrEmpty(pTabDesc.getNameFull()))
                throw new MyExceptionError("Db Table Full name is null or empty");

            List<SqliteParameter> listPrm = new List<SqliteParameter>();

            StringBuilder buffer = new StringBuilder();
            //

            //
            buffer.Append("INSERT INTO ");
            //buffer.Append("\"");
            buffer.Append(pTabDesc.getNameFull());
            //buffer.Append("\"");
            //buffer.Append(pTableFullName);
            buffer.Append(" (");

            foreach (ColumnDescriptor col in pTabDesc.getColumns())
            {
                //buffer.Append("\"");
                buffer.Append(col.name);
                //buffer.Append("\"");
                buffer.Append(",");
            }
            buffer.Remove(buffer.Length - 1, 1);
            buffer.Append(") VALUES (");
            int prmIndx = 0;
            foreach (ColumnDescriptor col in pTabDesc.getColumns())
            {

                SqliteParameter prm = new SqliteParameter();
                ++prmIndx;
                prm.ParameterName = ToolSqlText.getNewParamName(prmIndx);
                prm.SourceVersion = DataRowVersion.Current;
                prm.SourceColumn = col.name;
                //prm.Size = col.size;
                listPrm.Add(prm);

                buffer.Append(prm.ParameterName + ",");
            }
            buffer.Remove(buffer.Length - 1, 1);
            buffer.Append(")");
            //

            pAdp.InsertCommand = environment.getNewSqlCommand(buffer.ToString(), listPrm.ToArray());


        }
        void initCommandDelete(DbDataAdapter pAdp, ITableDescriptor pTabDesc)
        {

            if (string.IsNullOrEmpty(pTabDesc.getNameFull()))
                throw new MyExceptionError("Db Table Full name is null or empty");

            bool hasRecVers = pTabDesc.getColumn(TableDUMMY.RECVERS) != null;

            List<SqliteParameter> listPrm = new List<SqliteParameter>();

            StringBuilder buffer = new StringBuilder();
            //

            //
            buffer.Append("DELETE FROM ");
            //buffer.Append("\"");
            buffer.Append(pTabDesc.getNameFull());
            //buffer.Append("\"");
            //buffer.Append(pTableFullName);
            buffer.Append(" WHERE ");

            string[] arr_ = hasRecVers ? new string[] { TableDUMMY.LOGICALREF, TableDUMMY.RECVERS } : new string[] { TableDUMMY.LOGICALREF };
            int prmIndx = 0;
            buffer.Append("(");
            for (int i = 0; i < arr_.Length; ++i)
            {
                if (i > 0)
                    buffer.Append(" AND ");

                SqliteParameter prm = new SqliteParameter();
                ++prmIndx;
                prm.ParameterName = ToolSqlText.getNewParamName(prmIndx);
                prm.SourceVersion = DataRowVersion.Original;
                prm.SourceColumn = arr_[i];
                //prm.Size = col.size;
                listPrm.Add(prm);

                buffer.Append(string.Format("{0} = {1}", prm.SourceColumn, prm.ParameterName));


            }

            buffer.Append(")");
            //

            pAdp.DeleteCommand = environment.getNewSqlCommand(buffer.ToString(), listPrm.ToArray());


        }
        void initCommandUpdate(DbDataAdapter pAdp, ITableDescriptor pTabDesc)
        {


            if (string.IsNullOrEmpty(pTabDesc.getNameFull()))
                throw new MyExceptionError("Db Table Full name is null or empty");


            bool hasRecVers = pTabDesc.getColumn(TableDUMMY.RECVERS) != null;


            List<SqliteParameter> listPrm = new List<SqliteParameter>();

            StringBuilder buffer = new StringBuilder();
            //

            //
            buffer.Append("UPDATE ");
            //buffer.Append("\"");
            buffer.Append(pTabDesc.getNameFull());
            //buffer.Append("\"");
            //buffer.Append(pTableFullName);
            buffer.Append(" SET ");

            int prmIndx = 0;
            foreach (ColumnDescriptor col in pTabDesc.getColumns())
            {
                SqliteParameter prm = new SqliteParameter();
                ++prmIndx;
                prm.ParameterName = ToolSqlText.getNewParamName(prmIndx);
                prm.SourceVersion = DataRowVersion.Current;
                prm.SourceColumn = col.name;
                //prm.Size = col.size;
                listPrm.Add(prm);


                //buffer.Append("\"");
                buffer.Append(string.Format("{0} = {1}", col.name, prm.ParameterName));
                //buffer.Append("\"");
                buffer.Append(",");
            }
            buffer.Remove(buffer.Length - 1, 1);

            buffer.Append(" WHERE ");

            string[] arr_ = hasRecVers ? new string[] { TableDUMMY.LOGICALREF, TableDUMMY.RECVERS } : new string[] { TableDUMMY.LOGICALREF };
            buffer.Append("(");

            for (int i = 0; i < arr_.Length; ++i)
            {
                if (i > 0)
                    buffer.Append(" AND ");

                SqliteParameter prm = new SqliteParameter();
                ++prmIndx;
                prm.ParameterName = ToolSqlText.getNewParamName(prmIndx);
                prm.SourceVersion = DataRowVersion.Original;
                prm.SourceColumn = arr_[i];
                //prm.Size = col.size;
                listPrm.Add(prm);

                buffer.Append(string.Format("{0} = {1}", prm.SourceColumn, prm.ParameterName));
            }

            buffer.Append(")");

            pAdp.UpdateCommand = environment.getNewSqlCommand(buffer.ToString(), listPrm.ToArray());


        }
        public virtual string getName()
        {
            return source.getBuilder().getName();
        }

        public virtual object getNewId()
        {
            string nr_ = CurrentVersion.ENV.getAgentNr();
            string guid_ = ToolGUID.getNew();
            return nr_ + '/' + guid_;

            //Guid guid = Guid.NewGuid();
            //string data = guid.ToString().Replace("-", string.Empty).Replace(" ", string.Empty);
            //string nr = CurrentVersion.ENV.getAgentNr();// environment.getAppSettings().getString(SettingsAvaAgent.MOB_NR_S);
            //nr = nr.PadLeft(3, '0');
            //return nr + data;
            //// return ToolSeq.get(environment, newIdTable);
        }

        public virtual object get(object[] parArr)
        {
            source.getBuilder().reset();
            source.getBuilder().setCountMax();
            for (int i = 0; i < columns.Length; ++i)
                source.getBuilder().addParameterValue(columns[i], parArr[i]);
            return source.get();
        }



        public void Dispose()
        {
            if (source != null)
                source.Dispose();

            source = null;
        }
    }
}
