using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.ObjectSource;

namespace AvaExt.SQL.Dynamic.Preparing
{
    public class SqlBuilderPreparerFixedCondition : SqlBuilderPreparerObjectSource
    {

        public SqlBuilderPreparerFixedCondition(string pCol, object pValue)
            : base(pCol, new ImplObjectSourceStaticValue(pValue))
        {

        }
        public SqlBuilderPreparerFixedCondition(string pCol, object pValue, SqlTypeRelations pRelMath)
            : base(pCol, new ImplObjectSourceStaticValue(pValue), pRelMath)
        {

        }
        public SqlBuilderPreparerFixedCondition(string pCol, object pValue, SqlTypeRelations pRelMath, SqlTypeRelations pRelBool)
            : base(pCol, new ImplObjectSourceStaticValue(pValue), pRelMath, pRelBool)
        {

        }





    }

}
