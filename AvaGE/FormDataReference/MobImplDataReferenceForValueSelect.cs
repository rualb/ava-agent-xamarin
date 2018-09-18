using System;
using System.Collections.Generic;
using System.Text;

using AvaExt.AndroidEnv.ControlsBase;
using AvaGE.FormDataReference.UserForm;
using AvaExt.DataRefernce;
using AvaExt.Common;
using AvaExt.Common.Const;
using AvaExt.Manual.Table;
using AvaExt.Formating;

namespace AvaGE.FormDataReference
{
    public class MobImplDataReferenceForValueSelect : ImplDataReference
    {
       // public MobDataReferenceValueSelectForm form;


        public MobImplDataReferenceForValueSelect(string pCmd)
            : base(pCmd,null)
        {
            //getFlagStore().flagEnable(ReferenceFlags.dialog);
        }
        protected override void show(string pColumn, object pValue)
        {
            //  form.showMode = getFlagStore().isFlagEnabled(ReferenceFlags.showMode);

            // if (getFlagStore().isFlagEnabled(ReferenceFlags.dialog))
            //     form.ShowDialog();

            List<string> k = new List<string>();
            List<string> v = new List<string>();

            k.Add(ConstCmdLine.cmd);
            v.Add(cmd);

            if (pColumn != null && pValue != null)
            {
                k.Add(TableDUMMY.COLUMN);
                v.Add(pColumn);
                //
                k.Add(TableDUMMY.VALUE);
                v.Add(XmlFormating.helper.format(pValue));
            }

            ToolMobile.startForm(getActivityType(), k.ToArray(), v.ToArray());
        }
        

        public override void refresh()
        {
            
        }
        public override void refresh(object id)
        {
           
        }

        protected virtual Type getActivityType()
        {
            return null;
        }
    }
}
