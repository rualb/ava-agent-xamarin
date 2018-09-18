using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaGE.FormDataReference.UserForm;

namespace AvaGE.FormDataReference
{
    public class MobImplDataReferenceForValueSelectNumPercent : MobImplDataReferenceForValueSelectNum
    {
 

        public MobImplDataReferenceForValueSelectNumPercent(string pCmd)
            : base(pCmd)
        {
 
        }



        protected override Type getActivityType()
        {
            return typeof(MobDataReferenceValueSelectNumPercForm);
        }
    }
}
