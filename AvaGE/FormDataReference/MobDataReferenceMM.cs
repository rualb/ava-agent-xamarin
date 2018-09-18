using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.PagedSource;
using AvaGE.FormDataReference.UserForm;

namespace AvaGE.FormDataReference
{
    public class MobDataReferenceMM: MobImplDataReferenceForGridForm
    {
        public MobDataReferenceMM(string pCmd)
            : base(pCmd)
        {
            source = new PagedSourceSlip(null);


        }

        protected override Type getActivityType()
        {
            return typeof(MobDataReferenceMMForm);
        }
   
    }
}
