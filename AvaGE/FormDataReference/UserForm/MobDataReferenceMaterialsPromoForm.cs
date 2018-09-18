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
 
using AvaExt.ControlOperation;
using AvaExt.ObjectSource;
 
using AvaExt.SQL.Dynamic;
using AvaExt.Manual.Table;
using AvaExt.TableOperation;
using AvaExt.Adapter.Tools;
using AvaExt.Settings;
using Android.App;
using AvaGE.FormDataReference.UserForm;
using Android.Content.PM;
using AvaExt.Common.Const;

namespace AvaGE.FormDataReference.UserForm
{
       [Activity(Label = Form.FORM_NAME, Icon = Form.FORM_ICON, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, WindowSoftInputMode = Android.Views.SoftInput.StateAlwaysHidden)]
    public class MobDataReferenceMaterialsPromoForm:MobDataReferenceMaterialsForm  
    {
        protected override string globalStoreName()
        {
             
           return ConstRefCode.promoMaterial ;// "ref.mm.mat";
        }

        public MobDataReferenceMaterialsPromoForm()
            : base()
        {


           
             
        }

       



     

    }
}

