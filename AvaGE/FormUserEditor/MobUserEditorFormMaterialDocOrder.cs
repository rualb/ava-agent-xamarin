using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using AvaExt.AndroidEnv.ControlsBase;
using AvaExt.Common;
using AvaExt.Adapter.ForUser;

namespace AvaGE.FormUserEditor
{
    public partial class MobUserEditorFormMaterialDocOrder : MobUserEditorFormMaterialDoc
    {
        public MobUserEditorFormMaterialDocOrder(IEnvironment pEnv,int pLayout) 
            :base(  null,     0)
        {
             
        }

        protected override bool controlParameter(StockDocParameters pPar)
        {
            if (pPar == StockDocParameters.stockLevel)
                return false;
            return base.controlParameter(pPar);
        }
        protected override string getPrefix()
        {
            return "ORDER";
        }
    }
}

