using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using AvaExt.AndroidEnv.ControlsBase;
using AvaExt.DataRefernce;
using AvaExt.PagedSource;
using AvaExt.Common;
using AvaGE.MobControl.ControlsTools;
using AvaGE.MobControl.ControlsTools.UserMessanger;
using AvaExt.Settings;
using AvaExt.Translating.Tools;
using AvaGE.Common;

using AvaExt.SQL;
using AvaExt.SQL.Dynamic;
using AvaExt.Manual.Table;
using System.IO;
using AvaExt.ControlOperation;
using AvaExt.Common.Const;
using AvaGE.MobControl;
using Android.App;

namespace AvaGE.FormDataReference.UserForm
{
    [Activity(Label = Form.FORM_NAME, Icon = Form.FORM_ICON, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, WindowSoftInputMode = Android.Views.SoftInput.StateAlwaysHidden)]
    public partial class MobDataReferenceValueSelectNumPercForm: MobDataReferenceValueSelectNumForm  
    {

        public MobDataReferenceValueSelectNumPercForm()
            :base()
        {
            

        }

        protected override string globalStoreName()
        {
            return ConstRefCode.numberPercent;
        }

        protected override void initAfterSettings()
        {
            base.initAfterSettings();


            Maximum = 100;
            Minimum = 0;
        }
       
 
  
      

    }
}

