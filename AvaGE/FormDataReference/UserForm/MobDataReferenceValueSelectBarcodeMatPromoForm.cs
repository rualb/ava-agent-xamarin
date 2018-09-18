using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using AvaExt.AndroidEnv.ControlsBase;
using AvaExt.Common;
using AvaExt.Manual.Table;
using AvaExt.TableOperation;
using AvaExt.SQL.Dynamic;
using AvaExt.Translating.Tools;
using AvaExt.PagedSource;
using AvaExt.Common.Const;
using AvaGE.FormUserEditor;
using Android.App;

namespace AvaGE.FormDataReference.UserForm
{
    [Activity(Label = Form.FORM_NAME, Icon = Form.FORM_ICON, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, WindowSoftInputMode = Android.Views.SoftInput.StateVisible)]
    public partial class MobDataReferenceValueSelectBarcodeMatPromoForm:MobDataReferenceValueSelectBarcodeMatForm  
    {
   
        protected override string globalStoreName()
        {
            return ConstRefCode.promoMaterialBarcode;
        }

        public MobDataReferenceValueSelectBarcodeMatPromoForm()
            : base()
        {


         
        }
        
    }
}

