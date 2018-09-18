using System;
using System.Collections.Generic;
using System.Text;

namespace AvaExt.SQL.Dynamic.Preparing
{
    public interface ISqlBuilderPreparer
    {
        void set(ISqlBuilder pBuilder);
    }
}
