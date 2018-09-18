using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.PagedSource;
using AvaGE.FormDataReference.UserForm;
using AvaExt.Manual.Table;

namespace AvaGE.FormDataReference
{
    public class MobDataReferenceCashCollections: MobImplDataReferenceForGridForm
    {
 
        public MobDataReferenceCashCollections(string pCmd)
            : base(pCmd, TableKSLINES.TABLE)
        {
          //  source = new PagedSourceCashTrans(null);


        }

        protected override Type getActivityType()
        {
            return typeof(MobDataReferenceCashCollectionForm);
        }
    }
}
