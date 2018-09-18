using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using AvaExt.AndroidEnv.ControlsBase;
using AvaExt.PagedSource;
using AvaExt.Common;
using AvaExt.ControlOperation;
using AvaGE.MobControl.ControlsTools;
using AvaExt.SQL.Dynamic.Preparing;
using AvaExt.Manual.Table;
using AvaExt.Common.Const;

namespace AvaGE.FormDataReference.UserForm
{
    public class MobDataReferenceMMForm : AvaGE.FormDataReference.UserForm.MobDataReferenceGridFormBase
    {
        protected override string globalStoreName()
        {
            return "form.list.mm";
        }

        public MobDataReferenceMMForm()
            : base(0)
        {


        }

        protected override void setSource(IPagedSource pSource)
        {
            base.setSource(pSource);
            pSource.getBuilder().addPereparer(new SqlBuilderPreparerFixedCondition(TableSTFICHE.GRPCODE, ConstDocGroupCode.materialManagement));
        }
    }
}

