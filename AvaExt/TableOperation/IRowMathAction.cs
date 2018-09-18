using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AvaExt.TableOperation
{
    public interface IRowMathAction
    {
        void done(DataRow row, string col, object val1, object val2, object coif);
    }
}
