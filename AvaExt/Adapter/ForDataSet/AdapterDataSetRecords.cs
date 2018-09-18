using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AvaExt.Common;
using AvaExt.Adapter.ForDataTable;

namespace AvaExt.Adapter.ForDataSet
{
    public class AdapterDataSetRecords : ImplAdapterDataSet
    {
        protected object docId;
        protected string docNo;
        protected short docTrCode;
        protected short docNumModule;
        protected short docNumDocType;

        protected const string _constAdpNamePreix = "ADP_";
        protected override object getReturnResult()
        {
            return docId;
        }
        public AdapterDataSetRecords(IEnvironment pEnv, IAdapterTable[] pAdapterCol)
            : base(pEnv, pAdapterCol)
        { }
    }
}
