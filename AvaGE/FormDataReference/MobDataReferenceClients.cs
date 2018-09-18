using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.Common;
using AvaExt.PagedSource;
using AvaGE.FormDataReference.UserForm;
using Android.App;
using AvaExt.Manual.Table;

namespace AvaGE.FormDataReference
{
    public class MobDataReferenceClients : MobImplDataReferenceForGridForm
    {
        public MobDataReferenceClients(string pCmd)
            : base(pCmd,TableCLCARD.TABLE)
        {
           // source = new PagedSourceClient(null);


        }

        protected override Type getActivityType()
        {
            return typeof(MobDataReferenceClientsForm);
        }

   
    }
}
