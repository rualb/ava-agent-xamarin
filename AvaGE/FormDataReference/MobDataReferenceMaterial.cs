using System;
using System.Collections.Generic;
using System.Text;
 
 
using AvaExt.PagedSource;
using AvaGE.FormDataReference;
using AvaExt.Common;
using AvaGE.FormDataReference.UserForm;
using AvaExt.Manual.Table;

namespace AvaGE.FormDataReference
{
    public class MobDataReferenceMaterial: MobImplDataReferenceForGridForm
    {
        public MobDataReferenceMaterial(string pCmd)
            : base(pCmd,TableITEMS.TABLE)
        {
          //  source = new PagedSourceMaterial(null);
        }

        protected override Type getActivityType()
        {
            return typeof(MobDataReferenceMaterialsForm);
        }

   
    }
}
