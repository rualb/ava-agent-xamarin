using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.ObjectSource;

namespace AvaExt.SQL.Dynamic.Preparing
{
    public class SqlBuilderPreparerFixedConditionTable : SqlBuilderPreparerObjectSourceTable
    {
        public SqlBuilderPreparerFixedConditionTable(string pTab, string pCol, object pValue)
            : base(pTab, pCol, new ImplObjectSourceStaticValue(pValue))
        {

        }
        public SqlBuilderPreparerFixedConditionTable(string pTab, string pCol, object pValue, SqlTypeRelations pRelMath)
            : base(pTab, pCol, new ImplObjectSourceStaticValue(pValue), pRelMath)
        {

        }
        public SqlBuilderPreparerFixedConditionTable(string pTab, string pCol, object pValue, SqlTypeRelations pRelMath, SqlTypeRelations pRelBool)
            : base(pTab, pCol, new ImplObjectSourceStaticValue(pValue), pRelMath, pRelBool)
        {

        }


    }

}
