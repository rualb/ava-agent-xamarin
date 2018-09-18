using System;
using System.Collections.Generic;
using System.Text;

using AvaExt.AndroidEnv.ControlsBase;
using AvaGE.FormDataReference.UserForm;
using AvaExt.DataRefernce;
using AvaExt.Common;
using Android.Content;
using AvaExt.Common.Const;
using AvaExt.Formating;
using AvaExt.Manual.Table;

namespace AvaGE.FormDataReference
{
    public class MobImplDataReferenceForGridForm : ImplDataReference
    {



        public MobDataReferenceGridFormBase form;

        public MobImplDataReferenceForGridForm(string pCmd,string pName)
            : base(pCmd, pName)
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
        //public override void setFilter(string pColumn, object pValue)
        //{
        //    form.extender.requiredSortedData(pColumn, true);
        //    form.extender.searchData(pValue);
        //}

  

        public override void refresh()
        {
            if (form != null)
                form.refresh();
        }
        public override void refresh(object id)
        {
            if (form != null)
                form.refresh(id);
        }

        protected virtual Type getActivityType()
        {
            return null;
        }



    }
}
