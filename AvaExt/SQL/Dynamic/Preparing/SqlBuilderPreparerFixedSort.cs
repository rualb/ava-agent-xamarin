using System;
using System.Collections.Generic;
using System.Text;

namespace AvaExt.SQL.Dynamic.Preparing
{
    public class SqlBuilderPreparerFixedSort : ISqlBuilderPreparer
    {
        List<object[]> list = new List<object[]>();
        public SqlBuilderPreparerFixedSort(string col, SqlTypeRelations sort)
        {
            list.Add(new object[] { col, sort });
        }
        public void set(ISqlBuilder pBuilder)
        {
            for (int i = 0; i < list.Count; ++i)
                pBuilder.addSortColumn((string)list[i][0], (SqlTypeRelations)list[i][1]);
        }


    }

}
