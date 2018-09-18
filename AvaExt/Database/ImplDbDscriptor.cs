using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.Settings;
using AvaExt.SQL.Dynamic;
using AvaExt.SQL;
using AvaExt.PagedSource;
using System.Data;
using AvaExt.TableOperation;
using AvaExt.Manual.Table;
using AvaExt.MyException;
using AvaExt.Translating.Tools;
using AvaExt.Common;
using AvaExt.TableOperation;
using AvaExt.MyException;
using AvaExt.Translating.Tools;
using AvaExt.Manual.Table;
using AvaExt.PagedSource;
using AvaExt.SQL.Dynamic;
using AvaExt.Formating;
using Ava_Ext.Common;

namespace AvaExt.Database
{
    public class ImplDbDscriptor : IDbDscriptor
    {


        Dictionary<string, ITableDescriptor> dic = new Dictionary<string, ITableDescriptor>();
        protected IEnvironment environment { get { return ToolMobile.getEnvironment(); } }



        public ImplDbDscriptor()
        {

            addTableDesc(TableDUMMY.TABLE, new ImplDummyTableDescriptor());

            ///
            fillColumnsInfo();
        }


        void fillColumnsInfo()
        {
            string firmId = environment.prepareSqlText("$FIRM$");
            string periodId = environment.prepareSqlText("$PERIOD$");
            //

            DataTable tabs_ = new ImplPagedSource(environment, new ImplSqlBuilder(environment, "SELECT name FROM sqlite_master  where type= 'table'", string.Empty)).getAll();

            foreach (DataRow row1 in tabs_.Rows)
            {


                //  string type1_ = ToolCell.isNull(row1["type"], "").ToString().ToLowerInvariant();
                string name1_ = ToolCell.isNull(row1["name"], "").ToString();
                // if (type1_ == "table")
                {
                    string tableNameShort = name1_;
                    string tableNameFull = name1_;


                    if (tableNameFull.StartsWith("L_CAPI"))
                        tableNameShort = tableNameFull.Remove(0, 6);
                    else if (tableNameFull.StartsWith("L_"))
                        tableNameShort = tableNameFull.Remove(0, 2);
                    else
                        if (tableNameFull.StartsWith("LG_" + firmId + "_" + periodId + "_"))
                            tableNameShort = tableNameFull.Remove(0, 10);
                        else
                            if (tableNameFull.StartsWith("LG_" + firmId + "_"))
                                tableNameShort = tableNameFull.Remove(0, 7);



                    List<string> listCols = new List<string>();
                    List<int> listSize = new List<int>();
                    List<Type> listType = new List<Type>();

                    DataTable cols_ = new ImplPagedSource(environment, new ImplSqlBuilder(environment, "pragma table_info(" + name1_ + ")", string.Empty)).getAll();
                    foreach (DataRow row2 in cols_.Rows)
                    {


                        string type2_ = ToolCell.isNull(row2["type"], "").ToString().ToLowerInvariant();
                        string name2_ = ToolCell.isNull(row2["name"], "").ToString();
                        //store len as nvarchar(20)

                        StringBuilder sbName = new StringBuilder();
                        StringBuilder sbLen = new StringBuilder();

                        foreach (char c in type2_.ToCharArray())
                        {
                            if (char.IsLetter(c))
                                sbName.Append(c);
                            else
                                if (char.IsDigit(c) || c == '.')
                                    sbLen.Append(c);
                        }

                        if (sbLen.Length == 0)
                            sbLen.Append('0');


                        Type type3_ = ToolTypeSet.helper.tObject;
                        double len3_ = XmlFormating.helper.parseDouble(sbLen.ToString());


                        switch (sbName.ToString())
                        {
                            case "nvarchar":
                            case "varchar":
                                type3_ = ToolTypeSet.helper.tString;
                                break;
                            case "float":
                                type3_ = ToolTypeSet.helper.tDouble;
                                break;
                            case "datetime":
                                type3_ = ToolTypeSet.helper.tDateTime;
                                break;
                            case "smallint":
                                type3_ = ToolTypeSet.helper.tShort;
                                break;
                            case "int":
                                type3_ = ToolTypeSet.helper.tInt;
                                break;
                        }

                        {

                            string name = name2_;
                            int len = (int)len3_;
                            Type type = type3_;

                            listCols.Add(name);
                            listSize.Add(len);
                            listType.Add(type);
                        }
                    }

                    dic[tableNameShort] = new ImplTableDescriptor(tableNameShort, tableNameFull, listCols.ToArray(), listSize.ToArray(), listType.ToArray());

                }
            }





        }



        public ITableDescriptor getTable(string tableNameShort)
        {


            if (tableNameShort == string.Empty)
                return null;
            if (dic.ContainsKey(tableNameShort))
                return dic[tableNameShort];

            return null;
        }
        void addTableDesc(string pTabNameShort, ITableDescriptor pDesc)
        {
            dic.Add(pTabNameShort, pDesc);
        }




        public void Dispose()
        {
            if (dic != null)
            {
                foreach (object o_ in dic.Values)
                    ToolDispose.dispose(o_);
                dic.Clear();
            }
            dic = null;



        }

        public Type getColumnType(string pTableName, string pColName)
        {
            Type res_ = getColumnType(pTableName, pColName, null);
            if (res_ == null)
                throw new MyExceptionError(MessageCollection.T_MSG_ERROR_INVALID_VAR, new object[] { pTableName, pColName });
            return res_;
        }

        public Type getColumnType(string pTableName, string pColName, Type pDef)
        {
            if (ToolColumn.isColumnFullName(pColName))
            {
                pTableName = ToolColumn.extractTableName(pColName);
                pColName = ToolColumn.extractColumnName(pColName);
            }
            ITableDescriptor tds = getTable(pTableName);
            if (tds != null)
            {
                ColumnDescriptor cds = tds.getColumn(pColName);
                if (cds != null)
                    return cds.type;

            }
            return pDef;
        }


        public int getColumnSize(string pTableName, string pColName, int pDef)
        {
            if (ToolColumn.isColumnFullName(pColName))
            {
                pTableName = ToolColumn.extractTableName(pColName);
                pColName = ToolColumn.extractColumnName(pColName);
            }
            ITableDescriptor tds = getTable(pTableName);
            if (tds != null)
            {
                ColumnDescriptor cds = tds.getColumn(pColName);
                if (cds != null)
                    return cds.size;

            }
            return pDef;
        }



    }
}
