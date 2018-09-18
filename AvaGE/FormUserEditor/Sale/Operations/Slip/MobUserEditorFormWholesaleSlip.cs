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

namespace AvaGE.FormUserEditor.Sale.Operations.Slip
{
     [Android.App.Activity(Label = Form.FORM_NAME, Icon = Form.FORM_ICON, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, WindowSoftInputMode = Android.Views.SoftInput.StateAlwaysHidden)]
    public partial class MobUserEditorFormWholesaleSlip : MobUserEditorFormMaterialDoc
    {
        protected override string globalStoreName()
        {
            return ConstAdapterNames.adp_sls_doc_wholesale;// "form.sale.doc.wholesale";
        }
        public MobUserEditorFormWholesaleSlip() 
            :base(  null,     0)
        {
             
        }
    }
}

