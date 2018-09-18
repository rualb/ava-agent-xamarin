using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.ObjectSource;

namespace AvaExt.SQL.Dynamic.Preparing
{
    public class SqlBuilderPreparerEndGroup : ISqlBuilderPreparer
    {

 

        public void set(ISqlBuilder pBuilder)
        {
            pBuilder.endWhereGroup();
        }

      
    }

}
