using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.PagedSource;
using AvaGE.FormDataReference.UserForm;
using AvaExt.Manual.Table;

namespace AvaGE.FormDataReference
{
    public class MobDataReferenceSlipsReturns: MobImplDataReferenceForGridForm
    {
 

        public MobDataReferenceSlipsReturns(string pCmd)
            : base(pCmd, TableINVOICE.TABLE)
        {
            //source = new PagedSourceSlip(null);


        }

        protected override Type getActivityType()
        {
            return typeof(MobDataReferenceReturnsForm);
        }
    }
}
