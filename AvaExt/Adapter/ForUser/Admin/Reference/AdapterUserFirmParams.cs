using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.Adapter.ForDataSet;
using AvaExt.Manual.Table;

namespace AvaExt.Adapter.ForUser.Admin.Reference 
{
    public class AdapterUserFirmParams : AdapterUserRecords
    {
        public AdapterUserFirmParams(IEnvironment pEnv, IAdapterDataSet pAdapter)
            : base(pEnv, pAdapter, TableFIRMPARAMS.TABLE)
        {
             
        }
    }
}
