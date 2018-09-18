using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.SQL.Dynamic;

namespace AvaExt.Filter
{
    public interface IFilter
    {
        string getCode();
        string getDescription();
        void begin();
        void applyToBuilder(ISqlBuilder pBuilder);
    }
}
