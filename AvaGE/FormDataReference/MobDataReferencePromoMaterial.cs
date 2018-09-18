using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.SQL.Dynamic.Preparing;
using AvaExt.Manual.Table;
using AvaExt.Common.Const;
using AvaGE.FormDataReference.UserForm;

namespace AvaGE.FormDataReference
{
    public class MobDataReferencePromoMaterial : MobDataReferenceMaterial
    {
        public MobDataReferencePromoMaterial(string pCmd)
            : base(pCmd)
        {
            source.getBuilder().addPereparer(new SqlBuilderPreparerFixedCondition(TableITEMS.PROMO, (short)ConstBool.yes));
        }

        protected override Type getActivityType()
        {
            return typeof(MobDataReferenceMaterialsPromoForm);
        }
    }
}
