using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.PagedSource;
using AvaGE.FormDataReference.UserForm;

namespace AvaGE.FormDataReference
{
    public class MobDataReferenceMaterialBarcode : MobImplDataReferenceForValueSelect
    {
 


        public MobDataReferenceMaterialBarcode(string pCmd)
            : base(pCmd)
        {
            source = new PagedSourceMaterial(null);
        }

        protected override Type getActivityType()
        { 
            return typeof(MobDataReferenceValueSelectBarcodeMatForm);
        }
         
    }
}
