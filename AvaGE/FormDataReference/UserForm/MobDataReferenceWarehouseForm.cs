using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using AvaExt.AndroidEnv.ControlsBase;
using AvaExt.Common;
using AvaExt.PagedSource;
using AvaExt.SQL.Dynamic.Preparing;
using AvaGE.MobControl.ControlsTools;

namespace AvaGE.FormDataReference.UserForm
{
    public partial class MobDataReferenceWarehouseForm : MobDataReferenceForMoveForm
    {
        protected override string globalStoreName()
        {
            return "form.list.wh";
        }

         public MobDataReferenceOrdersSaleForm()
            : base(0)
        {


        }
        protected override void setSource(IPagedSource pSource)
        {
            base.setSource(pSource);
            extender = new ImplPagedGridExtender(DATAGRIDMAIN, source);
        }
    }
}

