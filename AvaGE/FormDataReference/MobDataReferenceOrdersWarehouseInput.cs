using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.PagedSource;
using AvaGE.FormDataReference.UserForm;
using AvaExt.Manual.Table;

namespace AvaGE.FormDataReference
{
    public class MobDataReferenceOrdersWarehouseInput: MobImplDataReferenceForGridForm
    {
        public MobDataReferenceOrdersWarehouseInput(string pCmd)
            : base(pCmd, TableORFICHE.TABLE)
        {
           // source = new PagedSourceOrder(null);


        }

        protected override Type getActivityType()
        {
            return typeof(MobDataReferenceOrdersWarehouseInputForm);
        }
 
    }
}
