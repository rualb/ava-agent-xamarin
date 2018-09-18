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

namespace AvaGE.FormUserEditor.Material.Operations.Slip
{
    [Android.App.Activity(Label = Form.FORM_NAME, Icon = Form.FORM_ICON, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, WindowSoftInputMode = Android.Views.SoftInput.StateAlwaysHidden)]
    public partial class MobUserEditorFormDeficitSlip : MobUserEditorFormMaterialDoc
    {
        protected override string globalStoreName()
        {
            return ConstAdapterNames.adp_mm_doc_deficit;// "form.mm.doc.deficit";
        }


        public MobUserEditorFormDeficitSlip()
            : base(null, 0)
        {



        }

        public override void reinitEditingForData()
        {
            base.reinitEditingForData();
 
            if (cPanelFin != null)
                cPanelFin.Visible = false;
        }
        protected override bool controlParameter(StockDocParameters pPar)
        {
            if (pPar == StockDocParameters.client || pPar == StockDocParameters.total)
                return false;
            return base.controlParameter(pPar);
        }
        protected override void refreshFin(bool setDefault)
        {

        }
    }
}

