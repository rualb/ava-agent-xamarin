using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace AvaExt.TableOperation.TableConverter
{
    public interface ITableConverter
    {
        DataTable convert(DataTable pTable);
    }
}
