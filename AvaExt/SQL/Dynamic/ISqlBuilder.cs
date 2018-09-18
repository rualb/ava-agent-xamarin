using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using AvaExt.Common;
using AvaExt.SQL.Dynamic.Preparing;
using Mono.Data.Sqlite;

namespace AvaExt.SQL.Dynamic
{
    public interface ISqlBuilder:IDisposable
    {
        //Type getColumnType(string col);
        //Type getColumnType(string tab, string col);

        SqliteCommand get();  //select $COLSLIST$ from $MAINTABLE$ $ALIAS$ where $ALIAS$.COL1 
        //SqliteCommand getDelete();
        //SqliteCommand getInsert();
        //SqliteCommand getUpdate();

        string getName();
        void setDefaultTable(string name);
        void addParameterValueTable(string table, string col, object value, SqlTypeRelations relMath, SqlTypeRelations relBool);
        void addParameterValueTable(string table, string col, object value, SqlTypeRelations relMath);
        void addParameterValueTable(string table, string col, object value);
        void addParameterValue(string col, object value, SqlTypeRelations relMath, SqlTypeRelations relBool);
        void addParameterValue(string col, object value, SqlTypeRelations relMath);
        void addParameterValue(string col, object value);

        void addFreeParameterValue(object value);
        void addFreeParameterValue(string name, object value);

        void addResultColumn(string col);
        void addResultColumn(string table, string col); 


        object getParameterValue(string table, string col);
        object getParameterValue(string col);
        void deleteParameter(string table, string col);
        void deleteParameter(string col);
        void addSortColumn(string table, string col, SqlTypeRelations sort);
        void addSortColumn(string col, SqlTypeRelations sort);
        void setCount(int count);
        void setCountMax();
        void setCountMin();
        int getCount();
        void beginWhereGroup(SqlTypeRelations relBool);
        void beginWhereGroup();
        void endWhereGroup();
        void reset();
        void resetSort();
        void resetWhere();
        void resetResultCols(); 
        int addPereparer(ISqlBuilderPreparer pPrep);
        void deletePreparer(int key);
        void deletePreparer(int[] keys);
        void addColumnToMeta(string tab, string col, Type type);
        void addColumnToMeta(string col, Type type);

        void setPatternWhere(string newPattern);
        void setPatternOrder(string newPattern);
    }
}
