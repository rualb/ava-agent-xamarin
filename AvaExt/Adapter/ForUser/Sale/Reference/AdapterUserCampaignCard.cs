using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.Adapter.ForDataSet;
using AvaExt.Manual.Table;

namespace AvaExt.Adapter.ForUser.Sale.Reference
{
    public class AdapterUserCampaignCard : AdapterUserHeaderedRecords
    {
        public AdapterUserCampaignCard(IEnvironment pEnv, IAdapterDataSet pAdapter)
            : base(pEnv, pAdapter, TableCAMPAIGN.TABLE, TableCMPGNLINE.TABLE)
        {

        }
    }
}
