using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using AvaExt.AndroidEnv.ControlsBase;
using AvaExt.Common;
using AvaExt.PagedSource;
using AvaExt.ControlOperation;
using AvaGE.MobControl.ControlsTools;
using AvaExt.Common.Const;
using AvaExt.Manual.Table;
using AvaExt.SQL.Dynamic.Preparing;
using AvaExt.Adapter.ForUser;
using AvaExt.Adapter.ForUser.Sale.Operation.Slip;
using AvaExt.Adapter.ForDataSet;
using AvaExt.Adapter.ForDataSet.Sale.Operation.Slip;
using AvaExt.Translating.Tools;
using AvaGE.FormUserEditor;

using AvaExt.ObjectSource;
using AvaExt.TableOperation;
using AvaGE.FormUserEditor.Const;
using Android.App;

namespace AvaGE.FormDataReference.UserForm
{
    [Activity(Label = Form.FORM_NAME, Icon = Form.FORM_ICON, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait, WindowSoftInputMode = Android.Views.SoftInput.StateAlwaysHidden)]
    public class MobDataReferencePurchaseReturnsForm : MobDataReferenceForMoveForm
    {


        protected override string globalStoreName()
        {
            return ConstRefCode.docPurchaseReturn;// "ref.sls.wholesale.return";
        }
        public MobDataReferencePurchaseReturnsForm()
            : base(0)
        {


        }
        protected override string getAdapterCode()
        {
            return ConstAdapterNames.adp_prch_doc_purchaseret;
        }

 
        protected override void setSource(IPagedSource pSource)
        {
            base.setSource(pSource);
            pSource.getBuilder().addPereparer(new SqlBuilderPreparerFixedCondition(TableINVOICE.GRPCODE, ConstDocGroupCode.purchasing));
            pSource.getBuilder().addPereparer(new SqlBuilderPreparerFixedCondition(TableINVOICE.TRCODE, ConstDocTypeMaterial.purchaseReturn));
  
        }

        
 

        
    
 
    
    }
}

