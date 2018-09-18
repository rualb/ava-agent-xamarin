using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using AvaExt.AndroidEnv.ControlsBase;
using AvaExt.Adapter.ForUser.Finance.Operation.Cash;
using AvaExt.Common;
using AvaExt.ControlOperation;
using AvaGE.FormUserEditor.Const;
using Android.App;
using Android.Content.PM;

namespace AvaGE.FormUserEditor.Finance.Operations.Cash
{
        [Activity(Label = Form.FORM_NAME, Icon = Form.FORM_ICON, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, WindowSoftInputMode = Android.Views.SoftInput.StateAlwaysHidden)]
    public partial class MobUserEditorFormCashCollection : MobUserEditorFormCashIO
    {
        protected override string globalStoreName()
        {
            return ConstAdapterNames.adp_fin_cash_client_input;
        }

        public MobUserEditorFormCashCollection()
            : base(null)
        {

        }
    }
}

