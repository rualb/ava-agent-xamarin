using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AvaExt.TableOperation.RowIniter
{
    public interface IRowIniter
    {
        void set(DataRow pRow);
    }
}
