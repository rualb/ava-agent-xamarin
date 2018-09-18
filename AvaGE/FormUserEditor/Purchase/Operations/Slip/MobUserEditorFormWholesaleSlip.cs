using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using AvaExt.AndroidEnv.ControlsBase;
using AvaExt.Adapter.ForUser;
using AvaExt.Common;
using AvaGE.FormUserEditor.Const;

namespace AvaGE.FormUserEditor.Purchase.Operations.Slip
{
     [Android.App.Activity(Label = Form.FORM_NAME, Icon = Form.FORM_ICON, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, WindowSoftInputMode = Android.Views.SoftInput.StateAlwaysHidden)]
    public partial class MobUserEditorFormPurchaseSlip : MobUserEditorFormMaterialDoc
    {
        protected override string globalStoreName()
        {
            return ConstAdapterNames.adp_prch_doc_purchase;// "form.sale.doc.wholesale";
        }
        public MobUserEditorFormPurchaseSlip() 
            :base(  null,     0)
        {
             
        }

        protected override bool controlParameter(StockDocParameters pPar)
        {
            if (pPar == StockDocParameters.stockLevel)
                return false;
            return base.controlParameter(pPar);
        }
    }
}

