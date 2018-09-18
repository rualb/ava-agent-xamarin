using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using AvaExt.AndroidEnv.ControlsBase;
using AvaExt.PagedSource;
using AvaExt.Common;
using AvaExt.ControlOperation;
using AvaGE.MobControl.ControlsTools;
using AvaExt.SQL.Dynamic.Preparing;
using AvaExt.Manual.Table;
using AvaExt.Common.Const;
using AvaExt.Translating.Tools;
using AvaExt.Adapter.ForUser.Finance.Operation.Cash;
using AvaExt.Adapter.ForDataSet;
using AvaExt.Adapter.ForDataSet.Finance.Operation.Cash;
using AvaGE.FormUserEditor;
using AvaExt.ObjectSource;
using AvaExt.TableOperation;
using AvaGE.FormUserEditor.Const;
using Android.App;

namespace AvaGE.FormDataReference.UserForm
{
    [Activity(Label = Form.FORM_NAME, Icon = Form.FORM_ICON, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, WindowSoftInputMode = Android.Views.SoftInput.StateAlwaysHidden)]
    public class MobDataReferenceCashPaymentForm : MobDataReferenceForCashForm
    {
        protected override string globalStoreName()
        {
            return ConstRefCode.docCashPayment;// "ref.fin.cash.payment";
        }


        public MobDataReferenceCashPaymentForm()
            : base(0)
        {
 
           
        }

        protected override void setSource(IPagedSource pSource)
        {
            base.setSource(pSource);
            pSource.getBuilder().addPereparer(new SqlBuilderPreparerFixedCondition(TableKSLINES.TRCODE, ConstCashDocType.clientFromCash));
 
        }

   


        protected override string getAdapterCode()
        {
            return ConstAdapterNames.adp_fin_cash_client_output;
        }

 

   


      
     

    }
}

