using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.ObjectSource;

namespace AvaExt.SQL.Dynamic.Preparing
{
    public class SqlBuilderPreparerObjectSourceTable : ISqlBuilderPreparer
    {
        string tab;
        string col;
        IObjectSource  value;
        SqlTypeRelations relMath;
        SqlTypeRelations relBool;
        public SqlBuilderPreparerObjectSourceTable(string pTab, string pCol,IObjectSource  pValue) 
        {
            tab = pTab;
            col = pCol;
            value = pValue;
            relMath = SqlTypeRelations.equal;
            relBool = SqlTypeRelations.boolAnd;
        }
        public SqlBuilderPreparerObjectSourceTable(string pTab, string pCol, IObjectSource  pValue, SqlTypeRelations pRelMath)
        {
            tab = pTab;
            col = pCol;
            value = pValue;
            relMath = pRelMath;
            relBool = SqlTypeRelations.boolAnd;
        }
        public SqlBuilderPreparerObjectSourceTable(string pTab, string pCol, IObjectSource  pValue, SqlTypeRelations pRelMath, SqlTypeRelations pRelBool)
        {
            tab = pTab;
            col = pCol;
            value = pValue;
            relMath = pRelMath;
            relBool = pRelBool;
        }
        public void set(ISqlBuilder pBuilder)
        {
            pBuilder.addParameterValueTable(tab, col, value.get(), relMath, relBool);
        }


    }

}
