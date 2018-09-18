using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.PagedSource;
using AvaGE.FormDataReference.UserForm;

namespace AvaGE.FormDataReference
{
    public class MobDataReferenceWarehouse: MobImplDataReferenceForGridForm
    {
 

        public MobDataReferenceWarehouse(string pCmd)
            : base(pCmd)
        {
            source = new PagedSourceWarehouse(null);


        }

        protected override Type getActivityType()
        {
            return typeof(MobDataReferenceWarehouseForm);
        }
    }
}
