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

namespace AvaGE.FormUserEditor.Sale.Operations.Slip
{
    [Android.App.Activity(Label = Form.FORM_NAME, Icon = Form.FORM_ICON, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, WindowSoftInputMode = Android.Views.SoftInput.StateAlwaysHidden)]
    public partial class MobUserEditorFormWholesaleReturnSlip : MobUserEditorFormMaterialDoc
    {
        protected override string globalStoreName()
        {
            return ConstAdapterNames.adp_sls_doc_wholesaleret;// "form.sale.doc.wholesale.return";
        }
        public MobUserEditorFormWholesaleReturnSlip() 
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

