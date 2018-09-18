using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.Common;
using AvaExt.Adapter.ForDataTable;
using AvaExt.Common.Const;
using AvaExt.Database.Tools;

namespace AvaExt.Adapter.ForDataSet
{
    public class AdapterDataSetDocument : AdapterDataSetRecords
    {


        public AdapterDataSetDocument(IEnvironment pEnv, IAdapterTable[] pAdapterCol)
            : base(pEnv, pAdapterCol)
        { }

        protected virtual string getDocNr()
        {
            string nr = string.Empty;
            object id = ToolSeq.get(environment);
            if (id != null)
                nr = ToolString.shrincDigit(id.ToString());
            return nr;
        }
    }
}
