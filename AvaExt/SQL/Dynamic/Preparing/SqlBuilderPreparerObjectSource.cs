using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.ObjectSource;

namespace AvaExt.SQL.Dynamic.Preparing
{
    public class SqlBuilderPreparerObjectSource : ISqlBuilderPreparer
    {
        string col;
        IObjectSource  value;
        SqlTypeRelations relMath;
        SqlTypeRelations relBool;
        public SqlBuilderPreparerObjectSource(string pCol, IObjectSource  pValue)
        {
            col = pCol;
            value = pValue;
            relMath = SqlTypeRelations.equal;
            relBool = SqlTypeRelations.boolAnd;
        }
        public SqlBuilderPreparerObjectSource(string pCol, IObjectSource  pValue, SqlTypeRelations pRelMath)
        {
            col = pCol;
            value = pValue;
            relMath = pRelMath;
            relBool = SqlTypeRelations.boolAnd;
        }
        public SqlBuilderPreparerObjectSource(string pCol, IObjectSource  pValue, SqlTypeRelations pRelMath, SqlTypeRelations pRelBool)
        {
            col = pCol;
            value = pValue;
            relMath = pRelMath;
            relBool = pRelBool;
        }


        public void set(ISqlBuilder pBuilder)
        {
            pBuilder.addParameterValue(col, value.get(), relMath, relBool);
        }


    }

}
