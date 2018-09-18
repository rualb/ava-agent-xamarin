using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.Adapter.ForDataTable;
using AvaExt.SQL.Dynamic.Preparing;
using AvaExt.Manual.Table;
using AvaExt.PagedSource;

namespace AvaExt.Adapter.ForDataSet.Sale.Reference
{
    public class AdapterDataSetCampaignCard : AdapterDataSetRecords
    {
        public AdapterDataSetCampaignCard(IEnvironment pEnv)
            : base(pEnv, new IAdapterTable[]
                {   
                     new  AdapterTableCampCard ( pEnv, TableCAMPAIGN.LOGICALREF ),
                    new AdapterTableCampCardLine ( pEnv, TableCMPGNLINE.CAMPCARDREF ) 
                }
            )

        { }

    }
}
