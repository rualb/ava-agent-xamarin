using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using AvaExt.TableOperation;
using AvaExt.Common;
using AvaExt.Manual.Table;

using AvaExt.SQL.Dynamic.Preparing;

using System.Collections;
using AvaExt.MyException;
using AvaExt.Translating.Tools;
using Mono.Data.Sqlite;
using AvaExt.SQL.Dynamic.Const;
using System.Text.RegularExpressions;
using AvaExt.Formating;

namespace AvaExt.SQL.Dynamic
{
    public class ImplSqlBuilder : ISqlBuilder
    {
        static Regex _exp = new Regex("\\$[A-Z]+[0-9]*\\$", RegexOptions.Compiled);
        const string _space = " ";
        const string _newLine = "\r\n";
        const string _prefixWhere = "WHERE" + _newLine;
        const string _prefixOrder = "ORDER BY" + _newLine;
        const string SPESIAL_TAB_PREFIX = "__SPESIAL_TAB_PREFIX__";
        const string TABLE_FREE_PAR = SPESIAL_TAB_PREFIX + "TABLE_FREE_PAR";
        const string TABLE_WHERE = SPESIAL_TAB_PREFIX + "TABLE_WHERE";
        const string TABLE_ORDER = SPESIAL_TAB_PREFIX + "TABLE_ORDER";
        const string TABLE_META = SPESIAL_TAB_PREFIX + "TABLE_META";
        const string TABLE_PERP = SPESIAL_TAB_PREFIX + "TABLE_PREP";
        const string TABLE_COLS = SPESIAL_TAB_PREFIX + "TABLE_COLS";

        string _patternWHERE;
        string _patternORDER;
        string _patternCOLSLIST = "$COLSLIST$";



        List<string> _listPatternWhere = new List<string>();
        List<string> _listPatternOrder = new List<string>();

        DataTable tableFreeParameter;
        DataTable tableWhere;
        DataTable tableOrder;
        // DataTable tableMetaInfo;
        DataTable tablePreparers;
        DataTable tableResultCols;
        IEnvironment environment { get { return ToolMobile.getEnvironment(); } set { } }
        int parmIndx = 0;
        int groupLevel = 0;
        protected string sqlPattern;
        string defTable = string.Empty;
        int rowsLimmit = CurrentVersion.ENV.getDsLimit();

        int idIndx = 0;
        public ImplSqlBuilder(IEnvironment env, string pPattern, string pDefTable)
        {

            sqlPattern = pPattern;
            environment = env;
            defTable = pDefTable;

            setPatternWhere(ConstSqlPattern.WHERE);
            setPatternOrder(ConstSqlPattern.ORDER);

            initMetaData();
        }

        public void setPatternWhere(string newPatter)
        {
            _patternWHERE = ((newPatter == string.Empty) ? ConstSqlPattern.WHERE : newPatter);
            if (!_listPatternWhere.Contains(_patternWHERE))
                _listPatternWhere.Add(_patternWHERE);
        }

        public void setPatternOrder(string newPatter)
        {
            _patternORDER = ((newPatter == string.Empty) ? ConstSqlPattern.ORDER : newPatter);
            if (!_listPatternOrder.Contains(_patternORDER))
                _listPatternOrder.Add(_patternORDER);
        }

        string getPatternWhere()
        {
            return _patternWHERE;
        }
        string getPatternOrder()
        {
            return _patternORDER;
        }
        void initMetaData()
        {
            tableFreeParameter = new DataTable(TABLE_FREE_PAR);
            tableFreeParameter.Columns.Add(TableDUMMY.NAME, typeof(string));
            tableFreeParameter.Columns.Add(TableDUMMY.VALUE, typeof(object));


            tableWhere = new DataTable(TABLE_WHERE);
            tableWhere.Columns.Add(TableDUMMY.TYPE, typeof(tableWhereRecodrType));
            tableWhere.Columns.Add(TableDUMMY.PARENTNAME, typeof(string));
            tableWhere.Columns.Add(TableDUMMY.CHILDNAME, typeof(string));
            tableWhere.Columns.Add(TableDUMMY.VALUE, typeof(object));
            tableWhere.Columns.Add(TableDUMMY.RELATIONMATH, typeof(SqlTypeRelations));
            tableWhere.Columns.Add(TableDUMMY.RELATIONBOOL, typeof(SqlTypeRelations));
            tableWhere.Columns.Add(TableDUMMY.PATTERN, typeof(string));

            tableOrder = new DataTable(TABLE_ORDER);
            tableOrder.PrimaryKey = new DataColumn[]{
            tableOrder.Columns.Add(TableDUMMY.PARENTNAME, typeof(string)),
            tableOrder.Columns.Add(TableDUMMY.CHILDNAME, typeof(string)) };
            tableOrder.Columns.Add(TableDUMMY.SORT, typeof(SqlTypeRelations));
            tableOrder.Columns.Add(TableDUMMY.PATTERN, typeof(string));

            //tableMetaInfo = new DataTable(TABLE_META);
            //tableMetaInfo.Columns.Add(TableDUMMY.PARENTNAME, typeof(string));
            //tableMetaInfo.Columns.Add(TableDUMMY.CHILDNAME, typeof(string));
            //tableMetaInfo.Columns.Add(TableDUMMY.TYPE, typeof(Type));
            //tableMetaInfo.PrimaryKey = new DataColumn[] { tableMetaInfo.Columns[TableDUMMY.PARENTNAME], tableMetaInfo.Columns[TableDUMMY.CHILDNAME] };

            tablePreparers = new DataTable(TABLE_PERP);
            tablePreparers.Columns.Add(TableDUMMY.ID, typeof(string));
            tablePreparers.Columns.Add(TableDUMMY.ITEM, typeof(ISqlBuilderPreparer));
            tablePreparers.PrimaryKey = new DataColumn[] { tablePreparers.Columns[TableDUMMY.ID] };

            tableResultCols = new DataTable(TABLE_COLS);
            tableResultCols.Columns.Add(TableDUMMY.PARENTNAME, typeof(string));
            tableResultCols.Columns.Add(TableDUMMY.CHILDNAME, typeof(string));
            tableResultCols.PrimaryKey = new DataColumn[] { tableResultCols.Columns[TableDUMMY.PARENTNAME], tableResultCols.Columns[TableDUMMY.CHILDNAME] };



        }
        public void addColumnToMeta(string tab, string col, Type type)
        {
            //  tableMetaInfo.Rows.Add(new object[] { tab, col, type });
        }
        public void addColumnToMeta(string col, Type type)
        {
            //  tableMetaInfo.Rows.Add(new object[] { defTable, col, type });
        }
        void resetParamIndex()
        {
            parmIndx = 0;
        }
        int getNextParamIndex()
        {
            return ++parmIndx;
        }
        public virtual SqliteCommand get()
        {
            callPreparers();
            resetParamIndex();
            List<SqliteParameter> parList = new List<SqliteParameter>();
            StringBuilder sqlTextBuilder = new StringBuilder(sqlPattern);



            //Parameters
            for (int r = 0; r < tableFreeParameter.Rows.Count; ++r)
            {
                DataRow row = tableFreeParameter.Rows[r];
                string name = string.Empty;
                name = getFreeParameterName(row);
                if (name == string.Empty)
                    name = getParamToStr(getNextParamIndex());
                parList.Add(new SqliteParameter(name, getValue(row)));

            }
            //Where
            foreach (string curPaternWhere in _listPatternWhere)
            {
                string sqlTextWhere = String.Empty;
                for (int r = 0; r < tableWhere.Rows.Count; ++r)
                    if (getLinePatern(tableWhere.Rows[r]) == curPaternWhere)
                    {
                        DataRow row = tableWhere.Rows[r];
                        string lastText;

                        if (isLineCommon(row))
                        {
                            if (isNeedBool(row))
                                sqlTextWhere += getBoolRelToStr(row);
                            sqlTextWhere += (lastText = getParameterName(row));
                            if (lastText != string.Empty)
                            {
                                sqlTextWhere += getMathRelToStr(row);
                                sqlTextWhere += (lastText = getParamToStr(getNextParamIndex()));
                                //
                                parList.Add(new SqliteParameter(lastText, getValue(row)));
                                //
                            }
                        }
                        else
                            if (isLineGroupBegin(row))
                            {
                                if (isNeedBool(row))
                                    sqlTextWhere += getBoolRelToStr(row);
                                sqlTextWhere += getGroupStateToStr(row);
                                ++groupLevel;
                            }
                            else
                                if (isLineGroupEnd(row))
                                {
                                    --groupLevel;
                                    sqlTextWhere += getGroupStateToStr(row);
                                }

                    }
                sqlTextWhere = sqlTextWhere.Trim();
                if (sqlTextWhere != string.Empty)
                    sqlTextWhere = _prefixWhere + sqlTextWhere;
                sqlTextBuilder.Replace(curPaternWhere, sqlTextWhere);
            }
            //Order
            foreach (string curPaternOrder in _listPatternOrder)
            {
                string sqlTextOrder = String.Empty;
                for (int r = 0; r < tableOrder.Rows.Count; ++r)
                    if (getLinePatern(tableOrder.Rows[r]) == curPaternOrder)
                    {
                        DataRow row = tableOrder.Rows[r];
                        if (r > 0)
                            sqlTextOrder += ',' + _newLine;
                        sqlTextOrder += getParameterName(row);
                        sqlTextOrder += ' ' + getSortTypeToStr(row);
                    }
                sqlTextOrder = sqlTextOrder.Trim();
                if (sqlTextOrder != string.Empty)
                    sqlTextOrder = _prefixOrder + sqlTextOrder;
                sqlTextBuilder.Replace(curPaternOrder, sqlTextOrder);
            }
            //

            //string finSqlText = _exp.Replace(sqlTextBuilder.ToString(), string.Empty);

            sqlTextBuilder.Replace("$LIMIT$", "LIMIT " + XmlFormating.helper.format(rowsLimmit));

            return environment.getNewSqlCommand(sqlTextBuilder.ToString(), parList.ToArray());
        }

        void callPreparers()
        {

            foreach (DataRow row in tablePreparers.Rows)
                ((ISqlBuilderPreparer)row[TableDUMMY.ITEM]).set(this);
        }


        private bool isNeedBool(DataRow row)
        {
            tableWhereRecodrType typePar = (tableWhereRecodrType)row[TableDUMMY.TYPE];
            if ((typePar == tableWhereRecodrType.common) || (typePar == tableWhereRecodrType.groupBegin))
            {
                int indx = row.Table.Rows.IndexOf(row) - 1;
                if (indx >= 0)
                {
                    tableWhereRecodrType typeCur = (tableWhereRecodrType)row.Table.Rows[indx][TableDUMMY.TYPE];
                    if ((typeCur == tableWhereRecodrType.common) || (typeCur == tableWhereRecodrType.groupEnd))
                        return true;
                }
            }
            return false;
        }

        public void addParameterValueTable(string table, string col, object value, SqlTypeRelations relMath, SqlTypeRelations relBool)
        {
            try
            {
                if ((table != null) && (col != null) && (value != null))
                {
                    // Type typeWate = getColumnType(table, col);
                    if (true
                        //isColumnExists(table, col) &&
                        //(value.GetType() == DBNull.Value.GetType() ||
                        // (value = Convert.ChangeType(value, typeWate, null)) != null
                        //)
                       )
                    {
                        object[] arr = new object[] { tableWhereRecodrType.common, table, col, value, relMath, relBool, getPatternWhere() };

                        int indx = tableWhere.Rows.Count - 1;
                        for (; indx >= 0; --indx)
                        {
                            DataRow lastRow = tableWhere.Rows[indx];
                            if (isLineCommon(lastRow) && (getLinePatern(lastRow) == getPatternWhere()))
                            {
                                string targetCol = ToolColumn.getColumnFullName(table, col);
                                string curCol = ToolColumn.getColumnFullName((string)lastRow[TableDUMMY.PARENTNAME], (string)lastRow[TableDUMMY.CHILDNAME]);
                                if (string.Compare(targetCol, curCol) < 0)
                                    continue;
                            }

                            ++indx;
                            break;

                        }

                        ToolTable.insertRowAt(tableWhere, indx, arr);
                        return;
                    }
                }

            }
            catch (Exception exc)
            {
                throw exc;
            }
            throw new MyException.MyExceptionInner(MessageCollection.T_MSG_ERROR_INNER);
        }
        public void addParameterValueTable(string table, string col, object value, SqlTypeRelations relMath)
        {
            addParameterValueTable(table, col, value, relMath, SqlTypeRelations.boolAnd);
        }
        public void addParameterValueTable(string table, string col, object value)
        {
            addParameterValueTable(table, col, value, SqlTypeRelations.equal);
        }
        public void addParameterValue(string col, object value, SqlTypeRelations relMath, SqlTypeRelations relBool)
        {
            addParameterValueTable(defTable, col, value, relMath, relBool);
        }
        public void addParameterValue(string col, object value, SqlTypeRelations relMath)
        {
            addParameterValueTable(defTable, col, value, relMath, SqlTypeRelations.boolAnd);
        }
        public void addParameterValue(string col, object value)
        {
            addParameterValueTable(defTable, col, value, SqlTypeRelations.equal, SqlTypeRelations.boolAnd);
        }
        public void addFreeParameterValue(object value)
        {
            addFreeParameterValue(string.Empty, value);
        }
        public void addFreeParameterValue(string name, object value)
        {
            tableFreeParameter.Rows.Add(new object[] { name, value });
        }
        public void addSortColumn(string table, string col, SqlTypeRelations sort)
        {

            if (
                (table != null) &&
                (col != null) //&&
                //   isColumnExists(table, col)
                )
                tableOrder.Rows.Add(new object[] { table, col, sort, getPatternOrder() });
            else
                throw new MyException.MyExceptionInner(MessageCollection.T_MSG_ERROR_INNER);
        }
        public void addSortColumn(string col, SqlTypeRelations sort)
        {
            addSortColumn(defTable, col, sort);
        }
        public void addSortColumn(string col)
        {
            addSortColumn(defTable, col, SqlTypeRelations.sortAsc);
        }
        public void setCount(int count)
        {
            count = Math.Max(count, 0);
            rowsLimmit = count;
        }
        public void setCountMax()
        {
            setCount(CurrentVersion.ENV.getDsLimit());
        }
        public void setCountMin()
        {
            setCount(0);
        }
        public int getCount()
        {
            return rowsLimmit;
        }
        public void beginWhereGroup(SqlTypeRelations relBool)
        {
            tableWhere.Rows.Add(new object[] { tableWhereRecodrType.groupBegin, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, relBool, getPatternWhere() });
        }
        public void beginWhereGroup()
        {
            beginWhereGroup(SqlTypeRelations.boolAnd);
        }
        public void endWhereGroup()
        {
            tableWhere.Rows.Add(new object[] { tableWhereRecodrType.groupEnd, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, DBNull.Value, getPatternWhere() });
        }

        string getNewParamName(int indx)
        {
            if (indx > 0)
                return string.Format("@P{0}", indx);
            return string.Empty;
        }
        //bool isColumnExists(string tab, string col)
        //{

        //    return tableMetaInfo.Rows.Contains(new object[] { tab, col });
        //}
        //bool isColumnExists(string col)
        //{
        //    return isColumnExists(defTable, col);
        //}
        //Type getColumnType(DataRow row)
        //{
        //    return getColumnType((string)row[TableDUMMY.PARENTNAME], (string)row[TableDUMMY.CHILDNAME]);
        //}
        //public Type getColumnType(string tab, string col)
        //{
        //    if (isColumnExists(tab, col))
        //        return (Type)tableMetaInfo.Rows.Find(new object[] { tab, col })[TableDUMMY.TYPE];

        //    ToolMsg.show(null,ToolColumn.getColumnFullName(tab, col),null);

        //    throw new MyException.MyExceptionError(MessageCollection.T_MSG_ERROR_INVALID_VAR, new object[] { ToolColumn.getColumnFullName(tab, col) });


        //}
        //public Type getColumnType(string col)
        //{
        //    string _tab = ToolColumn.extractTableName(col);
        //    if (_tab == string.Empty)
        //        _tab = defTable;
        //    string _col = ToolColumn.extractColumnName(col);
        //    return getColumnType(_tab, _col);
        //}


        object getValue(DataRow row)
        {
            return row[TableDUMMY.VALUE];
        }
        String getBoolRelToStr(DataRow row)
        {
            string res = SqlTypeRelationsString.convert((SqlTypeRelations)ToolCell.isNull((SqlTypeRelations)row[TableDUMMY.RELATIONBOOL], SqlTypeRelations.undef));
            if (res != string.Empty)
                res = _space + res + _newLine;
            return res;
        }
        String getMathRelToStr(DataRow row)
        {
            return ' ' + SqlTypeRelationsString.convert((SqlTypeRelations)ToolCell.isNull((SqlTypeRelations)row[TableDUMMY.RELATIONMATH], SqlTypeRelations.undef)) + ' ';
        }
        String getFreeParameterName(DataRow row)
        {
            return (string)ToolCell.isNull(row[TableDUMMY.NAME], string.Empty);
        }
        String getParameterName(DataRow row)
        {
            string tab = (string)ToolCell.isNull(row[TableDUMMY.PARENTNAME], string.Empty);
            string col = (string)ToolCell.isNull(row[TableDUMMY.CHILDNAME], string.Empty);
            if ((tab != string.Empty) && (col != string.Empty))
                return tab + '.' + col;
            if ((tab == string.Empty) && (col != string.Empty))
                return col;
            return string.Empty;
        }
        String getParamToStr(int indx)
        {
            return getNewParamName(indx);
        }
        String getSortTypeToStr(DataRow row)
        {
            return SqlTypeRelationsString.convert((SqlTypeRelations)ToolCell.isNull((SqlTypeRelations)row[TableDUMMY.SORT], SqlTypeRelations.undef));
        }
        String getGroupStateToStr(DataRow row)
        {
            string res = string.Empty;
            switch ((int)row[TableDUMMY.TYPE])
            {
                case (int)tableWhereRecodrType.groupBegin:
                    res = SqlTypeRelationsString.groupBegin + _newLine;
                    break;
                case (int)tableWhereRecodrType.groupEnd:
                    res = _newLine + SqlTypeRelationsString.groupEnd;
                    break;
            }
            return res;
        }

        bool isLineGroupBegin(DataRow row)
        {
            tableWhereRecodrType type = (tableWhereRecodrType)row[TableDUMMY.TYPE];
            if (type == tableWhereRecodrType.groupBegin)
                return true;
            return false;
        }
        bool isLineGroupEnd(DataRow row)
        {
            tableWhereRecodrType type = (tableWhereRecodrType)row[TableDUMMY.TYPE];
            if (type == tableWhereRecodrType.groupEnd)
                return true;
            return false;
        }
        bool isLineCommon(DataRow row)
        {
            tableWhereRecodrType type = (tableWhereRecodrType)row[TableDUMMY.TYPE];
            if (type == tableWhereRecodrType.common)
                return true;
            return false;
        }

        string getLinePatern(DataRow row)
        {
            return row[TableDUMMY.PATTERN].ToString();
        }

        public string getName()
        {
            return defTable;
        }


        public void setDefaultTable(string name)
        {
            defTable = name;
        }
        public void reset()
        {

            resetSort();
            resetWhere();
        }

        public void resetSort()
        {
            tableOrder.Clear();

        }
        public void resetWhere()
        {
            tableFreeParameter.Clear();
            tableWhere.Clear();

        }
        public void resetResultCols()
        {
            tableResultCols.Clear();

        }
        public int addPereparer(ISqlBuilderPreparer pPrep)
        {
            int newId = ++idIndx;
            tablePreparers.Rows.Add(new object[] { newId, pPrep });
            return newId;
        }


        public void deletePreparer(int key)
        {
            DataRow row = tablePreparers.Rows.Find(key);
            if (row != null)
                tablePreparers.Rows.Remove(row);
        }

        public void deletePreparer(int[] keys)
        {
            for (int i = 0; i < keys.Length; ++i)
                deletePreparer(keys[i]);
        }


        DataRow[] searchInWhere(string table, string col)
        {
            DataRow[] arr = new DataRow[] { };
            try
            {
                arr = tableWhere.Select(string.Format("{0} = '{1}' AND {2} = '{3}'", TableDUMMY.PARENTNAME, table, TableDUMMY.CHILDNAME, col));
            }
            catch (Exception exc)
            {
                throw new MyException.MyExceptionError(MessageCollection.T_MSG_ERROR_INNER, exc);
            }
            return arr;
        }


        public object getParameterValue(string table, string col)
        {
            DataRow[] rows = searchInWhere(table, col);
            if (rows != null && rows.Length > 0)
                return rows[0][TableDUMMY.VALUE];
            return null;
        }

        public object getParameterValue(string col)
        {
            return getParameterValue(defTable, col);
        }

        public void deleteParameter(string table, string col)
        {
            DataRow[] rows = searchInWhere(table, col);
            for (int i = 0; i < rows.Length; ++i)
                tableWhere.Rows.Remove(rows[i]);
        }

        public void deleteParameter(string col)
        {
            deleteParameter(defTable, col);
        }




        //public SqliteCommand getDelete()
        //{
        //    callPreparers();
        //    resetParamIndex();
        //    List<SqliteParameter> parList = new List<SqliteParameter>();
        //    StringBuilder sqlTextBuilder = new StringBuilder(sqlPattern);



        //    //Parameters
        //    for (int r = 0; r < tableFreeParameter.Rows.Count; ++r)
        //    {
        //        DataRow row = tableFreeParameter.Rows[r];
        //        string name = string.Empty;
        //        name = getFreeParameterName(row);
        //        if (name == string.Empty)
        //            name = getParamToStr(getNextParamIndex());
        //        parList.Add(new SqliteParameter(name, getValue(row)));

        //    }
        //    //Where
        //    foreach (string curPaternWhere in _listPatternWhere)
        //    {
        //        string sqlTextWhere = String.Empty;
        //        for (int r = 0; r < tableWhere.Rows.Count; ++r)
        //            if (getLinePatern(tableWhere.Rows[r]) == curPaternWhere)
        //            {
        //                DataRow row = tableWhere.Rows[r];
        //                string lastText;

        //                if (isLineCommon(row))
        //                {
        //                    if (isNeedBool(row))
        //                        sqlTextWhere += getBoolRelToStr(row);
        //                    sqlTextWhere += (lastText = getParameterName(row));
        //                    if (lastText != string.Empty)
        //                    {
        //                        sqlTextWhere += getMathRelToStr(row);
        //                        sqlTextWhere += (lastText = getParamToStr(getNextParamIndex()));
        //                        //
        //                        parList.Add(new SqliteParameter(lastText, getValue(row)));
        //                        //
        //                    }
        //                }
        //                else
        //                    if (isLineGroupBegin(row))
        //                    {
        //                        if (isNeedBool(row))
        //                            sqlTextWhere += getBoolRelToStr(row);
        //                        sqlTextWhere += getGroupStateToStr(row);
        //                        ++groupLevel;
        //                    }
        //                    else
        //                        if (isLineGroupEnd(row))
        //                        {
        //                            --groupLevel;
        //                            sqlTextWhere += getGroupStateToStr(row);
        //                        }

        //            }
        //        sqlTextWhere = sqlTextWhere.Trim();
        //        if (sqlTextWhere != string.Empty)
        //            sqlTextWhere = _prefixWhere + sqlTextWhere;
        //        sqlTextBuilder.Replace(curPaternWhere, sqlTextWhere);
        //    }

        //    return environment.getNewSqlCommand(sqlTextBuilder.ToString(), parList.ToArray());
        //}

        //public SqliteCommand getInsert()
        //{
        //    callPreparers();
        //    resetParamIndex();
        //    List<SqliteParameter> parList = new List<SqliteParameter>();
        //    StringBuilder sqlTextBuilder = new StringBuilder(sqlPattern);



        //    //Parameters
        //    for (int r = 0; r < tableFreeParameter.Rows.Count; ++r)
        //    {
        //        DataRow row = tableFreeParameter.Rows[r];
        //        string name = string.Empty;
        //        name = getFreeParameterName(row);
        //        if (name == string.Empty)
        //            name = getParamToStr(getNextParamIndex());
        //        parList.Add(new SqliteParameter(name, getValue(row)));

        //    }
        //    {
        //        string sqlTextList = String.Empty;
        //        foreach (DataRow row in tableResultCols.Rows)
        //        {
        //            sqlTextList += getParameterName(row) + ',';
        //        }
        //        sqlTextList = sqlTextList.Trim(',');
        //        if (sqlTextList != string.Empty)
        //            sqlTextBuilder.Replace(_patternCOLSLIST, sqlTextList);
        //    }

        //    return environment.getNewSqlCommand(sqlTextBuilder.ToString(), parList.ToArray());
        //}

        //public SqliteCommand getUpdate()
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}

        public void addResultColumn(string col)
        {
            addResultColumn(defTable, col);
        }

        public void addResultColumn(string table, string col)
        {
            tableResultCols.Rows.Add(new object[] { table, col });
        }



        public void Dispose()
        {

        }
    }

    enum tableWhereRecodrType
    {
        undef = 0,
        common = 1,
        groupBegin = 2,
        groupEnd = 3
    }


}
