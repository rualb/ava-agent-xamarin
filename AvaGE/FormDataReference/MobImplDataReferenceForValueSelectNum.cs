using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.PagedSource;
using AvaGE.FormDataReference.UserForm;

namespace AvaGE.FormDataReference
{
    public class MobImplDataReferenceForValueSelectNum : MobImplDataReferenceForValueSelect
    {
 

        public MobImplDataReferenceForValueSelectNum(string pCmd)
            : base(pCmd)
        {
 
        }

        protected override Type getActivityType()
        {
            return typeof(MobDataReferenceValueSelectNumForm);
        }
    }
}
