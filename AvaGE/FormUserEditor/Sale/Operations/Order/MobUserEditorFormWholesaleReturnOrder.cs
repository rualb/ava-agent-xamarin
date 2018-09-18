using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using AvaExt.AndroidEnv.ControlsBase;
using AvaExt.Common;
using AvaExt.Adapter.ForUser;
using AvaGE.FormUserEditor.Const;

namespace AvaGE.FormUserEditor.Sale.Operations.Order
{
    [Android.App.Activity(Label = Form.FORM_NAME, Icon = Form.FORM_ICON, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, WindowSoftInputMode = Android.Views.SoftInput.StateAlwaysHidden)]
    public partial class MobUserEditorFormWholesaleReturnOrder : MobUserEditorFormMaterialDocOrder
    {
        protected override string globalStoreName()
        {
            return ConstAdapterNames.adp_sls_doc_order_wholesaleret;// "form.sale.doc.wholesale.return.order";
        }
        public MobUserEditorFormWholesaleReturnOrder() 
            :base(  null,     0)
        {
            
        }
    }
}

